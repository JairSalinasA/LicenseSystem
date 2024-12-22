-- Crear Base de Datos
CREATE DATABASE LicenseSystem;
GO

USE LicenseSystem;
GO

-- Tabla de Clientes
CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CompanyName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);

-- Tabla de Productos
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    ProductCode NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(500),
    Version NVARCHAR(20) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);

-- Tabla de Tipos de Licencia
CREATE TABLE LicenseTypes (
    LicenseTypeId INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    DurationDays INT NOT NULL,
    MaxUsers INT,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Tabla de Licencias
CREATE TABLE Licenses (
    LicenseId INT IDENTITY(1,1) PRIMARY KEY,
    LicenseKey NVARCHAR(100) NOT NULL UNIQUE,
    CustomerId INT NOT NULL,
    ProductId INT NOT NULL,
    LicenseTypeId INT NOT NULL,
    IssueDate DATETIME NOT NULL DEFAULT GETDATE(),
    ExpirationDate DATETIME NOT NULL,
    MaxActivations INT NOT NULL DEFAULT 1,
    CurrentActivations INT NOT NULL DEFAULT 0,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Active', 'Expired', 'Suspended', 'Revoked')),
    Notes NVARCHAR(500),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Licenses_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    CONSTRAINT FK_Licenses_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT FK_Licenses_LicenseTypes FOREIGN KEY (LicenseTypeId) REFERENCES LicenseTypes(LicenseTypeId)
);

-- Tabla de Activaciones de Licencia
CREATE TABLE LicenseActivations (
    ActivationId INT IDENTITY(1,1) PRIMARY KEY,
    LicenseId INT NOT NULL,
    MachineId NVARCHAR(100) NOT NULL,
    ActivationDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastCheckDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_LicenseActivations_Licenses FOREIGN KEY (LicenseId) REFERENCES Licenses(LicenseId)
);

-- Tabla de Historial de Licencias
CREATE TABLE LicenseHistory (
    HistoryId INT IDENTITY(1,1) PRIMARY KEY,
    LicenseId INT NOT NULL,
    Action NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    ChangedBy NVARCHAR(100),
    ChangedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_LicenseHistory_Licenses FOREIGN KEY (LicenseId) REFERENCES Licenses(LicenseId)
);

-- Índices
CREATE INDEX IX_Licenses_CustomerId ON Licenses(CustomerId);
CREATE INDEX IX_Licenses_ProductId ON Licenses(ProductId);
CREATE INDEX IX_Licenses_Status ON Licenses(Status);
CREATE INDEX IX_LicenseActivations_LicenseId ON LicenseActivations(LicenseId);
CREATE INDEX IX_LicenseHistory_LicenseId ON LicenseHistory(LicenseId);

-- Stored Procedure para Crear Nueva Licencia
GO
CREATE PROCEDURE sp_CreateLicense
    @CustomerId INT,
    @ProductId INT,
    @LicenseTypeId INT,
    @LicenseKey NVARCHAR(100),
    @ExpirationDate DATETIME,
    @MaxActivations INT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Licenses (
        CustomerId, 
        ProductId,
        LicenseTypeId,
        LicenseKey,
        ExpirationDate,
        MaxActivations,
        Status
    )
    VALUES (
        @CustomerId,
        @ProductId,
        @LicenseTypeId,
        @LicenseKey,
        @ExpirationDate,
        @MaxActivations,
        'Active'
    );
    
    DECLARE @LicenseId INT = SCOPE_IDENTITY();
    
    INSERT INTO LicenseHistory (
        LicenseId,
        Action,
        Description
    )
    VALUES (
        @LicenseId,
        'Created',
        'License created successfully'
    );
    
    SELECT @LicenseId AS NewLicenseId;
END;

-- Stored Procedure para Validar Licencia
GO
CREATE PROCEDURE sp_ValidateLicense
    @LicenseKey NVARCHAR(100),
    @MachineId NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @LicenseId INT;
    DECLARE @Status NVARCHAR(20);
    DECLARE @CurrentActivations INT;
    DECLARE @MaxActivations INT;
    DECLARE @ExpirationDate DATETIME;
    
    SELECT 
        @LicenseId = LicenseId,
        @Status = Status,
        @CurrentActivations = CurrentActivations,
        @MaxActivations = MaxActivations,
        @ExpirationDate = ExpirationDate
    FROM Licenses
    WHERE LicenseKey = @LicenseKey;
    
    -- Validar si la licencia existe y está activa
    IF @LicenseId IS NULL OR @Status != 'Active'
        RETURN -1; -- Licencia no válida
        
    -- Validar fecha de expiración
    IF @ExpirationDate < GETDATE()
    BEGIN
        UPDATE Licenses SET Status = 'Expired' WHERE LicenseId = @LicenseId;
        RETURN -2; -- Licencia expirada
    END
    
    -- Validar activaciones
    IF NOT EXISTS (SELECT 1 FROM LicenseActivations WHERE LicenseId = @LicenseId AND MachineId = @MachineId)
    BEGIN
        IF @CurrentActivations >= @MaxActivations
            RETURN -3; -- Máximo de activaciones alcanzado
            
        -- Registrar nueva activación
        INSERT INTO LicenseActivations (LicenseId, MachineId)
        VALUES (@LicenseId, @MachineId);
        
        UPDATE Licenses 
        SET CurrentActivations = CurrentActivations + 1
        WHERE LicenseId = @LicenseId;
    END
    ELSE
    BEGIN
        -- Actualizar fecha de último chequeo
        UPDATE LicenseActivations
        SET LastCheckDate = GETDATE()
        WHERE LicenseId = @LicenseId AND MachineId = @MachineId;
    END
    
    RETURN 1; -- Licencia válida
END;