using AppCore.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppCore.Data.Controllers
{
    
    [ApiController]
    [Route("api/camps/{moniker}/talks")]
    public class TalksController : ControllerBase
    {
        private readonly ICampRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public TalksController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<TalkModel[]>> Get(string moniker)
        {
            try
            {
                var talks = await repository.GetTalksByMonikerAsync(moniker, true);
                return mapper.Map<TalkModel[]>(talks);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TalkModel>> Get(string moniker, int id)
        {
            try
            {
                var talk = await repository.GetTalkByMonikerAsync(moniker, id, true);
                if (talk == null) return NotFound();

                return mapper.Map<TalkModel>(talk);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
            }
        }
        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel model)
        {
            try
            {
                var camp = await repository.GetCampAsync(moniker);
                if (camp == null) return BadRequest("Camp does not exist");

                var talk = mapper.Map<Talk>(model);
                talk.Camp = camp;

                Contract.Requires(model != null);

                if (model.Speaker == null) return BadRequest("Speaker Id is required");

                var speaker = await repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                if (speaker == null) return BadRequest("Speaker could not be found");

                talk.Speaker = speaker;

                repository.Add(talk);

                if(await repository.SaveChangesAsync())
                {
                    var url = linkGenerator.GetPathByAction(HttpContext, "Get", 
                        values: new { moniker, id = talk.TalkId });

                    return Created(url, mapper.Map<TalkModel>(talk));
                }
                else
                {
                    return BadRequest("Failed to save new Talk");
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model)
        {
            try
            {
                var talk = await repository.GetTalkByMonikerAsync(moniker, id, true);
                if (talk == null) return NotFound("Couldn't find the talk");

                mapper.Map(model, talk);

                if (model.Speaker != null)
                {
                    var speaker = await repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                    if(speaker != null)
                    {
                        talk.Speaker = speaker;
                    }
                }
                

                if(await repository.SaveChangesAsync())
                {
                    return mapper.Map<TalkModel>(talk);
                }
                else
                {
                    return BadRequest("Failed to update Talk in database");
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete (string moniker, int id)
        {
            try
            {
                var talk = await repository.GetTalkByMonikerAsync(moniker, id);
                if (talk == null) return NotFound("Failed to find the talk to delete");
                repository.Delete(talk);

                if(await repository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to delete talk");
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
            }
        }
    }
}
