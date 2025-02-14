SELECT UsuarioID, NombreUsuario, CorreoElectronico, ContrasenaHash, Sal, RolID, EmpresaID, Activo FROM Usuarios WHERE NombreUsuario = 'admin'
-- Insertar datos iniciales en la tabla Empresas
INSERT INTO Empresas (NombreEmpresa, ContactoPrincipal, CorreoContacto, Telefono, Direccion, Activa)
VALUES
    ('Tech Solutions', 'Juan Perez', 'contacto@techsolutions.com', '555-1234', 'Av. Principal 123, Ciudad', 1),
    ('SoftCorp', 'Maria Gomez', 'maria@softcorp.com', '555-5678', 'Calle Secundaria 45, Ciudad', 1);

-- Insertar datos iniciales en la tabla Roles
INSERT INTO Roles (NombreRol, Descripcion)
SELECT 'Administrador', 'Tiene acceso total al sistema' WHERE NOT EXISTS (SELECT 1 FROM Roles WHERE NombreRol = 'Administrador');
INSERT INTO Roles (NombreRol, Descripcion)
SELECT 'Usuario', 'Tiene acceso limitado' WHERE NOT EXISTS (SELECT 1 FROM Roles WHERE NombreRol = 'Usuario');

-- Insertar datos iniciales en la tabla Productos
INSERT INTO Productos (NombreProducto, Descripcion, Version, PrecioBase, TipoLicencia)
VALUES
    ('Software X', 'Solución integral para gestión empresarial', '1.0', 299.99, 'Permanente'),
    ('App Y', 'Aplicación móvil para control de inventarios', '2.3', 99.99, 'Suscripción');

-- Insertar datos iniciales en la tabla Usuarios
INSERT INTO Usuarios (NombreUsuario, CorreoElectronico, ContrasenaHash, Sal, RolID)
VALUES
    ('admin', 'admin@sistema.com', 'hashed_password', 'random_salt', 1),
    ('user1', 'user1@sistema.com', 'hashed_password', 'random_salt', 2);


	UPDATE Usuarios
SET ContrasenaHash = '$2a$11$OFBqGb/m.ID/3PFkvAjQ7.IrHkSSa5SAU.Bfb0jgnQjyoyJ4L9U2C' 
WHERE UsuarioID = 2;


select *from Usuarios