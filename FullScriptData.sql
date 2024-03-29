USE [master]
GO
/****** Object:  Database [ASPCMSDB]    Script Date: 13-01-2024 17:05:05 ******/
CREATE DATABASE [ASPCMSDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ASPCMSDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ASPCMSDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ASPCMSDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ASPCMSDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ASPCMSDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ASPCMSDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO	
ALTER DATABASE [ASPCMSDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ASPCMSDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ASPCMSDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ASPCMSDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ASPCMSDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ASPCMSDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ASPCMSDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ASPCMSDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ASPCMSDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ASPCMSDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ASPCMSDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ASPCMSDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ASPCMSDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ASPCMSDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ASPCMSDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ASPCMSDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ASPCMSDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ASPCMSDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ASPCMSDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ASPCMSDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ASPCMSDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ASPCMSDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ASPCMSDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ASPCMSDB] SET  MULTI_USER 
GO
ALTER DATABASE [ASPCMSDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ASPCMSDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ASPCMSDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ASPCMSDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ASPCMSDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ASPCMSDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ASPCMSDB] SET QUERY_STORE = OFF
GO
USE [ASPCMSDB]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[appointment_Id] [int] IDENTITY(2001,1) NOT NULL,
	[token_No] [int] NOT NULL,
	[appointment_Date] [date] NOT NULL,
	[PatientId] [int] NOT NULL,
	[docId] [int] NOT NULL,
	[CheckUp_Status] [varchar](25) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[appointment_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consult_Bill]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consult_Bill](
	[bill_Id] [int] IDENTITY(3001,1) NOT NULL,
	[appointment_Id] [int] NOT NULL,
	[register_Fees] [decimal](18, 0) NULL,
	[total_Amt] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[bill_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[PatientId] [int] IDENTITY(1001,1) NOT NULL,
	[RegisterNo] [varchar](15) NOT NULL,
	[Patient_Name] [varchar](20) NOT NULL,
	[Patient_DOB] [date] NOT NULL,
	[Patient_Addr] [varchar](25) NOT NULL,
	[Gender] [varchar](1) NOT NULL,
	[BloodGroup] [varchar](3) NOT NULL,
	[PhoneNumber] [bigint] NOT NULL,
	[patient_Email] [varchar](25) NULL,
	[patient_status] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BillGeneration]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BillGeneration](
	[billId] [int] IDENTITY(12001,1) NOT NULL,
	[billDate] [date] NULL,
	[totalAmount] [decimal](18, 0) NULL,
	[reportId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[billId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Departments]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Departments](
	[departmentId] [int] IDENTITY(301,1) NOT NULL,
	[departmentName] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[departmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Diagnosis]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Diagnosis](
	[diagnosisId] [int] IDENTITY(3001,1) NOT NULL,
	[symptoms] [varchar](100) NOT NULL,
	[diagnosis] [varchar](50) NULL,
	[note] [varchar](100) NULL,
	[appointmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[diagnosisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Doctors]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Doctors](
	[docId] [int] IDENTITY(801,1) NOT NULL,
	[staffId] [int] NULL,
	[specializationId] [int] NULL,
	[consultationFee] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[docId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_LabPrescriptions]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LabPrescriptions](
	[labPrescriptionId] [int] IDENTITY(30001,1) NOT NULL,
	[labTestId] [int] NULL,
	[labNote] [varchar](100) NULL,
	[labTestStatus] [varchar](50) NOT NULL,
	[appointmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[labPrescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_LabTests]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LabTests](
	[testId] [int] IDENTITY(201,1) NOT NULL,
	[testName] [varchar](50) NOT NULL,
	[lowRange] [varchar](20) NOT NULL,
	[highRange] [varchar](20) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[testId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_LoginDetails]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LoginDetails](
	[StaffId] [int] NULL,
	[StaffName] [varchar](50) NULL,
	[RoleName] [varchar](50) NULL,
	[LoginTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_LoginUsers]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LoginUsers](
	[loginId] [int] IDENTITY(101,1) NOT NULL,
	[userName] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[roleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[loginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_MedicinePrescriptions]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_MedicinePrescriptions](
	[medPrescriptionId] [int] IDENTITY(20001,1) NOT NULL,
	[prescribedMedicineId] [bigint] NULL,
	[dosage] [varchar](10) NULL,
	[dosageDays] [int] NULL,
	[medicineQuantity] [int] NULL,
	[appointmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[medPrescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Medicines]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Medicines](
	[medicineId] [bigint] IDENTITY(501,1) NOT NULL,
	[medicineCode] [varchar](10) NOT NULL,
	[medicineName] [varchar](50) NOT NULL,
	[genericName] [varchar](30) NOT NULL,
	[companyName] [varchar](50) NOT NULL,
	[stockQuantity] [varchar](10) NOT NULL,
	[unitPrice] [decimal](8, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[medicineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PatientHistory]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PatientHistory](
	[patientHistoryId] [int] IDENTITY(70001,1) NOT NULL,
	[reportId] [int] NULL,
	[diagnosisId] [int] NULL,
	[medPrescriptionId] [int] NULL,
	[labPrescriptionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[patientHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Qualifications]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Qualifications](
	[qualificationId] [int] IDENTITY(601,1) NOT NULL,
	[qualification] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[qualificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_ReportGeneration]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ReportGeneration](
	[reportId] [int] IDENTITY(11001,1) NOT NULL,
	[reportDate] [date] NULL,
	[testResult] [varchar](20) NOT NULL,
	[remarks] [varchar](100) NULL,
	[appointment_Id] [int] NULL,
	[testId] [int] NULL,
	[staffId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[reportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Roles]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Roles](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Specializations]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Specializations](
	[specializationId] [int] IDENTITY(401,1) NOT NULL,
	[specialization] [varchar](40) NULL,
	[departmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[specializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Staffs]    Script Date: 13-01-2024 17:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Staffs](
	[staffId] [int] IDENTITY(701,1) NOT NULL,
	[fullName] [varchar](20) NULL,
	[dob] [date] NULL,
	[gender] [varchar](10) NOT NULL,
	[address] [varchar](75) NOT NULL,
	[bloodGroup] [varchar](10) NOT NULL,
	[joiningDate] [date] NOT NULL,
	[salary] [decimal](18, 0) NOT NULL,
	[mobileNo] [bigint] NOT NULL,
	[loginId] [int] NULL,
	[qualificationId] [int] NULL,
	[departmentId] [int] NULL,
	[Email] [varchar](50) NULL,
	[specializationId] [int] NULL,
	[roleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[staffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([appointment_Id], [token_No], [appointment_Date], [PatientId], [docId], [CheckUp_Status]) VALUES (2002, 10, CAST(N'2024-01-25' AS Date), 1002, 801, N'CONFIRMED')
INSERT [dbo].[Appointment] ([appointment_Id], [token_No], [appointment_Date], [PatientId], [docId], [CheckUp_Status]) VALUES (2004, 11, CAST(N'2024-11-25' AS Date), 1002, 801, N'CONFIRMED')
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO
SET IDENTITY_INSERT [dbo].[Consult_Bill] ON 

INSERT [dbo].[Consult_Bill] ([bill_Id], [appointment_Id], [register_Fees], [total_Amt]) VALUES (3001, 2002, CAST(350 AS Decimal(18, 0)), CAST(500 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Consult_Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([PatientId], [RegisterNo], [Patient_Name], [Patient_DOB], [Patient_Addr], [Gender], [BloodGroup], [PhoneNumber], [patient_Email], [patient_status]) VALUES (1002, N'R1001', N'Akshay S', CAST(N'1990-05-15' AS Date), N'123 Main St, Cityville', N'M', N'O+', 9876543210, N'akshay.doe@email.com', N'Active')
INSERT [dbo].[Patient] ([PatientId], [RegisterNo], [Patient_Name], [Patient_DOB], [Patient_Addr], [Gender], [BloodGroup], [PhoneNumber], [patient_Email], [patient_status]) VALUES (1003, N'R1002', N'Kavya M', CAST(N'1985-08-22' AS Date), N'456 Oak St, Townsville', N'F', N'A-', 8765432109, N'kavya.smith@email.com', N'Active')
INSERT [dbo].[Patient] ([PatientId], [RegisterNo], [Patient_Name], [Patient_DOB], [Patient_Addr], [Gender], [BloodGroup], [PhoneNumber], [patient_Email], [patient_status]) VALUES (1004, N'R1003', N'Oliver G', CAST(N'1978-11-10' AS Date), N'789 Elm St, Villagetown', N'M', N'B+', 7654321098, N'oliver.johnson@email.com', N'Inactive')
INSERT [dbo].[Patient] ([PatientId], [RegisterNo], [Patient_Name], [Patient_DOB], [Patient_Addr], [Gender], [BloodGroup], [PhoneNumber], [patient_Email], [patient_status]) VALUES (1005, N'R1004', N'Aaron J', CAST(N'1979-11-10' AS Date), N' Elk St, Village', N'M', N'B+', 7654322098, N'aaron.johnson@email.com', N'Inactive')
INSERT [dbo].[Patient] ([PatientId], [RegisterNo], [Patient_Name], [Patient_DOB], [Patient_Addr], [Gender], [BloodGroup], [PhoneNumber], [patient_Email], [patient_status]) VALUES (1006, N'R1005', N'Amy J', CAST(N'1977-11-10' AS Date), N' Elm St, Village vk', N'F', N'B+', 7654322078, N'amy.johnson@email.com', N'Active')
SET IDENTITY_INSERT [dbo].[Patient] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_BillGeneration] ON 

INSERT [dbo].[tbl_BillGeneration] ([billId], [billDate], [totalAmount], [reportId]) VALUES (12001, CAST(N'2024-01-28' AS Date), CAST(1500 AS Decimal(18, 0)), 11001)
INSERT [dbo].[tbl_BillGeneration] ([billId], [billDate], [totalAmount], [reportId]) VALUES (12002, CAST(N'2024-01-29' AS Date), CAST(1800 AS Decimal(18, 0)), 11002)
SET IDENTITY_INSERT [dbo].[tbl_BillGeneration] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Departments] ON 

INSERT [dbo].[tbl_Departments] ([departmentId], [departmentName]) VALUES (301, N'Cardiology')
INSERT [dbo].[tbl_Departments] ([departmentId], [departmentName]) VALUES (302, N'Pediatrician')
INSERT [dbo].[tbl_Departments] ([departmentId], [departmentName]) VALUES (303, N'Neurology')
INSERT [dbo].[tbl_Departments] ([departmentId], [departmentName]) VALUES (304, N'Dermatology')
INSERT [dbo].[tbl_Departments] ([departmentId], [departmentName]) VALUES (305, N'General Medicine')
SET IDENTITY_INSERT [dbo].[tbl_Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Diagnosis] ON 

INSERT [dbo].[tbl_Diagnosis] ([diagnosisId], [symptoms], [diagnosis], [note], [appointmentId]) VALUES (3001, N'Fever, Headache', N'Common Cold', N'Prescribed rest and fluids', 2002)
INSERT [dbo].[tbl_Diagnosis] ([diagnosisId], [symptoms], [diagnosis], [note], [appointmentId]) VALUES (3002, N'Cough, Shortness of breath', N'Bronchitis', N'Prescribed antibiotics', 2004)
SET IDENTITY_INSERT [dbo].[tbl_Diagnosis] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Doctors] ON 

INSERT [dbo].[tbl_Doctors] ([docId], [staffId], [specializationId], [consultationFee]) VALUES (801, 703, 401, CAST(350 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[tbl_Doctors] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_LabPrescriptions] ON 

INSERT [dbo].[tbl_LabPrescriptions] ([labPrescriptionId], [labTestId], [labNote], [labTestStatus], [appointmentId]) VALUES (30001, 201, N'Blood test for cholesterol levels', N'Pending', 2002)
INSERT [dbo].[tbl_LabPrescriptions] ([labPrescriptionId], [labTestId], [labNote], [labTestStatus], [appointmentId]) VALUES (30002, 202, N'Urine test for kidney function', N'Completed', 2004)
SET IDENTITY_INSERT [dbo].[tbl_LabPrescriptions] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_LabTests] ON 

INSERT [dbo].[tbl_LabTests] ([testId], [testName], [lowRange], [highRange], [price]) VALUES (201, N'Blood Count', N'4.5', N'5.5', CAST(25.00 AS Decimal(10, 2)))
INSERT [dbo].[tbl_LabTests] ([testId], [testName], [lowRange], [highRange], [price]) VALUES (202, N'Cholesterol', N'100', N'200', CAST(35.50 AS Decimal(10, 2)))
INSERT [dbo].[tbl_LabTests] ([testId], [testName], [lowRange], [highRange], [price]) VALUES (203, N'Glucose', N'70', N'110', CAST(18.75 AS Decimal(10, 2)))
INSERT [dbo].[tbl_LabTests] ([testId], [testName], [lowRange], [highRange], [price]) VALUES (204, N'Liver Function', N'10', N'40', CAST(30.25 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[tbl_LabTests] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_LoginUsers] ON 

INSERT [dbo].[tbl_LoginUsers] ([loginId], [userName], [password], [roleId]) VALUES (101, N'Sofiya', N'sofiya123', 1)
INSERT [dbo].[tbl_LoginUsers] ([loginId], [userName], [password], [roleId]) VALUES (102, N'Ardra', N'ardra123', 2)
INSERT [dbo].[tbl_LoginUsers] ([loginId], [userName], [password], [roleId]) VALUES (103, N'Arun', N'arun123', 3)
INSERT [dbo].[tbl_LoginUsers] ([loginId], [userName], [password], [roleId]) VALUES (104, N'Adarsh', N'adarsh123', 4)
INSERT [dbo].[tbl_LoginUsers] ([loginId], [userName], [password], [roleId]) VALUES (105, N'Meenu', N'meenu123', 5)
SET IDENTITY_INSERT [dbo].[tbl_LoginUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_MedicinePrescriptions] ON 

INSERT [dbo].[tbl_MedicinePrescriptions] ([medPrescriptionId], [prescribedMedicineId], [dosage], [dosageDays], [medicineQuantity], [appointmentId]) VALUES (20001, NULL, N'0-0-1', 7, 30, 2002)
INSERT [dbo].[tbl_MedicinePrescriptions] ([medPrescriptionId], [prescribedMedicineId], [dosage], [dosageDays], [medicineQuantity], [appointmentId]) VALUES (20002, NULL, N'1-0-0', 10, 60, 2004)
SET IDENTITY_INSERT [dbo].[tbl_MedicinePrescriptions] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Medicines] ON 

INSERT [dbo].[tbl_Medicines] ([medicineId], [medicineCode], [medicineName], [genericName], [companyName], [stockQuantity], [unitPrice]) VALUES (501, N'M001', N'ParaRelief', N'Paracetamol', N'ABC Pharma', N'500', CAST(15.50 AS Decimal(8, 2)))
INSERT [dbo].[tbl_Medicines] ([medicineId], [medicineCode], [medicineName], [genericName], [companyName], [stockQuantity], [unitPrice]) VALUES (502, N'M002', N'PacetoTabs', N'Paracetamol', N'XYZ Pharmaceuticals', N'300', CAST(25.75 AS Decimal(8, 2)))
INSERT [dbo].[tbl_Medicines] ([medicineId], [medicineCode], [medicineName], [genericName], [companyName], [stockQuantity], [unitPrice]) VALUES (503, N'M003', N'AmoxyCaps', N'Amoxicillin', N'Medicare Ltd.', N'200', CAST(30.00 AS Decimal(8, 2)))
INSERT [dbo].[tbl_Medicines] ([medicineId], [medicineCode], [medicineName], [genericName], [companyName], [stockQuantity], [unitPrice]) VALUES (504, N'M004', N'AmoxiDrops', N'Amoxicillin', N'Healthcare Solutions', N'400', CAST(18.25 AS Decimal(8, 2)))
INSERT [dbo].[tbl_Medicines] ([medicineId], [medicineCode], [medicineName], [genericName], [companyName], [stockQuantity], [unitPrice]) VALUES (505, N'M005', N'Omeprazole', N'Omeprazole', N'PharmaCare Inc.', N'250', CAST(22.50 AS Decimal(8, 2)))
SET IDENTITY_INSERT [dbo].[tbl_Medicines] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_PatientHistory] ON 

INSERT [dbo].[tbl_PatientHistory] ([patientHistoryId], [reportId], [diagnosisId], [medPrescriptionId], [labPrescriptionId]) VALUES (70001, 11001, 3001, 20001, 30001)
INSERT [dbo].[tbl_PatientHistory] ([patientHistoryId], [reportId], [diagnosisId], [medPrescriptionId], [labPrescriptionId]) VALUES (70002, 11002, 3002, 20002, 30002)
SET IDENTITY_INSERT [dbo].[tbl_PatientHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Qualifications] ON 

INSERT [dbo].[tbl_Qualifications] ([qualificationId], [qualification]) VALUES (601, N'MBBS')
INSERT [dbo].[tbl_Qualifications] ([qualificationId], [qualification]) VALUES (602, N' MBBS MD')
INSERT [dbo].[tbl_Qualifications] ([qualificationId], [qualification]) VALUES (603, N'BSC Nursing')
INSERT [dbo].[tbl_Qualifications] ([qualificationId], [qualification]) VALUES (604, N'Medical Laboratory')
INSERT [dbo].[tbl_Qualifications] ([qualificationId], [qualification]) VALUES (605, N'B Pharm')
INSERT [dbo].[tbl_Qualifications] ([qualificationId], [qualification]) VALUES (606, N'CCMA')
INSERT [dbo].[tbl_Qualifications] ([qualificationId], [qualification]) VALUES (607, N'BBA Hospital Management')
SET IDENTITY_INSERT [dbo].[tbl_Qualifications] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_ReportGeneration] ON 

INSERT [dbo].[tbl_ReportGeneration] ([reportId], [reportDate], [testResult], [remarks], [appointment_Id], [testId], [staffId]) VALUES (11001, CAST(N'2024-01-30' AS Date), N'Normal', N'No abnormalities detected', 2002, 201, 704)
INSERT [dbo].[tbl_ReportGeneration] ([reportId], [reportDate], [testResult], [remarks], [appointment_Id], [testId], [staffId]) VALUES (11002, CAST(N'2024-01-29' AS Date), N'Abnormal', N'Requires further investigation', 2004, 202, 704)
SET IDENTITY_INSERT [dbo].[tbl_ReportGeneration] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Roles] ON 

INSERT [dbo].[tbl_Roles] ([roleId], [roleName]) VALUES (1, N'Admin')
INSERT [dbo].[tbl_Roles] ([roleId], [roleName]) VALUES (2, N'Receptionist')
INSERT [dbo].[tbl_Roles] ([roleId], [roleName]) VALUES (3, N'Doctor')
INSERT [dbo].[tbl_Roles] ([roleId], [roleName]) VALUES (4, N'Lab Technician')
INSERT [dbo].[tbl_Roles] ([roleId], [roleName]) VALUES (5, N'Pharmacist')
SET IDENTITY_INSERT [dbo].[tbl_Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Specializations] ON 

INSERT [dbo].[tbl_Specializations] ([specializationId], [specialization], [departmentId]) VALUES (401, N'Cardiologist', 301)
INSERT [dbo].[tbl_Specializations] ([specializationId], [specialization], [departmentId]) VALUES (402, N'Pediatric', 302)
INSERT [dbo].[tbl_Specializations] ([specializationId], [specialization], [departmentId]) VALUES (403, N'Neurologist', 303)
INSERT [dbo].[tbl_Specializations] ([specializationId], [specialization], [departmentId]) VALUES (404, N'Dermatologist', 304)
INSERT [dbo].[tbl_Specializations] ([specializationId], [specialization], [departmentId]) VALUES (405, N'Physician', 305)
SET IDENTITY_INSERT [dbo].[tbl_Specializations] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Staffs] ON 

INSERT [dbo].[tbl_Staffs] ([staffId], [fullName], [dob], [gender], [address], [bloodGroup], [joiningDate], [salary], [mobileNo], [loginId], [qualificationId], [departmentId], [Email], [specializationId], [roleId]) VALUES (701, N'Sofiya', CAST(N'1990-05-15' AS Date), N'Female', N'123 Main St, Cityville', N'O+', CAST(N'2022-01-01' AS Date), CAST(60000 AS Decimal(18, 0)), 9876543210, 101, 606, NULL, N'Sofiya@email.com', NULL, 1)
INSERT [dbo].[tbl_Staffs] ([staffId], [fullName], [dob], [gender], [address], [bloodGroup], [joiningDate], [salary], [mobileNo], [loginId], [qualificationId], [departmentId], [Email], [specializationId], [roleId]) VALUES (702, N'Ardra', CAST(N'1985-08-22' AS Date), N'Female', N'456 Oak St, Townsville', N'A-', CAST(N'2022-02-01' AS Date), CAST(75000 AS Decimal(18, 0)), 8765432109, 102, 607, NULL, N'ardra@email.com', NULL, 2)
INSERT [dbo].[tbl_Staffs] ([staffId], [fullName], [dob], [gender], [address], [bloodGroup], [joiningDate], [salary], [mobileNo], [loginId], [qualificationId], [departmentId], [Email], [specializationId], [roleId]) VALUES (703, N'Arun', CAST(N'1978-11-10' AS Date), N'Male', N'789 Elm St, Villagetown', N'B+', CAST(N'2022-03-01' AS Date), CAST(90000 AS Decimal(18, 0)), 7654321098, 103, 602, 301, N'arun@email.com', 401, 3)
INSERT [dbo].[tbl_Staffs] ([staffId], [fullName], [dob], [gender], [address], [bloodGroup], [joiningDate], [salary], [mobileNo], [loginId], [qualificationId], [departmentId], [Email], [specializationId], [roleId]) VALUES (704, N'Adarsh', CAST(N'1978-11-10' AS Date), N'Male', N'789 Elm St, Villagetown', N'B+', CAST(N'2022-03-01' AS Date), CAST(90000 AS Decimal(18, 0)), 7654321098, 104, 604, NULL, N'arun@email.com', NULL, 4)
INSERT [dbo].[tbl_Staffs] ([staffId], [fullName], [dob], [gender], [address], [bloodGroup], [joiningDate], [salary], [mobileNo], [loginId], [qualificationId], [departmentId], [Email], [specializationId], [roleId]) VALUES (705, N'Meenu', CAST(N'1978-11-10' AS Date), N'Female', N'789 Elm St, Villagetown', N'B+', CAST(N'2022-03-01' AS Date), CAST(90000 AS Decimal(18, 0)), 7654321098, 105, 605, NULL, N'meenu@email.com', NULL, 5)
SET IDENTITY_INSERT [dbo].[tbl_Staffs] OFF
GO
ALTER TABLE [dbo].[Appointment] ADD  DEFAULT ('CONFIRMED') FOR [CheckUp_Status]
GO
ALTER TABLE [dbo].[Patient] ADD  DEFAULT ('Active') FOR [patient_status]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Doctor1] FOREIGN KEY([docId])
REFERENCES [dbo].[tbl_Doctors] ([docId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Doctor1]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Patient1] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Patient1]
GO
ALTER TABLE [dbo].[Consult_Bill]  WITH CHECK ADD  CONSTRAINT [FK_Appointment1] FOREIGN KEY([appointment_Id])
REFERENCES [dbo].[Appointment] ([appointment_Id])
GO
ALTER TABLE [dbo].[Consult_Bill] CHECK CONSTRAINT [FK_Appointment1]
GO
ALTER TABLE [dbo].[tbl_BillGeneration]  WITH CHECK ADD FOREIGN KEY([reportId])
REFERENCES [dbo].[tbl_ReportGeneration] ([reportId])
GO
ALTER TABLE [dbo].[tbl_Diagnosis]  WITH CHECK ADD FOREIGN KEY([appointmentId])
REFERENCES [dbo].[Appointment] ([appointment_Id])
GO
ALTER TABLE [dbo].[tbl_Doctors]  WITH CHECK ADD FOREIGN KEY([specializationId])
REFERENCES [dbo].[tbl_Specializations] ([specializationId])
GO
ALTER TABLE [dbo].[tbl_Doctors]  WITH CHECK ADD FOREIGN KEY([staffId])
REFERENCES [dbo].[tbl_Staffs] ([staffId])
GO
ALTER TABLE [dbo].[tbl_LabPrescriptions]  WITH CHECK ADD FOREIGN KEY([appointmentId])
REFERENCES [dbo].[Appointment] ([appointment_Id])
GO
ALTER TABLE [dbo].[tbl_LabPrescriptions]  WITH CHECK ADD FOREIGN KEY([labTestId])
REFERENCES [dbo].[tbl_LabTests] ([testId])
GO
ALTER TABLE [dbo].[tbl_LoginUsers]  WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[tbl_Roles] ([roleId])
GO
ALTER TABLE [dbo].[tbl_MedicinePrescriptions]  WITH CHECK ADD FOREIGN KEY([appointmentId])
REFERENCES [dbo].[Appointment] ([appointment_Id])
GO
ALTER TABLE [dbo].[tbl_MedicinePrescriptions]  WITH CHECK ADD FOREIGN KEY([prescribedMedicineId])
REFERENCES [dbo].[tbl_Medicines] ([medicineId])
GO
ALTER TABLE [dbo].[tbl_PatientHistory]  WITH CHECK ADD FOREIGN KEY([diagnosisId])
REFERENCES [dbo].[tbl_Diagnosis] ([diagnosisId])
GO
ALTER TABLE [dbo].[tbl_PatientHistory]  WITH CHECK ADD FOREIGN KEY([labPrescriptionId])
REFERENCES [dbo].[tbl_LabPrescriptions] ([labPrescriptionId])
GO
ALTER TABLE [dbo].[tbl_PatientHistory]  WITH CHECK ADD FOREIGN KEY([medPrescriptionId])
REFERENCES [dbo].[tbl_MedicinePrescriptions] ([medPrescriptionId])
GO
ALTER TABLE [dbo].[tbl_PatientHistory]  WITH CHECK ADD FOREIGN KEY([reportId])
REFERENCES [dbo].[tbl_ReportGeneration] ([reportId])
GO
ALTER TABLE [dbo].[tbl_ReportGeneration]  WITH CHECK ADD FOREIGN KEY([appointment_Id])
REFERENCES [dbo].[Appointment] ([appointment_Id])
GO
ALTER TABLE [dbo].[tbl_ReportGeneration]  WITH CHECK ADD FOREIGN KEY([staffId])
REFERENCES [dbo].[tbl_Staffs] ([staffId])
GO
ALTER TABLE [dbo].[tbl_ReportGeneration]  WITH CHECK ADD FOREIGN KEY([testId])
REFERENCES [dbo].[tbl_LabTests] ([testId])
GO
ALTER TABLE [dbo].[tbl_Specializations]  WITH CHECK ADD FOREIGN KEY([departmentId])
REFERENCES [dbo].[tbl_Departments] ([departmentId])
GO
ALTER TABLE [dbo].[tbl_Staffs]  WITH CHECK ADD FOREIGN KEY([departmentId])
REFERENCES [dbo].[tbl_Departments] ([departmentId])
GO
ALTER TABLE [dbo].[tbl_Staffs]  WITH CHECK ADD FOREIGN KEY([loginId])
REFERENCES [dbo].[tbl_LoginUsers] ([loginId])
GO
ALTER TABLE [dbo].[tbl_Staffs]  WITH CHECK ADD FOREIGN KEY([qualificationId])
REFERENCES [dbo].[tbl_Qualifications] ([qualificationId])
GO
ALTER TABLE [dbo].[tbl_Staffs]  WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[tbl_Roles] ([roleId])
GO
ALTER TABLE [dbo].[tbl_Staffs]  WITH CHECK ADD FOREIGN KEY([specializationId])
REFERENCES [dbo].[tbl_Specializations] ([specializationId])
GO
USE [master]
GO
ALTER DATABASE [ASPCMSDB] SET  READ_WRITE 
GO
