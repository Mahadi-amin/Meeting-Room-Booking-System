# Meeting-Room-Booking-System
# Meeting Room Booking System

The Meeting Room Booking System is a web-based application designed to streamline the process of reserving and managing meeting rooms in an organization. With role-based access, bulk user upload, and robust logging, it simplifies room management for both users and administrators.

## Features
- Book and manage meeting rooms efficiently.
- Role-based user access (Admin, User).
- Bulk user upload using CSV files.
- Structured logging with Serilog.
- Dependency Injection with Autofac.
- Simplified object mapping with AutoMapper.


How to run this app:

1. Clone the repository

2. Open the solution in Visual Studio.

3. Set the connection string in the appsettings.json 

Example: 

"DefaultConnection": "Server=.\\SQLEXPRESS;Database=DatabaseName;User Id=UserId;Password=Password;Trust Server Certificate=True"

4. Apply the migration to the database:

Example: dotnet ef database update --project Presentation --context ApplicationDbContext

Note: You have to change Database name and password according you database.

5. Run the project.

6.User upload using a CSV file format 

Example:
Name,Pin,Email,Phone,Department,Designation,Status,RoleName
John Doe,12345,johndaaoe@example.com,123-456-7890,HR,Manager,true,Admin
Jane Smith,67890,janeaasmith@example.com,987-654-3210,IT,Developer,false,User

