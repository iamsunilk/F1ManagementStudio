using F1ManagementStudioWebAPI.Models.Domain;
using F1ManagementStudioWebAPI.Models.DTOs.CarDtos;
using Microsoft.EntityFrameworkCore;

namespace F1ManagementStudioWebAPI.Repositories.Interface
{
    public interface ICar
    {
        Task<List<Car>> GetAll();
        Task<Car> GetById(Guid carid);
        Task<Car> Create(Car car);
        Task<Car> Update(Guid carid,Car car);
        Task<Car> Delete(Guid carid);
    }
}
