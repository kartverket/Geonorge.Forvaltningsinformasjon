using Microsoft.EntityFrameworkCore;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Georef
{
    internal class GeorefContext : DbContext
    {
        public GeorefContext()
        {
        }

        public GeorefContext(string connectionString) : this(new DbContextOptionsBuilder<GeorefContext>()
            .UseNpgsql(connectionString).Options)
        {
        }


        public GeorefContext(DbContextOptions<GeorefContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataQualityClassification>(entity =>
            {
                entity.ToTable("fkbstr_kommune");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("objid");

                entity.Property(e => e.Class)
                    .HasColumnName("fkb_standard");
                entity.Property(e => e.Area)
                    .HasColumnName("areal_m2");
                entity.Property(e => e.MunicipalityNumber)
                    .HasColumnName("kommunenummer");
                entity.Property(e => e.MunicipalityName)
                    .HasColumnName("kommunenavn");
            });
        }
    }
}
