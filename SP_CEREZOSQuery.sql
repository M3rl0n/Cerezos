--PROCEDIMIENTOS ALMACENADOS
Select * from Usuario

--REGISTRAR USUARIO
create proc sp_RegistrarUsuario(
@Nombre varchar (100),
@Apellido varchar (100),
@Email varchar (100),
@Clave varchar (150),
@Activo bit,
@IDRol varchar(100),
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuario WHERE Email = @Email)
	begin
		insert into Usuario(Nombre,Apellido,Email,Clave,Activo,IDRol) values
		(@Nombre,@Apellido,@Email,@Clave,@Activo, @IDRol)

		SET @Resultado = SCOPE_IDENTITY()
		end
		else
		set @Mensaje = 'El correo del usuario ya existe'
end
---EDITAR USUARIO-----
create proc sp_EditarUsuario(
@IDUsuario int,
@Nombre varchar (100),
@Apellido varchar (100),
@Email varchar (100),
@Activo bit,
@IDRol varchar(100),
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuario WHERE Nombre = @Nombre and IDUsuario != @IDUsuario)
	begin
		update top(1) Usuario set
		Nombre = @Nombre,
		Apellido = @Apellido,
		Email = @Email,
		Activo = @Activo,
		IDRol = @IDRol
		where IDUsuario = @IDUsuario
		
		SET @Resultado = 1
	end
	else
	 set @Mensaje = 'El correo del usuario ya existe'
end

--REGISTRAR CATEGORIA PROD
SELECT * FROM Categoria_Productos

create proc sp_RegistrarCategoriaProd(
@Nombre varchar (100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Categoria_Productos WHERE Nombre = @Nombre)
	begin
		insert into Categoria_Productos(Nombre,Activo) values
		(@Nombre,@Activo)

		SET @Resultado = SCOPE_IDENTITY()
		end
		else
		set @Mensaje = 'La categoria de producto ya existe'
end

--ACTUALIZAR CATEGORIA PROD
create proc sp_EditarCategoriaProd(
@IDCategoria int,
@Nombre varchar (100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Categoria_Productos WHERE Nombre = @Nombre and IDCategoria != @IDCategoria)
	begin
		update top(1) Categoria_Productos set
		Nombre = @Nombre,
		Activo = @Activo
		where IDCategoria = @IDCategoria
		SET @Resultado = 1
		end
		else
		set @Mensaje = 'La categoria de producto ya existe'
end

--ELIMINAR CATEGORIA PROD
create proc sp_EliminarCategoriaProd(
@IDCategoria int,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Productos p
	inner join Categoria_Productos cp on cp.IDCategoria = p.IDCategoria
	where p.IDCategoria = @IDCategoria)
	begin
		delete top(1) from Categoria_Productos where IDCategoria = @IDCategoria
		set @Resultado = 1
	end
	else
		set @Mensaje = 'La categoria de producto ya existe'
end

--REGISTRAR PRODUCTO
SELECT * FROM Productos

create proc sp_RegistrarProducto(
@Nombre varchar (100),
@Descripcion varchar (100),
@Precio money,
@Stock int,
@Activo bit,
@IDCategoria int,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Productos WHERE Nombre = @Nombre)
	begin
		insert into Productos(Nombre, Descripcion, Precio, Stock, Activo, IDCategoria) values
		(@Nombre, @Descripcion, @Precio, @Stock, @Activo, @IDCategoria)

		SET @Resultado = SCOPE_IDENTITY()
		end
		else
		set @Mensaje = 'El producto ya existe'
end

--ACTUALIZAR PRODUCTO
create proc sp_EditarProducto(
@IDProducto int,
@Nombre varchar (100),
@Descripcion varchar (100),
@Precio money,
@Stock int,
@Activo bit,
@IDCategoria int,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Productos WHERE Nombre = @Nombre and IDProducto != @IDProducto)
	begin
		update Productos set
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		Precio = @Precio,
		Stock = @Stock,
		Activo = @Activo,
		IDCategoria = @IDCategoria
		where IDProducto = @IDProducto
		SET @Resultado = 1
		end
		else
		set @Mensaje = 'El producto ya existe'
end

--ELIMINAR PRODUCTO
create proc sp_EliminarProducto(
@IDProducto int,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Detalle_Venta dv
	inner join Productos p on p.IDProducto = dv.IDProducto
	where p.IDProducto = @IDProducto)
	begin
		delete top(1) from Productos where IDProducto = @IDProducto
		set @Resultado = 1
	end
	else
		set @Mensaje = 'El producto se encuentra relacionado a una venta'
end


Select * from Cliente

create proc sp_RegistrarCliente(
@Nombre varchar(100),
@Apellido varchar(100),
@Email varchar(100),
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	set @Resultado = 0
	if not exists (select * from cliente where email = @Email)
	begin
		insert into cliente (Nombre, Apellido, email) values
		(@Nombre,@Apellido,@Email)

		set @Resultado = scope_identity()
	end
	else
		set @Mensaje = 'El correo del usuario ya existe'
end