using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RST.Models.DTOs.AddDTOs;
using RST.Models.DTOs.UpdateDTOs;
using RST.Models.Services;
using RST.Models.Tables;

namespace RST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : Controller
    {
        private readonly IGenericRepository<Agent> _service;
        private readonly IMapper _mapper;
        public AgentController(IGenericRepository<Agent> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var agents = await _service.GetAllAgents();
            if (agents == null)
            {
                return StatusCode(
                    StatusCodes.Status404NotFound,"No agents added");
            }

            return StatusCode(StatusCodes.Status200OK, agents);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var agent = await _service.GetById(id);

            if (agent == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK,agent);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Create(AgentDTO agent)
        {
            if (ModelState.IsValid)
            {
                if(await _service.AgentPhoneNumberExists(agent.PhoneNumber) != -1)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Phone Number already exists");
                }
                var Agent = _mapper.Map<Agent>(agent);
                if(await _service.Add(Agent))
                {
                    return StatusCode(StatusCodes.Status201Created, await _service.GetByPhoneNumber(agent.PhoneNumber)); 
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status400BadRequest, agent);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateAgentDTO agent)
        {
            if(ModelState.IsValid)
            {
                var Agent = await _service.GetById(agent.Id);
                if(Agent == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Id not found");
                }
                if(await _service.AgentPhoneNumberExists(agent.PhoneNumber,agent.Id) == 1)
                {
                    return StatusCode(StatusCodes.Status409Conflict,"Phone number already exists");
                }
                var AGENT =  _mapper.Map<UpdateAgentDTO, Agent>(agent,Agent);
                if (await _service.Update(AGENT))
                {
                    return StatusCode(StatusCodes.Status200OK, await _service.GetById(agent.Id));
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status400BadRequest, agent);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var agent = await _service.GetById(Id);
            if(agent == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Id not found");
            }
            agent.IsDeleted = true;
            if(await _service.Delete(agent))
            {
                return StatusCode(StatusCodes.Status200OK, "Agent Deleted");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
