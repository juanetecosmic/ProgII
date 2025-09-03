create database Problema1_5
go
use Problema1_5
go

create table articulos
(id_articulo int identity not null,
descripcion varchar(50) not null,
stock int,
precio money,
activo bit,
constraint pk_articulos primary key (id_articulo));

create table formas_pago
(id_forma_pago int identity not null,
forma_pago varchar(50) not null,
constraint pk_formas_pago primary key (id_forma_pago));

create table barrios
(id_barrio int identity not null,
barrio varchar(50),
constraint pk_barrios primary key (id_barrio));

create table clientes
(id_cliente int identity not null,
nombre varchar(50) not null,
apellido varchar(50) not null,
calle varchar(50),
altura int,
id_barrio int,
telefono varchar(15),
constraint pk_clientes primary key (id_cliente),
constraint fk_clientes_barrios foreign key (id_barrio)
references barrios(id_barrio));

create table vendedores
(id_vendedor int identity not null,
nombre varchar(50) not null,
apellido varchar(50) not null,
calle varchar(50),
altura int,
id_barrio int,
telefono varchar(15),
--activo bit, (no es útil para nuestros fines)
constraint pk_vendedores primary key (id_vendedor),
constraint fk_vendedores_barrios foreign key (id_barrio)
references barrios(id_barrio));

create table facturas
(id_factura int identity not null,
id_cliente int,
id_vendedor int,
fecha datetime,
id_forma_pago int,
constraint pk_facturas primary key (id_factura),
constraint fk_facturas_clientes foreign key (id_cliente)
references clientes(id_cliente),
constraint fk_facturas_empleados foreign key (id_vendedor)
references vendedores(id_vendedor),
constraint fk_facturas_formas_pago foreign key (id_forma_pago)
references formas_pago(id_forma_pago));

create table detalles_facturas
(id_detalle int identity not null,
id_factura int,
id_articulo int,
cantidad int,
importe int,
constraint pk_detalles primary key (id_detalle),
constraint fk_detalles_facturas foreign key (id_factura)
references facturas(id_factura),
constraint fk_detalles_articulos foreign key (id_articulo)
references articulos(id_articulo));

--alta de una forma de pago, un barrio, un cliente y un vendedor para hacer las transacciones

insert into formas_pago(forma_pago)
values('Crédito'),('Débito'),('Efectivo')

insert into barrios(barrio)
values('San Martin'),('Centro'),('Nueva Córdoba')

insert into clientes(nombre,apellido,calle,altura,id_barrio,telefono)
values('Juan Manuel','Torrejón','Av. Canadá',140,1,'3884567237')

insert into vendedores(nombre,apellido,calle,altura,id_barrio,telefono)
values('Juanito','Laguna','Yerba Verdeflor',500,2,'3884123456')

GO
--Procedimiento para guardar articulo (sirve para actualizar y para crear articulos)
CREATE PROCEDURE SP_GUARDAR_ARTICULO
@codigo int ,
@descripcion varchar(50),
@stock int,
@precio money
AS
BEGIN 
	IF @codigo = 0
		BEGIN
			insert into articulos(descripcion, stock, precio, activo) 
			values (@descripcion,@stock,@precio,1)	
		END
	ELSE
		BEGIN
			update articulos 
			set descripcion= @descripcion, stock= @stock, precio = @precio
			where id_articulo=@codigo
		END
END
GO
--Procedimiento para llevar un articulo buscándolo por su ID
CREATE PROCEDURE SP_CONSULTAR_ARTICULO_POR_ID
@codigo int
AS
BEGIN
	select * from articulos where id_articulo=@codigo
END
GO
--Procedimiento para llevar una lista de artículos
CREATE PROCEDURE SP_CONSULTAR_ARTICULOS
AS
BEGIN
	select * from articulos
END
GO
--Procedimiento para dar de baja un artículo
create PROCEDURE SP_BAJA_ARTICULO 
	@codigo int 
AS
BEGIN
	UPDATE articulos SET activo = 0 WHERE id_articulo = @codigo;
END

GO
--Procedimiento para insertar una factura
create PROCEDURE SP_INSERTAR_CABECERA
	@id int output,
	@cliente int,
	@vendedor int,
	@fecha datetime,
	@formapago int	
AS
BEGIN
	INSERT INTO Facturas(id_cliente, id_vendedor, fecha, id_forma_pago)
	VALUES (@cliente,@vendedor, @fecha, @formapago);
	SET @id = SCOPE_IDENTITY();
END

GO
--Procedimiento para insertar detalles
create PROCEDURE SP_INSERTAR_DETALLE
	@factura int,
	@articulo int,
	@cantidad int,
	@importe money
as
begin
	insert into detalles_facturas(id_factura, id_articulo,cantidad,importe)
	values (@factura,@articulo,@cantidad,@importe)
end