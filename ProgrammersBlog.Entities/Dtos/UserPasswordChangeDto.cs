﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Mevcut Şifre")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır!")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifre")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifre Onayı")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage = "Girmiş olduğunuz şifreler eşleşmemektedir.")] //NewPassword alanıyla karşılaştırılır.
        public string NewPasswordConfirm { get; set; }
    }
}
