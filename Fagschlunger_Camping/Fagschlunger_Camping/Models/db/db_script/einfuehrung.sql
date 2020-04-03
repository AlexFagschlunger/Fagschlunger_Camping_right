create database if not exists db_einfuehrung collate utf8_general_ci;
use db_einfuehrung;

create table users(
   id int not null auto_increment,
   firstname varchar(100) null,
   lastname varchar(100) not null,
   ankunftsdatum date not null,
   abreisedatum date null,
   personen int not null,
   
   constraint id_PK primary key(id)
)engine=InnoDB;

Insert Into users Values(null, "Fabus", "Eggus", "2020-04-08", "2021-01-25", "null");

select * from users;

