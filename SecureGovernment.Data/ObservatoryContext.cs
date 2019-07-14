using Microsoft.EntityFrameworkCore;
using ScanRunner.Data.Entities;
using SecureGovernment.Domain.Interfaces.Infastructure;
using System;

namespace SecureGovernment.Data
{
    public class ObservatoryContext : DbContext
    {
        public ObservatoryContext(){}
        public ObservatoryContext(DbContextOptions<ObservatoryContext> options): base(options){}

        public virtual DbSet<Analysis> Analysis { get; set; }
        public virtual DbSet<Certificates> Certificates { get; set; }
        public virtual DbSet<Scans> Scans { get; set; }
        public virtual DbSet<Trust> Trust { get; set; }
        public virtual DbSet<WebsiteCategories> WebsiteCategories { get; set; }
        public virtual DbSet<Websites> Websites { get; set; }

        public ISettings Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Settings.ConnectionString, x => { x.CommandTimeout(60); x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.HasIndex(e => e.ScanId)
                    .HasName("analysis_scan_id_idx");

                entity.HasIndex(e => e.WorkerName)
                    .HasName("analysis_worker_name_idx");

                entity.HasOne(d => d.Scan)
                    .WithMany(p => p.Analysis)
                    .HasForeignKey(d => d.ScanId)
                    .HasConstraintName("analysis_scan_id_fkey");
            });

            modelBuilder.Entity<Certificates>(entity =>
            {
                entity.HasIndex(e => e.CiscoUmbrellaRank)
                    .HasName("certificates_cisco_umbrella_rank");

                entity.HasIndex(e => e.FirstSeen)
                    .HasName("certificates_first_seen_idx");

                entity.HasIndex(e => e.LastSeen)
                    .HasName("certificates_last_seen_idx");

                entity.HasIndex(e => e.Sha256Fingerprint)
                    .HasName("certificates_sha256_fingerprint_idx");

                entity.HasIndex(e => e.Subject)
                    .HasName("certificates_subject_idx");

                entity.Property(e => e.CiscoUmbrellaRank).HasDefaultValueSql("2147483647");

                entity.Property(e => e.ExcludedDnsDomains).HasDefaultValueSql("'{}'::character varying[]");

                entity.Property(e => e.ExcludedIpAddresses).HasDefaultValueSql("'{}'::character varying[]");

                entity.Property(e => e.PermittedDnsDomains).HasDefaultValueSql("'{}'::character varying[]");

                entity.Property(e => e.PermittedIpAddresses).HasDefaultValueSql("'{}'::character varying[]");
            });

            modelBuilder.Entity<Scans>(entity =>
            {
                entity.HasIndex(e => e.Ack)
                    .HasName("scans_ack_idx");

                entity.HasIndex(e => e.CertId)
                    .HasName("scans_cert_id_idx");

                entity.HasIndex(e => e.HasTls)
                    .HasName("scans_has_tls_idx");

                entity.HasIndex(e => e.Target)
                    .HasName("scans_target_idx");

                entity.HasIndex(e => e.Timestamp)
                    .HasName("scans_timestamp_idx");

                entity.HasIndex(e => new { e.CompletionPerc, e.Attempts })
                    .HasName("scans_completion_attempts_idx");

                entity.HasOne(d => d.Cert)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.CertId)
                    .HasConstraintName("scans_cert_id_fkey");

                entity.HasOne(d => d.Trust)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.TrustId)
                    .HasConstraintName("scans_trust_id_fkey");

                entity.HasOne(d => d.Website)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.WebsiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("scans_website_id_fkey");
            });

            modelBuilder.Entity<Trust>(entity =>
            {
                entity.HasIndex(e => e.CertId)
                    .HasName("trust_cert_id_idx");

                entity.HasIndex(e => e.IsCurrent)
                    .HasName("trust_is_current_idx");

                entity.HasIndex(e => e.IssuerId)
                    .HasName("trust_issuer_id_idx");

                entity.HasOne(d => d.Cert)
                    .WithMany(p => p.TrustCert)
                    .HasForeignKey(d => d.CertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("trust_cert_id_fkey");

                entity.HasOne(d => d.Issuer)
                    .WithMany(p => p.TrustIssuer)
                    .HasForeignKey(d => d.IssuerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("trust_issuer_id_fkey");
            });

            modelBuilder.Entity<Websites>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Websites)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("websites_category_id_fkey");
            });
        }
    }
}
