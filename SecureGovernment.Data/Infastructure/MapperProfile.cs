using AutoMapper;
using ScanRunner.Data.Entities;
using SecureGovernment.Domain.Dtos;

namespace SecureGovernment.Data.Infastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<WebsiteDto, Websites>(MemberList.Source);
            CreateMap<WebsiteCategoryDto, WebsiteCategories>(MemberList.Source);
        }
    }
}
