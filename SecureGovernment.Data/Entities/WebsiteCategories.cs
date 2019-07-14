using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanRunner.Data.Entities
{
    [Table("website_categories")]
    public partial class WebsiteCategories
    {
        public WebsiteCategories()
        {
            Websites = new HashSet<Websites>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("city", TypeName = "character varying")]
        public string City { get; set; }
        [Column("state", TypeName = "character varying")]
        public string State { get; set; }
        [Column("organization", TypeName = "character varying")]
        public string Organization { get; set; }
        [Column("agency", TypeName = "character varying")]
        public string Agency { get; set; }
        [Column("government_type")]
        public int GovernmentType { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Websites> Websites { get; set; }
    }
}
