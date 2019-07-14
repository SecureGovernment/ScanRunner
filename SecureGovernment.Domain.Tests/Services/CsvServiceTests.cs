using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Services;

namespace SecureGovernment.Domain.Tests.Services
{
    [TestClass]
    public class CsvServiceTests
    {
        [TestMethod]
        public void WebsiteService_ParseWebsiteCsvList()
        {
            //Arrange
            var csvFile = "Domain Name,Domain Type,Agency,Organization,City,State,Security Contact Email\n" +
                      "ABERDEENMD.GOV,City,Non - Federal Agency,City of Aberdeen,Aberdeen,MD,(blank)\n" +
                      "ADAMSCOUNTYMS.GOV,County,Non-Federal Agency,Adams County Board of Supervisors,Natchez,MS,(blank)\n" +
                      "511TX.GOV,State/Local Govt,Non-Federal Agency,Tx. Dept. of Transportation,Austin,TX,(blank)\n" +
                      "BOONECOUNTYFPDMO.GOV,Interstate Agency,Non-Federal Agency,Boone County Fire Protection District,Columbia,MO,(blank)\n" +
                      "AOC.GOV,Federal Agency - Legislative,Architect of the Capitol,The Architect of the Capitol,Washington,DC,(blank)\n" +
                      "ACUS.GOV,Federal Agency - Executive,Administrative Conference of the United States,ADMINISTRATIVE CONFERENCE OF THE UNITED STATES,Washington,DC,(blank)\n" +
                      "SC-US.GOV,Federal Agency - Judicial,The Supreme Court,Supreme Court of the United States,Washington,DC,(blank)\n" +
                      "29PALMSBOMI-NSN.GOV,Native Sovereign Nation,Indian Affairs,Twenty-Nine Palms Band of Mission Indians,Coachella,CA,(blank)\n";

            var csvService = new CsvService();
            //Act
            var websiteEntries = csvService.ParseWebsiteCsvList(csvFile);

            //Assert
            Assert.AreEqual(8, websiteEntries.Count);
            AssertHelper.AssertWebsiteEntry(websiteEntries[0], "ABERDEENMD.GOV", GovernmentType.CITY, "Non - Federal Agency", "City of Aberdeen", "Aberdeen", "MD", "(blank)");
            AssertHelper.AssertWebsiteEntry(websiteEntries[1], "ADAMSCOUNTYMS.GOV", GovernmentType.COUNTY, "Non-Federal Agency", "Adams County Board of Supervisors", "Natchez", "MS", "(blank)");
            AssertHelper.AssertWebsiteEntry(websiteEntries[2], "511TX.GOV", GovernmentType.STATE_LOCAL, "Non-Federal Agency", "Tx. Dept. of Transportation", "Austin", "TX", "(blank)");
            AssertHelper.AssertWebsiteEntry(websiteEntries[3], "BOONECOUNTYFPDMO.GOV", GovernmentType.INTERSTATE_AGENCY, "Non-Federal Agency", "Boone County Fire Protection District", "Columbia", "MO", "(blank)");
            AssertHelper.AssertWebsiteEntry(websiteEntries[4], "AOC.GOV", GovernmentType.FEDERAL_LEGISLATIVE, "Architect of the Capitol", "The Architect of the Capitol", "Washington", "DC", "(blank)");
            AssertHelper.AssertWebsiteEntry(websiteEntries[5], "ACUS.GOV", GovernmentType.FEDERAL_EXECUTIVE, "Administrative Conference of the United States", "ADMINISTRATIVE CONFERENCE OF THE UNITED STATES", "Washington", "DC", "(blank)");
            AssertHelper.AssertWebsiteEntry(websiteEntries[6], "SC-US.GOV", GovernmentType.FEDERAL_JUDICIAL, "The Supreme Court", "Supreme Court of the United States", "Washington", "DC", "(blank)");
            AssertHelper.AssertWebsiteEntry(websiteEntries[7], "29PALMSBOMI-NSN.GOV", GovernmentType.NATIVE_SOVEREIGN_NATION, "Indian Affairs", "Twenty-Nine Palms Band of Mission Indians", "Coachella", "CA", "(blank)");
        }
    }
}
