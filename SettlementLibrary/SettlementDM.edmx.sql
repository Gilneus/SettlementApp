










































-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/29/2016 10:55:29

-- Generated from EDMX file: C:\Settlement\SettlementApp\SettlementApp\SettlementLibrary\SettlementDM.edmx
-- Target version: 3.0.0.0

-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;

SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------


CREATE TABLE `Literatures`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (100));

ALTER TABLE `Literatures` ADD PRIMARY KEY (Id);





CREATE TABLE `LiteratureReferences`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Literature` int, 
	`Settlement` int, 
	`Pages` varchar (50), 
	`TempToken` varchar (50));

ALTER TABLE `LiteratureReferences` ADD PRIMARY KEY (Id);





CREATE TABLE `Settlements`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (100), 
	`Description` longtext, 
	`latitude` varchar (50), 
	`longitude` varchar (50), 
	`TimeperiodAbsolute` varchar (100), 
	`TimeperiodRelative` int, 
	`NumberBuildings` int, 
	`ActivityYears` int);

ALTER TABLE `Settlements` ADD PRIMARY KEY (Id);





CREATE TABLE `TimeperiodRelatives`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (100));

ALTER TABLE `TimeperiodRelatives` ADD PRIMARY KEY (Id);





CREATE TABLE `Users`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`UserName` varchar (100), 
	`Password` varchar (200));

ALTER TABLE `Users` ADD PRIMARY KEY (Id);





CREATE TABLE `ErrorLogs`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Message` longtext, 
	`Class` varchar (100), 
	`LineNumber` int, 
	`Time` datetime, 
	`Method` varchar (100));

ALTER TABLE `ErrorLogs` ADD PRIMARY KEY (Id);







-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
