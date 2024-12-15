# Project Setup and Running Guide

This guide will walk you through the steps to set up and run the .NET solution, which consists of the following projects:

1. **Domain**: Contains the models.
2. **Application**: Handles business logic and data access.
3. **Infrastructure**: Includes the `DbContext` for MySQL database integration.
4. **Web**: A Web API project.
5. **UI**: An MVC project that consumes the Web API.

## Prerequisites

Before starting, ensure you have the following installed on your system:

- [.NET SDK](https://dotnet.microsoft.com/) (version 6.0 or later)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) (optional but recommended)
- A code editor, such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/) or any tool to test APIs (optional)

## Step 1: Clone the Repository

Clone the project repository to your local machine using the following command:

```bash
git clone https://github.com/tahaalfoghi/DP424.git
```

Navigate to the project directory:

```bash
cd DP424
```

## Step 2: Apply Migrations

1. Update the connection string in the `appsettings.json` file located in the **Infrastructure** project. Replace the placeholders with your MySQL credentials:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost; Database=DP424; User=root; Password=yourpassword;"
   }
   ```
2. Delete the folder Migrations in the Infrastructure Project
3. for visual studio ide in the Package Manager choose the default package as DP424.Infrastructure then type `add-migration InitialCreate`
4. for vscode naviagate to DP424.Infrastructure then type `dotnet ef migrations add InitialCreate`
5. run the following command vscode: `dotnet ef database update` visual studio ide `update-database`





## Step 3: Build the Solution

1. Open the solution in Visual Studio or your preferred IDE.
2. Build the solution to restore dependencies and compile the projects:
   ```bash
   dotnet build
   ```

## Step 4: Run the Web API

1. Navigate to the **Web** project directory:

   ```bash
   cd Web
   ```

2. Start the Web API project:

   ```bash
   dotnet run
   ```

   The Web API will be accessible at `http://localhost:5000` by default.

## Step 6: Run the MVC UI

1. Open a new terminal and navigate to the **UI** project directory:

   ```bash
   cd UI
   ```

2. Start the MVC project:

   ```bash
   dotnet run
   ```

   The MVC application will be accessible at `http://localhost:5001` by default.

## Step 7: Testing the Application

- Use Postman or any API testing tool to test the Web API endpoints at `http://localhost:5000`.
- Access the UI application in your browser at `http://localhost:5001` to interact with the front end.

## Notes

- Ensure both the Web API and MVC projects are running simultaneously for full functionality.
- If you encounter issues with port conflicts, update the `launchSettings.json` file in the respective project directories.
- Add seed data as required by modifying the `DbContext` or adding custom initialization scripts.

## Troubleshooting

- **Database Connection Issues**: Double-check the connection string in `appsettings.json`.

- **Migration Errors**: Ensure `dotnet-ef` is installed globally. If not, install it using:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

- **Dependency Issues**: Restore dependencies using:

  ```bash
  dotnet restore
  ```

---

With this setup, your solution should be ready to run. If you have additional questions, refer to the project documentation or contact the project maintainers.

