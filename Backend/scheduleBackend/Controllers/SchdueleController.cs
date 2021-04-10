using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheduleBackend.Models.ClientResponse;
using scheduleBackend.Models.Database;
using scheduleBackend.Models.ServerResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace scheduleBackend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/")]
    public class SchdueleController:Controller
    {
        private SchdueleContext _Context;
        public SchdueleController(SchdueleContext C)
        {
            _Context = C;
        }


        [HttpPost("task/create")]
        public async Task<IActionResult> SchedueleTask([FromBody] CreateTask res)
        {
            try
            {
                if (res != null)
                {

                  string UserKey =   HttpContext.User?.FindFirst("UserKey").Value;
                    if (string.IsNullOrEmpty(UserKey))
                    {
                        return BadRequest();
                    }

                    await _Context.schedueles.AddAsync(new SchedueleTable {
                        Key = Guid.NewGuid().ToString(),
                        date = res.date,
                        TaskName = res.TaskName,
                        Location = res.Location,
                        StartTime = res.StartTime,
                        EndTime = res.EndTime,
                        IsFree = res.IsFree,
                        UserId = UserKey
                    }) ;

                    await _Context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();

            }catch(Exception e)
            {
                return StatusCode(500 , new { e.Message });
            }
           
        }

        [HttpGet("task/get")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {      
              string UserKey = HttpContext.User?.FindFirst("UserKey").Value;
                if (string.IsNullOrEmpty(UserKey))
                {
                    return BadRequest();
                }
               List<GetTaskResponse> schedueles =  _Context.schedueles.Where(x => x.UserId == UserKey).Select( i => new GetTaskResponse { 
                    TaskName =  i.TaskName,
                    IsFree = i.IsFree,
                    date = i.date,
                    Location = i.Location,
                    StartTime = i.StartTime,
                    EndTime =i.EndTime
                    }).ToList();
                return Ok(schedueles);
                
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }

        }


    }
}
