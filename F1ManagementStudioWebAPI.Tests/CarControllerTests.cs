using F1ManagementStudioWebAPI.Controllers;
using F1ManagementStudioWebAPI.Models.Domain;
using F1ManagementStudioWebAPI.Models.DTOs.CarDtos;
using F1ManagementStudioWebAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace F1ManagementStudioWebAPI.Tests
{
    public class CarControllerTests
    {
        [Fact]
        public async Task GetById_ReturnsNotFound_WhenCarDoesNotExist()
        {
            var repo = new FakeCarRepository();
            var controller = new CarController(null, repo, null);
            var result = await controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenCarExists()
        {
            var car = new Car { CarId = Guid.NewGuid(), CarName = "F1", CarModel = "Speedster", year = "2025" };
            var repo = new FakeCarRepository(new[] { car });
            var controller = new CarController(null, repo, null);

            var result = await controller.GetById(car.CarId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCar = Assert.IsType<Car>(okResult.Value);

            Assert.Equal(car.CarId, returnedCar.CarId);
            Assert.Equal(car.CarName, returnedCar.CarName);
            Assert.Equal(car.CarModel, returnedCar.CarModel);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithCreatedCarDto()
        {
            var repo = new FakeCarRepository();
            var controller = new CarController(null, repo, null);
            var createDto = new CreateCarDto { CarName = "F1", CarModel = "Speedster", year = "2025" };

            var result = await controller.Create(createDto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedDto = Assert.IsType<CarDto>(createdResult.Value);

            Assert.Equal(createDto.CarName, returnedDto.CarName);
            Assert.Equal(createDto.CarModel, returnedDto.CarModel);
            Assert.Equal(createDto.year, returnedDto.year);
            Assert.Equal(nameof(CarController.GetById), createdResult.ActionName);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenCarDoesNotExist()
        {
            var repo = new FakeCarRepository();
            var controller = new CarController(null, repo, null);
            var updateDto = new UpdateCarDto { CarName = "F1", CarModel = "Speedster", year = "2025" };

            var result = await controller.Update(Guid.NewGuid(), updateDto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenCarExists()
        {
            var car = new Car { CarId = Guid.NewGuid(), CarName = "Old", CarModel = "Classic", year = "2024" };
            var repo = new FakeCarRepository(new[] { car });
            var controller = new CarController(null, repo, null);
            var updateDto = new UpdateCarDto { CarName = "F1", CarModel = "Speedster", year = "2025" };

            var result = await controller.Update(car.CarId, updateDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<CarDto>(okResult.Value);

            Assert.Equal(car.CarId, returnedDto.CarId);
            Assert.Equal(updateDto.CarName, returnedDto.CarName);
            Assert.Equal(updateDto.CarModel, returnedDto.CarModel);
            Assert.Equal(updateDto.year, returnedDto.year);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenCarDoesNotExist()
        {
            var repo = new FakeCarRepository();
            var controller = new CarController(null, repo, null);

            var result = await controller.Delete(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenCarExists()
        {
            var car = new Car { CarId = Guid.NewGuid(), CarName = "F1", CarModel = "Speedster", year = "2025" };
            var repo = new FakeCarRepository(new[] { car });
            var controller = new CarController(null, repo, null);

            var result = await controller.Delete(car.CarId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<CarDto>(okResult.Value);

            Assert.Equal(car.CarId, returnedDto.CarId);
            Assert.Equal(car.CarName, returnedDto.CarName);
            Assert.Equal(car.CarModel, returnedDto.CarModel);
            Assert.Equal(car.year, returnedDto.year);
        }

        private class FakeCarRepository : ICar
        {
            private readonly Dictionary<Guid, Car> _cars;

            public FakeCarRepository(IEnumerable<Car> seedCars = null)
            {
                _cars = seedCars?.ToDictionary(c => c.CarId) ?? new Dictionary<Guid, Car>();
            }

            public Task<Car> Create(Car car)
            {
                car.CarId = Guid.NewGuid();
                _cars[car.CarId] = car;
                return Task.FromResult(car);
            }

            public Task<Car> Delete(Guid carid)
            {
                if (!_cars.TryGetValue(carid, out var car))
                {
                    return Task.FromResult<Car>(null);
                }

                _cars.Remove(carid);
                return Task.FromResult(car);
            }

            public Task<List<Car>> GetAll()
            {
                return Task.FromResult(_cars.Values.ToList());
            }

            public Task<Car> GetById(Guid carid)
            {
                _cars.TryGetValue(carid, out var car);
                return Task.FromResult(car);
            }

            public Task<Car> Update(Guid carid, Car car)
            {
                if (!_cars.ContainsKey(carid))
                {
                    return Task.FromResult<Car>(null);
                }

                car.CarId = carid;
                _cars[carid] = car;
                return Task.FromResult(car);
            }
        }
    }
}
