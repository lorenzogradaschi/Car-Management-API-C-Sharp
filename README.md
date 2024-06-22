Car Management API
This project is a Car Management API built with ASP.NET Core and Entity Framework Core. It allows you to manage cars, customers, and purchases through HTTP requests.

Prerequisites
Visual Studio 2019 or later or Visual Studio for Mac
.NET Core SDK 5.0 or later
SQL Server
Setup
1. Create the Project in Visual Studio for Mac
Open Visual Studio for Mac.
Click on New to create a new project.
Select App under the Web and Console category.
Choose ASP.NET Core Web API and click Next.
Name your project (e.g., CarManagementAPI) and click Create.
2. Add Required Packages
Right-click on the project in the Solution Explorer and select Manage NuGet Packages.
Search for and install the following packages:
Microsoft.EntityFrameworkCore (version compatible with .NET 7.0)
Microsoft.EntityFrameworkCore.SqlServer (version compatible with .NET 7.0)
Microsoft.EntityFrameworkCore.Tools (version compatible with .NET 7.0)
Microsoft.AspNetCore.Mvc.NewtonsoftJson
3. Add the Code
Replace the contents of the Program.cs file with the provided code.

4. Configure the Database
Update the appsettings.json file with your SQL Server connection string:


{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
5. Create the Database
Run the following commands in the Terminal or Package Manager Console to create the initial migration and update the database:


dotnet ef migrations add InitialCreate
dotnet ef database update
6. Run the Application
Press F5 in Visual Studio for Mac or use the .NET CLI to start the application:


dotnet run
The API will be available at http://localhost:5000.

API Endpoints
Cars
GET /api/cars - Retrieve all cars.
POST /api/cars - Add a new car.
PUT /api/cars - Update an existing car.
DELETE /api/cars/{id} - Delete a car by ID.
Customers
GET /api/customers - Retrieve all customers.
POST /api/customers - Add a new customer.
PUT /api/customers - Update an existing customer.
DELETE /api/customers/{id} - Delete a customer by ID.
Purchases
GET /api/purchases - Retrieve all purchases.
POST /api/purchases - Add a new purchase.
PUT /api/purchases - Update an existing purchase.
DELETE /api/purchases/{id} - Delete a purchase by ID.
Example Usage
You can use tools like curl or Postman to interact with the API. Here are some example requests:

Add a Car

curl -X POST http://localhost:5000/api/cars -H "Content-Type: application/json" -d '{"brand":"Toyota", "model":"Camry", "price":30000}'
Get All Cars

curl http://localhost:5000/api/cars
Update a Car

curl -X PUT http://localhost:5000/api/cars -H "Content-Type: application/json" -d '{"id":1, "brand":"Toyota", "model":"Corolla", "price":20000}'
Delete a Car

curl -X DELETE http://localhost:5000/api/cars/1
Project Structure
Models: Contains the Auto, Customer, and Purchase classes.
Data: Contains the CarDbContext class which is the Entity Framework Core DbContext.
Repositories: Contains the CarArchive, CustomerArchive, and PurchaseArchive classes that handle database operations.
Services: Contains the CarList, CustomerList, and PurchaseList classes that provide business logic.
Controllers: Contains the CarsController, CustomersController, and PurchasesController classes that handle HTTP requests.
Startup.cs: Configures services and middleware for the application.
Program.cs: Entry point of the application.
