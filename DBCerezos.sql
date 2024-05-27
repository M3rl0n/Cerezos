select * from cliente

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
		set @Mensaje = 'El correo del usuario ya existe'
end