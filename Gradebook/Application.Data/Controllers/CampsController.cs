using AppCore.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppCore.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public CampsController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Camps Could not be found");
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Camps Could not be found");
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Camps Could not be found");
            }
        }
        public async Task<ActionResult<CampModel>> Post([FromBody]CampModel model)
        {
            try
            {
                var existingMoniker = await repository.GetCampAsync(model.Moniker);
                if(existingMoniker != null)
                {
                    return BadRequest("Moniker in Use");
                }

                var location = linkGenerator.GetPathByAction("Get", "Camps", new { moniker = model.Moniker });
                
                if(string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current moniker");
                }

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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Camps Could not be found");
            }

            return BadRequest();
        }

        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModel>> Put(string moniker, CampModel model)
        {
            try
            {
                var oldCamp = await repository.GetCampAsync(moniker);
                if (oldCamp == null) return NotFound($"Could not find camp with moniker of {moniker}");

                mapper.Map(model, oldCamp);

                if(await repository.SaveChangesAsync())
                {
                    return mapper.Map<CampModel>(oldCamp);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Camps Could not be found");
            }

            return BadRequest();
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)
        {
            try
            {
                var oldCamp = await repository.GetCampAsync(moniker);
                if (oldCamp == null) return NotFound();

                repository.Delete(oldCamp);

                if(await repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Camps Could not be found");
            }

            return BadRequest();
        }
    }
}
