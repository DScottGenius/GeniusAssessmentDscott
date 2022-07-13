CREATE DATABASE MiniAdeptDB;

USE MiniAdeptDB;


CREATE TABLE [MiniAdeptDB].[dbo].[user](
	debt_type varchar(20),
	account_number varchar(20) NOT NULL UNIQUE,
	account_name varchar(50),
	date_of_birth date,
	balance decimal(16,2),
	adept_ref varchar(7)
);

CREATE TABLE [MiniAdeptDB].[dbo].[email](
	email_owner varchar (20) NOT NULL,
	email_address varchar (60) NOT NULL
);

CREATE TABLE [MiniAdeptDB].[dbo].[phone](
	phone_owner varchar (20) NOT NULL,
	phone_number varchar (10) NOT NULL
);
CREATE TABLE [MiniAdeptDB].[dbo].[address](
	address_owner varchar (20) NOT NULL,
	line_1 varchar (60) NOT NULL,
	line_2 varchar (60),
	city varchar(20),
	postcode varchar(15) NOT NULL
);
CREATE TABLE [MiniAdeptDB].[dbo].[payment](
	adept_ref varchar(7) NOT NULL,
	amount decimal(16,2),
	effective_date date NOT NULL,
	[source] varchar(20),
	method varchar (20)
);

ALTER TABLE [MiniAdeptDB].[dbo].[user]
ADD CONSTRAINT Pk_user PRIMARY KEY (account_number);

ALTER TABLE [MiniAdeptDB].[dbo].[email]
ADD CONSTRAINT Pk_email PRIMARY KEY (email_address),
	CONSTRAINT Fk_email_user FOREIGN KEY(email_owner) REFERENCES [user](account_number) ON DELETE CASCADE;

ALTER TABLE [MiniAdeptDB].[dbo].[phone]
ADD CONSTRAINT Pk_phone PRIMARY KEY (phone_number),
	CONSTRAINT Fk_phone_user FOREIGN KEY(phone_owner) REFERENCES [user](account_number) ON DELETE CASCADE;

ALTER TABLE [MiniAdeptDB].[dbo].[address]
ADD CONSTRAINT Pk_address PRIMARY KEY (address_owner, line_1, postcode),
	CONSTRAINT Fk_address_user FOREIGN KEY(address_owner) REFERENCES [user](account_number) ON DELETE CASCADE;

ALTER TABLE [MiniAdeptDB].[dbo].[payment]
ADD CONSTRAINT Pk_payment PRIMARY KEY (adept_ref, effective_date);


--The following state will insert a user that will end up linked to the payments
--INSERT INTO [MiniAdeptDB].[dbo].[user](debt_type, account_number, account_name, date_of_birth, balance, adept_ref) VALUES('Residential', '12456789', 'Bob', '1999/04/04', 300, 'A118598');

