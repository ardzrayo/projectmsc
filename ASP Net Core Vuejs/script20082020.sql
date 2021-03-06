USE [master]
GO
/****** Object:  Database [proyectmsc]    Script Date: 20/08/2020 09:36:56 p. m. ******/
CREATE DATABASE [proyectmsc]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'proyectmsc', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\proyectmsc.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'proyectmsc_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\proyectmsc_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [proyectmsc].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [proyectmsc] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [proyectmsc] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [proyectmsc] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [proyectmsc] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [proyectmsc] SET ARITHABORT OFF 
GO
ALTER DATABASE [proyectmsc] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [proyectmsc] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [proyectmsc] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [proyectmsc] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [proyectmsc] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [proyectmsc] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [proyectmsc] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [proyectmsc] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [proyectmsc] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [proyectmsc] SET  ENABLE_BROKER 
GO
ALTER DATABASE [proyectmsc] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [proyectmsc] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [proyectmsc] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [proyectmsc] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [proyectmsc] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [proyectmsc] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [proyectmsc] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [proyectmsc] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [proyectmsc] SET  MULTI_USER 
GO
ALTER DATABASE [proyectmsc] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [proyectmsc] SET DB_CHAINING OFF 
GO
ALTER DATABASE [proyectmsc] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [proyectmsc] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [proyectmsc] SET DELAYED_DURABILITY = DISABLED 
GO
USE [proyectmsc]
GO
/****** Object:  Table [dbo].[configmail]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[configmail](
	[idperiodo] [int] IDENTITY(1,1) NOT NULL,
	[nameperiodo] [varchar](50) NOT NULL,
	[dia] [int] NOT NULL,
	[asunto] [varchar](100) NOT NULL,
	[cuerpomail] [varchar](max) NOT NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idperiodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[networkbond]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[networkbond](
	[idnw] [int] IDENTITY(1,1) NOT NULL,
	[nwbond] [varchar](100) NOT NULL,
	[nwestado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idnw] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notifivps]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notifivps](
	[idnotivps] [int] IDENTITY(1,1) NOT NULL,
	[idvps] [int] NOT NULL,
	[vmname] [varchar](50) NOT NULL,
	[idclient] [int] NOT NULL,
	[clientname] [varchar](50) NULL,
	[clientcontact] [varchar](100) NULL,
	[emailcontact_tecnico] [varchar](200) NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idnotivps] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[osfamily]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[osfamily](
	[idos] [int] IDENTITY(1,1) NOT NULL,
	[osfamilyname] [varchar](100) NOT NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[osversion]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[osversion](
	[idversion] [int] IDENTITY(1,1) NOT NULL,
	[idos] [int] NOT NULL,
	[osversion] [varchar](50) NOT NULL,
	[descripcion] [varchar](256) NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idversion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[periodo]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[periodo](
	[idperiodo] [int] IDENTITY(1,1) NOT NULL,
	[dia1] [int] NULL,
	[dia2] [int] NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idperiodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pools]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pools](
	[idpool] [int] IDENTITY(1,1) NOT NULL,
	[poolname] [varchar](50) NOT NULL,
	[pooldescripcion] [varchar](256) NULL,
	[poolestado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idpool] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rol]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rol](
	[idrol] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idrol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sqlfamily]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sqlfamily](
	[idsql] [int] IDENTITY(1,1) NOT NULL,
	[mssql] [varchar](100) NOT NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idsql] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sqlversion]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sqlversion](
	[idsqlversion] [int] IDENTITY(1,1) NOT NULL,
	[idsql] [int] NOT NULL,
	[mssqlversion] [varchar](100) NOT NULL,
	[mssqldescription] [varchar](256) NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idsqlversion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[idusuario] [int] IDENTITY(1,1) NOT NULL,
	[idrol] [int] NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[direccion] [varchar](70) NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](50) NOT NULL,
	[password_hash] [varbinary](max) NOT NULL,
	[password_salt] [varbinary](max) NOT NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idusuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vmclient]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vmclient](
	[idclient] [int] IDENTITY(1,1) NOT NULL,
	[clientname] [varchar](50) NOT NULL,
	[clientfullname] [varchar](256) NULL,
	[clientemail] [varchar](100) NULL,
	[clientphone] [varchar](20) NULL,
	[clientcontact] [varchar](100) NULL,
	[emailcontact_tecnico] [varchar](100) NOT NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idclient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vmtype]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vmtype](
	[idvmtype] [int] IDENTITY(1,1) NOT NULL,
	[vmtypename] [varchar](50) NOT NULL,
	[vmtypedescription] [varchar](256) NULL,
	[vmtypeestado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idvmtype] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vps]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vps](
	[idvps] [int] IDENTITY(1,1) NOT NULL,
	[idclient] [int] NOT NULL,
	[vmname] [varchar](50) NOT NULL,
	[vm_uuid] [varchar](256) NOT NULL,
	[vcpus] [int] NOT NULL,
	[ram] [int] NOT NULL,
	[hdisk] [int] NOT NULL,
	[bandw] [int] NULL,
	[idnw] [int] NOT NULL,
	[idos] [int] NOT NULL,
	[idversion] [int] NULL,
	[idsql] [int] NULL,
	[idsqlversion] [int] NULL,
	[internal_ip] [varchar](256) NOT NULL,
	[external_ip] [varchar](256) NOT NULL,
	[createon] [datetime] NOT NULL,
	[idusuario] [int] NOT NULL,
	[dnsname] [varchar](256) NULL,
	[idvmtype] [int] NOT NULL,
	[idpool] [int] NOT NULL,
	[notes] [varchar](256) NULL,
	[rmtaccesssal] [int] NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idvps] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[configmail] ON 

INSERT [dbo].[configmail] ([idperiodo], [nameperiodo], [dia], [asunto], [cuerpomail], [estado]) VALUES (1, N'Primer aviso', 10, N'Primer Aviso - Notificación  BTU Cloud', N'Buen día, el motivo del mensaje presente es para comunicarles que no ha sido recibido el pago de renta de su servicio de Servidor Virtual Privado. A partir del presente día usted cuenta con 5 días para realizar su pago antes de concluir con su periodo de pago, de lo contrario su servicio se vera suspendido.', 1)
INSERT [dbo].[configmail] ([idperiodo], [nameperiodo], [dia], [asunto], [cuerpomail], [estado]) VALUES (2, N'Segundo aviso', 12, N'Segundo aviso - Notificación  BTU Cloud', N'Buen día, el motivo del mensaje presente es para comunicarles que no ha sido recibido el pago de renta de su servicio de Servidor Virtual Privado. A partir del presente día usted cuenta con 3 días para realizar su pago antes de concluir con su periodo de pago, de lo contrario su servicio se vera suspendido.', 1)
SET IDENTITY_INSERT [dbo].[configmail] OFF
GO
SET IDENTITY_INSERT [dbo].[networkbond] ON 

INSERT [dbo].[networkbond] ([idnw], [nwbond], [nwestado]) VALUES (1, N'LUNA01', 1)
INSERT [dbo].[networkbond] ([idnw], [nwbond], [nwestado]) VALUES (3, N'LUNA02', 1)
INSERT [dbo].[networkbond] ([idnw], [nwbond], [nwestado]) VALUES (4, N'LUNA03', 1)
SET IDENTITY_INSERT [dbo].[networkbond] OFF
GO
SET IDENTITY_INSERT [dbo].[notifivps] ON 

INSERT [dbo].[notifivps] ([idnotivps], [idvps], [vmname], [idclient], [clientname], [clientcontact], [emailcontact_tecnico], [estado]) VALUES (1, 26, N'Macro x36', 7, N'Cinepolis', N'Demetrio Rodríguez', N'ardzrayo@gmail.com', 1)
SET IDENTITY_INSERT [dbo].[notifivps] OFF
GO
SET IDENTITY_INSERT [dbo].[osfamily] ON 

INSERT [dbo].[osfamily] ([idos], [osfamilyname], [estado]) VALUES (1, N'Microsoft Windows Server', 1)
INSERT [dbo].[osfamily] ([idos], [osfamilyname], [estado]) VALUES (2, N'Ubuntu', 1)
SET IDENTITY_INSERT [dbo].[osfamily] OFF
GO
SET IDENTITY_INSERT [dbo].[osversion] ON 

INSERT [dbo].[osversion] ([idversion], [idos], [osversion], [descripcion], [estado]) VALUES (1, 1, N'2012 R2', N'Microsoft Windows Server 2012', 1)
INSERT [dbo].[osversion] ([idversion], [idos], [osversion], [descripcion], [estado]) VALUES (5, 1, N'2014 R2', N'Microsoft Windows Server 2014 R2', 1)
INSERT [dbo].[osversion] ([idversion], [idos], [osversion], [descripcion], [estado]) VALUES (6, 2, N'10.4', N'12/12/2018', 1)
SET IDENTITY_INSERT [dbo].[osversion] OFF
GO
SET IDENTITY_INSERT [dbo].[periodo] ON 

INSERT [dbo].[periodo] ([idperiodo], [dia1], [dia2], [estado]) VALUES (1, 10, 19, 1)
SET IDENTITY_INSERT [dbo].[periodo] OFF
GO
SET IDENTITY_INSERT [dbo].[pools] ON 

INSERT [dbo].[pools] ([idpool], [poolname], [pooldescripcion], [poolestado]) VALUES (1, N'BTU01', N'Servidor DELL 1', 1)
INSERT [dbo].[pools] ([idpool], [poolname], [pooldescripcion], [poolestado]) VALUES (3, N'BTU02', N'Servidor Acer 256 RAM', 1)
SET IDENTITY_INSERT [dbo].[pools] OFF
GO
SET IDENTITY_INSERT [dbo].[rol] ON 

INSERT [dbo].[rol] ([idrol], [nombre], [descripcion], [estado]) VALUES (1, N'Administrador', N'Administrador del Sistema y Jefe de Departamento', 1)
INSERT [dbo].[rol] ([idrol], [nombre], [descripcion], [estado]) VALUES (2, N'Soporte', N'Soporte Técnico', 1)
INSERT [dbo].[rol] ([idrol], [nombre], [descripcion], [estado]) VALUES (3, N'Gerente', N'Gerente del Área Administrativa', 1)
SET IDENTITY_INSERT [dbo].[rol] OFF
GO
SET IDENTITY_INSERT [dbo].[sqlfamily] ON 

INSERT [dbo].[sqlfamily] ([idsql], [mssql], [estado]) VALUES (1, N'Microsoft SQL Server', 1)
INSERT [dbo].[sqlfamily] ([idsql], [mssql], [estado]) VALUES (3, N'Solaris', 1)
SET IDENTITY_INSERT [dbo].[sqlfamily] OFF
GO
SET IDENTITY_INSERT [dbo].[sqlversion] ON 

INSERT [dbo].[sqlversion] ([idsqlversion], [idsql], [mssqlversion], [mssqldescription], [estado]) VALUES (1, 1, N'Microsoft SQL Web Edition 2014', N'Recomendada para base de datos no mayores a 10 GB de almacenamiento.', 1)
INSERT [dbo].[sqlversion] ([idsqlversion], [idsql], [mssqlversion], [mssqldescription], [estado]) VALUES (3, 1, N'2016', N'Cuenta con la opción de respaldo espejo.', 1)
SET IDENTITY_INSERT [dbo].[sqlversion] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([idusuario], [idrol], [nombre], [direccion], [telefono], [email], [password_hash], [password_salt], [estado]) VALUES (1, 1, N'Ángel Rodríguez Rayo', N'C. Cipreses No. 16, Col. Jardín Mangos, C.P. 39412', N'7442900402', N'ardzrayo18@gmail.com', 0x5AD5BA07A03D2ACB1C616DFA537DBB7AB4A3382C46ED98F187459353AEC7020022F1BA5A1C64CB6CBE3E5ACD0E8A2516A10CAA5C2D6B537E6F0CA91A9E681832, 0x8766F1260366230A36F8F907ACB011781BB687DF7CA9C9901AF421346BE2A075F04D036A492C78F06D76F6C5D06A30F08F9F11AF1BBC4D8B481CE360AAB5034477BF1D05486571D2F3E29AADEC3D3F2D6C9D6C70A885831D899E8A2F1843CD87ED82A19C4F825DB273509A29C4AC1741F80A55DC746A9891A86ADD204B0A1C22, 1)
INSERT [dbo].[usuario] ([idusuario], [idrol], [nombre], [direccion], [telefono], [email], [password_hash], [password_salt], [estado]) VALUES (2, 2, N'Eduardo Rodríguez Rayo', N'C. Cipreses No. 16', N'7443113747', N'ed.rodriguez@outlook.com', 0x19A23822187777A81FE61293B3D3D8309BFC4C2DF0B4ECA2967FA1912CAB85A608F15ABCC76FBF0653980306FFEF19D3953A24FE1F6988D403ABEE2072EAA7E8, 0xBC22178C6F8C9382EA2BF903633220E95047E5DC062925717F69FB34AE4F8704C32A6AC66A5AA17331B2A3C5DBB4711A1F355303D6C31C1A9E5A09B009A302E3019903A56A3914E148D692BF79FF447B73F3D264EB1BA3F9DA3D1849E4B71BB90AA2B38EF6356DF826272894DAD28ADAE2AA8727BA60D2AA9745A75E954779B9, 1)
INSERT [dbo].[usuario] ([idusuario], [idrol], [nombre], [direccion], [telefono], [email], [password_hash], [password_salt], [estado]) VALUES (3, 3, N'Ángel', N'Jardín Mangos', N'7442900402', N'ardzrayo@gmail.com', 0x3674CBBBCEDC7CE50191BA74FCF74E9C737F7CBF5B8F9F748D817256E066838217EB52D9ED38691D5833EEDD230668C7CC17FFE075141FEC6BAD551F6C253994, 0xC79CD9DCB07E19CD62EF1210DF07DC8DD359AB002A516C2534ED6A745DD16D879245036E0D507B264B44954126CAF5AA5296AFF9A2E087590B74B89C8B0F44FDC53E4F74F138F7D37B525AD7889CECE1D86553ACDCE733040ADC376A9411D38DD4612BCEB63BA74C702325F38AA2B536D4AA1197DC031A8599B75DCFDA534D65, 1)
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[vmclient] ON 

INSERT [dbo].[vmclient] ([idclient], [clientname], [clientfullname], [clientemail], [clientphone], [clientcontact], [emailcontact_tecnico], [estado]) VALUES (1, N'BTU', N'BTU Comunicación', N'btu@btu.com.mx', N'72222222', N'Luis A Bajos', N'soporte@btu.com.mx', 1)
INSERT [dbo].[vmclient] ([idclient], [clientname], [clientfullname], [clientemail], [clientphone], [clientcontact], [emailcontact_tecnico], [estado]) VALUES (2, N'ITA', N'Instituto Tecnologico de Acapulco', N'itacapulco@ita.com', N'7442900001', N'Dr. Gámez', N'gamezeduardo@yahoo.com', 1)
INSERT [dbo].[vmclient] ([idclient], [clientname], [clientfullname], [clientemail], [clientphone], [clientcontact], [emailcontact_tecnico], [estado]) VALUES (4, N'Syscolin', N'Syscolin 22', N'Ivan@syscolin.com', N'7488996655', N'Ivan Colin', N'ivsys@syscoin.com', 1)
INSERT [dbo].[vmclient] ([idclient], [clientname], [clientfullname], [clientemail], [clientphone], [clientcontact], [emailcontact_tecnico], [estado]) VALUES (5, N'100% Natural', N'100% Natural S.A. de C.V.', N'100natural@natural.com.mx', N'7444444498', N'Ing. Edgar', N'edgar@natural.com.mx', 1)
INSERT [dbo].[vmclient] ([idclient], [clientname], [clientfullname], [clientemail], [clientphone], [clientcontact], [emailcontact_tecnico], [estado]) VALUES (7, N'Cinepolis', N'Macro x33', N'cinepolis@cinepolis.com', N'7442900707', N'Demetrio Rodríguez', N'ardzrayo@gmail.com', 1)
SET IDENTITY_INSERT [dbo].[vmclient] OFF
GO
SET IDENTITY_INSERT [dbo].[vmtype] ON 

INSERT [dbo].[vmtype] ([idvmtype], [vmtypename], [vmtypedescription], [vmtypeestado]) VALUES (1, N'Renta', N'Se cobra una cuota mensual del servicio.', 1)
INSERT [dbo].[vmtype] ([idvmtype], [vmtypename], [vmtypedescription], [vmtypeestado]) VALUES (2, N'Demo', N'No se cobra una cuota mensual del servicio, disponible 30 días.', 1)
INSERT [dbo].[vmtype] ([idvmtype], [vmtypename], [vmtypedescription], [vmtypeestado]) VALUES (3, N'Demo Escuelas', N'No se cobra una cuota mensual del servicio, disponible 40 días.', 1)
INSERT [dbo].[vmtype] ([idvmtype], [vmtypename], [vmtypedescription], [vmtypeestado]) VALUES (5, N'Demo Organizaciones Publicas', N'Demo otorgado a instituciones sin fines de lucro.', 1)
SET IDENTITY_INSERT [dbo].[vmtype] OFF
GO
SET IDENTITY_INSERT [dbo].[vps] ON 

INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (8, 1, N'BTU Win ', N'123456789', 8, 16, 300, 10, 1, 1, 1, 1, 1, N'192.168.10.1', N'201.131.100.10', CAST(N'2020-02-25T07:07:07.000' AS DateTime), 1, N'Test', 5, 1, N'Test', 2, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (18, 1, N'ITA03', N'1234569877', 8, 16, 100, 10, 1, 1, 1, 1, 1, N'192.168.10.222', N'201.131.100.222', CAST(N'2020-02-25T20:04:51.493' AS DateTime), 1, N'test', 1, 1, N'Test', 1, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (19, 1, N'ITA04', N'12345699877', 8, 16, 100, 10, 1, 1, 1, 1, 1, N'192.168.10.223', N'201.131.100.223', CAST(N'2020-02-25T20:06:07.933' AS DateTime), 1, N'test', 1, 1, N'Test', 1, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (20, 2, N'ITA 04', N'741258963', 10, 20, 200, 20, 3, 1, 1, 1, 3, N'Prueba', N'Prueba', CAST(N'2020-02-25T20:08:07.870' AS DateTime), 3, N'Prueba', 2, 3, N'Preuba', 10, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (21, 2, N'nombre', N'123456789', 4, 16, 300, 10, 1, 1, 1, 3, 3, N'192.1688.10.10', N'201.131.20.10', CAST(N'2020-03-18T14:56:28.287' AS DateTime), 1, N'', 1, 3, N'', 10, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (22, 7, N'Macrodfsdfsdf x33', N'8716871786178917', 8, 16, 100, 10, 4, 1, 5, 1, 3, N'192.168.16.200', N'210.131.20.200', CAST(N'2020-07-02T15:06:38.900' AS DateTime), 1, N'8.8.8.8.8', 1, 1, N'454242fdfg', 0, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (23, 7, N'macrox34', N'78946542123477817', 16, 48, 200, 10, 3, 2, 6, 1, 3, N'192.168.15.15', N'200.10.30.15', CAST(N'2020-07-02T17:33:52.157' AS DateTime), 1, N'', 1, 3, N'sin nota', 0, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (24, 7, N'Macro x35', N'789456123741852', 8, 16, 300, 10, 4, 1, 5, 1, 1, N'192.168.40.10', N'215.10.20.10', CAST(N'2020-07-02T18:18:16.157' AS DateTime), 1, N'', 1, 3, N'', 0, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (25, 2, N'Centro de computo', N'2877817878737117', 2, 4, 100, 10, 1, 1, 1, 3, 3, N'192.168.40.40', N'220.110.30.40', CAST(N'2020-07-02T20:48:06.720' AS DateTime), 2, N'', 1, 3, N'eeeee', 0, 1)
INSERT [dbo].[vps] ([idvps], [idclient], [vmname], [vm_uuid], [vcpus], [ram], [hdisk], [bandw], [idnw], [idos], [idversion], [idsql], [idsqlversion], [internal_ip], [external_ip], [createon], [idusuario], [dnsname], [idvmtype], [idpool], [notes], [rmtaccesssal], [estado]) VALUES (26, 7, N'Macro x36', N'7534218697896541233', 8, 16, 300, 10, 1, 1, 5, 1, 1, N'192.168.90.10', N'210.100.200.10', CAST(N'2020-07-03T19:38:03.137' AS DateTime), 1, N'', 1, 1, N'Acapulco gro', 10, 1)
SET IDENTITY_INSERT [dbo].[vps] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__osversio__38EADACD1EBB7DD4]    Script Date: 20/08/2020 09:36:56 p. m. ******/
ALTER TABLE [dbo].[osversion] ADD UNIQUE NONCLUSTERED 
(
	[osversion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__sqlversi__5E85655AF21F5A9B]    Script Date: 20/08/2020 09:36:56 p. m. ******/
ALTER TABLE [dbo].[sqlversion] ADD UNIQUE NONCLUSTERED 
(
	[mssqlversion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__vmclient__FDBA7FAF75E32F95]    Script Date: 20/08/2020 09:36:56 p. m. ******/
ALTER TABLE [dbo].[vmclient] ADD UNIQUE NONCLUSTERED 
(
	[clientname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[configmail] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[networkbond] ADD  DEFAULT ((1)) FOR [nwestado]
GO
ALTER TABLE [dbo].[notifivps] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[osfamily] ADD  CONSTRAINT [DF_osfamily_estado]  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[osversion] ADD  CONSTRAINT [DF_osversion_estado]  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[periodo] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[pools] ADD  DEFAULT ((1)) FOR [poolestado]
GO
ALTER TABLE [dbo].[rol] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[sqlfamily] ADD  CONSTRAINT [DF_sqlfamily_estado]  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[sqlversion] ADD  CONSTRAINT [DF_sqlversion_estado]  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[usuario] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[vmclient] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[vmtype] ADD  DEFAULT ((1)) FOR [vmtypeestado]
GO
ALTER TABLE [dbo].[vps] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[osversion]  WITH CHECK ADD FOREIGN KEY([idos])
REFERENCES [dbo].[osfamily] ([idos])
GO
ALTER TABLE [dbo].[sqlversion]  WITH CHECK ADD FOREIGN KEY([idsql])
REFERENCES [dbo].[sqlfamily] ([idsql])
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD FOREIGN KEY([idrol])
REFERENCES [dbo].[rol] ([idrol])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idclient])
REFERENCES [dbo].[vmclient] ([idclient])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idnw])
REFERENCES [dbo].[networkbond] ([idnw])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idos])
REFERENCES [dbo].[osfamily] ([idos])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idpool])
REFERENCES [dbo].[pools] ([idpool])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idsql])
REFERENCES [dbo].[sqlfamily] ([idsql])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idsqlversion])
REFERENCES [dbo].[sqlversion] ([idsqlversion])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idusuario])
REFERENCES [dbo].[usuario] ([idusuario])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idversion])
REFERENCES [dbo].[osversion] ([idversion])
GO
ALTER TABLE [dbo].[vps]  WITH CHECK ADD FOREIGN KEY([idvmtype])
REFERENCES [dbo].[vmtype] ([idvmtype])
GO
/****** Object:  Trigger [dbo].[TR_VMClient_UP_Contact]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_VMClient_UP_Contact]
	on [dbo].[vmclient]
	after update
	as
	if update (clientcontact)
	begin
		set nocount on;
	update notifivps
	set clientcontact = vmclient.clientcontact
	from notifivps
	INNER JOIN vmclient on vmclient.idclient = notifivps.idclient
	end
GO
ALTER TABLE [dbo].[vmclient] ENABLE TRIGGER [TR_VMClient_UP_Contact]
GO
/****** Object:  Trigger [dbo].[TR_VMClient_UP_EmailC]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_VMClient_UP_EmailC]
	on [dbo].[vmclient]
	after update
	as
	if update (emailcontact_tecnico)
	begin
		set nocount on;
	update notifivps
	set emailcontact_tecnico = vmclient.emailcontact_tecnico
	from notifivps
	INNER JOIN vmclient on vmclient.idclient = notifivps.idclient
	end
GO
ALTER TABLE [dbo].[vmclient] ENABLE TRIGGER [TR_VMClient_UP_EmailC]
GO
/****** Object:  Trigger [dbo].[TR_VMClient_UP_Name]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_VMClient_UP_Name]
	on [dbo].[vmclient]
	after update
	as
	if update (clientname)
	begin
		set nocount on;
	update notifivps
	set clientname = vmclient.clientname
	from notifivps
	INNER JOIN vmclient on vmclient.idclient = notifivps.idclient
	end
GO
ALTER TABLE [dbo].[vmclient] ENABLE TRIGGER [TR_VMClient_UP_Name]
GO
/****** Object:  Trigger [dbo].[TR_VPS_UP]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_VPS_UP]
on [dbo].[vmclient]
after update
as
if update (estado)
begin
set nocount on;
update vps
set estado = vmclient.estado
from vps
INNER JOIN vmclient on vmclient.idclient = vps.idclient
end
GO
ALTER TABLE [dbo].[vmclient] ENABLE TRIGGER [TR_VPS_UP]
GO
/****** Object:  Trigger [dbo].[TR_NOTI_IN]    Script Date: 20/08/2020 09:36:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_NOTI_IN]
	on [dbo].[vps]
	after insert
	as
		set nocount on;
	--Declarar variables
	Declare @idvmtype int
	Declare @idvps int
	Declare @vmname varchar(50)
	Declare @idclient int
	Declare @clientname varchar(50)
	Declare @clientcontact varchar(100)
	Declare @emailcontact_tecnico varchar (200)
	--Asignar variables
	set @idvmtype = (Select idvmtype from inserted)
	set @idvps = (Select idvps from inserted)
	set @vmname = (Select vmname from inserted)
	set @idclient = (Select idclient from inserted)
	set @clientname = (Select clientname from vmclient as v inner join inserted as d on d.idclient=v.idclient)
	set @clientcontact = (Select clientcontact from vmclient as v inner join inserted as d on d.idclient=v.idclient)
	set @emailcontact_tecnico = (Select emailcontact_tecnico from vmclient as v inner join inserted as d on d.idclient=v.idclient)
	if @idvmtype < 2
	insert into notifivps (idvps,vmname,idclient,clientname,clientcontact,emailcontact_tecnico)
	values (@idvps, @vmname, @idclient,@clientname,@clientcontact,@emailcontact_tecnico)
GO
ALTER TABLE [dbo].[vps] ENABLE TRIGGER [TR_NOTI_IN]
GO
/****** Object:  Trigger [dbo].[TR_NOTI_UP]    Script Date: 20/08/2020 09:36:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_NOTI_UP]
	on [dbo].[vps]
	after update
	as
	if update (estado)
	begin
		set nocount on;
	update notifivps
	set estado = vps.estado
	from notifivps
	INNER JOIN vps on vps.idvps = notifivps.idvps
	end
GO
ALTER TABLE [dbo].[vps] ENABLE TRIGGER [TR_NOTI_UP]
GO
/****** Object:  Trigger [dbo].[TR_VPS_UP_Name]    Script Date: 20/08/2020 09:36:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_VPS_UP_Name]
	on [dbo].[vps]
	after update
	as
	if update (vmname)
	begin
		set nocount on;
	update notifivps
	set vmname = vps.vmname
	from notifivps
	INNER JOIN vps on vps.idvps = notifivps.idvps
	end
GO
ALTER TABLE [dbo].[vps] ENABLE TRIGGER [TR_VPS_UP_Name]
GO
USE [master]
GO
ALTER DATABASE [proyectmsc] SET  READ_WRITE 
GO
