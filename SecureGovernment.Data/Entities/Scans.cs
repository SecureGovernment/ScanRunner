using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanRunner.Data.Entities
{
    [Table("scans")]
    public partial class Scans
    {
        public Scans()
        {
            Analysis = new HashSet<Analysis>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        [Required]
        [Column("target", TypeName = "character varying")]
        public string Target { get; set; }
        [Column("replay")]
        public int? Replay { get; set; }
        [Column("has_tls")]
        public bool HasTls { get; set; }
        [Column("cert_id")]
        public int? CertId { get; set; }
        [Column("trust_id")]
        public int? TrustId { get; set; }
        [Column("is_valid")]
        public bool IsValid { get; set; }
        [Column("completion_perc")]
        public int CompletionPerc { get; set; }
        [Required]
        [Column("validation_error", TypeName = "character varying")]
        public string ValidationError { get; set; }
        [Required]
        [Column("scan_error", TypeName = "character varying")]
        public string ScanError { get; set; }
        [Required]
        [Column("conn_info", TypeName = "jsonb")]
        public string ConnInfo { get; set; }
        [Column("ack")]
        public bool Ack { get; set; }
        [Column("attempts")]
        public int? Attempts { get; set; }
        [Required]
        [Column("analysis_params", TypeName = "jsonb")]
        public string AnalysisParams { get; set; }
        [Column("website_id")]
        public int? WebsiteId { get; set; }

        [ForeignKey("CertId")]
        [InverseProperty("Scans")]
        public virtual Certificates Cert { get; set; }
        [ForeignKey("TrustId")]
        [InverseProperty("Scans")]
        public virtual Trust Trust { get; set; }
        [ForeignKey("WebsiteId")]
        [InverseProperty("Scans")]
        public virtual Websites Website { get; set; }
        [InverseProperty("Scan")]
        public virtual ICollection<Analysis> Analysis { get; set; }
    }
}
