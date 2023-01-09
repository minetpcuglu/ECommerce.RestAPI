using System;
using System.Threading.Tasks;

namespace ECommerce.Data.EF
{
    public interface ICrud<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByGuidAsync(Guid guid);
        Task<int> CreateAsync(T dto);
        //Task DeleteAsync(int id);
        Task DeleteAsync(Guid guid);
        Task<int> UpdateAsync(T dto);
    }
}
