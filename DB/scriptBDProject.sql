

create database db_project
go
use db_project
go
create table documento(
id int not null primary key identity(1,1),
nroExpediente varchar(100),
nombre varchar(100),
apellido varchar(100),
codigoModular varchar(100),
motivo varchar(100),
observacion varchar(100),
fecha date,
dni varchar(8),
tituloProfesional varchar(100),
especialidad varchar(100),
establecimiento varchar(100),
nivelMagisterial varchar(100),
jornadaLaboral varchar(100),
regimenPension varchar(100),
nroIpss varchar(100),
fechaIngreso date,
fechaCese date,
codigoEscalafon varchar(100),
anios int,
meses int,
dias int,
cargo varchar(100),
tipoServidor varchar(100),
otros varchar(100)
)
go