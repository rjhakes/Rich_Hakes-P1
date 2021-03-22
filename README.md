# Hakes Auto Parts

## Project Description

Hakes Auto Parts is a prototype store web application. Users can create a customer account, view store inventories, and place orders for our mock items. 
Additionally, users can view/edit their account information, and view their order histories. An admin account can create, update, and delete administrators, 
locations, products, and inventories.

## Technologies Used

* C#
* Entity Framework
* PostgreSQL
* Microsoft Azure
* XUnit

## Features

List of features ready and TODOs for future development
* Register as a new customer
* Display details of an order
* Place an order to store locations for 1 or more items
* View a customer's order history
* View location inventories
* As a manager, create, update, and delete administrators, locations, products, and inventories

To-do list:
* Search customers by email
* View location order histories
* Sort order histories by various parameters
* Fully test application and database
* Implemenet CI/CD pipeline and SonarCloud
* Create new admin account for new project initialization/ DB connection

## Getting Started
   
git clone https://github.com/rjhakes/Rich_Hakes-P1.git

create appsettings.json file; include the following replacing "Connection" with a proper string to connect to your database
  
  {
    "ConnectionStrings": {
    
      "StoreDB": "Connection"
      
    }
    
  }
  
Execute the following command lines (any text between "<>" should be replaced, and "<>" symbols removed)  
- dotnet ef migrations add <migrationName> -c StoreDBContext --startup-project ../StoreMVC
- dotnet ef database update

## License

This project uses the following license: MIT.

