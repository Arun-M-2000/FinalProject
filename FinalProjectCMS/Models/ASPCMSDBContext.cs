using System;
using FinalProjectCMS.ViewModel.Pharmacist;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalProjectCMS.Models
{
    public partial class ASPCMSDBContext : DbContext
    {
        public ASPCMSDBContext()
        {
        }

        public ASPCMSDBContext(DbContextOptions<ASPCMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<ConsultBill> ConsultBill { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<TblBillGeneration> TblBillGeneration { get; set; }
        public virtual DbSet<TblDepartments> TblDepartments { get; set; }
        public virtual DbSet<TblDiagnosis> TblDiagnosis { get; set; }
        public virtual DbSet<TblDoctors> TblDoctors { get; set; }
        public virtual DbSet<TblLabPrescriptions> TblLabPrescriptions { get; set; }
        public virtual DbSet<TblLabTests> TblLabTests { get; set; }
        public virtual DbSet<TblLoginDetails> TblLoginDetails { get; set; }
        public virtual DbSet<TblLoginUsers> TblLoginUsers { get; set; }
        public virtual DbSet<TblMedicinePrescriptions> TblMedicinePrescriptions { get; set; }
        public virtual DbSet<TblMedicines> TblMedicines { get; set; }
        public virtual DbSet<TblPatientHistory> TblPatientHistory { get; set; }
        public virtual DbSet<TblQualifications> TblQualifications { get; set; }
        public virtual DbSet<TblReportGeneration> TblReportGeneration { get; set; }
        public virtual DbSet<TblRoles> TblRoles { get; set; }
        public virtual DbSet<TblSpecializations> TblSpecializations { get; set; }
        public virtual DbSet<TblStaffs> TblStaffs { get; set; }
        

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source= DESKTOP-OF7QM0G\\SQLEXPRESS; Initial Catalog=ASPCMSDB; Integrated security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_Id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnName("appointment_Date")
                    .HasColumnType("date");

                entity.Property(e => e.CheckUpStatus)
                    .IsRequired()
                    .HasColumnName("CheckUp_Status")
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CONFIRMED')");

                entity.Property(e => e.DocId).HasColumnName("docId");

                entity.Property(e => e.TokenNo).HasColumnName("token_No");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor1");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient1");
            });

            modelBuilder.Entity<ConsultBill>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PK__Consult___D733892BA9712AA2");

                entity.ToTable("Consult_Bill");

                entity.Property(e => e.BillId).HasColumnName("bill_Id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_Id");

                entity.Property(e => e.RegisterFees)
                    .HasColumnName("register_Fees")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalAmt)
                    .HasColumnName("total_Amt")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.ConsultBill)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment1");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.BloodGroup)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PatientAddr)
                    .IsRequired()
                    .HasColumnName("Patient_Addr")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PatientDob)
                    .HasColumnName("Patient_DOB")
                    .HasColumnType("date");

                entity.Property(e => e.PatientEmail)
                    .HasColumnName("patient_Email")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PatientName)
                    .IsRequired()
                    .HasColumnName("Patient_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PatientStatus)
                    .IsRequired()
                    .HasColumnName("patient_status")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Active')");

                entity.Property(e => e.RegisterNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBillGeneration>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PK__tbl_Bill__6D903F03F1A4997C");

                entity.ToTable("tbl_BillGeneration");

                entity.Property(e => e.BillId).HasColumnName("billId");

                entity.Property(e => e.BillDate)
                    .HasColumnName("billDate")
                    .HasColumnType("date");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("totalAmount")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.TblBillGeneration)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK__tbl_BillG__repor__28B808A7");
            });

            modelBuilder.Entity<TblDepartments>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("PK__tbl_Depa__F9B8346D4EF3EECF");

                entity.ToTable("tbl_Departments");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("departmentName")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDiagnosis>(entity =>
            {
                entity.HasKey(e => e.DiagnosisId)
                    .HasName("PK__tbl_Diag__F1D4AC1866B6C7AD");

                entity.ToTable("tbl_Diagnosis");

                entity.Property(e => e.DiagnosisId).HasColumnName("diagnosisId");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");

                entity.Property(e => e.Diagnosis)
                    .HasColumnName("diagnosis")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Symptoms)
                    .IsRequired()
                    .HasColumnName("symptoms")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TblDiagnosis)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__tbl_Diagn__appoi__14B10FFA");
            });

            modelBuilder.Entity<TblDoctors>(entity =>
            {
                entity.HasKey(e => e.DocId)
                    .HasName("PK__tbl_Doct__0639C4229E68A56D");

                entity.ToTable("tbl_Doctors");

                entity.Property(e => e.DocId).HasColumnName("docId");

                entity.Property(e => e.ConsultationFee)
                    .HasColumnName("consultationFee")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SpecializationId).HasColumnName("specializationId");

                entity.Property(e => e.StaffId).HasColumnName("staffId");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.TblDoctors)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK__tbl_Docto__speci__3DE82FB7");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.TblDoctors)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__tbl_Docto__staff__3EDC53F0");
            });

            modelBuilder.Entity<TblLabPrescriptions>(entity =>
            {
                entity.HasKey(e => e.LabPrescriptionId)
                    .HasName("PK__tbl_LabP__22C099BA31BB8C58");

                entity.ToTable("tbl_LabPrescriptions");

                entity.Property(e => e.LabPrescriptionId).HasColumnName("labPrescriptionId");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");

                entity.Property(e => e.LabNote)
                    .HasColumnName("labNote")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LabTestId).HasColumnName("labTestId");

                entity.Property(e => e.LabTestStatus)
                    .IsRequired()
                    .HasColumnName("labTestStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TblLabPrescriptions)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__tbl_LabPr__appoi__1C5231C2");

                entity.HasOne(d => d.LabTest)
                    .WithMany(p => p.TblLabPrescriptions)
                    .HasForeignKey(d => d.LabTestId)
                    .HasConstraintName("FK__tbl_LabPr__labTe__1B5E0D89");
            });

            modelBuilder.Entity<TblLabTests>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("PK__tbl_LabT__A29BFB884EA66C64");

                entity.ToTable("tbl_LabTests");

                entity.Property(e => e.TestId).HasColumnName("testId");

                entity.Property(e => e.HighRange)
                    .IsRequired()
                    .HasColumnName("highRange")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LowRange)
                    .IsRequired()
                    .HasColumnName("lowRange")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TestName)
                    .IsRequired()
                    .HasColumnName("testName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLoginDetails>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbl_LoginDetails");

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLoginUsers>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("PK__tbl_Logi__1F5EF4CF7BFF76EE");

                entity.ToTable("tbl_LoginUsers");

                entity.Property(e => e.LoginId).HasColumnName("loginId");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblLoginUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__tbl_Login__roleI__3FD07829");
            });

            modelBuilder.Entity<TblMedicinePrescriptions>(entity =>
            {
                entity.HasKey(e => e.MedPrescriptionId)
                    .HasName("PK__tbl_Medi__E780C026A6C7C25D");

                entity.ToTable("tbl_MedicinePrescriptions");

                entity.Property(e => e.MedPrescriptionId).HasColumnName("medPrescriptionId");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");

                entity.Property(e => e.Dosage)
                    .HasColumnName("dosage")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DosageDays).HasColumnName("dosageDays");

                entity.Property(e => e.MedicineQuantity).HasColumnName("medicineQuantity");

                entity.Property(e => e.PrescribedMedicineId).HasColumnName("prescribedMedicineId");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TblMedicinePrescriptions)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__tbl_Medic__appoi__1881A0DE");

                entity.HasOne(d => d.PrescribedMedicine)
                    .WithMany(p => p.TblMedicinePrescriptions)
                    .HasForeignKey(d => d.PrescribedMedicineId)
                    .HasConstraintName("FK__tbl_Medic__presc__178D7CA5");
            });

            modelBuilder.Entity<TblMedicines>(entity =>
            {
                entity.HasKey(e => e.MedicineId)
                    .HasName("PK__tbl_Medi__BA9E65EECA6DF29B");

                entity.ToTable("tbl_Medicines");

                entity.Property(e => e.MedicineId).HasColumnName("medicineId");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("companyName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenericName)
                    .IsRequired()
                    .HasColumnName("genericName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MedicineCode)
                    .IsRequired()
                    .HasColumnName("medicineCode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MedicineName)
                    .IsRequired()
                    .HasColumnName("medicineName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StockQuantity)
                    .IsRequired()
                    .HasColumnName("stockQuantity")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("unitPrice")
                    .HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<TblPatientHistory>(entity =>
            {
                entity.HasKey(e => e.PatientHistoryId)
                    .HasName("PK__tbl_Pati__45445C68BE4DD024");

                entity.ToTable("tbl_PatientHistory");

                entity.Property(e => e.PatientHistoryId).HasColumnName("patientHistoryId");

                entity.Property(e => e.DiagnosisId).HasColumnName("diagnosisId");

                entity.Property(e => e.LabPrescriptionId).HasColumnName("labPrescriptionId");

                entity.Property(e => e.MedPrescriptionId).HasColumnName("medPrescriptionId");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.TblPatientHistory)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK__tbl_Patie__diagn__2C88998B");

                entity.HasOne(d => d.LabPrescription)
                    .WithMany(p => p.TblPatientHistory)
                    .HasForeignKey(d => d.LabPrescriptionId)
                    .HasConstraintName("FK__tbl_Patie__labPr__2E70E1FD");

                entity.HasOne(d => d.MedPrescription)
                    .WithMany(p => p.TblPatientHistory)
                    .HasForeignKey(d => d.MedPrescriptionId)
                    .HasConstraintName("FK__tbl_Patie__medPr__2D7CBDC4");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.TblPatientHistory)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK__tbl_Patie__repor__2B947552");
            });

            modelBuilder.Entity<TblQualifications>(entity =>
            {
                entity.HasKey(e => e.QualificationId)
                    .HasName("PK__tbl_Qual__8EA9F5837853C91A");

                entity.ToTable("tbl_Qualifications");

                entity.Property(e => e.QualificationId).HasColumnName("qualificationId");

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasColumnName("qualification")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblReportGeneration>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__tbl_Repo__1C9B4E2D1133141B");

                entity.ToTable("tbl_ReportGeneration");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_Id");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReportDate)
                    .HasColumnName("reportDate")
                    .HasColumnType("date");

                entity.Property(e => e.StaffId).HasColumnName("staffId");

                entity.Property(e => e.TestId).HasColumnName("testId");

                entity.Property(e => e.TestResult)
                    .IsRequired()
                    .HasColumnName("testResult")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TblReportGeneration)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__tbl_Repor__appoi__23F3538A");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.TblReportGeneration)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__tbl_Repor__staff__25DB9BFC");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TblReportGeneration)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK__tbl_Repor__testI__24E777C3");
            });

            modelBuilder.Entity<TblRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__tbl_Role__CD98462AC3723C09");

                entity.ToTable("tbl_Roles");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasColumnName("roleName")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSpecializations>(entity =>
            {
                entity.HasKey(e => e.SpecializationId)
                    .HasName("PK__tbl_Spec__7E8C9BE7ACCAC0E3");

                entity.ToTable("tbl_Specializations");

                entity.Property(e => e.SpecializationId).HasColumnName("specializationId");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

                entity.Property(e => e.Specialization)
                    .HasColumnName("specialization")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblSpecializations)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__tbl_Speci__depar__40C49C62");
            });

            modelBuilder.Entity<TblStaffs>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__tbl_Staf__6465E07EB738765B");

                entity.ToTable("tbl_Staffs");

                entity.Property(e => e.StaffId).HasColumnName("staffId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroup)
                    .IsRequired()
                    .HasColumnName("bloodGroup")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasColumnName("fullName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.JoiningDate)
                    .HasColumnName("joiningDate")
                    .HasColumnType("date");

                entity.Property(e => e.LoginId).HasColumnName("loginId");

                entity.Property(e => e.MobileNo).HasColumnName("mobileNo");

                entity.Property(e => e.QualificationId).HasColumnName("qualificationId");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SpecializationId).HasColumnName("specializationId");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblStaffs)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__tbl_Staff__depar__41B8C09B");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.TblStaffs)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__tbl_Staff__login__42ACE4D4");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.TblStaffs)
                    .HasForeignKey(d => d.QualificationId)
                    .HasConstraintName("FK__tbl_Staff__quali__43A1090D");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblStaffs)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__tbl_Staff__roleI__44952D46");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.TblStaffs)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK__tbl_Staff__speci__4589517F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
