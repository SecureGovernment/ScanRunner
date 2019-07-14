using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Interfaces.Facades;
using SecureGovernment.Domain.Interfaces.Repositories;
using SecureGovernment.Domain.Interfaces.Services;
using System;
using System.Linq;

namespace SecureGovernment.Domain.Facades
{
    public class WebsiteFacade : IWebsiteFacade
    {
        public IWebsiteRepository WebsiteRepository { get; set; }
        public IWebsiteService WebsiteService { get; set; }
        public ICsvService CsvService { get; set; }
        public IWebsiteCategoryService WebsiteCategoryService { get; set; }
        public IWebsiteCategoryRepository WebsiteCategoryRepository { get; set; }

        public void AddWebsitesFromCsv(string rawCsv)
        {
            var websiteEntries = CsvService.ParseWebsiteCsvList(rawCsv);
            var hostnames = websiteEntries.Select(x => x.Hostname.ToLower()).ToList();
            var doWebsitesExist = WebsiteRepository.DoWebsitesExist(hostnames);

            foreach (var websiteEntry in websiteEntries)
            {
                if (!doWebsitesExist[websiteEntry.Hostname.ToLower()])
                {
                    var websiteCategory = WebsiteCategoryService.GetOrCreateWebsiteCategory(websiteEntry);

                    WebsiteRepository.Add(new WebsiteDto()
                    {
                        Id = WebsiteService.GetNextId(),
                        Hostname = websiteEntry.Hostname.ToLower(),
                        LastScan = DateTime.MinValue,
                        Status = StatusType.NEW,
                        CategoryId = websiteCategory.Id
                    });
                }
            }
        }
    }
}
