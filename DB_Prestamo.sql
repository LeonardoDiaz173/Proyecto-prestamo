select * from usuario;

update usuario set estado = 0;

create table cliente(
id int primary key,
nombre	varchar(30) not null,
telefono	int,
DNI	int,
representante varchar(30),
zona int,
cobrador int,
agente int,
balance decimal(8,2),
trabajo	varchar(30),
direccion int,
fecha_creacion datetime,
fecha_actualizacion datetime);

select * from cliente;

create table zona(
id int primary key,
nombre varchar(25));

create table agentes (
id int primary key,
nombre_age	varchar(30),
telefono_age int,
documento_identidad int,
zona_age int,
provincia int,
fecha_creacion datetime,
fecha_modificacion datetime
);

create table cobradores(
id	int primary key,
nombre varchar(30),
direccion int,
telefono	int,
DNI	varchar(15),
zona int,
provincia int);

create table telefono(
id	int primary key,
numero varchar(15),
estado bit);

create table direccion(
id int primary key,
id_provincia int,
id_sector int,
calle varchar(20),
numero varchar(5),
piso_apartamento varchar(5),
referencia_adicional varchar(40)
);

create table sector(
id	int primary key,
nombre	varchar(30)
);

create table empleado(
id	int primary key,
nombre varchar(30),
sexo varchar(5),
tipo_empleado int,
Direccion int,
Telefono int);

create table tipo_empleado(
id	int primary key,
nombre	varchar(40),
estado bit);


--AddOwnedForm
--Investigar la funcion

create table provincia(
id int primary key,
nombre varchar(30));

SELECT a.id, f.numero, a.nombre, a.DNI, a.representante, g.nombre as zona, a.balance, a.trabajo, 
b.sector + ' ' +b.calle+' '+b.numero 
FROM cliente a
LEFT JOIN direccion b ON a.direccion = b.id
LEFT JOIN telefono f ON a.telefono = f.id
LEFT JOIN zona g ON a.zona = g.id
LEFT JOIN provincia h ON b.id_provincia = h.id;

select a.id from telefono a LEFT JOIN cliente b ON a.id = b.telefono where a.numero = '';

alter table zona add provincia int;

alter table empleado add estado bit not null;

delete from provincia;

Select * from provincia;

update zona set provincia = 3;

select a.nombre from zona a LEFT JOIN provincia b ON a.provincia = b.id where a.estado = 0 and b.nombre = 'santiago';

select id from provincia where nombre = 'Santiago'

alter table cliente alter column DNI varchar(30);

select top 1 a.id from telefono a LEFT JOIN cliente b ON a.id = b.telefono where a.numero = '123' and a.estado = 0

select * from cliente;

alter table cliente alter column representante int;

select a.id from zona a LEFT JOIN provincia b ON a.provincia = b.id where a.estado = 0 and b.nombre = 'santiago';
 
select id from provincia where nombre = 'santiago' and estado = 0;

select top 1 (id) from provincia order by id desc;

alter table representante alter column id int not null;

alter table representante add primary key (id)  ;

create table cliente_vs_representante(
cliente int,
representante int);

ALTER TABLE cliente_vs_representante
ADD CONSTRAINT fk_representante_cliente FOREIGN KEY (representante) REFERENCES representante (id);


alter table cliente drop column representante;

select top 1 * from cliente order by id desc;

INSERT INTO cliente (id, nombre, DNI, trabajo, sexo)
VALUES
(1, 'Juan Pérez', '12345678A', 'Desarrollador', 'm'),
(2, 'María García', '98765432B', 'Diseñadora', 'm');

select * from Representante;
select * from direccion_vs_cliente;

select * from zona where estado = 0;

select zona.id, zona.nombre, provincia.nombre from zona LEFT JOIN provincia ON zona.provincia = provincia.id where zona.estado = 0

select id from provincia where nombre = 'santiago' and estado = 0;

select id from provincia where nombre = 'Santiago and estado = 0'

select 
a.nombre, a.DNI, d.numero, a.Sexo 'sexo_cliente', a.trabajo, e.nombre 'provincia', f.nombre 'zona',
b.sector, b.calle, b.numero , b.referencia_adicional,
c.Nombre 'representante', c.Sexo 'sexo_representante', c.DNI 'dni_representante', c.telefono
FROM cliente a
LEFT JOIN direccion_vs_cliente ab ON ab.cliente = a.id
LEFT join direccion b ON ab.direccion = b.id
LEFT JOIN cliente_vs_representante ac ON ac.cliente = a.id
LEFT JOIN Representante c ON ac.representante = c.id
LEFT JOIN cliente_vs_telefono ad ON ad.cliente = a.id
LEFT JOIN telefono d ON ad.telefono = d.id
LEFT JOIN provincia e ON b.id_provincia = e.id
LEFT JOIN zona f ON a.zona = f.id;


create table cliente_vs_telefono(
cliente int,
telefono int);

ALTER TABLE cliente_vs_telefono
ADD CONSTRAINT fk_cliente_telefono FOREIGN KEY (cliente) REFERENCES cliente (id);

select a.nombre, a.DNI, d.numero, a.Sexo 'sexo_cliente', a.trabajo, e.nombre 'provincia', f.nombre 'zona', b.sector, b.calle, b.numero, b.referencia_adicional, c.Nombre 'representante', c.Sexo 'sexo_representante', c.DNI 'dni_representante', c.telefono FROM cliente a 
LEFT JOIN direccion_vs_cliente ab ON ab.cliente = a.id LEFT join direccion b ON ab.direccion = b.id LEFT JOIN cliente_vs_representante ac ON ac.cliente = a.id LEFT JOIN Representante c ON ac.representante = c.id LEFT JOIN cliente_vs_telefono ad ON ad.cliente = a.id LEFT JOIN telefono d ON ad.telefono = d.id LEFT JOIN provincia e ON b.id_provincia = e.id LEFT JOIN zona f ON a.zona = f.id

select * from cliente

select * from telefono;

select a.id, a.nombre, a.DNI, c.numero, a.trabajo, 
e.nombre, f.nombre, b.sector, b.calle, b.numero,
d.Nombre, d.DNI, d.Sexo, d.telefono from cliente a
LEFT JOIN direccion_vs_cliente ab ON ab.cliente = a.id
LEFT JOIN direccion b ON ab.direccion =  b.id
LEFT JOIN cliente_vs_telefono ac ON ac.cliente = a.id
LEFT JOIN telefono c ON  ac.telefono = c.id
LEFT JOIN cliente_vs_representante ad ON ad.cliente = a.id
LEFT JOIN Representante d ON ad.representante = d.id
LEFT JOIN provincia e ON b.id_provincia = e.id 
LEFT JOIN zona f ON a.zona = f.id
WHERE a.estado = 0 and (a.nombre like '%l%')




alter table cliente drop column fecha_actualizacion;

update cliente set estado = 0;

select top 1 id from cliente order by id desc

delete from cliente where estado = 1;

select * from direccion;

select b.id
From cliente a
LEFT JOIN cliente_vs_representante ab on ab.cliente = a.id
LEFT JOIN Representante b on ab.representante = b.id
where a.id = 3;

select id from provincia where nombre = 'santiago'

select b.id from cliente a LEFT JOIN direccion_vs_cliente ab on ab.cliente = a.id LEFT JOIN direccion b on ab.direccion = b.id where a.id = 3;

select * from cliente where id = 3;

select top 1 id from zona where nombre = 'villa olimpica' and estado = 0 order by id asc

select telefono from cliente_vs_telefono where cliente = '3';

insert into cliente_vs_telefono values (4, 16)

select * from cliente_vs_telefono;

delete from cliente_vs_telefono where cliente in (select top 1 cliente from cliente_vs_telefono where cliente = 4)

select * from telefono;

select * from cliente;


select telefono from cliente_vs_telefono where cliente = '3'

update telefono set numero = '809-909-9090' where id = '15' and estado = 0

select telefono from cliente_vs_telefono where cliente = '3'

select telefono from cliente_vs_telefono where cliente = '3'

select * from usuario;

select 
	a.id, 
	a.nombre, 
	a.DNI, 
	c.numero 'telefono', 
	a.trabajo, 
	e.nombre 'provincia', 
	f.nombre 'zona', 
	Concat(b.sector ,' ' , b.calle ,' '  , b.numero, ' ' ,b.referencia_adicional) as 'direccion' ,
	d.Nombre 'representante' , 
	d.DNI 'DNIRepresentante', 
	d.Sexo 'SexoRepresentante', 
	d.telefono 'TelefonoRepresentante'
	
	from cliente a 
LEFT JOIN direccion_vs_cliente ab ON ab.cliente = a.id 
LEFT JOIN direccion b ON ab.direccion =  b.id 
LEFT JOIN cliente_vs_telefono ac ON ac.cliente = a.id 
LEFT JOIN telefono c ON  ac.telefono = c.id 
LEFT JOIN cliente_vs_representante ad ON ad.cliente = a.id 
LEFT JOIN Representante d ON ad.representante = d.id 
LEFT JOIN provincia e ON b.id_provincia = e.id 
LEFT JOIN zona f ON a.zona = f.id WHERE a.estado = 0;

select * from usuario;


delete from usuario where id > 4;


create table empleado_vs_telefono(
empleado int,
telefono int);

create table empleado_vs_direccion(
empleado int,
direccion int);

select * from empleado

alter table empleado add estado bit;

select 	
a.id, 
a.nombre,  
a.sexo, 
a.dni, 
b.numero 'telefono', 
CONCAT(d.nombre, ' ', c.sector, ' ', c.calle, ' ', c.numero, ' ', c.referencia_adicional) as 'direccion' 
from empleado a 
FULL JOIN empleado_vs_telefono ab ON ab.empleado = a.id 
FULL JOIN telefono b ON ab.telefono = b.id 
FULL JOIN empleado_vs_direccion ac ON ac.empleado = a.id 
FULL JOIN direccion c ON ac.direccion = c.id
FULL JOIN provincia d ON c.id_provincia = d.id;

select * from empleado
select * from provincia

select * from direccion

delete from provincia where id = 1

select * from 
alter table empleado drop column ;

select top 1 id from telefono order by id desc

SELECT * FROM empleado_vs_telefono

delete from empleado

select a.id, a.nombre, a.sexo, a.dni, b.numero 'telefono', d.nombre, c.sector, c.calle, c.numero, c.referencia_adicional from empleado a LEFT JOIN empleado_vs_telefono ab ON ab.empleado = a.id LEFT JOIN telefono b ON ab.telefono = b.id LEFT JOIN empleado_vs_direccion ac ON ac.empleado = a.id LEFT JOIN direccion c ON ac.direccion = c.id LEFT JOIN provincia d ON c.id_provincia = d.id;

select * from cliente;

select id, nombre, dni, trabajo 
from cliente 
where estado = 0 and (id like '%%' or nombre like 'l%' or dni like '%%' or trabajo like '%%');

select 
a.id, a.nombre, a.sexo, a.DNI, b.numero 'Telefono', 
concat(e.nombre, c.sector, c.calle, c.numero, c.referencia_adicional) 'Direccion',
d.nombre 'tipo empleado'
from empleado a
LEFT JOIN empleado_vs_telefono ab ON ab.empleado = a.id
LEFT JOIN telefono b ON ab.telefono = b.id
LEFT JOIN empleado_vs_direccion ac ON ac.empleado = a.id
LEFT JOIN direccion c ON ac.direccion = c.id
LEFT JOIN tipo_empleado d ON a.tipo_empleado = d.id
LEFT JOIN provincia e ON c.id_provincia = e.id

CREATE TABLE prestamos (
  id int primary key NOT NULL,
  cod_cli_pre int  DEFAULT NULL,
  cobrador_cob_pre int  DEFAULT NULL,
  agente_age_pre  int DEFAULT NULL,
  garante_pre varchar(25)  DEFAULT NULL,
  zona_zon_pre int DEFAULT NULL,
  detalle_pre varchar(25)  DEFAULT NULL,
  moneda_pre varchar(255)  DEFAULT NULL,
  fecha_pre varchar(255)  DEFAULT NULL,
  tipo_pre varchar(255)  DEFAULT NULL,
  cuotas_pagada_pre varchar(255)  DEFAULT NULL,
  balance_pre varchar(255)  DEFAULT NULL,
  monto_pre varchar(255)  DEFAULT NULL,
  interes_pre varchar(255)  DEFAULT NULL,
  cuotas_pre varchar(255)  DEFAULT NULL,
  dia_pre varchar(255)  DEFAULT NULL,
  dias_cuota_pre varchar(255)  DEFAULT NULL,
  estado_pre varchar(255)  DEFAULT NULL,
) 

drop table prestamos

alter table prestamos alter column agente_age_pre int

drop table agentes

select * from prestamos

create table cobradores(
id int primary key, 
zona int,
provincia int,
empleado int,
estado bit
);

select 
a.id, 
d.nombre 'empleado',
b.nombre 'Zona',
c.nombre 'Provincia'
from cobradores a
LEFT JOIN zona b ON a.zona = b.id
LEFT JOIN provincia c ON a.provincia = c.id
LEFT JOIN empleado d ON a.empleado = d.id

select * from zona;

select 
a.id,
b.nombre,
e.numero,
CONCAT(c.nombre, ', ', d.nombre) as 'Zona'
from cobradores a
LEFT JOIN empleado b ON a.empleado = b.id
LEFT JOIN provincia c ON a.provincia = c.id
LEFT JOIN zona d ON a.zona = d.id 
LEFT JOIN empleado_vs_telefono be ON be.empleado = b.id
LEFT JOIN telefono e ON be.telefono = e.id;

select * from cliente;


alter table prestamos drop column gastos_ley_pre

select 
a.id, b.nombre, d2.nombre 'agente', d.nombre 'cobrador', a.detalle_pre, a.moneda_pre, a.fecha_pre, a.tipo_pre, a.cuotas_pagada_pre,
a.balance_pre, a.monto_pre, a.interes_pre, a.cuotas_pre, a.dia_pre, a.dias_cuota_pre, a.estado_pre
from prestamos a 
Left join cliente b ON a.cod_cli_pre = b.id
LEFT join cobradores c ON a.cobrador_cob_pre = c.id
LEFT JOIN empleado d ON c.empleado = d.id
LEFT JOIN empleado d2 ON a.agente_age_pre = d2.id
where a.fecha_pre between '17/12/2023' and '18/12/2023'


select a.id, b.nombre, d2.nombre 'agente', d.nombre 'cobrador', a.detalle_pre, a.moneda_pre, a.fecha_pre, a.tipo_pre, a.cuotas_pagada_pre, a.balance_pre, a.monto_pre, a.interes_pre, a.cuotas_pre, a.dia_pre, a.dias_cuota_pre, a.estado_pre from prestamos a LEFT JOIN cliente b ON a.cod_cli_pre = b.id LEFT join cobradores c ON a.cobrador_cob_pre = c.id LEFT JOIN empleado d ON c.empleado = d.id LEFT JOIN empleado d2 ON a.agente_age_pre = d2.id
