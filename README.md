# DronProyect
# The project is developed in Visual Studio 2019, it is a type of project: ASP .NET Web Application (.NET Framework), Web API.
# For the mapping of the Database, the Entity Framework was used.
# The Global.asax file defines the data working as JSON format only
# Controller classes are established for each of the database entries, including all required GET, POS, PUT and DELETE methods.
# A model class is created that serves as a repository for handling the main queries.
# A class for validations related to the drones entity is created using the data annotations.
# The database is the file called drones.bak inside the repository. It already comes with a data set. Mount on a local server SQL Server version equal to or greater than 2016. Go to the web.config file of the project and modify the Connection String for that of the Database manager in which the backup is mounted
