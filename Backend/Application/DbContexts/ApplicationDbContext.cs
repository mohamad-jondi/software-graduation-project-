﻿using Data.Models;
using Data.Models.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Allergy> Allergies { get; set; }
        public virtual DbSet<Avaliability> Avaliabilities { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<EmergencyContactInfo> EmergencyContactInfos { get; set; }
        public virtual DbSet<LifestyleFactors> LifestyleFactors { get; set; }
        public virtual DbSet<Mother> Mothers { get; set; }
        public virtual DbSet<Child> Children { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vaccination> Vaccinations { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<DoctorRating> DoctorRatings { get; set; }
        public virtual DbSet<Symptoms> Symptoms { get; set; }
        public virtual DbSet<MedicalSecondOpinion> MedicalSecondOpinions { get; set; }
        public virtual DbSet<Nurse> Nurses { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<JWTTokensRefresh> JWTTokensRefresh { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.EmergancyContact)
                .WithOne(e => e.Patient)
                .HasForeignKey<EmergencyContactInfo>(e => e.PatientId);

            modelBuilder.Entity<EmergencyContactInfo>()
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.EmergancyContactID);

            modelBuilder.Entity<Avaliability>()
                .HasKey(e => e.AvalibailityID);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.FirstParty)
                .WithMany()
                .HasForeignKey(c => c.FirstPartyID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.SecondParty)
                .WithMany()
                .HasForeignKey(c => c.SecondPartyID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Symptoms>()
                .HasKey(c => c.SymptomID);

            modelBuilder.Entity<MedicalSecondOpinion>()
                .HasKey(c => c.SecondOpinionId);

           

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Child)
                .WithMany(ch => ch.Cases)
                .HasForeignKey(c => c.ChildID);

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.Cases)
                .HasForeignKey(c => c.DoctorId);

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointment)
            .HasForeignKey(a => a.DoctorId);
            modelBuilder.Entity<Picture>().HasKey(p => p.Id);


        }
    }
}
