using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanRunner.Data.Entities
{
    [Table("websites")]
    public partial class Websites
    {
        public Websites()
        {
            Scans = new HashSet<Scans>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("hostname", TypeName = "character varying")]
        public string Hostname { get; set; }
        [Column("last_scan")]
        public DateTime LastScan { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("category_id")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Websites")]
        public virtual WebsiteCategories Category { get; set; }
        [InverseProperty("Website")]
        public virtual ICollection<Scans> Scans { get; set; }
    }
}
