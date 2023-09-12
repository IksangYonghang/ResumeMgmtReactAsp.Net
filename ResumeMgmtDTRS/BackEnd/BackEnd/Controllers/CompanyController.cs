using AutoMapper;
using BackEnd.Core.Context;
using BackEnd.Core.Dtos.Company;
using BackEnd.Core.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CompanyController(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		//CRUD Functionality

		//Create 

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto companyCreateDto)
		{
			//Searching if the detail of the Company

			var existingCompany = await _context.Companies.FirstOrDefaultAsync(c =>
					c.Name == companyCreateDto.Name && c.Size == companyCreateDto.Size);

			//Chedking if the searched details matches with the company that is going to be created or not

			if (existingCompany != null)
			{
				
				return Conflict("Company with the same details already exists");
			}
			Company newCompany = _mapper.Map<Company>(companyCreateDto);
			await _context.Companies.AddAsync(newCompany);
			await _context.SaveChangesAsync();

			return Ok("Company created successfully");
		}

		//Read

		[HttpGet]
		[Route("Get")]
		public async Task<ActionResult<IEnumerable<CompanyGetDto>>> GetCompanies()
		{
			var companies = await _context.Companies.OrderBy (c => c.Id).ToListAsync();
			var convertedCompanies = _mapper.Map<IEnumerable<CompanyGetDto>>(companies);

			return Ok(convertedCompanies);
		}

		//ReadByID
		[HttpGet]
		[Route("GetById")]
		public async Task<ActionResult> GetCompany(int id)
		{
			
			var company =await _context.Companies.FirstOrDefaultAsync(c => c.Id ==id);
			if (company == null)
			{
				return NotFound("Company not found, either it is not listed or the request is wrong");
			}
			return Ok(company);			
		}

		//Update company

		[HttpPut]
		[Route("Update/{id}")]
		public async Task<IActionResult> UpateCompany(long id, [FromBody] CompanyUpdateDto updatedCompanyDto)
		{
			var company = await _context.Companies.FindAsync(id);
			if (company == null)
			{
				return NotFound("Company not found");
			}
			company.Name = updatedCompanyDto.Name;
			company.Size= updatedCompanyDto.Size;
			await _context.SaveChangesAsync();

			return Ok("Comoany updated successfully");
			
		}

		//Delete company

		[HttpDelete]
		public async Task<IActionResult> DeleteCompany(long id)
		{
			var company = _context.Companies.FirstOrDefault(c => c.Id == id);
			if (company == null)
			{
				return NotFound("Company not found in the list");
			}
			_context.Companies.Remove(company);
			await _context.SaveChangesAsync();
			return Ok("Company deleted successfully");
		}
    }
}
