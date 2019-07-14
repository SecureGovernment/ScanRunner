using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanRunner.Data.Entities
{
    [Table("trust")]
    public partial class Trust
    {
        public Trust()
        {
            Scans = new HashSet<Scans>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("cert_id")]
        public int CertId { get; set; }
        [Column("issuer_id")]
        public int IssuerId { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        [Column("trusted_ubuntu")]
        public bool? TrustedUbuntu { get; set; }
        [Column("trusted_mozilla")]
        public bool? TrustedMozilla { get; set; }
        [Column("trusted_microsoft")]
        public bool? TrustedMicrosoft { get; set; }
        [Column("trusted_apple")]
        public bool? TrustedApple { get; set; }
        [Column("trusted_android")]
        public bool? TrustedAndroid { get; set; }
        [Column("is_current")]
        public bool IsCurrent { get; set; }

        [ForeignKey("CertId")]
        [InverseProperty("TrustCert")]
        public virtual Certificates Cert { get; set; }
        [ForeignKey("IssuerId")]
        [InverseProperty("TrustIssuer")]
        public virtual Certificates Issuer { get; set; }
        [InverseProperty("Trust")]
        public virtual ICollection<Scans> Scans { get; set; }
    }
}
