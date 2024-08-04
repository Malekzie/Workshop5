# Travel Experts Workshop 5 Documentation (Robbie)

## Table of Contents
1. [Introduction](#introduction)
1. [Features](#features)
1. [Contributions](#my-contributions)


## Introduction
	This document is a general overview of the Travel Experts Workshop 5 project. 
	It will cover the features of the project, where it is implemented,
	and the general structure of the project.

	I divided up the solution into 3 main sections:
	1. The DataAccess Project
	2. The Models Project
	3. The Web Application

	### DataAccess Project
		The **DataAccess** project is where all the database operations are handled.
		It contains the TravelExpertsContext class, which is the main class that
		communicates with the database. It also contains the services for each
		table in the database. The services are used to perform CRUD operations
		on the database.

		In this project, I've implemented the strategy called Unit of Work.

		Unit of Work is a design pattern that allows you to perform multiple operations
		on the database in a single transaction. This is useful when you need to perform
		multiple operations on the database and you want to ensure that all operations
		are successful before committing the transaction.

	### Models Project
		The **Models** project contains all the models for the project. Each model
		represents a table in the database. The models are used to pass data between
		the DataAccess project and the Web Application.

		There is also the **ViewModel** folder, which contains the view models for the project.
		If you're unfamiliar with view models, they are used to pass data between the
		controller and the view. They are useful when you need to pass data that is not
		directly related to the model or when you want to customize the data that is
		passed to the view.

	### Web Application
		The **Web Application** is the main project of the solution. It contains all
		the controllers, views, and other files that make up the web application.
		

		In registration, I've implemented Input Masking for the phone number field.
		When the user enters their phone number, it will automatically format the
		phone number as they type. This is useful when you want to ensure that the
		phone number is entered in a specific format.
		I've also implemented that to the postal code field in the Personal Details 
		section.


	### Utils Project

		Currently, It has the password hashing algorithm. It is used to hash the password
		before storing it in the database. This is useful when you want to ensure that
		the password is stored securely in the database.

## Features

	A cool new feature I found, was using a Global Using class that injects
	the required services in a project. This is useful when you have multiple files
	requiring the same services. Instead of injecting the services in each file,
	you can inject them in the Global Using class and then use the Global Using class
	I've named them **GlobalInjector**


## My Contributions
	### Web Application
		- Configured Program.cs for database Dependency Injection
		- Took care of authentication and home page

	### DataAccess
		- Implemented the Unit of Work pattern,
		- Implemented the services for each table in the database,
		- Set up the project to use Entity Framework Core and Cookie authentication

	### Models
		- Cascaded the required models from the database
		- Created the view models for the project
	
	### Utils
		- Implemented the EmailSender util,
		- Added the StaticDefinitions class for constants