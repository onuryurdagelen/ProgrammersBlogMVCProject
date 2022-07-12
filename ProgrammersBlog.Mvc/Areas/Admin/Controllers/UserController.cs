using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Models;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Result.ComplexTypes;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")] //Bu Controller'a sadece Admin erişebilir demektir.
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper, IWebHostEnvironment env, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
            _signInManager = signInManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")] //"Admin,Editor" => Admin ve Editorler erişebilir.
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email); //Email yoluyla kullanıcıyı alırız.
                if (user !=null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    //1.parametre => kullanıcı,şifre,isPersistence,hesap kitleme durumu
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home"); //Admin paneline yönlendirme işlemi gerçekleştirilir.
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-posta adresinizi veya şifreniz yanlıştır.");
                    //Belirli bir hata mesajı yazmayız.Cünkü hackerlar için açık vermiş oluruz.
                    return View("UserLogin");
                }
            }
            else
            {
                ModelState.AddModelError("", "E-posta adresinizi veya şifreniz yanlıştır.");
                return View("UserLogin");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<PartialViewResult> Update(int userId)
        {

            var result = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(result);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var user = _userManager.Users.FirstOrDefaultAsync(u => u.Id == userUpdateDto.Id);
           
            //Gonderilen verilerin icinin hepsi dolu mu?
            if (ModelState.IsValid)
            {
                bool isNewPictureUpload = false; //yeni resim ekleme işlemi yapıldıktan sonra veritaban
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture; //Eski resmi silmek için eski resmi değişkene atarız

                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    isNewPictureUpload = true;
                }
                
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser); //UserUpdateDto veriyoruz ve yeni User oluşturuyoruz.
                var result = await _userManager.UpdateAsync(updatedUser);

                if (result.Succeeded)
                {
                    if (isNewPictureUpload)
                    {
                        ImageDelete(oldUserPicture);
                    }
                    var userUpdateAjaxModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{updatedUser.UserName} adlı kullanıcı adına sahip kullanıcının bilgileri başarıyla güncellenmiştir.",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateAjaxModel);
                }
                else
                {
                    string errorMessages = String.Empty;
                    foreach (var error in result.Errors)
                    {
                        errorMessages += $"*{error.Description}";
                    }
                    var userUpdateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = userUpdateDto,
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Error,
                            Message = $"{updatedUser.UserName} adlı kullanıcı adına sahip kullanıcı silinirken bazı hatalar oluştu\n{errorMessages}"
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto),
                        
                        
                    });
                    return Json(userUpdateErrorViewModel);
                }
            }
            else
            {
                
                var userUpdateModelStateViewError = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDto = userUpdateDto,
                    UserDto = new UserDto
                    {
                        ResultStatus = ResultStatus.Error,
                        Message = $"{userUpdateDto.UserName} adlı kullanıcı adına sahip kullanıcı güncellenirken hata meydana geldi"
                    },
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto),


                }); ;
                return Json(userUpdateModelStateViewError);
            }
           
        }
        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        
        public async Task<ViewResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);

                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword, userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user); //Önemli bir değişiklik yaptığında SecurityStamp'ı güncellememiz gerekir.
                        await _signInManager.SignOutAsync(); //Şifre güncellendikten sonra hemen çıkış yaptırırız.
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false); 
                        //Çıkış yaptıktan sonra hemen ardından giriş yaptırırız.
                        TempData.Add("SuccessMessage", $"{user.UserName} adlı kullanıcı şifresi başarıyla değiştirilmiştir.");
                        return View();
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen girmiş olduğunuz şu anki şifrenizi kontrol ediniz");
                    return View(userPasswordChangeDto);
                }
               
            }
            return View(userPasswordChangeDto);
            
        }
        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var updateDto = _mapper.Map<UserUpdateDto>(user);

            return View(updateDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUpload = false; //yeni resim ekleme işlemi yapıldıktan sonra veritaban
                var oldUser = await _userManager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture; //Eski resmi silmek için eski resmi değişkene atarız

                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    //Diğer kullanıcılar da defaultUser.png resmini kullanabilir.Bu resmin silinmemesini önlemek için eski resim defaultUser.png den farklı olması şartını koyarız.
                    if (oldUserPicture != "defaultUser.png")
                    {
                        isNewPictureUpload = true;
                    }
                }

                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser); //UserUpdateDto veriyoruz ve yeni User oluşturuyoruz.
                var result = await _userManager.UpdateAsync(updatedUser);

                if (result.Succeeded)
                {
                    if (isNewPictureUpload)
                    {
                        ImageDelete(oldUserPicture);
                    }
                    TempData.Add("SuccessMessage", $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.");
                    return View(userUpdateDto);
                }
                else
                {
                    return View(userUpdateDto);
                }
                
            }
            else
            {
                return View(userUpdateDto);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int userId)
        {
            //var deletedUser = _userManager.Users
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    User = user,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı başarıyla silinmiştir.",
                    ResultStatus = ResultStatus.Success
                });
                return Json(deletedUser);

            }
            else
            {
                string errorMessages = String.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessages += $"*{error.Description}";
                }
                var deletedUserErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı silinirken bazı hatalar oluştu\n{errorMessages}",
                    User = user
                });
                return Json(deletedUserErrorModel);
            }
        }
        [Authorize(Roles = "Admin,editor")]
        public async Task<string> ImageUpload(string userName,IFormFile pictureFile)
        {
            //~/img/user.Picture
            string wwwroot = _env.WebRootPath; //wwwroot'un path'ini verir.

            //onuryurdagelen
            //string fileName2 = Path.GetFileNameWithoutExtension(userAddDto.Picture.FileName); //sonundaki dosya uzantısı olmadan almayı sağlar.
            //.png
            string fileExtension = Path.GetExtension(pictureFile.FileName); //dosya formatını alır.
            DateTime dateTime = DateTime.Now;
            //OnurYurdagelen_546_5_38_12_3_10_2022.png
            string fileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            //ilk parametre bunlar nereye kaydedilmeli ve hangi isimle kaydedilmeli;
            //İkinci parametre hangi işlemi gerçekleştireceğimizi belirtir.
            {
                await pictureFile.CopyToAsync(stream);
            }
            return fileName; //OnurYurdagelen_546_5_38_12_3_10_2022.png - "~/img/user.Picture
        }
        [Authorize(Roles = "Admin,editor")]
        public bool ImageDelete(string pictureName)
        {
            pictureName = "kaanyurdagelen_319_28_16_16_1_7_2022.png";
            string wwwroot = _env.WebRootPath;
            var fileToDelete = Path.Combine($"{wwwroot}/img", pictureName);
            if (System.IO.File.Exists(fileToDelete)) //Böyle bir path var mı?
            {
                
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
            {
                if (ModelState.IsValid)
                {
                    userAddDto.Picture = await ImageUpload(userAddDto.Picture,userAddDto.PictureFile);
                    var user = _mapper.Map<User>(userAddDto);
                    var result = await _userManager.CreateAsync(user, userAddDto.Password); //result => IdentityResult döner
                                                                                            //user ekleme işlemi başarıyla gerçekleşti mi?
                    if (result.Succeeded)
                    {
                        var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                        {
                            UserDto = new UserDto
                            {
                                ResultStatus = ResultStatus.Success,
                                Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı başarıyla eklenmiştir",
                                User = user
                            },
                            UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                        });
                        return Json(userAddAjaxModel);
                    }
                    //user ekleme başarılı değilse uygulanacak kod.result içindeki Error'ları foreach'te dönerek front-ende hataları göndeririz.
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                        {
                            UserAddDto = userAddDto,
                            UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                        });
                        return Json(userAddAjaxErrorModel);
                    }
                }
                //ModelState Valid değilse uygulanacak kod.
                var userAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                {
                    UserAddDto = userAddDto,
                    UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                });
                return Json(userAddAjaxModelStateErrorModel);
            }

        }

        
    }

