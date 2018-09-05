using Geonorge.Forvaltningsinformasjon.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Database
{
    public partial class FDV_Drift2Context : DbContext
    {
        public FDV_Drift2Context()
        {
        }
        
        public FDV_Drift2Context(string connectionString) : this(new DbContextOptionsBuilder<FDV_Drift2Context>()
            .UseSqlServer(connectionString).Options)
        { }
        

        public FDV_Drift2Context(DbContextOptions<FDV_Drift2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Datasett> Datasett { get; set; }
        public virtual DbSet<Distribusjon> Distribusjon { get; set; }
        public virtual DbSet<DistribusjonArbeid> DistribusjonArbeid { get; set; }
        public virtual DbSet<DistribusjonArbeidLogg> DistribusjonArbeidLogg { get; set; }
        public virtual DbSet<DistribusjonDatasett> DistribusjonDatasett { get; set; }
        public virtual DbSet<Fdvdatamottak> Fdvdatamottak { get; set; }
        public virtual DbSet<Fdvdatasett> Fdvdatasett { get; set; }
        public virtual DbSet<FdvdatasettArbeid> FdvdatasettArbeid { get; set; }
        public virtual DbSet<FdvdatasettArbeidLogg> FdvdatasettArbeidLogg { get; set; }
        public virtual DbSet<FdvdatasettForvaltningstype> FdvdatasettForvaltningstype { get; set; }
        public virtual DbSet<Fdvnotat> Fdvnotat { get; set; }
        public virtual DbSet<Fdvpart> Fdvpart { get; set; }
        public virtual DbSet<Fdvprosjekt> Fdvprosjekt { get; set; }
        public virtual DbSet<FdvprosjektLenke> FdvprosjektLenke { get; set; }
        public virtual DbSet<FdvprosjektLogg> FdvprosjektLogg { get; set; }
        public virtual DbSet<Fdvrunde> Fdvrunde { get; set; }
        public virtual DbSet<FdvrundeDistribusjon> FdvrundeDistribusjon { get; set; }
        public virtual DbSet<Fdvstatus1> Fdvstatus1 { get; set; }
        public virtual DbSet<Fylke> Fylke { get; set; }
        public virtual DbSet<Kartkontor> Kartkontor { get; set; }
        public virtual DbSet<Kommune> Kommune { get; set; }
        public virtual DbSet<Koordsys> Koordsys { get; set; }
        public virtual DbSet<Kunde> Kunde { get; set; }
        public virtual DbSet<KundeGruppe> KundeGruppe { get; set; }
        public virtual DbSet<Kurs> Kurs { get; set; }
        public virtual DbSet<KursDato> KursDato { get; set; }
        public virtual DbSet<KursPart> KursPart { get; set; }
        public virtual DbSet<KursPartDeltatt> KursPartDeltatt { get; set; }
        public virtual DbSet<KursType> KursType { get; set; }
        public virtual DbSet<Part> Part { get; set; }
        public virtual DbSet<PartsKontakt> PartsKontakt { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PlanInfo> PlanInfo { get; set; }
        public virtual DbSet<PuljeManed> PuljeManed { get; set; }
        public virtual DbSet<Rolle> Rolle { get; set; }
        public virtual DbSet<SentralFkb> SentralFkb { get; set; }
        public virtual DbSet<Tilgang> Tilgang { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Datasett>(entity =>
            {
                entity.Property(e => e.Dokstatus)
                    .HasColumnName("DOKStatus")
                    .HasMaxLength(50);

                entity.Property(e => e.Fdvforvaltning)
                    .HasColumnName("FDVForvaltning")
                    .HasMaxLength(50);

                entity.Property(e => e.GmlskjemaUrl)
                    .HasColumnName("GMLSkjemaURL")
                    .HasMaxLength(255);

                entity.Property(e => e.Navn).HasMaxLength(50);

                entity.Property(e => e.ObjektkatUrl)
                    .HasColumnName("ObjektkatURL")
                    .HasMaxLength(255);

                entity.Property(e => e.ProduktspekUrl)
                    .HasColumnName("ProduktspekURL")
                    .HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.Underversjon).HasMaxLength(50);

                entity.Property(e => e.Versjon).HasMaxLength(50);
            });

            modelBuilder.Entity<Distribusjon>(entity =>
            {
                entity.Property(e => e.AntallEndringerFkb).HasColumnName("AntallEndringerFKB");

                entity.Property(e => e.DatamottaksDato).HasMaxLength(8);

                entity.Property(e => e.DatamottaksDatoForrige).HasMaxLength(8);

                entity.Property(e => e.DistribusjonsDato).HasMaxLength(8);

                entity.Property(e => e.Distribusjontype).HasMaxLength(255);

                entity.Property(e => e.Kommentar).HasMaxLength(1000);

                entity.Property(e => e.KommuneKommunenr)
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);

                entity.HasOne(d => d.KommuneKommunenrNavigation)
                    .WithMany(p => p.Distribusjon)
                    .HasForeignKey(d => d.KommuneKommunenr)
                    .HasConstraintName("FK_Distribusjon_Kommune");
            });

            modelBuilder.Entity<DistribusjonArbeid>(entity =>
            {
                entity.Property(e => e.Brukernavn).HasMaxLength(50);

                entity.Property(e => e.Dato).HasMaxLength(8);

                entity.Property(e => e.FdvrundeId).HasColumnName("FDVRunde_Id");

                entity.Property(e => e.Kommentar).HasMaxLength(255);

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.Property(e => e.Navn).HasMaxLength(100);

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.HasOne(d => d.Fdvrunde)
                    .WithMany(p => p.DistribusjonArbeid)
                    .HasForeignKey(d => d.FdvrundeId)
                    .HasConstraintName("FK_DistribusjonArbeid_FDVRunde");
            });

            modelBuilder.Entity<DistribusjonArbeidLogg>(entity =>
            {
                entity.Property(e => e.Brukernavn).HasMaxLength(50);

                entity.Property(e => e.Dato).HasMaxLength(8);

                entity.Property(e => e.DistribusjonArbeidId).HasColumnName("DistribusjonArbeid_Id");

                entity.Property(e => e.Kommentar).HasMaxLength(255);

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.Property(e => e.Navn).HasMaxLength(100);

                entity.HasOne(d => d.DistribusjonArbeid)
                    .WithMany(p => p.DistribusjonArbeidLogg)
                    .HasForeignKey(d => d.DistribusjonArbeidId)
                    .HasConstraintName("FK_DistribusjonArbeidLogg_DistribusjonArbeid");
            });

            modelBuilder.Entity<DistribusjonDatasett>(entity =>
            {
                entity.Property(e => e.DatasettId).HasColumnName("Datasett_Id");

                entity.Property(e => e.DistribusjonId).HasColumnName("Distribusjon_Id");

                entity.HasOne(d => d.Datasett)
                    .WithMany(p => p.DistribusjonDatasett)
                    .HasForeignKey(d => d.DatasettId)
                    .HasConstraintName("FK_DistribusjonDatasett_Datasett");

                entity.HasOne(d => d.Distribusjon)
                    .WithMany(p => p.DistribusjonDatasett)
                    .HasForeignKey(d => d.DistribusjonId)
                    .HasConstraintName("FK_DistribusjonDatasett_Distribusjon");
            });

            modelBuilder.Entity<Fdvdatamottak>(entity =>
            {
                entity.ToTable("FDVDatamottak");

                entity.Property(e => e.Brukernavn).HasMaxLength(50);

                entity.Property(e => e.Dato).HasMaxLength(8);

                entity.Property(e => e.FdvdatasettId).HasColumnName("FDVDatasett_Id");

                entity.Property(e => e.FdvpartId).HasColumnName("FDVPart_Id");

                entity.Property(e => e.FdvrundeId).HasColumnName("FDVRunde_Id");

                entity.Property(e => e.Kommentar).HasMaxLength(255);

                entity.HasOne(d => d.Fdvdatasett)
                    .WithMany(p => p.Fdvdatamottak)
                    .HasForeignKey(d => d.FdvdatasettId)
                    .HasConstraintName("FK_FDVDatamottak_FDVDatasett");

                entity.HasOne(d => d.Fdvpart)
                    .WithMany(p => p.Fdvdatamottak)
                    .HasForeignKey(d => d.FdvpartId)
                    .HasConstraintName("FK_FDVDatamottak_FDVPart");

                entity.HasOne(d => d.Fdvrunde)
                    .WithMany(p => p.Fdvdatamottak)
                    .HasForeignKey(d => d.FdvrundeId)
                    .HasConstraintName("FK_FDVDatamottak_FDVRunde");
            });

            modelBuilder.Entity<Fdvdatasett>(entity =>
            {
                entity.ToTable("FDVDatasett");

                entity.Property(e => e.DatasettId).HasColumnName("Datasett_Id");

                entity.Property(e => e.FdvdatasettForvaltningstypeId).HasColumnName("FDVDatasettForvaltningstype_Id");

                entity.Property(e => e.FdvprosjektId).HasColumnName("FDVProsjekt_Id");

                entity.HasOne(d => d.Datasett)
                    .WithMany(p => p.Fdvdatasett)
                    .HasForeignKey(d => d.DatasettId)
                    .HasConstraintName("FK_FDVDatasett_Datasett");

                entity.HasOne(d => d.FdvdatasettForvaltningstype)
                    .WithMany(p => p.Fdvdatasett)
                    .HasForeignKey(d => d.FdvdatasettForvaltningstypeId)
                    .HasConstraintName("FK_FDVDatasett_FDVDatasettForvaltningstype");

                entity.HasOne(d => d.Fdvprosjekt)
                    .WithMany(p => p.Fdvdatasett)
                    .HasForeignKey(d => d.FdvprosjektId)
                    .HasConstraintName("FK_FDVDatasett_FDVProsjekt");
            });

            modelBuilder.Entity<FdvdatasettArbeid>(entity =>
            {
                entity.ToTable("FDVDatasettArbeid");

                entity.Property(e => e.Brukernavn).HasMaxLength(50);

                entity.Property(e => e.DatasettId).HasColumnName("Datasett_Id");

                entity.Property(e => e.Dato).HasMaxLength(8);

                entity.Property(e => e.FdvrundeId).HasColumnName("FDVRunde_Id");

                entity.Property(e => e.Kommentar).HasMaxLength(255);

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.HasOne(d => d.Datasett)
                    .WithMany(p => p.FdvdatasettArbeid)
                    .HasForeignKey(d => d.DatasettId)
                    .HasConstraintName("FK_FDVDatasettArbeid_Datasett");

                entity.HasOne(d => d.Fdvrunde)
                    .WithMany(p => p.FdvdatasettArbeid)
                    .HasForeignKey(d => d.FdvrundeId)
                    .HasConstraintName("FK_FDVDatasettArbeid_FDVRunde");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.FdvdatasettArbeid)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_FDVDatasettArbeid_FDVStatus1");
            });

            modelBuilder.Entity<FdvdatasettArbeidLogg>(entity =>
            {
                entity.ToTable("FDVDatasettArbeidLogg");

                entity.Property(e => e.Brukernavn).HasMaxLength(50);

                entity.Property(e => e.Dato).HasMaxLength(8);

                entity.Property(e => e.FdvdatasettArbeidId).HasColumnName("FDVDatasettArbeid_Id");

                entity.Property(e => e.Kommentar).HasMaxLength(255);

                entity.Property(e => e.Link).HasMaxLength(100);

                entity.Property(e => e.Navn).HasMaxLength(100);

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.HasOne(d => d.FdvdatasettArbeid)
                    .WithMany(p => p.FdvdatasettArbeidLogg)
                    .HasForeignKey(d => d.FdvdatasettArbeidId)
                    .HasConstraintName("FK_FDVDatasettArbeidLogg_FDVDatasettArbeid");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.FdvdatasettArbeidLogg)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_FDVDatasettArbeidLogg_FDVStatus1");
            });

            modelBuilder.Entity<FdvdatasettForvaltningstype>(entity =>
            {
                entity.ToTable("FDVDatasettForvaltningstype");

                entity.Property(e => e.Beskrivelse).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Fdvnotat>(entity =>
            {
                entity.ToTable("FDVNotat");

                entity.Property(e => e.KommuneKommunenr)
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);

                entity.Property(e => e.Tekst).HasMaxLength(1000);

                entity.HasOne(d => d.KommuneKommunenrNavigation)
                    .WithMany(p => p.Fdvnotat)
                    .HasForeignKey(d => d.KommuneKommunenr)
                    .HasConstraintName("FK_FDVNotat_Kommune");
            });

            modelBuilder.Entity<Fdvpart>(entity =>
            {
                entity.ToTable("FDVPart");

                entity.Property(e => e.FdvprosjektId).HasColumnName("FDVProsjekt_Id");

                entity.Property(e => e.PartId).HasColumnName("Part_Id");

                entity.HasOne(d => d.Fdvprosjekt)
                    .WithMany(p => p.Fdvpart)
                    .HasForeignKey(d => d.FdvprosjektId)
                    .HasConstraintName("FK_FDVPart_FDVProsjekt");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.Fdvpart)
                    .HasForeignKey(d => d.PartId)
                    .HasConstraintName("FK_FDVPart_Part");
            });

            modelBuilder.Entity<Fdvprosjekt>(entity =>
            {
                entity.ToTable("FDVProsjekt");

                entity.Property(e => e.Ar).HasMaxLength(4);

                entity.Property(e => e.KommuneKommunenr)
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);

                entity.Property(e => e.Prosjektnavn).HasMaxLength(50);

                entity.HasOne(d => d.KommuneKommunenrNavigation)
                    .WithMany(p => p.Fdvprosjekt)
                    .HasForeignKey(d => d.KommuneKommunenr)
                    .HasConstraintName("FK_FDVProsjekt_Kommune");

                entity.HasOne(d => d.StatusArsmoteNavigation)
                    .WithMany(p => p.FdvprosjektStatusArsmoteNavigation)
                    .HasForeignKey(d => d.StatusArsmote)
                    .HasConstraintName("FK_FDVProsjekt_FDVStatusÅrsmøte");

                entity.HasOne(d => d.StatusAvtaleNavigation)
                    .WithMany(p => p.FdvprosjektStatusAvtaleNavigation)
                    .HasForeignKey(d => d.StatusAvtale)
                    .HasConstraintName("FK_FDVProsjekt_FDVStatusAvtale");

                entity.HasOne(d => d.StatusFaktureringNavigation)
                    .WithMany(p => p.FdvprosjektStatusFaktureringNavigation)
                    .HasForeignKey(d => d.StatusFakturering)
                    .HasConstraintName("FK_FDVProsjekt_FDVStatusFakturering");
            });

            modelBuilder.Entity<FdvprosjektLenke>(entity =>
            {
                entity.ToTable("FDVProsjektLenke");

                entity.Property(e => e.Beskrivelse).HasMaxLength(255);

                entity.Property(e => e.FdvprosjektId).HasColumnName("FDVProsjekt_Id");

                entity.Property(e => e.Lenke).HasMaxLength(255);

                entity.HasOne(d => d.Fdvprosjekt)
                    .WithMany(p => p.FdvprosjektLenke)
                    .HasForeignKey(d => d.FdvprosjektId)
                    .HasConstraintName("FK_FDVProsjektLenke_FDVProsjekt");
            });

            modelBuilder.Entity<FdvprosjektLogg>(entity =>
            {
                entity.ToTable("FDVProsjektLogg");

                entity.Property(e => e.Brukernavn).HasMaxLength(50);

                entity.Property(e => e.Dato).HasMaxLength(8);

                entity.Property(e => e.FdvprosjektId).HasColumnName("FDVProsjekt_Id");

                entity.Property(e => e.Logg).HasMaxLength(1000);

                entity.HasOne(d => d.Fdvprosjekt)
                    .WithMany(p => p.FdvprosjektLogg)
                    .HasForeignKey(d => d.FdvprosjektId)
                    .HasConstraintName("FK_FDVProsjektLogg_FDVProsjekt");
            });

            modelBuilder.Entity<Fdvrunde>(entity =>
            {
                entity.ToTable("FDVRunde");

                entity.Property(e => e.Ar5ferdig).HasColumnName("AR5Ferdig");

                entity.Property(e => e.Ar5merknad)
                    .HasColumnName("AR5Merknad")
                    .HasMaxLength(1000);

                entity.Property(e => e.AvsluttetDato).HasMaxLength(8);

                entity.Property(e => e.FdvprosjektId).HasColumnName("FDVProsjekt_Id");

                entity.Property(e => e.FkbDistribusjonId).HasColumnName("FKB_Distribusjon_Id");

                entity.Property(e => e.Fkbferdig).HasColumnName("FKBFerdig");

                entity.Property(e => e.Fkbmerknad)
                    .HasColumnName("FKBMerknad")
                    .HasMaxLength(1000);

                entity.Property(e => e.Kommentar).HasMaxLength(1000);

                entity.Property(e => e.LedningMerknad).HasMaxLength(1000);

                entity.Property(e => e.PaminningSendt).HasMaxLength(8);

                entity.Property(e => e.PlanMerknad).HasMaxLength(1000);

                entity.Property(e => e.PlanlagtDato).HasMaxLength(8);

                entity.Property(e => e.TemadataMerknad).HasMaxLength(1000);

                entity.Property(e => e.VegnettMerknad).HasMaxLength(1000);

                entity.HasOne(d => d.Fdvprosjekt)
                    .WithMany(p => p.Fdvrunde)
                    .HasForeignKey(d => d.FdvprosjektId)
                    .HasConstraintName("FK_FDVRunde_FDVProsjekt");

                entity.HasOne(d => d.FkbDistribusjon)
                    .WithMany(p => p.Fdvrunde)
                    .HasForeignKey(d => d.FkbDistribusjonId)
                    .HasConstraintName("FK_FDVRunde_DistribusjonFKB");
            });

            modelBuilder.Entity<FdvrundeDistribusjon>(entity =>
            {
                entity.ToTable("FDVRundeDistribusjon");

                entity.Property(e => e.DistribusjonId).HasColumnName("Distribusjon_Id");

                entity.Property(e => e.FdvrundeId).HasColumnName("FDVRunde_Id");

                entity.HasOne(d => d.Distribusjon)
                    .WithMany(p => p.FdvrundeDistribusjon)
                    .HasForeignKey(d => d.DistribusjonId)
                    .HasConstraintName("FK_FDVRundeDistribusjon_Distribusjon");

                entity.HasOne(d => d.Fdvrunde)
                    .WithMany(p => p.FdvrundeDistribusjon)
                    .HasForeignKey(d => d.FdvrundeId)
                    .HasConstraintName("FK_FDVRundeDistribusjon_FDVRunde");
            });

            modelBuilder.Entity<Fdvstatus1>(entity =>
            {
                entity.ToTable("FDVStatus1");

                entity.Property(e => e.Navn).HasMaxLength(100);
            });

            modelBuilder.Entity<Fylke>(entity =>
            {
                entity.HasKey(e => e.Fylkesnr);

                entity.Property(e => e.Fylkesnr)
                    .HasMaxLength(2)
                    .ValueGeneratedNever();

                entity.Property(e => e.BbNordOstE).HasColumnName("BB_NordOst_E");

                entity.Property(e => e.BbNordOstN).HasColumnName("BB_NordOst_N");

                entity.Property(e => e.BbSorVestE).HasColumnName("BB_SorVest_E");

                entity.Property(e => e.BbSorVestN).HasColumnName("BB_SorVest_N");

                entity.Property(e => e.Fylkesnavn).HasMaxLength(255);

                entity.Property(e => e.FylkesnavnDos)
                    .HasColumnName("Fylkesnavn_dos")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Kartkontor>(entity =>
            {
                entity.HasKey(e => e.Nr);

                entity.Property(e => e.Nr).ValueGeneratedNever();

                entity.Property(e => e.Navn)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Kommune>(entity =>
            {
                entity.HasKey(e => e.Kommunenr);

                entity.Property(e => e.Kommunenr)
                    .HasMaxLength(4)
                    .ValueGeneratedNever();

                entity.Property(e => e.BbNordOstE).HasColumnName("BB_NordOst_E");

                entity.Property(e => e.BbNordOstN).HasColumnName("BB_NordOst_N");

                entity.Property(e => e.BbSorVestE).HasColumnName("BB_SorVest_E");

                entity.Property(e => e.BbSorVestN).HasColumnName("BB_SorVest_N");

                entity.Property(e => e.Fkboppdatering)
                    .HasColumnName("FKBOppdatering")
                    .HasMaxLength(255);

                entity.Property(e => e.Forelopig).HasMaxLength(255);

                entity.Property(e => e.FylkeFylkesnr)
                    .HasColumnName("Fylke_Fylkesnr")
                    .HasMaxLength(2);

                entity.Property(e => e.KartkontorNr).HasColumnName("Kartkontor_Nr");

                entity.Property(e => e.Kommunenavn).HasMaxLength(255);

                entity.Property(e => e.KommunenavnDos)
                    .HasColumnName("Kommunenavn_dos")
                    .HasMaxLength(255);

                entity.Property(e => e.KoordsysKoordsys).HasColumnName("Koordsys_Koordsys");

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.SystemGis)
                    .HasColumnName("SystemGIS")
                    .HasMaxLength(50);

                entity.Property(e => e.Tjenesteserver).HasMaxLength(255);

                entity.Property(e => e.Vertdatum).HasMaxLength(20);

                entity.Property(e => e.WebKart).HasMaxLength(255);

                entity.HasOne(d => d.FylkeFylkesnrNavigation)
                    .WithMany(p => p.Kommune)
                    .HasForeignKey(d => d.FylkeFylkesnr)
                    .HasConstraintName("FK_Kommune_Fylke");

                entity.HasOne(d => d.KartkontorNrNavigation)
                    .WithMany(p => p.Kommune)
                    .HasForeignKey(d => d.KartkontorNr)
                    .HasConstraintName("FK_Kommune_Kartkontor");

                entity.HasOne(d => d.KoordsysKoordsysNavigation)
                    .WithMany(p => p.Kommune)
                    .HasForeignKey(d => d.KoordsysKoordsys)
                    .HasConstraintName("FK_Kommune_Koordsys");
            });

            modelBuilder.Entity<Koordsys>(entity =>
            {
                entity.HasKey(e => e.Koordsys1);

                entity.Property(e => e.Koordsys1)
                    .HasColumnName("Koordsys")
                    .ValueGeneratedNever();

                entity.Property(e => e.Epsg)
                    .HasColumnName("EPSG")
                    .HasMaxLength(255);

                entity.Property(e => e.EpsgWgs).HasColumnName("EPSG_WGS");

                entity.Property(e => e.Navn).HasMaxLength(255);

                entity.Property(e => e.UtmSone)
                    .HasColumnName("UTM_sone")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Kunde>(entity =>
            {
                entity.Property(e => e.Aktiv).HasDefaultValueSql("((1))");

                entity.Property(e => e.Epost).HasMaxLength(100);

                entity.Property(e => e.KundeGruppeId)
                    .HasColumnName("KundeGruppe_Id")
                    .HasMaxLength(10);

                entity.Property(e => e.Navn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Postgate).HasMaxLength(50);

                entity.Property(e => e.Postnummer).HasMaxLength(4);

                entity.Property(e => e.Poststed).HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(15);

                entity.HasOne(d => d.KundeGruppe)
                    .WithMany(p => p.Kunde)
                    .HasForeignKey(d => d.KundeGruppeId)
                    .HasConstraintName("FK_Kunde_KundeGruppe");
            });

            modelBuilder.Entity<KundeGruppe>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Navn).HasMaxLength(100);
            });

            modelBuilder.Entity<Kurs>(entity =>
            {
                entity.Property(e => e.KartkontorId).HasColumnName("Kartkontor_Id");

                entity.Property(e => e.KursTypeId).HasColumnName("KursType_Id");

                entity.Property(e => e.Merknad).HasMaxLength(500);

                entity.Property(e => e.PlanlagtManed).HasMaxLength(8);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TilgangBrukernavn)
                    .HasColumnName("Tilgang_Brukernavn")
                    .HasMaxLength(50);

                entity.HasOne(d => d.KursType)
                    .WithMany(p => p.Kurs)
                    .HasForeignKey(d => d.KursTypeId)
                    .HasConstraintName("FK_Kurs_KursType");

                entity.HasOne(d => d.TilgangBrukernavnNavigation)
                    .WithMany(p => p.Kurs)
                    .HasForeignKey(d => d.TilgangBrukernavn)
                    .HasConstraintName("FK_Kurs_Tilgang");
            });

            modelBuilder.Entity<KursDato>(entity =>
            {
                entity.Property(e => e.Dato).HasMaxLength(8);

                entity.Property(e => e.KursId).HasColumnName("Kurs_Id");

                entity.HasOne(d => d.Kurs)
                    .WithMany(p => p.KursDato)
                    .HasForeignKey(d => d.KursId)
                    .HasConstraintName("FK_KursDato_Kurs");
            });

            modelBuilder.Entity<KursPart>(entity =>
            {
                entity.Property(e => e.KursId).HasColumnName("Kurs_Id");

                entity.Property(e => e.PartId).HasColumnName("Part_Id");

                entity.HasOne(d => d.Kurs)
                    .WithMany(p => p.KursPart)
                    .HasForeignKey(d => d.KursId)
                    .HasConstraintName("FK_KursPart_Kurs");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.KursPart)
                    .HasForeignKey(d => d.PartId)
                    .HasConstraintName("FK_KursPart_Part");
            });

            modelBuilder.Entity<KursPartDeltatt>(entity =>
            {
                entity.Property(e => e.KursDatoId).HasColumnName("KursDato_Id");

                entity.Property(e => e.KursPartId).HasColumnName("KursPart_Id");

                entity.HasOne(d => d.KursDato)
                    .WithMany(p => p.KursPartDeltatt)
                    .HasForeignKey(d => d.KursDatoId)
                    .HasConstraintName("FK_KursPartDeltatt_KursDato");

                entity.HasOne(d => d.KursPart)
                    .WithMany(p => p.KursPartDeltatt)
                    .HasForeignKey(d => d.KursPartId)
                    .HasConstraintName("FK_KursPartDeltatt_KursPart");
            });

            modelBuilder.Entity<KursType>(entity =>
            {
                entity.Property(e => e.Beskrivelse).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(100);
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.Property(e => e.KartkontorNr).HasColumnName("Kartkontor_Nr");

                entity.Property(e => e.Kode).HasMaxLength(20);

                entity.Property(e => e.KundeId).HasColumnName("Kunde_Id");

                entity.Property(e => e.Navn).HasMaxLength(100);

                entity.Property(e => e.PartskontoNavn).HasMaxLength(50);

                entity.Property(e => e.PartskontoNr).HasMaxLength(8);

                entity.Property(e => e.Referanse).HasMaxLength(50);

                entity.HasOne(d => d.KartkontorNrNavigation)
                    .WithMany(p => p.Part)
                    .HasForeignKey(d => d.KartkontorNr)
                    .HasConstraintName("FK_Part_Kartkontor");

                entity.HasOne(d => d.Kunde)
                    .WithMany(p => p.Part)
                    .HasForeignKey(d => d.KundeId)
                    .HasConstraintName("FK_Part_Kunde");
            });

            modelBuilder.Entity<PartsKontakt>(entity =>
            {
                entity.Property(e => e.PartId).HasColumnName("Part_Id");

                entity.Property(e => e.PersonId).HasColumnName("Person_Id");

                entity.Property(e => e.RolleNavn)
                    .HasColumnName("Rolle_Navn")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.PartsKontakt)
                    .HasForeignKey(d => d.PartId)
                    .HasConstraintName("FK_PartsKontakt_Part");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PartsKontakt)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PartsKontakt_Person");

                entity.HasOne(d => d.RolleNavnNavigation)
                    .WithMany(p => p.PartsKontakt)
                    .HasForeignKey(d => d.RolleNavn)
                    .HasConstraintName("FK_PartsKontakt_Rolle");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Epost).HasMaxLength(50);

                entity.Property(e => e.KundeId).HasColumnName("Kunde_Id");

                entity.Property(e => e.Mobil).HasMaxLength(15);

                entity.Property(e => e.Navn).HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(15);

                entity.HasOne(d => d.Kunde)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.KundeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_Kunde");
            });

            modelBuilder.Entity<PlanInfo>(entity =>
            {
                entity.Property(e => e.GeosynkInnfort).HasMaxLength(8);

                entity.Property(e => e.KommuneKommunenr)
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);

                entity.Property(e => e.Merknad).HasMaxLength(500);

                entity.Property(e => e.Planregisterlink).HasMaxLength(1000);

                entity.HasOne(d => d.KommuneKommunenrNavigation)
                    .WithMany(p => p.PlanInfo)
                    .HasForeignKey(d => d.KommuneKommunenr)
                    .HasConstraintName("FK_Plan_Kommune");
            });

            modelBuilder.Entity<PuljeManed>(entity =>
            {
                entity.HasKey(e => e.Pulje);

                entity.Property(e => e.Maned).HasMaxLength(50);

                entity.Property(e => e.Merknad).HasMaxLength(100);
            });

            modelBuilder.Entity<Rolle>(entity =>
            {
                entity.HasKey(e => e.Navn);

                entity.Property(e => e.Navn)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Beskrivelse).HasMaxLength(250);

                entity.Property(e => e.KartkontorNr).HasColumnName("Kartkontor_Nr");

                entity.HasOne(d => d.KartkontorNrNavigation)
                    .WithMany(p => p.Rolle)
                    .HasForeignKey(d => d.KartkontorNr)
                    .HasConstraintName("FK_Rolle_Kartkontor");
            });

            modelBuilder.Entity<SentralFkb>(entity =>
            {
                entity.ToTable("SentralFKB");

                entity.Property(e => e.BekreftetKlar).HasMaxLength(8);

                entity.Property(e => e.BekreftetPulje).HasMaxLength(100);

                entity.Property(e => e.DirekteoppdateringInfort).HasMaxLength(8);

                entity.Property(e => e.KommuneKommunenr)
                    .HasColumnName("Kommune_Kommunenr")
                    .HasMaxLength(4);

                entity.Property(e => e.KonverteringFerdig).HasMaxLength(8);

                entity.Property(e => e.Merknad).HasMaxLength(500);

                entity.Property(e => e.OnsketInnforing).HasMaxLength(100);

                entity.Property(e => e.PlanlagtInnforing).HasMaxLength(8);

                entity.Property(e => e.PriListe).HasMaxLength(100);

                entity.HasOne(d => d.KommuneKommunenrNavigation)
                    .WithMany(p => p.SentralFkb)
                    .HasForeignKey(d => d.KommuneKommunenr)
                    .HasConstraintName("FK_SentralFKB_Kommune");

                entity.HasOne(d => d.PuljeNavigation)
                    .WithMany(p => p.SentralFkb)
                    .HasForeignKey(d => d.Pulje)
                    .HasConstraintName("FK_SentralFKB_PuljeManed");
            });

            modelBuilder.Entity<Tilgang>(entity =>
            {
                entity.HasKey(e => e.Brukernavn);

                entity.Property(e => e.Brukernavn)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Enhet).HasMaxLength(50);

                entity.Property(e => e.Epost).HasMaxLength(50);

                entity.Property(e => e.Etternavn).HasMaxLength(50);

                entity.Property(e => e.Fornavn).HasMaxLength(50);
            });
        }
    }
}
