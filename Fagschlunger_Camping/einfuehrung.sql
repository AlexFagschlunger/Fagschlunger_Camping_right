create database if not exists db_einfuehrung collate utf8_general_ci;
use db_einfuehrung;

create table reservierung(
   id int not null auto_increment,
   firstname varchar(100) not null,
   lastname varchar(100) not null,
   bearbeitet bool,
   ankunftsdatum date not null,
   abreisedatum date not null,
   personen int not null,
   
   constraint id_PK primary key(id)
)engine=InnoDB;

Insert Into reservierung Values(null, "Fabus", "Eggus", false, "2020-04-08", "2021-01-25", "1");

select * from reservierung order by ankunftsdatum ASC;

