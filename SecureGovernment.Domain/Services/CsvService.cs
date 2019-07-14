using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Interfaces.Services;
using SecureGovernment.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SecureGovernment.Domain.Services
{
    public class CsvService : ICsvService
    {
        public IList<WebsiteEntry> ParseWebsiteCsvList(string csvFile)
        {
            var websiteEntries = new List<WebsiteEntry>();
            TextReader textReader = new StringReader(csvFile);
            using (var csvReader = new CsvReader(textReader, new Configuration() { HasHeaderRecord = true }))
            {
                csvReader.Configuration.RegisterClassMap<DomainCsvMap>();
                websiteEntries = csvReader.GetRecords<WebsiteEntry>().ToList();
            }

            return websiteEntries;
        }
    }

    public class DomainCsvMap : ClassMap<WebsiteEntry>
    {
        public DomainCsvMap()
        {
            Map(x => x.Hostname).Name("Domain Name");
            Map(x => x.GovernmentType).Name("Domain Type").TypeConverter<DomainTypeConverter>();
            Map(x => x.Agency).Name("Agency");
            Map(x => x.Organization).Name("Organization");
            Map(x => x.City).Name("City");
            Map(x => x.State).Name("State");
            Map(x => x.ContactEmail).Name("Security Contact Email");
        }
    }

    public class DomainTypeConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            switch (text.ToLower())
            {
                case "city":
                    return GovernmentType.CITY;
                case "county":
                    return GovernmentType.COUNTY;
                case "state/local govt":
                    return GovernmentType.STATE_LOCAL;
                case "interstate agency":
                    return GovernmentType.INTERSTATE_AGENCY;
                case "federal agency - executive":
                    return GovernmentType.FEDERAL_EXECUTIVE;
                case "federal agency - legislative":
                    return GovernmentType.FEDERAL_LEGISLATIVE;
                case "federal agency - judicial":
                    return GovernmentType.FEDERAL_JUDICIAL;
                case "native sovereign nation":
                    return GovernmentType.NATIVE_SOVEREIGN_NATION;
                default:
                    throw new NotImplementedException();
            }
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }
    }
}
