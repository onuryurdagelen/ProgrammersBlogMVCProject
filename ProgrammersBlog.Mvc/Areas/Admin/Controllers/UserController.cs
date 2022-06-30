using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
        }
        [HttpGet]
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
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            },new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int userId)
        {
            //var deletedUser = _userManager.Users
            var result = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == userId);
            if (result != null)
            {
                var deletedUser = await _userManager.DeleteAsync(result);
                var user = JsonSerializer.Serialize(new UserDto
                {
                    User = result,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{result.UserName} adlı kullanıcı başarıyla silinmiştir."
                });

                return Json(user);
            }
            return Json(new UserDto
            {
                User = result,
                Message = $"Böyle bir kullanıcı bulunamadı.",
                ResultStatus = ResultStatus.Error
            });
          
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto);
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
        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            //~/img/user.Picture
            string wwwroot = _env.WebRootPath; //wwwroot'un path'ini verir.

            //onuryurdagelen
            //string fileName2 = Path.GetFileNameWithoutExtension(userAddDto.Picture.FileName); //sonundaki dosya uzantısı olmadan almayı sağlar.
            //.png
            string fileExtension = Path.GetExtension(userAddDto.PictureFile.FileName); //dosya formatını alır.
            DateTime dateTime = DateTime.Now;
            //OnurYurdagelen_546_5_38_12_3_10_2022.png
            string fileName = $"{userAddDto.UserName}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path,FileMode.Create))
            //ilk parametre bunlar nereye kaydedilmeli ve hangi isimle kaydedilmeli;
            //İkinci parametre hangi işlemi gerçekleştireceğimizi belirtir.
            {
                await userAddDto.PictureFile.CopyToAsync(stream);
            }
            return fileName; //OnurYurdagelen_546_5_38_12_3_10_2022.png - "~/img/user.Picture
        }
    }
}
