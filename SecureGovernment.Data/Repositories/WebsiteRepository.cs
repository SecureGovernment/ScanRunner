using AutoMapper;
using ScanRunner.Data.Entities;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Interfaces.Infastructure;
using SecureGovernment.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecureGovernment.Data.Repositories
{
    public class WebsiteRepository : DatabaseRepository<Websites>, IWebsiteRepository
    {
        public WebsiteRepository(IDatabaseFactory factory) : base(factory){}
        public IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        public void Add(WebsiteDto website)
        {
            var entity = Mapper.Map<Websites>(website);
            using(var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                unitOfWork.Queue(new EntityCommand<Websites>(CommandType.CREATE, entity));
            }
        }

        public IDictionary<string, bool> DoWebsitesExist(IList<string> hostNames)
        {
            hostNames = hostNames.Select(x => x.ToLower()).ToList();

            var websites = (from website in this.DbSet
                            where hostNames.Any(s => s.Equals(website.Hostname, StringComparison.OrdinalIgnoreCase))
                            select website.Hostname).Select(x => x.ToLower()).ToList();

            var websitesExist = new Dictionary<string, bool>();
            foreach (var website in hostNames)
                websitesExist.Add(website, websites.Contains(website));

            return websitesExist;
        }

        public int GetLargestId()
        {
            var anyWebsites = (from website in this.DbSet
                               select website).Any();
            if (!anyWebsites) return 0;

            return (from website in this.DbSet
                    select website.Id).Max();
        }

        public IList<WebsiteDto> GetNextScanTargets(int numberOfTargets = 10)
        {
            return (from websites in this.DbSet
                    orderby websites.LastScan
                    select websites).Take(numberOfTargets).Select(Mapper.Map<WebsiteDto>).ToList();
        }



        public void UpdateLastScanDateToNow(int id)
        {
            var website = (from websites in this.DbSet
                           where websites.Id == id
                           select websites).Single();
            website.LastScan = DateTime.Now;

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                unitOfWork.Queue(new EntityCommand<Websites>(CommandType.UPDATE, website));
            }

        }
    }
}