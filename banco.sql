create database dbAtividade1;

use dbAtividade1;

create table Usuarios(
IdUser int auto_increment primary key,
Nome varchar(50) not null,
Email varchar(50) not null,
Senha varchar(20) not null
);

create table Produtos(
IdProd int auto_increment primary key,
Nome varchar(50) not null,
Descricao varchar(100) not null,
Preco double not null,
Quantidade int not null
);