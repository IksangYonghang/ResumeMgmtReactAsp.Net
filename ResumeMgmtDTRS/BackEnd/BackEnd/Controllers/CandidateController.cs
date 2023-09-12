using AutoMapper;
using BackEnd.Core.Context;
using BackEnd.Core.Dtos.Candidate;
using BackEnd.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CandidateController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CandidateController(ApplicationDbContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
		//CRUD

		//Create

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> CeateCandidate([FromForm] CandidateCreateDto candidateCreateDto, IFormFile pdfFile)
		{
			var existingCandidate =await _context.Candidates.FirstOrDefaultAsync(c => 
			c.Email == candidateCreateDto.Email || 
			c.PhoneNumber == candidateCreateDto.PhoneNumber);

			if (existingCandidate != null)
			{
				return Conflict("The email or phone number provided already exists");
			}

			//Save pdf to server
			//Save URL into our entity

			var fiveMegaByte = 5 * 1024 * 1024;
			var pdfMimeType = "application/pdf";

			if (pdfFile.Length > fiveMegaByte || pdfFile.ContentType !=pdfMimeType)
			{
				return BadRequest("File type is not supported, only pdf tyoe and less than 5 megabye is supported");
			}
			var resumeUrl = Guid.NewGuid().ToString() + ".pdf";
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", "Resumes", resumeUrl);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await pdfFile.CopyToAsync(stream);
			}
			
			Candidate newCandidate = _mapper.Map<Candidate>(candidateCreateDto);
			newCandidate.ResumeUrl = resumeUrl;
			await _context.Candidates.AddAsync(newCandidate);
			await _context.SaveChangesAsync();

			return Ok("Candidate saved successfully");
		}

		//Read

		[HttpGet]
		[Route("Get")]
		public async Task<ActionResult<IEnumerable<CandidateCreateDto>>> GetCandidates()
		{
			var candidates = await _context.Candidates.Include(c => c.Job).OrderBy(c => c.Id).ToListAsync();
			var convertedCandidate = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);

			return Ok(convertedCandidate);
		}

		//Read by id
		[HttpGet]
		[Route("GetById")]
		public async Task<ActionResult> GetCandidate(int id)
		{
			var candidate = await _context.Candidates.FirstOrDefaultAsync(c => id== c.Id);

			if (candidate == null)
			{
				return NotFound("Candidate not listed or the request is wrong");
			}
			return Ok(candidate);
		}

		//Read or download pdf file
		[HttpGet]
		[Route("download/{url}")]
		public ActionResult DownloadPdfFile (string url)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", "Resumes", url);
			 
			if (!System.IO.File.Exists(filePath))
			{
				return NotFound("File not found");
			}
			var pdfBytes = System.IO.File.ReadAllBytes(filePath);
			var file = File(pdfBytes, "application/pdf", url);

			return file;
		}

		//Update Candidate

		[HttpPut]
		[Route("Update/{id}")]
		public async Task<IActionResult> UpdateCandidate(long id, [FromForm]CandidateUpdateDto updatedCandidate)
		{
			var candidate = _context.Candidates.Find(id);
			if (candidate == null)
			{
				return NotFound("Candidate not found");
			}
			candidate.FirstName = updatedCandidate.FirstName;
			candidate.LastName = updatedCandidate.LastName;
			candidate.Email = updatedCandidate.Email;
			candidate.PhoneNumber = updatedCandidate.PhoneNumber;
			candidate.CoverLetter = updatedCandidate.CoverLetter;

			await _context.SaveChangesAsync();
			return Ok(updatedCandidate);
		}

		//Delete candidate

		[HttpDelete]
		[Route("Delete/{id}")]
		public async Task<ActionResult> DeleteCandidate(long id)
		{
			var candidate = await _context.Candidates.FindAsync(id);
			if (candidate == null)
			{
				return NotFound("Candidate not found");
			}
			_context.Candidates.Remove(candidate);
			await _context.SaveChangesAsync();

			return Ok("Candidate deleted successfully");
		}
    }
}
