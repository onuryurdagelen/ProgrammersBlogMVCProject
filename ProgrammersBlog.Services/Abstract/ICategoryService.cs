using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> Get(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAll();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive();


        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto,string createdByName);
        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto,string modifiedByName);

        Task<IResult> Delete(int categoryId,string modifiedByName); //GetAll yaptigimizda sadece ilgili veri gozukmez.
        Task<IResult> HardDelete(int categoryId); //Veritabanin silmek icin kullanilir.
    }
}
