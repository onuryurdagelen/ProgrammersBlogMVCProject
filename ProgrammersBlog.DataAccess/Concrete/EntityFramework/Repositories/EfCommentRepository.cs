using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.DataAccess.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext context) : base(context)
        {
        }
    }
}
