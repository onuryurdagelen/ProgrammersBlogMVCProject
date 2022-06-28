using ProgrammersBlog.DataAccess.Abstract;
using ProgrammersBlog.DataAccess.Concrete.EntityFramework.Repositories;
using ProgrammersBlog.DataAccess.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProgrammersBlogContext _context;
        private EfArticleRepository _articleRepository;
        private EfCommentRepository _commentRepository;
        private EfCategoryRepository _categoryRepository;

        public UnitOfWork(ProgrammersBlogContext context)
        {
            _context = context;
        }

        public IArticleRepository Articles => _articleRepository ?? new EfArticleRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);

   

        //
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
