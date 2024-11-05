USE [master]
GO
/****** Object:  Database [ApiFactura]    Script Date: 5/11/2024 6:25:33 ******/
CREATE DATABASE [ApiFactura]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ApiFactura', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ApiFactura.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ApiFactura_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ApiFactura_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ApiFactura] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ApiFactura].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ApiFactura] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ApiFactura] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ApiFactura] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ApiFactura] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ApiFactura] SET ARITHABORT OFF 
GO
ALTER DATABASE [ApiFactura] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ApiFactura] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ApiFactura] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ApiFactura] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ApiFactura] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ApiFactura] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ApiFactura] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ApiFactura] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ApiFactura] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ApiFactura] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ApiFactura] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ApiFactura] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ApiFactura] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ApiFactura] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ApiFactura] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ApiFactura] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ApiFactura] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ApiFactura] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ApiFactura] SET  MULTI_USER 
GO
ALTER DATABASE [ApiFactura] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ApiFactura] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ApiFactura] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ApiFactura] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ApiFactura] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ApiFactura] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ApiFactura] SET QUERY_STORE = ON
GO
ALTER DATABASE [ApiFactura] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ApiFactura]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[IdClient] [int] IDENTITY(1,1) NOT NULL,
	[Identification] [varchar](10) NULL,
	[Name] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[IdProduct] [int] IDENTITY(1,1) NOT NULL,
	[CodeProduct] [int] NULL,
	[NameProduct] [varchar](50) NULL,
	[UnitPrice] [decimal](18, 0) NULL,
	[StatusProduct] [tinyint] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Identification] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[DateOrigin] [date] NULL,
	[isAdmin] [tinyint] NULL,
	[NameUser] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Client] ON 
GO
INSERT [dbo].[Client] ([IdClient], [Identification], [Name], [Telephone], [Email]) VALUES (3, N'0932144165', N'John Doe', N'5551234', N'johndoe@example.com')
GO
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([IdProduct], [CodeProduct], [NameProduct], [UnitPrice], [StatusProduct]) VALUES (2, 1234, N'Teclado Gaming F15', CAST(45 AS Decimal(18, 0)), 1)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
/****** Object:  StoredProcedure [dbo].[AddClient]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddClient]
    @Identification NVARCHAR(15),
    @Name NVARCHAR(100),
    @Telephone NVARCHAR(15),
    @Email NVARCHAR(100),
    @Message NVARCHAR(100) OUTPUT -- Agregamos el parámetro de salida
AS
BEGIN
    DECLARE @ClienteExiste INT;
    
    -- Verificamos si ya existe un cliente con la identificación dada
    SELECT @ClienteExiste = COUNT(*)
    FROM Client
    WHERE Identification = @Identification;

    IF @ClienteExiste > 0
    BEGIN
        -- Si existe, establecemos el mensaje de error en el parámetro de salida
        SET @Message = 'El usuario ya existe';
        RETURN;
    END
    ELSE
    BEGIN
        -- Si no existe, insertamos el nuevo cliente
        INSERT INTO Client (Identification, Name, Telephone, Email)
        VALUES (@Identification, @Name, @Telephone, @Email);
        
        -- Establecemos el mensaje de éxito en el parámetro de salida
        SET @Message = 'El usuario se registró correctamente.';
    END
END
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddProduct]
    @CodeProduct INT,
    @NameProduct NVARCHAR(50),
    @UnitPrice DECIMAL(18, 0),
    @StatusProduct TINYINT,
    @Message NVARCHAR(100) OUTPUT
AS
BEGIN
    -- Verificar si el producto ya existe en la tabla
    IF EXISTS (SELECT 1 FROM Product WHERE CodeProduct = @CodeProduct)
    BEGIN
        -- Si el producto ya existe, asigna un mensaje de error
        SET @Message = 'El producto ya existe.';
    END
    ELSE
    BEGIN
        -- Insertar el nuevo producto
        INSERT INTO Product (CodeProduct, NameProduct, UnitPrice, StatusProduct)
        VALUES (@CodeProduct, @NameProduct, @UnitPrice, @StatusProduct);

        -- Asigna el mensaje de éxito
        SET @Message = 'El producto se registró correctamente.';
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
    @Identification VARCHAR(50),
    @Username VARCHAR(50),
    @Password VARCHAR(50),
    @DateOrigin DATE,
    @isAdmin TINYINT,
    @NameUser VARCHAR(50),
    @Email VARCHAR(50),
    @Message NVARCHAR(100) OUTPUT
AS
BEGIN
    -- Verificar si el usuario ya existe por Identificación o Nombre de Usuario
    IF EXISTS (SELECT 1 FROM [User] WHERE Identification = @Identification OR Username = @Username)
    BEGIN
        SET @Message = 'El usuario ya existe.'
    END
    ELSE
    BEGIN
        -- Insertar el nuevo usuario
        INSERT INTO [User] (Identification, Username, Password, DateOrigin, isAdmin, NameUser, Email)
        VALUES (@Identification, @Username, @Password, @DateOrigin, @isAdmin, @NameUser, @Email)

        -- Confirmación de registro exitoso
        SET @Message = 'Usuario registrado correctamente.'
    END
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteClient]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteClient]
    @Identification INT,
    @Message NVARCHAR(100) OUTPUT
AS
BEGIN
    -- Verificar si el cliente existe en la tabla
    IF EXISTS (SELECT 1 FROM Client WHERE Identification = @Identification)
    BEGIN
        -- Si existe, elimina el cliente
        DELETE FROM Client
        WHERE Identification = @Identification;

        -- Asigna el mensaje de éxito
        SET @Message = 'El cliente se eliminó correctamente.';
    END
    ELSE
    BEGIN
        -- Si no existe, devuelve un mensaje de error
        SET @Message = 'El cliente no existe.';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteProducts]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProducts]
    @CodeProduct INT,
    @Message NVARCHAR(100) OUTPUT
AS
BEGIN
    -- Verificar si el cliente existe en la tabla
    IF EXISTS (SELECT 1 FROM Product WHERE CodeProduct = @CodeProduct)
    BEGIN
        -- Si existe, elimina el cliente
        DELETE FROM Product
        WHERE CodeProduct = @CodeProduct;

        -- Asigna el mensaje de éxito
        SET @Message = 'El producto se eliminó correctamente.';
    END
    ELSE
    BEGIN
        -- Si no existe, devuelve un mensaje de error
        SET @Message = 'El producto no existe.';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllClients]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllClients]
AS
BEGIN
    SELECT IdClient, Identification, Name, Telephone, Email
    FROM Client;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducts]
AS
BEGIN
    SELECT CodeProduct, NameProduct, UnitPrice, StatusProduct
    FROM Product;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
    SELECT 
        IdUser,
        Identification,
        Username,
        DateOrigin,
        isAdmin,
        NameUser,
        Email
    FROM [Users]
END
GO
/****** Object:  StoredProcedure [dbo].[GetClientByIdentification]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetClientByIdentification]
    @Identification INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Client WHERE Identification = @Identification)
    BEGIN
        SELECT IdClient, Identification, Name, Telephone, Email
        FROM Client
        WHERE Identification = @Identification;
    END
    ELSE
    BEGIN
        RAISERROR ('Cliente no encontrado con la identificación proporcionada.', 16, 1);
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductById]
    @CodeProduct INT
AS
BEGIN
    -- Comprobar si el producto existe
    IF EXISTS (SELECT 1 FROM Product WHERE CodeProduct = @CodeProduct)
    BEGIN
        -- Si el producto existe, devolver sus datos
        SELECT CodeProduct, NameProduct, UnitPrice, StatusProduct
        FROM Product
        WHERE CodeProduct = @CodeProduct;
    END
    ELSE
    BEGIN
        -- Si el producto no existe, devolver un mensaje de error
        DECLARE @ErrorMessage NVARCHAR(100) = 'El producto con el ID especificado no existe.';
        RAISERROR (@ErrorMessage, 16, 1);
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateClient]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateClient]
    @Identification INT,
    @Name NVARCHAR(100),
    @Telephone NVARCHAR(15),
    @Email NVARCHAR(100),
    @Message NVARCHAR(100) OUTPUT
AS
BEGIN
    -- Verificar si el cliente existe en la tabla
    IF EXISTS (SELECT 1 FROM Client WHERE Identification = @Identification)
    BEGIN
        -- Si existe, se realiza el update
        UPDATE Client
        SET Name = @Name,
            Telephone = @Telephone,
            Email = @Email
        WHERE Identification = @Identification;

        -- Asigna el mensaje de éxito
        SET @Message = 'El cliente se actualizó correctamente.';
    END
    ELSE
    BEGIN
        -- Si no existe, devuelve un mensaje de error
        SET @Message = 'El cliente no existe.';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 5/11/2024 6:25:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateProduct]
    @CodeProduct INT,
    @NameProduct NVARCHAR(50),
    @UnitPrice DECIMAL(18, 0),
    @StatusProduct TINYINT,
    @Message NVARCHAR(100) OUTPUT
AS
BEGIN
    -- Verificar si el cliente existe en la tabla
    IF EXISTS (SELECT 1 FROM Product WHERE CodeProduct = @CodeProduct)
    BEGIN
        -- Si existe, se realiza el update
        UPDATE Product
        SET NameProduct = @NameProduct,
            UnitPrice = @UnitPrice,
            StatusProduct = @StatusProduct
        WHERE CodeProduct = @CodeProduct;

        -- Asigna el mensaje de éxito
        SET @Message = 'El producto se actualizó correctamente.';
    END
    ELSE
    BEGIN
        -- Si no existe, devuelve un mensaje de error
        SET @Message = 'El producto no existe.';
    END
END;
GO
USE [master]
GO
ALTER DATABASE [ApiFactura] SET  READ_WRITE 
GO
