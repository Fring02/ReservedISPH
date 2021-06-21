using AutoMapper;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Dtos;
using ISPH.Domain.Dtos.Users;

namespace ISPH.Infrastructure.Business.Mapping
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Advertisement, AdvertisementViewDto>();
            CreateMap<Advertisement, AdvertisementElementViewDto>();
            CreateMap<AdvertisementCreateDto, Advertisement>();
            CreateMap<AdvertisementUpdateDto, Advertisement>();

            CreateMap<Company, CompanyElementViewDto>();
            CreateMap<Company, CompanyViewDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();

            CreateMap<Position, PositionElementViewDto>();
            CreateMap<Position, PositionViewDto>();
            CreateMap<PositionCreateDto, Position>();
            CreateMap<PositionUpdateDto, Position>();

            CreateMap<FeaturedAdvertisement, FeaturedAdvertisementViewDto>();
            CreateMap<FeaturedAdvertisementCreateDto, FeaturedAdvertisement>();

            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentViewDto>();
            CreateMap<StudentUpdateDto, Student>();
            
            CreateMap<EmployerCreateDto, Employer>();
            CreateMap<Employer, EmployerViewDto>();
            CreateMap<EmployerUpdateDto, Employer>();

            CreateMap<Resume, ResumeDto>().ReverseMap();
        }
    }
}
