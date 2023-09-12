using AutoMapper;
using BackEnd.Core.Dtos.Candidate;
using BackEnd.Core.Dtos.Company;
using BackEnd.Core.Dtos.Job;
using BackEnd.Core.Entites;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BackEnd.Core.AutoMapperConfig
{
	public class AutoMapperConfigProfile : Profile
	{
        public AutoMapperConfigProfile()
        {
            //Company data conversion

            CreateMap<CompanyCreateDto, Company>();
            CreateMap<Company, CompanyGetDto>();
            CreateMap<Company, CompanyUpdateDto>();

            //Job data conversion

            CreateMap<JobCreateDto, Job>();

            //Because I included Company name in JobGetDto which is not preset in Job entity

            CreateMap<Job, JobGetDto>()
                .ForMember(destination => destination.CompanyName, options => options.MapFrom(source => source.Company.Name));
            CreateMap<Job, JobUpdateDto>();

            //Candidate data conversion

            CreateMap<CandidateCreateDto, Candidate>();
            CreateMap<Candidate, CandidateGetDto>()
                .ForMember(destination => destination.JobTitle, options => options.MapFrom(source => source.Job.Title));
            CreateMap<Candidate, CandidateGetDto>();
        }
    }
} 
