
# Introduction 
DataAccess is mainly for CRUD operation on database, it is using EntityFramework Core to talk with SQL server database.

# Getting Started
For most of the projects they are using .Net core stack, including:
1.	DataAccess
2.	DataAccess.Console
3.	DataAccess.Test

For these projects they are all included in DataAccess.sln

There is also a project targeting standard .Net framework, which is used for validating and generating database schema while some features are not support in EntityFramework Core:
1.	DataAccess.DotNetStd

For this project, itself and its references are included in DataAccess.DotNetStd.sln

# Build and Test
You can use Visual Studio 2017 to build these slns 

# Database migration

There are 2 ways to use CodeFirst solution to update targeting database:
1. Use DataAccess.DotNetStd to automatically initilize database and use standard migration support to update it
	a. Auto-Migration is enabled in DataAccess.DotNetStd project which will generate migration plan automatically when models are updated
	b. Run "Upate-Database" in PMC(Packager Manager Console) to update target database
	
2. Use "dotnet ef migrations" in PMC(Packager Manager Console) on DataAccess project to init/update database
	a. Make sure you are at project level which contains DbContext
	b. Make sure IDesignTimeDbContextFactory is added
	c. Run **"dotnet ef migrations add <name> -c <context> "**
	`dotnet ef migrations add UserContextInit -c UserContext`
	d. Run **"dotnet ef database update -c <context>"**
	`dotnet ef database update -c UserContext` 


>Note: IDesignTimeDbContextFactory is needed for generating migration plan
