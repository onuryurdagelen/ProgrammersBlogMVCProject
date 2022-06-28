using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.DataAccess.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IArticleRepository Articles { get; } //unitofwork.Articles
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        
        //_unifOfWork.Categories.AddAsync();
        //_unitOfWork.Categories.AddAsync();
        //_unitOfWork.Users.AddAsync();
        //_unitOfWork.SaveAsync();
        Task<int> SaveAsync();

    }
}
