using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Entities.Concrete
{
    public class User:IdentityUser<int>
    {
      
        public string Picture { get; set; }

        public ICollection<Article> Articles { get; set; }


    }
}
