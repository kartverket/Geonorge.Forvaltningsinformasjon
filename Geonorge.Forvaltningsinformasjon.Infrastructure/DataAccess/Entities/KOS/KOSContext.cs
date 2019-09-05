using Microsoft.EntityFrameworkCore;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Kos
{
    internal partial class KosContext : DbContext
    {
        public KosContext()
        {
        }
        
        public KosContext(string connectionString) : this(new DbContextOptionsBuilder<KosContext>()
            .UseSqlServer(connectionString).Options)
        {
        }
        

        public KosContext(DbContextOptions<KosContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataSet>(entity =>
            {
                entity.ToTable("Datasett");

                entity.Property(e => e.Name).HasColumnName("Navn").HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(255);
                entity.Property(e => e.Active).HasColumnName("Aktiv");

                entity.HasQueryFilter(e => e.Active == 1);
            });

            modelBuilder.Entity<FdvDataSet>(entity =>
            {
                entity.ToTable("FDVDatasett");

                entity.Property(e => e.Id).HasConversion(v => (long)v, v => (int)v);
                entity.Property(e => e.DataSetId).HasColumnName("Datasett_Id");
                entity.Property(e => e.UpdateTypeId).HasColumnName("FDVDatasettForvaltningstype_Id");
                entity.Property(e => e.ProjectId).HasColumnName("FDVProsjekt_Id");

                entity.HasOne(d => d.DataSet)
                    .WithMany(p => p.FdvDataSet)
                    .HasForeignKey(d => d.DataSetId)
                    .HasConstraintName("FK_FDVDatasett_Datasett");

                entity.HasOne(d => d.UpdateType)
                    .WithMany(p => p.FdvDataSet)
                    .HasForeignKey(d => d.UpdateTypeId)
                    .HasConstraintName("FK_FDVDatasett_FDVDatasettForvaltningstype");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.FdvDataSet)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_FDVDatasett_FDVProsjekt");
            });

            modelBuilder.Entity<DataSetUpdateType>(entity =>
            {
                entity.ToTable("FDVDatasettForvaltningstype");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description).HasColumnName("Beskrivelse").HasMaxLength(255);
                entity.Property(e => e.Name).HasColumnName("Type").HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("FDVProsjekt");

                entity.Property(e => e.Year).HasColumnName("Ar").HasMaxLength(4);

                entity.Property(e => e.MunicipalityNumber)
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);

                entity.Property(e => e.Name).HasColumnName("Prosjektnavn").HasMaxLength(50);

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.MunicipalityNumber)
                    .HasConstraintName("FK_FDVProsjekt_Kommune");

                entity.Property(e => e.Active).HasColumnName("Aktiv");

                entity.HasQueryFilter(e => e.Active == 1);
            });

            modelBuilder.Entity<Round>(entity =>
            { 
                entity.ToTable("FDVRunde");

                entity.Property(e => e.ProjectId).HasColumnName("FDVProsjekt_Id");
                entity.Property(e => e.Active).HasColumnName("Aktiv");
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Round)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_FDVRunde_FDVProsjekt");
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.ToTable("Fylke");

                entity.HasKey(e => e.Number);

                entity.Property(e => e.Number)
                        .HasColumnName("Fylkesnr")
                        .HasMaxLength(2)
                        .ValueGeneratedNever();
                entity.Property(e => e.BBoxNorthEastE).HasColumnName("BB_NordOst_E");
                entity.Property(e => e.BBoxNorthEastN).HasColumnName("BB_NordOst_N");
                entity.Property(e => e.BBoxSouthWestE).HasColumnName("BB_SorVest_E");
                entity.Property(e => e.BBoxSouthWestN).HasColumnName("BB_SorVest_N");
                entity.Property(e => e.Name).HasColumnName("Fylkesnavn").HasMaxLength(255);
                entity.Property(e => e.Active).HasColumnName("Aktiv");

                entity.HasQueryFilter(e => e.Active == 1);
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.ToTable("Kommune");
                entity.HasKey(e => e.Number);

                entity.Property(e => e.Number).HasColumnName("Kommunenr")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();
                entity.Property(e => e.Name).HasColumnName("Kommunenavn").HasMaxLength(255);
                entity.Property(e => e.BBoxNorthEastE).HasColumnName("BB_NordOst_E");
                entity.Property(e => e.BBoxNorthEastN).HasColumnName("BB_NordOst_N");
                entity.Property(e => e.BBoxSouthVestNE).HasColumnName("BB_SorVest_E");
                entity.Property(e => e.BBoxSouthVestN).HasColumnName("BB_SorVest_N");
                entity.Property(e => e.CountyId)
                    .HasColumnName("Fylke_Fylkesnr")
                    .HasMaxLength(2);
                entity.Property(e => e.CoordSys).HasColumnName("Koordsys_Koordsys");
                entity.Property(e => e.VerticalDatum).HasColumnName("VertDatum").HasMaxLength(20);
                entity.Property(e => e.Active).HasColumnName("Aktiv");

                entity.HasOne(d => d.County)
                        .WithMany(p => p.Municipalities)
                        .HasForeignKey(d => d.CountyId)
                        .HasConstraintName("FK_Kommune_Fylke");
                
                entity.HasQueryFilter(e => e.Active == 1);
            });

            modelBuilder.Entity<CentralFkb>(entity =>
            {
                entity.ToTable("SentralFKB");

                entity.Property(e => e.DirectUpdateInroduced)
                    .HasColumnName("Direkteoppdateringinfort")
                    .HasMaxLength(8);
                entity.Property(e => e.MunicipalitzNumber)
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);
                entity.Property(e => e.PlannedIntroduction)
                    .HasColumnName("PlanlagtInnforing")
                    .HasMaxLength(8);

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.CentralFkb)
                    .HasForeignKey(d => d.MunicipalitzNumber)
                    .HasConstraintName("FK_SentralFKB_Kommune");
            });

            modelBuilder.Entity<TransactionData>(entity =>
            {
                entity.ToTable("SentralFKB_Statistikk");

                entity.Property(e => e.ObjectCount).HasColumnName("Ant_objekter");

                entity.Property(e => e.SumLastYear).HasColumnName("Ant_trans_ar");

                entity.Property(e => e.SumLastMonth).HasColumnName("Ant_trans_mnd");

                entity.Property(e => e.SumLastWeek).HasColumnName("Ant_trans_uke");

                entity.Property(e => e.DataSetId).HasColumnName("Datasett_Id");

                entity.Property(e => e.GeonorgeFileDate)
                    .HasColumnName("Geonorge_fildato")
                    .HasColumnType("date");

                entity.Property(e => e.MunicipalityNumber)
                    .IsRequired()
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);

                entity.HasOne(d => d.DataSet)
                    .WithMany(p => p.TransactionData)
                    .HasForeignKey(d => d.DataSetId)
                    .HasConstraintName("FK_SentralFkbStatistikk_Datasett");

            });
        }
    }
}
