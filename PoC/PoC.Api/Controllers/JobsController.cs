using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoC.BL.AbstractProducts;
using PoC.DomainEntities;
using PoC.Dtos.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
     
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

      

        [HttpPost("startCalculation")]
        public async Task<IActionResult> PostJob(PostJobRequestDto job)
        {
            if (job == null)
                throw new BadHttpRequestException("Request content is empty!");
            
            var jobId = await _jobService.PostJob(job);
            return Accepted(jobId);              
        }

        [HttpGet("GetStatus/{id}")]
        public async Task<IActionResult> GetStatus(Guid id)
        {
            if (id == Guid.Empty)
                throw new BadHttpRequestException("JobId is empty!");

            var jobStatus = await _jobService.GetStatus(id);
            return Ok(jobStatus);
        }

        [HttpGet]
        public async Task<List<JobResponseDto>> GetAll()
        {
           
            var jobs = await _jobService.GetAllAsync();
            return jobs;
        }


    }
}
