using AutoMapper;
using F1ManagementStudioWebAPI.Data;
using F1ManagementStudioWebAPI.Models.Domain;
using F1ManagementStudioWebAPI.Models.DTOs.CarDtos;
using F1ManagementStudioWebAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1ManagementStudioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarDbContext _context;
        private readonly ICar _repo;
        private readonly IMapper _map;
        public CarController(CarDbContext cardbcontext,ICar carrepo,IMapper mapping) { 
            _context= cardbcontext;
            _repo= carrepo;
            _map= mapping;
        }
        //GET : api/Car
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _repo.GetAll();
           
            return Ok(_map.Map<List<CarDto>>(cars));
        }

        //GET : api/Car/{id}
        [HttpGet]
        [Route("{carid:guid}")]
        
        public async Task<IActionResult> GetById([FromRoute] Guid carid)
        {
            var cars = await _repo.GetById(carid);
            if (cars == null)
            {
                return NotFound();
            }


            return Ok(cars);
        }
        //POST : api/Car
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarDto createcardto)
        {
            var carmodel = new Car()
            {
                CarName = createcardto.CarName,
                CarModel = createcardto.CarModel,
                year = createcardto.year
            };

            carmodel = await _repo.Create(carmodel);

            var cardto = new CarDto
            {
                CarId = carmodel.CarId,
                CarName = carmodel.CarName,
                CarModel = carmodel.CarModel,
                year = carmodel.year
            };
           
            return CreatedAtAction(nameof(GetById),new {CarId = cardto.CarId }, cardto);
        }
        [HttpPut]
        [Route("{carid:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid carid, [FromBody] UpdateCarDto updatecardto)
        {
            var carmodel = new Car()
            {
                CarName = updatecardto.CarName,
                CarModel = updatecardto.CarModel,
                year = updatecardto.year
            };

            carmodel = await _repo.Update(carid,carmodel);
            if(carmodel == null)
            {
                return NotFound();
            }

            var cardto = new CarDto
            {
                CarId=carmodel.CarId,
                CarName = carmodel.CarName,
                CarModel = carmodel.CarModel,
                year = carmodel.year
            };

            return Ok(cardto);
        }

        [HttpDelete]
        [Route("{carid:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid carid)
        {
            var carmodel = await _repo.Delete(carid);
            if (carmodel == null)
            {
                return NotFound();
            }

            var cardto= new CarDto
            {
                CarId = carmodel.CarId,
                CarName = carmodel.CarName,
                CarModel = carmodel.CarModel,
                year = carmodel.year
            };
            return Ok(cardto);
        }
    }
}
