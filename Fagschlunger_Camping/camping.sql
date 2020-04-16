create database if not exists db_camping collate utf8_general_ci;
use db_camping;

create table users(
   id int not null auto_increment,
   rolle int not null,
   firstname varchar(100) null,
   lastname varchar(100) not null,
   gender int not null,
   birthdate date null,
   username varchar(100) not null unique,
   password varchar(128) not null,
   
   constraint id_PK primary key(id)
)engine=InnoDB;

Insert Into users Values(null, 0, "Fabus", "Eggus", 0, "2002-01-25", "egutsch", sha2("fabian", 512));

select * from users;

