using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Models;

namespace SecureGovernment.Domain.Tests
{
    public sealed class AssertHelper
    {
        public static void AssertWebsiteEntry(WebsiteEntry websiteEntry, string hostname, GovernmentType governmentType, string agency, string organization, string city, string state, string contactEmail)
        {
            Assert.AreEqual(hostname, websiteEntry.Hostname);
            Assert.AreEqual(governmentType, websiteEntry.GovernmentType);
            Assert.AreEqual(agency, websiteEntry.Agency);
            Assert.AreEqual(organization, websiteEntry.Organization);
            Assert.AreEqual(city, websiteEntry.City);
            Assert.AreEqual(state, websiteEntry.State);
            Assert.AreEqual(contactEmail, websiteEntry.ContactEmail);
        }
    }
}
