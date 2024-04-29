﻿// <auto-generated />
using System;
using Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240429124138_AddedJWTrefreshToken")]
    partial class AddedJWTrefreshToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "6.0.0-preview.7.21378.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AddressID");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Data.Models.Allergy", b =>
                {
                    b.Property<int>("AllergyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Allergen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<string>("ReactionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AllergyID");

                    b.HasIndex("PatientID");

                    b.ToTable("Allergies");
                });

            modelBuilder.Entity("Data.Models.Avaliability", b =>
                {
                    b.Property<int>("AvalibailityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AvaliabilityDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AvaliabilityTimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AvaliabilityTimeStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.HasKey("AvalibailityID");

                    b.HasIndex("DoctorID");

                    b.ToTable("Avaliabilities");
                });

            modelBuilder.Entity("Data.Models.Callender", b =>
                {
                    b.Property<int>("CallenderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CallenderID");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PersonID");

                    b.ToTable("Callender");
                });

            modelBuilder.Entity("Data.Models.Chat", b =>
                {
                    b.Property<int>("ChatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.HasKey("ChatID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("PatientID");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Data.Models.ChatMessage", b =>
                {
                    b.Property<int>("ChatMessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChatID")
                        .HasColumnType("int");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sender")
                        .HasColumnType("int");

                    b.Property<DateTime>("SentDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ChatMessageID");

                    b.HasIndex("ChatID");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Data.Models.Credential", b =>
                {
                    b.Property<int>("CredentialID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CredentialType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CredentialValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.HasKey("CredentialID");

                    b.HasIndex("DoctorID");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("Data.Models.Data.Models.EmergencyContactInfo", b =>
                {
                    b.Property<int>("EmergencyContactInfoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmergancyContactID")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId1")
                        .HasColumnType("int");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmergencyContactInfoID");

                    b.HasIndex("EmergancyContactID");

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.HasIndex("PersonId1");

                    b.ToTable("EmergencyContactInfos");
                });

            modelBuilder.Entity("Data.Models.JWTTokensRefresh", b =>
                {
                    b.Property<int>("JWTTokensRefreshID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("RefreshToken")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("JWTTokensRefreshID");

                    b.HasIndex("UserID");

                    b.ToTable("JWTTokensRefresh");
                });

            modelBuilder.Entity("Data.Models.LifestyleFactors", b =>
                {
                    b.Property<int>("LifestyleFactorsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DietaryPreferences")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseHabits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAlcoholConsumer")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSmoker")
                        .HasColumnType("bit");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("LifestyleFactorsId");

                    b.HasIndex("PatientId");

                    b.ToTable("LifestyleFactors");
                });

            modelBuilder.Entity("Data.Models.Operation", b =>
                {
                    b.Property<int>("OperationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<int>("SurgeonID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SurgeryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OperationID");

                    b.HasIndex("PatientID");

                    b.HasIndex("SurgeonID");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Data.Models.Vaccination", b =>
                {
                    b.Property<int>("VaccinationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AdministeredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<int>("ShotsLeft")
                        .HasColumnType("int");

                    b.Property<int>("VaccineStatus")
                        .HasColumnType("int");

                    b.HasKey("VaccinationID");

                    b.HasIndex("PatientID");

                    b.ToTable("Vaccinations");
                });

            modelBuilder.Entity("DoctorPatient", b =>
                {
                    b.Property<int>("DoctorsId")
                        .HasColumnType("int");

                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.HasKey("DoctorsId", "PatientsId");

                    b.HasIndex("PatientsId");

                    b.ToTable("DoctorPatient");
                });

            modelBuilder.Entity("Data.Models.Person", b =>
                {
                    b.HasBaseType("Data.Models.User");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<double?>("LatestRecordedHeight")
                        .HasColumnType("float");

                    b.Property<double?>("LatestRecordedWeight")
                        .HasColumnType("float");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Person");
                });

            modelBuilder.Entity("Data.Models.Doctor", b =>
                {
                    b.HasBaseType("Data.Models.Person");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("Data.Models.Patient", b =>
                {
                    b.HasBaseType("Data.Models.Person");

                    b.Property<int>("EmergancyContactID")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("Data.Models.Address", b =>
                {
                    b.HasOne("Data.Models.User", "user")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Data.Models.Allergy", b =>
                {
                    b.HasOne("Data.Models.Patient", "Patient")
                        .WithMany("Allergies")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Data.Models.Avaliability", b =>
                {
                    b.HasOne("Data.Models.Doctor", "Doctor")
                        .WithMany("Avalible")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Data.Models.Callender", b =>
                {
                    b.HasOne("Data.Models.Doctor", null)
                        .WithMany("Callender")
                        .HasForeignKey("DoctorId");

                    b.HasOne("Data.Models.Person", "Patient")
                        .WithMany("CallenderAppointments")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Data.Models.Chat", b =>
                {
                    b.HasOne("Data.Models.Doctor", "Doctor")
                        .WithMany("Chats")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Data.Models.ChatMessage", b =>
                {
                    b.HasOne("Data.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("Data.Models.Credential", b =>
                {
                    b.HasOne("Data.Models.Doctor", "Doctor")
                        .WithMany("credential")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Data.Models.Data.Models.EmergencyContactInfo", b =>
                {
                    b.HasOne("Data.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("EmergancyContactID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Patient", "Patient")
                        .WithOne("EmergancyContact")
                        .HasForeignKey("Data.Models.Data.Models.EmergencyContactInfo", "PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Person", null)
                        .WithMany("EmergencyContactInfo")
                        .HasForeignKey("PersonId1");

                    b.Navigation("Patient");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Data.Models.JWTTokensRefresh", b =>
                {
                    b.HasOne("Data.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Data.Models.LifestyleFactors", b =>
                {
                    b.HasOne("Data.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Data.Models.Operation", b =>
                {
                    b.HasOne("Data.Models.Patient", "Patient")
                        .WithMany("operations")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Doctor", "Surgeon")
                        .WithMany()
                        .HasForeignKey("SurgeonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Surgeon");
                });

            modelBuilder.Entity("Data.Models.Vaccination", b =>
                {
                    b.HasOne("Data.Models.Patient", "Patient")
                        .WithMany("Vaccinations")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DoctorPatient", b =>
                {
                    b.HasOne("Data.Models.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Data.Models.Person", b =>
                {
                    b.Navigation("CallenderAppointments");

                    b.Navigation("EmergencyContactInfo");
                });

            modelBuilder.Entity("Data.Models.Doctor", b =>
                {
                    b.Navigation("Avalible");

                    b.Navigation("Callender");

                    b.Navigation("Chats");

                    b.Navigation("credential");
                });

            modelBuilder.Entity("Data.Models.Patient", b =>
                {
                    b.Navigation("Allergies");

                    b.Navigation("EmergancyContact");

                    b.Navigation("Vaccinations");

                    b.Navigation("operations");
                });
#pragma warning restore 612, 618
        }
    }
}
