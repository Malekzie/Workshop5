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
		communicates with the database. It also contains the repositories for each
		table in the database. The repositories are used to perform CRUD operations
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

		There is also the ViewModel folder, which contains the view models for the project.
		If you're unfamiliar with view models, they are used to pass data between the
		controller and the view. They are useful when you need to pass data that is not
		directly related to the model or when you want to customize the data that is
		passed to the view.

	### Web Application
		The **Web Application** is the main project of the solution. It contains all
		the controllers, views, and other files that make up the web application.
		With the implementation of Identity, I've placed things related to Customer inside
		the Customer folder under Areas.


	### Utils Project
		The **Utils** project contains utility classes that are used throughout the project.
		Currently, it contains the EmailSender util, which when removed, will break the project.
		Please do not remove this project.

		As for the why:
		Identity requires you to implement the IEmailSender interface. I've implemented it
		in the EmailSender class in the Utils project. If you remove this project, you will
		get an error when you try to register a new user.

		It also contains Static Definitions for the project. This is where I've placed
		constants that are used throughout the project. If you need to add a new constant,
		you can add it to the StaticDefinitions class in the Utils project.
		It currently has the constants for the roles, and the constants for
		the provinces drop down in the register page.

		If you are to add new constants:
		Please add them to the StaticDefinitions class
		in the Utils project.

## Features
	I've integrated Identity into the project to allow for user authentication.
	Users can register, login, and logout. I've tweaked it so that email is now optional
	and it includes the Customer fields, integrate it with the IdentityUser.

	The reason why I chose Identity:
	Albeit tricky to implement, Identity is a powerful tool that allows for easy
	authentication and authorization in ASP.NET Core. It provides a lot of features
	out of the box, such as user registration, login, and password recovery. It also
	allows you to customize the user model and add additional fields to it.

	A cool new feature I found, was using a Global Using class that injects
	the required services in a project. This is useful when you have multiple files
	requiring the same services. Instead of injecting the services in each file,
	you can inject them in the Global Using class and then use the Global Using class
	I've named them **GlobalInjector**


## My Contributions
	### Web Application
		- Configured Program.cs for database Dependency Injection,
		- Configured Identity to map Areas and the Default route,

	### DataAccess
		- Implemented the Unit of Work pattern,
		- Implemented the repositories for each table in the database,
		- Set up the project to use Entity Framework Core

	### Models
		- Cascaded the required models from the database
		- Created the view models for the project
		- Tweaked the Customer model to be of type IdentityUser
	
	### Utils
		- Implemented the EmailSender util,
		- Added the StaticDefinitions class for constants