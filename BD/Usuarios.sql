USE bd_Punto_de_Venta
go
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] IDENTITY(1,1),
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos][varchar](50) NOT NULL,
	[Login] [varchar](50) NULL, --Cambair a USUARIO
	[Password] [varchar](50) NOT NULL,
	[Icono] [image] NULL,
	[Nombre_de_icono] [varchar](max) NULL,
	[Correo] [varchar](max) NOT NULL,
	[Rol] [varchar](20) NOT NULL,
	[Estado][varchar](50) NOT NULL,
 CONSTRAINT [pk_usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE PROC insertar_usuario
@nombres varchar(50),
@apellidos varchar(50),
@Login varchar(50),
@Password VARCHAR(50),
@Icono image,
@Nombre_de_icono varchar(max),
@Correo varchar(max),
@Rol varchar(20),
@Estado varchar(50)
AS
	IF EXISTS (SELECT Login FROM Usuario where Login=@Login and Estado='ACTIVO')
		RAISERROR('El nombre de usuario que desea ingresar, ya existe existe \nPor favor ingrese uno nuevo',16,1 )--Ubicacion de la pantalla
	ELSE
		INSERT INTO Usuario 
		VALUES(@nombres,@apellidos,@Login,@Password,@Icono,@Nombre_de_icono,@Correo,@Rol, @Estado  )

GO

CREATE PROC [dbo].[modificar_usuario]
@idUsuario int,
@nombres varchar(50),
@apellidos varchar(50),
@Login varchar(50),
@Password VARCHAR(50),
@Icono image,
@Nombre_de_icono varchar(max),
@Correo varchar(max),
@Rol varchar(max)
AS
	IF EXISTS (SELECT Login,idUsuario FROM Usuario
	WHERE (Login  = @login  AND idUsuario<>@idUsuario AND Estado='ACTIVO') 
	OR (Nombres = @nombres AND Apellidos= @Apellidos AND idUsuario<>@idUsuario AND Estado='ACTIVO'))
		RAISERROR('Ya existe un usuario con esos datos, por favor inténtelo de nuevo',16,1 )
	ELSE
		UPDATE Usuario 
		SET Nombres=@nombres, Apellidos=@apellidos, Password=@Password, 
			Icono=@Icono ,Nombre_de_icono=@Nombre_de_icono,Correo=@Correo, Rol=@rol, Login=@Login
		WHERE idUsuario=@idUsuario 
GO

CREATE PROC [dbo].[mostrar_usuario]
AS
SELECT idUsuario,Nombres, Apellidos,Login AS Usuario,Password,Icono, Nombre_de_icono, Correo,
Rol FROM Usuario
WHERE Estado='ACTIVO'
GO

CREATE PROC [dbo].[eliminar_usuario]
@idusuario int,
@login varchar(50)
AS
	IF EXISTS (SELECT login FROM Usuario where @login ='admin')
		RAISERROR('El Usuario *Admin* NO puede ser eliminado, Accion Denegada', 16, 1)
	ELSE
		UPDATE Usuario 
		SET Estado='ELIMINADO'
		WHERE idUsuario=@idusuario  AND Login <> 'admin'
 
GO

CREATE PROC [dbo].[buscar_usuario] --por nombre, apellidos y login
@letra varchar(50)
AS
	SELECT idUsuario,Nombres, Apellidos,Login AS Usuario,Password,
	Icono, Nombre_de_icono, Correo,Rol FROM Usuario
	WHERE Nombres + Apellidos + Login LIKE '%'+ @letra +'%' AND Estado='ACTIVO'
GO

SELECT * FROM Usuario WHERE Estado='ACTIVO'
-----------------------------

CREATE PROC [dbo].[validar_usuario]
@password varchar(50),
@login varchar(50)
AS
	SELECT * FROM Usuario
	WHERE  Password = @password AND Login=@Login and Estado ='ACTIVO'
GO

CREATE PROC [dbo].[	]
@correo VARCHAR(MAX)
AS 
	SELECT Password FROM Usuario						 
	WHERE Correo=@correo
GO