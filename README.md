# Employee Cafe Manager

## Overview
This is a full-stack application built with ReactJS for the front end and .NET 8 Web API integrated with SQL Server using EF Core for the back end.

## Prerequisites and Getting Started
- **SQL Server** (latest version recommended)
- **Visual Studio 2022** (or any IDE supporting .NET 8)
- **.NET 8 SDK**
- The project consists of two main folders: one for the backend and one for the frontend.

## Setting up the Backend

### 1. Clone the Repository
Clone the project from GitHub:
```bash
git clone https://github.com/shehanks/GICEmployee.git
```

### 2. Configure the Database Connection
- Open `appsettings.json` in the backend project `(GICEmployee.API)`.
- Replace the `DefaultConnection` string with your SQL Server instance
  
For local SQL Server instance with Windows authentication:
  ```json
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=GICEmployee;Trusted_Connection=True;TrustServerCertificate=True"
  ```
For remote SQL Server with credentials:
```json
  "DefaultConnection": "Server=your_server;Database=GICEmployee;User Id=your_username;Password=your_password;TrustServerCertificate=True"
  ```

### 3. Run the Application
Once the connection string is set, run the project from the `GICEmployee.API` (the startup project).The application will automatically create the database using migration scripts. 
The Web API will be available at the URL `http://localhost:5109/`, and the health probe endpoint will be loaded on launch.

Swagger is configured for API testing, and you can access it from `http://localhost:5109/swagger/index.html`.


### 4. Test the Backend Application
You can now test the backend using Swagger or Postman.


## Setting up the Frontend

### 1. Navigate to the Root Folder
- Open the `frontend` folder (`frontend\cafe-employee-manager`) in VS Code or another compatible editor, then open PowerShell.
- Alternatively, navigate to the folder path using PowerShell.

### Install Node Packages
- Run the following command to install the necessary node packages:
```bash
npm install
```

### Start the Frontend Application
- Run the following command to start the frontend, which will be available at `http://localhost:3000/`:
```bash
npm start
```
