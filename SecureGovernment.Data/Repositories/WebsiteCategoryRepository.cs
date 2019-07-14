using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScanRunner.Data.Entities;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Interfaces.Infastructure;
using SecureGovernment.Domain.Interfaces.Repositories;
using System;
using System.Linq;

namespace SecureGovernment.Data.Repositories
{
    public class WebsiteCategoryRepository : DatabaseRepository<WebsiteCategories>, IWebsiteCategoryRepository
    {
        public WebsiteCategoryRepository(IDatabaseFactory factory) : base(factory){}
        public IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        public void Add(WebsiteCategoryDto websiteCategoryDto)
        {
            var entity = Mapper.Map<WebsiteCategories>(websiteCategoryDto);
            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                unitOfWork.Queue(new EntityCommand<WebsiteCategories>(CommandType.CREATE, entity));
            }
        }

        public WebsiteCategoryDto GetWebsiteCategory(GovernmentType domainType, string agency, string organization, string city, string state)
        {
            var entity = (from websiteCategory in this.DbSet
                    where websiteCategory.GovernmentType == (int)domainType &&
                    websiteCategory.Agency.Equals(agency, StringComparison.OrdinalIgnoreCase) &&
                    websiteCategory.Organization.Equals(organization, StringComparison.OrdinalIgnoreCase) &&
                    websiteCategory.State.Equals(state, StringComparison.OrdinalIgnoreCase) &&
                    websiteCategory.City.Equals(city, StringComparison.OrdinalIgnoreCase)
                    select websiteCategory).SingleOrDefault();

            if (entity == null) return null;
            return Mapper.Map<WebsiteCategoryDto>(entity);
        }

        public int GetLargestId()
        {
            var anyWebsiteCategories = (from websiteCategory in this.DbSet
                               select websiteCategory).Any();
            if (!anyWebsiteCategories) return 0;

            return (from websiteCategory in this.DbSet
                    select websiteCategory.Id).Max();
        }
    }
}
