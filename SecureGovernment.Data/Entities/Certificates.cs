using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanRunner.Data.Entities
{
    [Table("certificates")]
    public partial class Certificates
    {
        public Certificates()
        {
            Scans = new HashSet<Scans>();
            TrustCert = new HashSet<Trust>();
            TrustIssuer = new HashSet<Trust>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("sha1_fingerprint", TypeName = "character varying")]
        public string Sha1Fingerprint { get; set; }
        [Required]
        [Column("sha256_fingerprint", TypeName = "character varying")]
        public string Sha256Fingerprint { get; set; }
        [Required]
        [Column("sha256_spki", TypeName = "character varying")]
        public string Sha256Spki { get; set; }
        [Required]
        [Column("sha256_subject_spki", TypeName = "character varying")]
        public string Sha256SubjectSpki { get; set; }
        [Required]
        [Column("pkp_sha256", TypeName = "character varying")]
        public string PkpSha256 { get; set; }
        [Column("serial_number", TypeName = "character varying")]
        public string SerialNumber { get; set; }
        [Column("version")]
        public int? Version { get; set; }
        [Column("domains", TypeName = "character varying")]
        public string Domains { get; set; }
        [Column("subject", TypeName = "jsonb")]
        public string Subject { get; set; }
        [Column("issuer", TypeName = "jsonb")]
        public string Issuer { get; set; }
        [Column("is_ca")]
        public bool? IsCa { get; set; }
        [Column("not_valid_before")]
        public DateTime? NotValidBefore { get; set; }
        [Column("not_valid_after")]
        public DateTime? NotValidAfter { get; set; }
        [Column("first_seen")]
        public DateTime? FirstSeen { get; set; }
        [Column("last_seen")]
        public DateTime? LastSeen { get; set; }
        [Column("key_alg", TypeName = "character varying")]
        public string KeyAlg { get; set; }
        [Column("key", TypeName = "jsonb")]
        public string Key { get; set; }
        [Column("x509_basicconstraints", TypeName = "character varying")]
        public string X509Basicconstraints { get; set; }
        [Column("x509_crldistributionpoints", TypeName = "jsonb")]
        public string X509Crldistributionpoints { get; set; }
        [Column("x509_extendedkeyusage", TypeName = "jsonb")]
        public string X509Extendedkeyusage { get; set; }
        [Column("x509_extendedkeyusageoid", TypeName = "jsonb")]
        public string X509Extendedkeyusageoid { get; set; }
        [Column("x509_authoritykeyidentifier", TypeName = "character varying")]
        public string X509Authoritykeyidentifier { get; set; }
        [Column("x509_subjectkeyidentifier", TypeName = "character varying")]
        public string X509Subjectkeyidentifier { get; set; }
        [Column("x509_keyusage", TypeName = "jsonb")]
        public string X509Keyusage { get; set; }
        [Column("x509_certificatepolicies", TypeName = "jsonb")]
        public string X509Certificatepolicies { get; set; }
        [Column("x509_authorityinfoaccess", TypeName = "character varying")]
        public string X509Authorityinfoaccess { get; set; }
        [Column("x509_subjectaltname", TypeName = "jsonb")]
        public string X509Subjectaltname { get; set; }
        [Column("x509_issueraltname", TypeName = "character varying")]
        public string X509Issueraltname { get; set; }
        [Column("is_name_constrained")]
        public bool? IsNameConstrained { get; set; }
        [Column("permitted_names", TypeName = "jsonb")]
        public string PermittedNames { get; set; }
        [Column("signature_algo", TypeName = "character varying")]
        public string SignatureAlgo { get; set; }
        [Column("in_ubuntu_root_store")]
        public bool? InUbuntuRootStore { get; set; }
        [Column("in_mozilla_root_store")]
        public bool? InMozillaRootStore { get; set; }
        [Column("in_microsoft_root_store")]
        public bool? InMicrosoftRootStore { get; set; }
        [Column("in_apple_root_store")]
        public bool? InAppleRootStore { get; set; }
        [Column("in_android_root_store")]
        public bool? InAndroidRootStore { get; set; }
        [Column("is_revoked")]
        public bool? IsRevoked { get; set; }
        [Column("revoked_at")]
        public DateTime? RevokedAt { get; set; }
        [Required]
        [Column("raw_cert", TypeName = "character varying")]
        public string RawCert { get; set; }
        [Required]
        [Column("permitted_dns_domains", TypeName = "character varying[]")]
        public string[] PermittedDnsDomains { get; set; }
        [Required]
        [Column("permitted_ip_addresses", TypeName = "character varying[]")]
        public string[] PermittedIpAddresses { get; set; }
        [Required]
        [Column("excluded_dns_domains", TypeName = "character varying[]")]
        public string[] ExcludedDnsDomains { get; set; }
        [Required]
        [Column("excluded_ip_addresses", TypeName = "character varying[]")]
        public string[] ExcludedIpAddresses { get; set; }
        [Column("is_technically_constrained")]
        public bool IsTechnicallyConstrained { get; set; }
        [Column("cisco_umbrella_rank")]
        public int CiscoUmbrellaRank { get; set; }
        [Column("mozillapolicyv2_5", TypeName = "jsonb")]
        public string Mozillapolicyv25 { get; set; }

        [InverseProperty("Cert")]
        public virtual ICollection<Scans> Scans { get; set; }
        [InverseProperty("Cert")]
        public virtual ICollection<Trust> TrustCert { get; set; }
        [InverseProperty("Issuer")]
        public virtual ICollection<Trust> TrustIssuer { get; set; }
    }
}
