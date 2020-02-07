using Microsoft.EntityFrameworkCore;
using System;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
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

                entity.HasQueryFilter(e => e.Active > 0);
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
                    .HasForeignKey(d => d.DataSetId);

                entity.HasOne(d => d.UpdateType)
                    .WithMany(p => p.FdvDataSet)
                    .HasForeignKey(d => d.UpdateTypeId);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.FdvDataSet)
                    .HasForeignKey(d => d.ProjectId);
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
                    .HasForeignKey(d => d.MunicipalityNumber);

                entity.Property(e => e.Active).HasColumnName("Aktiv");

                entity.HasQueryFilter(e => e.Active > 0);
            });

            modelBuilder.Entity<Round>(entity =>
            { 
                entity.ToTable("FDVRunde");

                entity.Property(e => e.ProjectId).HasColumnName("FDVProsjekt_Id");
                entity.Property(e => e.Active).HasColumnName("Aktiv");
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Round)
                    .HasForeignKey(d => d.ProjectId);
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.ToTable("Fylke");

                entity.HasKey(e => e.Number);

                entity.Property(e => e.Number)
                        .HasColumnName("Fylkesnr")
                        .HasMaxLength(2)
                        .ValueGeneratedNever();
                entity.Property(e => e.MaxX).HasColumnName("BB_NordOst_E");
                entity.Property(e => e.MaxY).HasColumnName("BB_NordOst_N");
                entity.Property(e => e.MinX).HasColumnName("BB_SorVest_E");
                entity.Property(e => e.MinY).HasColumnName("BB_SorVest_N");
                entity.Property(e => e.Name).HasColumnName("Fylkesnavn").HasMaxLength(255);
                entity.Property(e => e.Active).HasColumnName("Aktiv");
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.ToTable("Kommune");
                entity.HasKey(e => e.Number);

                entity.Property(e => e.Number).HasColumnName("Kommunenr")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();
                entity.Property(e => e.Name).HasColumnName("Kommunenavn").HasMaxLength(255);
                entity.Property(e => e.MaxX).HasColumnName("BB_NordOst_E");
                entity.Property(e => e.MaxY).HasColumnName("BB_NordOst_N");
                entity.Property(e => e.MinX).HasColumnName("BB_SorVest_E");
                entity.Property(e => e.MinY).HasColumnName("BB_SorVest_N");
                entity.Property(e => e.CountyId)
                    .HasColumnName("Fylke_Fylkesnr")
                    .HasMaxLength(2);
                entity.Property(e => e.CoordinateSystemId).HasColumnName("Koordsys_Koordsys")
                    .HasConversion(v => (short)v, v => v); ;
                entity.Property(e => e.VerticalDatum).HasColumnName("VertDatum").HasMaxLength(20);
                entity.Property(e => e.Active).HasColumnName("Aktiv");

                entity.HasOne(d => d.County)
                        .WithMany(p => p.Municipalities)
                        .HasForeignKey(d => d.CountyId);

                entity.HasOne(d => d.CoordinateSystemObject)
                    .WithMany(p => p.Municipalities)
                    .HasForeignKey(d => d.CoordinateSystemId);
            });

            modelBuilder.Entity<CentralFkb>(entity =>
            {
                entity.ToTable("SentralFKB");

                entity.Property(e => e.DirectUpdateIntroduced)
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
                    .HasForeignKey(d => d.MunicipalitzNumber);
            });

            modelBuilder.Entity<TransactionData>(entity =>
            {
                entity.ToTable("SentralFKB_Statistikk");

                entity.Property(e => e.ObjectCount).HasColumnName("Ant_objekter").HasConversion(v => (int?)v, v => (int?)v ?? 0);

                entity.Property(e => e.SumLastYear).HasColumnName("Ant_trans_ar");

                entity.Property(e => e.SumLastMonth).HasColumnName("Ant_trans_mnd");

                entity.Property(e => e.SumLastWeek).HasColumnName("Ant_trans_uke");

                entity.Property(e => e.DataSetId).HasColumnName("Datasett_Id");

                entity.Property(e => e.Year0).HasColumnName("Alder_1").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.Year1).HasColumnName("Alder_2").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.Year2).HasColumnName("Alder_3").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.Year3).HasColumnName("Alder_4").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.Year4).HasColumnName("Alder_5").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.Years5To9).HasColumnName("Alder_6_10").HasConversion(v => (double?)v, v => (long?)v ?? 0); ;
                entity.Property(e => e.Years10To19).HasColumnName("Alder_11_20").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.Older).HasColumnName("Alder_21_Over").HasConversion(v => (double?)v, v => (long?)v ?? 0);

                entity.Property(e => e.Measured).HasColumnName("Kvalitet_Landmalt").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.PhotogrammetricB).HasColumnName("Kvalitet_B").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.PhotogrammetricC).HasColumnName("Kvalitet_C").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.DigitalizedM200).HasColumnName("Kvalitet_Dig_M200").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.DigitalizedS200).HasColumnName("Kvalitet_Dig_S200").HasConversion(v => (double?)v, v => (long?)v ?? 0);
                entity.Property(e => e.NotMeasured).HasColumnName("Kvalitet_IkkeMalt").HasConversion(v => (double?)v, v => (long?)v ?? 0);

                entity.Property(e => e.GeonorgeFileDate)
                    .HasColumnName("Geonorge_fildato")
                    .HasColumnType("date");

                entity.Property(e => e.MunicipalityNumber)
                    .IsRequired()
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);

                entity.HasOne(d => d.DataSet)
                    .WithMany(p => p.TransactionData)
                    .HasForeignKey(d => d.DataSetId);

                entity.HasOne(e => e.Municipality)
                    .WithMany(m => m.TransactionData)
                    .HasForeignKey(e => e.MunicipalityNumber);

                entity.HasQueryFilter(e => e.Municipality.Active > 0 && e.DataSet.Active > 0);
            });

            modelBuilder.Entity<DataQualityClassification>(entity =>
            {
                entity.ToTable("FKBKommuneKvalitet");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.MunicipalityNumber).HasColumnName("Kommune_Kommunenr");
                entity.Property(e => e.AreaA).HasColumnName("ArealA");
                entity.Property(e => e.AreaB).HasColumnName("ArealB");
                entity.Property(e => e.AreaC).HasColumnName("ArealC");
                entity.Property(e => e.AreaD).HasColumnName("ArealD");
            });

            modelBuilder.Entity<CoordintateSystem>(entity =>
            {
                entity.ToTable("Koordsys");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("Koordsys");
                entity.Property(e => e.EpsgName).HasColumnName("EPSG");
            });
        }
    }
}
