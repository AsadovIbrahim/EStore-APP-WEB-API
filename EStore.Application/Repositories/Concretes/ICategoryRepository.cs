using EStore.Application.Repositories.Abstracts;
using EStore.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Repositories.Concretes
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<Category>GetCategoryById(int id);
        Task<List<Category>> GetAllCategoriesAsync(int page, int size);

    }
}
