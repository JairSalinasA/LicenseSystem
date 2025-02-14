-- Crear base de datos
CREATE DATABASE SistemaLicenciamiento
GO

USE SistemaLicenciamiento
GO

-- Tabla de Empresas
CREATE TABLE Empresas (
    EmpresaID INT PRIMARY KEY IDENTITY(1,1),
    NombreEmpresa NVARCHAR(200) NOT NULL,
    ContactoPrincipal NVARCHAR(100),
    CorreoContacto NVARCHAR(100),
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(300),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activa BIT DEFAULT 1
);

-- Tabla de Roles de Usuario
CREATE TABLE Roles (
    RolID INT PRIMARY KEY IDENTITY(1,1),
    NombreRol NVARCHAR(50) NOT NULL UNIQUE,
    Descripcion NVARCHAR(200)
);

-- Tabla de Productos 
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    NombreProducto NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(500),
    Version NVARCHAR(50),
    PrecioBase DECIMAL(10,2),
    TipoLicencia NVARCHAR(50),
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Tabla de Usuarios (Mejorada)
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    CorreoElectronico NVARCHAR(100) NOT NULL UNIQUE,
    ContrasenaHash NVARCHAR(255) NOT NULL, -- Hash bcrypt/PBKDF2
    Sal NVARCHAR(100) NOT NULL, -- Salt para hash
    RolID INT NOT NULL,
    EmpresaID INT,
    Activo BIT DEFAULT 1,
    UltimoAcceso DATETIME,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    IntentosFallidos INT DEFAULT 0,
    UltimoIntentoFallido DATETIME,
    BloqueadoHasta DATETIME,
    Autenticacion2FA BIT DEFAULT 0,
    
    FOREIGN KEY (RolID) REFERENCES Roles(RolID),
    FOREIGN KEY (EmpresaID) REFERENCES Empresas(EmpresaID)
);

-- Tabla de Autenticación en Dos Pasos (2FA)
CREATE TABLE AutenticacionDosFactores (
    AutenticacionID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT NOT NULL,
    SecretKey NVARCHAR(255) NOT NULL,
    FechaGeneracion DATETIME DEFAULT GETDATE(),
    FechaExpiracion DATETIME NOT NULL,
    Activo BIT DEFAULT 1,
    
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

-- Tabla de Licencias (Mejorada)
CREATE TABLE Licencias (
    LicenciaID INT PRIMARY KEY IDENTITY(1,1),
    EmpresaID INT NOT NULL,
    ProductoID INT NOT NULL,
    FechaInicio DATETIME NOT NULL,
    FechaVencimiento DATETIME NOT NULL,
    NumeroLicencia NVARCHAR(100) UNIQUE NOT NULL,
    ClaveActivacion NVARCHAR(255) NOT NULL, -- Nueva clave criptográfica
    CantidadUsuarios INT NOT NULL,
    CantidadDispositivosPermitidos INT DEFAULT 1,
    TipoLicencia NVARCHAR(50),
    Estado NVARCHAR(50) DEFAULT 'Activa',
    FechaRegistro DATETIME DEFAULT GETDATE(),
    
    FOREIGN KEY (EmpresaID) REFERENCES Empresas(EmpresaID),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);

-- Tabla de Dispositivos Licenciados
CREATE TABLE DispositivosLicenciados (
    DispositivoID INT PRIMARY KEY IDENTITY(1,1),
    LicenciaID INT NOT NULL,
    IdentificadorUnico NVARCHAR(255) NOT NULL,
    NombreDispositivo NVARCHAR(200),
    UltimaConexion DATETIME,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1,
    
    FOREIGN KEY (LicenciaID) REFERENCES Licencias(LicenciaID)
);

-- Tabla de Auditoría Mejorada
CREATE TABLE Auditoria (
    AuditoriaID BIGINT PRIMARY KEY IDENTITY(1,1),
    Entidad NVARCHAR(50) NOT NULL, -- Tabla afectada
    EntidadID INT NOT NULL,
    UsuarioID INT,
    TipoOperacion NVARCHAR(50) NOT NULL, -- INSERT, UPDATE, DELETE
    ValorAnterior NVARCHAR(MAX),
    ValorNuevo NVARCHAR(MAX),
    Fecha DATETIME DEFAULT GETDATE(),
    Descripcion NVARCHAR(500)
);

-- Tabla de Bitácora de Accesos (Con más detalles)
CREATE TABLE BitacoraAccesos (
    BitacoraID BIGINT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT,
    FechaAcceso DATETIME DEFAULT GETDATE(),
    DireccionIP NVARCHAR(45),
    Dispositivo NVARCHAR(100),
    UserAgent NVARCHAR(500),
    Exito BIT NOT NULL,
    TipoAcceso NVARCHAR(50), -- Login, Logout, Cambio Contraseña, etc.
    
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

-- Índices para optimización
CREATE NONCLUSTERED INDEX IX_Usuarios_CorreoElectronico 
ON Usuarios(CorreoElectronico);

CREATE NONCLUSTERED INDEX IX_Licencias_NumeroLicencia 
ON Licencias(NumeroLicencia);

CREATE NONCLUSTERED INDEX IX_BitacoraAccesos_UsuarioFecha
ON BitacoraAccesos(UsuarioID, FechaAcceso)
INCLUDE (Exito);

-- Insertar roles predeterminados
INSERT INTO Roles (NombreRol, Descripcion) VALUES 
('Administrador', 'Acceso total al sistema'),
('Supervisor', 'Acceso a funciones administrativas limitadas'),
('Usuario', 'Acceso básico'),
('Cliente', 'Acceso restringido para clientes');
GO

-- Procedimiento para Registro Seguro de Usuarios
CREATE PROCEDURE sp_RegistrarUsuario
    @NombreUsuario NVARCHAR(50),
    @CorreoElectronico NVARCHAR(100),
    @ContrasenaHash NVARCHAR(255),
    @Sal NVARCHAR(100),
    @RolID INT,
    @EmpresaID INT = NULL,
    @Autenticacion2FA BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validaciones previas
        IF EXISTS (SELECT 1 FROM Usuarios WHERE NombreUsuario = @NombreUsuario)
        BEGIN
            RAISERROR('El nombre de usuario ya existe.', 16, 1);
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Usuarios WHERE CorreoElectronico = @CorreoElectronico)
        BEGIN
            RAISERROR('El correo electrónico ya está registrado.', 16, 1);
            RETURN;
        END

        -- Inserción de usuario
        DECLARE @UsuarioID INT;
        INSERT INTO Usuarios (
            NombreUsuario, 
            CorreoElectronico, 
            ContrasenaHash, 
            Sal,
            RolID, 
            EmpresaID,
            Autenticacion2FA
        ) VALUES (
            @NombreUsuario,
            @CorreoElectronico,
            @ContrasenaHash,
            @Sal,
            @RolID,
            @EmpresaID,
            @Autenticacion2FA
        );

        SET @UsuarioID = SCOPE_IDENTITY();

        -- Registro en auditoría
        INSERT INTO Auditoria (
            Entidad, 
            EntidadID, 
            TipoOperacion, 
            ValorNuevo, 
            Descripcion
        ) VALUES (
            'Usuarios',
            @UsuarioID,
            'INSERT',
            @NombreUsuario,


            -+Registro de nuevo usuario'
        );

        COMMIT TRANSACTION;
        SELECT @UsuarioID AS UsuarioID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        THROW;
    END CATCH
END
GO