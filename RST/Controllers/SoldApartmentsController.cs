using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RST.Models.Services;
using RST.Models.Tables;

namespace RST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldApartmentsController : Controller
    {
        private readonly IGenericRepository<SoldApartments> _service;
        private readonly IMapper _mapper;
        public SoldApartmentsController(IGenericRepository<SoldApartments> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var soldapartments = await _service.GetAllSoldApartments();
            if(soldapartments == null) return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, soldapartments);
        }
    }
}
