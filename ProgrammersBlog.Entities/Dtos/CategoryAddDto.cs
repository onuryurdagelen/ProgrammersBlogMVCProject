﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        [MaxLength(70,ErrorMessage = "{0} {1} karakterden büyük olmamalıdır!")]
        [MinLength(3,ErrorMessage = "{0} {1} karakterden az olmamalıdır!")]
        public string Name { get; set; }
        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır!")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır!")]
        public string Description { get; set; }
        [DisplayName("Kategori Özel Not Alanı")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır!")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır!")]
        public string Note { get; set; }
        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} Adı Boş olmamalıdır!")]
        public bool IsActive { get; set; }

    }
}
