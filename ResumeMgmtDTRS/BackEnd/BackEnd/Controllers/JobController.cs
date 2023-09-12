using AutoMapper;
using BackEnd.Core.Context;
using BackEnd.Core.Dtos.Job;
using BackEnd.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JobController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public JobController(ApplicationDbContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		//CRUD

		//Create

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> CreateJob([FromBody] JobCreateDto jobCreateDto)
		{
			var existingJob = await _context.Jobs.FirstOrDefaultAsync(j =>
				j.Title == jobCreateDto.Title && j.Level == jobCreateDto.Level);

			if (existingJob != null)
			{ 
				return Conflict("This job is already listed");
			}

			Job newJob = _mapper.Map<Job>(jobCreateDto);
			await _context.Jobs.AddAsync(newJob);
			await _context.SaveChangesAsync();

			return Ok("Job created successfully");
		}

		//Read

		[HttpGet]
		[Route("Get")]
		public async Task <ActionResult<IEnumerable<JobGetDto>>> GetJobs()
		{
			//Include query to join company name from company class that is mapped from Automapper

			var jobs = await _context.Jobs.Include(job => job.Company).OrderBy(j => j.Id).ToListAsync();
			var convertedJobs =_mapper.Map<IEnumerable<JobGetDto>>(jobs);
			return Ok(convertedJobs);
		}

		[HttpGet]
		[Route("GetById")]
		public async Task<ActionResult> GetJob(int id)
		{
			var job = await _context.Jobs.FirstOrDefaultAsync(job => job.Id == id);
			if (job != null)
			{
				return Ok(job);
			}
			return NotFound("Job not found, either it is not listed or the request is wrong");
		}

		//Update job
		[HttpPut]
		[Route("Update/{id}")]
		public async Task<IActionResult> UpdateJob(long id, [FromBody] JobUpdateDto updatedJobDto)
		{
			var job =await _context.Jobs.FindAsync(id);
			if (job == null)
			{
				return NotFound("Job not found");
			}
			job.Title = updatedJobDto.Title;
			job.Level = updatedJobDto.Level;
			await _context.SaveChangesAsync();

			return Ok(updatedJobDto);

		}

		//Delete Job 

		[HttpDelete]
		[Route("Delete/{id}")]
		public async Task <ActionResult> DeleteJob(long id)
		{
			var job = _context.Jobs.FirstOrDefault(job => job.Id == id);
			if (job == null)
			{
				return NotFound("Job not found");
			}
			_context.Remove(job);
			await _context.SaveChangesAsync();

			return Ok("Job deleted successfully");
		}

    }
}
