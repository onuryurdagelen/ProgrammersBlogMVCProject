﻿using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Entities.Concrete
{
    //deritabanında integer primary key ile oluşacak
    public class Role:IdentityRole<int>
    {
     

    }
}
