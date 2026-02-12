using F1ManagementStudioWebAPI.Data;
using F1ManagementStudioWebAPI.Models.Domain;
using F1ManagementStudioWebAPI.Models.DTOs.CarDtos;
using F1ManagementStudioWebAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace F1ManagementStudioWebAPI.Repositories.Implementation
{
    public class CarRepository : ICar
    {
        private readonly CarDbContext carDbContext;
        public CarRepository(CarDbContext dbContext)
        {
            carDbContext = dbContext;
        }

        public async Task<Car> Create(Car car)
        {
           await carDbContext.Cars.AddAsync(car);
           await carDbContext.SaveChangesAsync();

            return car;

        }

        public async Task<Car> Delete(Guid carid)
        {
            var carmodel = await carDbContext.Cars.FirstOrDefaultAsync(x => x.CarId == carid);

            carDbContext.Cars.Remove(carmodel);

            await carDbContext.SaveChangesAsync();

            return carmodel;
        }

        public async Task<List<Car>> GetAll()
        {
            return await carDbContext.Cars.ToListAsync();
        }

        public async Task<Car> GetById(Guid carid)
        {
            var carmodel = carDbContext.Cars.FirstOrDefaultAsync(x => x.CarId==carid);
            return await carmodel;
        }

        public async Task<Car> Update(Guid carid, Car car)
        {
            var exitcar = await carDbContext.Cars.FirstOrDefaultAsync(x => x.CarId == carid);

            exitcar.CarId=carid;
            exitcar.CarName = car.CarName;
            exitcar.CarModel = car.CarModel;
            exitcar.year = car.year;

            await carDbContext.SaveChangesAsync();

            return exitcar;
        }
    }
}

