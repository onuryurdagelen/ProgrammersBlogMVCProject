using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> Get(int articleId);
        Task<IDataResult<ArticleListDto>> GetAll();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeleted();

        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive();

        Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId);

        Task<IResult> AddAsync(ArticleAddDto articleAddDto, string createdByName);
        Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName);

        Task<IResult> Delete(int articleId, string modifiedByName); //GetAll yaptigimizda sadece ilgili veri gozukmez.
        Task<IResult> HardDelete(int articleId); //Veritabanin silmek icin kullanilir.
    }
}
