using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository repository;
        private readonly IMapper mapper;

        public CampsController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CampModel[]>> Get(bool includeTalks = false)
        {
            try
            {
                var results = await repository.GetAllCampsAsync(true);
                return mapper.Map<CampModel[]>(results);
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var result = await repository.GetCampAsync(moniker);
                if (result == null) return NotFound();
                return mapper.Map<CampModel>(result);
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<CampModel[]>>SearchByDate(DateTime theDate, bool includeTalks = false)
        {
            try
            {
                var results = await repository.GetAllCampsByEventDate(theDate, includeTalks);
                
                if (!results.Any()) return NotFound();

                return mapper.Map<CampModel[]>(results);
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        public async Task<ActionResult<CampModel>> Post([FromBody]CampModel model)
        {
            try
            {
                //Create a new Camp
                var camp = mapper.Map<Camp>(model);
                repository.Add(camp);
                if(await repository.SaveChangesAsync())
                {
                    return Created($"/api/camps/{camp.Moniker}", mapper.Map<CampModel>(camp));
                }
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }
    }
}
