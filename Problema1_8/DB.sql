create database Pets
go
use Pets
go
create table PetTypes
(id_pettype int identity not null,
descriptiontype varchar(50),
constraint pk_PetTypes primary key(id_pettype))

create table Pets
(id_pet int identity not null,
namepet varchar(50),
age int,
id_pettype int,
active bit,
constraint pk_Pets primary key(id_pet),
constraint fk_pets_pettypes foreign key(id_pettype)
	references pettypes(id_pettype))

create table Clients
(id_client int identity not null,
nameclient varchar(50),
gender bit,
active bit,
constraint pk_clients primary key(id_client))

create table ClientsPets
(id_client int not null,
id_pet int not null,
constraint fk_clientspets foreign key (id_client)
	references clients(id_client),
constraint fk_petsclients foreign key (id_pet)
	references pets(id_pet),
constraint pk_clientspets primary key(id_client,id_pet))

create table Attentions
(   id_attention int identity not null primary key,
    attentiondate datetime not null,
    id_client int not null,
    id_pet int not null,
    discount decimal(10,2),
	active bit,
    foreign key (id_client) references Clients(id_client),
    foreign key (id_pet) references Pets(id_pet)
);

create table AttentionServices
(	id_service int identity not null primary key,
	servicedescription varchar(50),
	price decimal(10,2),
	active bit)

create table AttentionDetails
(   id_attention int not null,
    id_service int not null,
    price decimal(10,2) not null,
    quantity int not null,
    foreign key (id_attention) references Attentions(id_attention),
	foreign key (id_service) references AttentionServices(id_service),
	primary key (id_attention,id_service)
);

insert into PetTypes(descriptiontype)
values('Perro'),('Gato'),('Ave'),('Pez')

insert into AttentionServices(servicedescription,price,active)
values('Vacuna antirrábica', 3000, 1),('Castración',20000,1),('Peluquería',6000,1)

go
create proc SP_GETPETSBYCLIENTID
@clientid int
as
begin
select cp.id_pet, p.namepet, p.age, p.active, pt.id_pettype, pt.descriptiontype
from ClientsPets cp
join Pets p on cp.id_pet = p.id_pet
join PetTypes pt on pt.id_pettype = p.id_pettype
where cp.id_client = @clientid
end


go
create proc SP_GETCLIENTBYID
@id int
as
begin
select id_client, nameclient, gender, active
from Clients where id_client = @id
end

go
create proc SP_DELETECLIENT
@id int
as begin
update Clients set active = 0 where id_client = @id
select @@ROWCOUNT as RowsAffected
end

go
create proc SP_GETALLCLIENTS
as begin
select * from Clients
end

go
create proc SP_SAVECLIENT
@id_client int,
@nameclient varchar(50),
@gender bit,
@active bit
as
begin
if @id_client = 0
	begin insert into Clients(nameclient,gender,active)
		values(@nameclient,@gender,@active)
		select @@ROWCOUNT as RowsAffected
	end
	else
	begin update clients set nameclient = @nameclient,
		gender = @gender, active = @active
		where id_client = @id_client
		select @@ROWCOUNT as RowsAffected
	end
end

go
create proc sp_deletepet
@id int
as begin
update Pets set active = 0 where id_pet = @id
select @@ROWCOUNT as RowsAffected
end

go
create proc SP_SAVEPET
@id_pet int,
@namepet varchar(50),
@age bit,
@id_pettype int,
@active bit
as
begin
if @id_pet = 0
	begin insert into Pets(namepet,age,id_pettype, active)
		values(@namepet,@age,@id_pettype,@active)
		select @@ROWCOUNT as RowsAffected
	end
	else
	begin update pets set namepet = @namepet,
		age = @age, id_pettype=@id_pettype, active=@active
		where id_pet = @id_pet
		select @@ROWCOUNT as RowsAffected
	end
end

go
create proc SP_GETPETBYID
@id int
as
begin
select id_pet, namepet, age, active, p.id_pettype, pt.descriptiontype
from Pets p join PetTypes pt on p.id_pettype=pt.id_pettype
where id_pet = @id
end

go
create proc SP_GETALLPETS
as begin
select p.id_pet, p.namepet, p.age, p.id_pettype, pt.descriptiontype,p.active
from Pets p join PetTypes pt on p.id_pettype=pt.id_pettype
end

--
go
create proc sp_deleteservice
@id int
as begin
update AttentionServices set active = 0 where id_service = @id
select @@ROWCOUNT as RowsAffected
end

go
create proc SP_SAVESERVICE
@id_service int,
@description varchar(50),
@price decimal(10,2),
@active bit
as
begin
if @id_service = 0
	begin insert into AttentionServices(servicedescription,price,active)
		values(@description,@price,@active)
		select @@ROWCOUNT as RowsAffected
	end
	else
	begin update AttentionServices set servicedescription = @description,
		price = @price, active=@active
		where id_service = @id_service
		select @@ROWCOUNT as RowsAffected
	end
end

go
create proc SP_GETserviceBYID
@id int
as
begin
select *
from AttentionServices
where id_service = @id
end

go
create proc SP_GETALLSERVICES
as begin
select * from AttentionServices
end

go
create proc SP_ADDPETTOCLIENT
@id_pet int,
@id_client int
as begin
insert into ClientsPets(id_pet,id_client)
values(@id_pet,@id_client)
select @@ROWCOUNT as 'RowsAffected'
end
