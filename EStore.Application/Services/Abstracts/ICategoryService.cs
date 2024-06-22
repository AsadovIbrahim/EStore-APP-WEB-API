using EStore.Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<AddCategoryResponseDTO> AddCategoryAsync(AddCategoryRequestDTO addCategoryRequestDTO);
        Task<List<GetAllCategoryResponseDTO>> GetAllCategoriesAsync(GetAllCategoryRequestDTO getAllCategoryRequestDTO);
        Task<GetAllCategoryResponseDTO> GetCategoryByIdAsync(int id);
        Task<RequestResponseDTO> UpdateCategoryAsync(UpdateCategoryRequestDTO updateCategoryRequestDTO);
        Task<RequestResponseDTO> DeleteCategoryAsync(int id);
    }
}
