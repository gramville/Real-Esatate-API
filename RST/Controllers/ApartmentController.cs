using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RST.Models.DTOs.AddDTOs;
using RST.Models.Services;
using RST.Models.Tables;
using RST.Configurations;
using RST.Models.DTOs.UpdateDTOs;

namespace RST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : Controller
    {
        private readonly IGenericRepository<Apartment> _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApartmentController(IGenericRepository<Apartment> service, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _mapper = mapper;   
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var apartments = await _service.GetAllApartments();
            if(apartments == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, apartments);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var apartment = await _service.GetById(Id);
            if(apartment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,"Invalid Id");
            }
            return StatusCode(StatusCodes.Status200OK, apartment);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromForm] ApartmentDTO apartment)
        {
            if (ModelState.IsValid)
            {
                var Apartment = _mapper.Map<Apartment>(apartment);
                Apartment.Image1 = await AddImage.AddImageAsync(apartment.Image1,_webHostEnvironment);
                Apartment.Image2 = await AddImage.AddImageAsync(apartment.Image2, _webHostEnvironment);
                Apartment.Image3 = await AddImage.AddImageAsync(apartment.Image3, _webHostEnvironment);
                Apartment.Image4 = await AddImage.AddImageAsync(apartment.Image4, _webHostEnvironment);
                if(await _service.Add(Apartment))
                {
                    return StatusCode(StatusCodes.Status201Created, await _service.GetById(Apartment.Id));
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status400BadRequest,apartment);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] UpdateApartmentDTO apartment)
        {
            if (ModelState.IsValid)
            {
                var Apartment = await _service.GetById(apartment.Id);
                if(Apartment == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Invalid id");
                }
                IFormFile Img1 = null, Img2 = null, Img3 = null, Img4 = null;
                if(apartment.Image1 != null) Img1 = apartment.Image1;
                if(apartment.Image2 != null) Img2 = apartment.Image2;
                if(apartment.Image3 != null) Img3 = apartment.Image3;
                if(apartment.Image4 != null) Img4 = apartment.Image4;
                var APARTMENT = _mapper.Map<UpdateApartmentDTO, Apartment>(apartment, Apartment);
                if (Img1 != null) Apartment.Image1 = await AddImage.AddImageAsync(Img1,_webHostEnvironment);
                if (Img2 != null) Apartment.Image2 = await AddImage.AddImageAsync(Img2, _webHostEnvironment);
                if (Img3 != null) Apartment.Image3 = await AddImage.AddImageAsync(Img3, _webHostEnvironment);
                if (Img4 != null) Apartment.Image4 = await AddImage.AddImageAsync(Img4, _webHostEnvironment);
                if(await _service.Update(APARTMENT))
                {
                    return StatusCode(StatusCodes.Status200OK, await _service.GetById(apartment.Id));
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status400BadRequest, apartment);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apartment = await _service.GetById(Id);
            if (apartment == null) return StatusCode(StatusCodes.Status404NotFound, "Invalid Id");
            apartment.IsDeleted = true;
            if (await _service.Delete(apartment)) return StatusCode(StatusCodes.Status204NoContent);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
