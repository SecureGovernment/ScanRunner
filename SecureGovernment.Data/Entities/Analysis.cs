using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanRunner.Data.Entities
{
    [Table("analysis")]
    public partial class Analysis
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("scan_id")]
        public int? ScanId { get; set; }
        [Required]
        [Column("worker_name", TypeName = "character varying")]
        public string WorkerName { get; set; }
        [Column("success")]
        public bool Success { get; set; }
        [Column("output", TypeName = "jsonb")]
        public string Output { get; set; }

        [ForeignKey("ScanId")]
        [InverseProperty("Analysis")]
        public virtual Scans Scan { get; set; }
    }
}
