--CALCULOS NECESARIOS ETIQUETAS DASHBOAR

--ETIQUETAS DEL DASHBOARD
Create proc sp_ReporteDashboard
as
begin

Select

(select count(*) from Cliente)[TotalClientes],
(Select COUNT(*) from Solicitud)[TotalSolicitudes],
(select COUNT(*) from Productos)[TotalProductos],
(select ISNULL (sum(Cantidad),0) from Detalle_Venta)[TotalVenta]

end

exec sp_ReporteDashboard

create proc sp_ReporteVentas(
@fechainicio varchar(10),
@fechafin varchar(10),
@idtransaccion varchar(50)
)
as
begin

set dateformat dmy;

select CONVERT(char(10), v.FechaVenta,103)[FechaVenta], CONCAT(c.Nombre,' ',c.Apellido)[Cliente],
p.Nombre[Producto], p.Precio, dv.Cantidad, dv.Total, v.IDTransaccion
from Detalle_Venta dv
inner join Productos p on p.IDProducto = dv.IDProducto
inner join Venta v on v.IDVenta = dv.IDVenta
inner join Cliente c on c.IDCliente = v.IDCliente
where CONVERT(date, v.FechaVenta)  between @fechainicio and @fechafin
and v.IDTransaccion = iif(@idtransaccion = '', v.IDTransaccion, @idtransaccion)



end



