
# Application Setup and Running Guide

## Prerequisites

1. **Visual Studio 2022** - Ensure that Visual Studio 2022 is installed on your machine.

2. **.NET Core 6.0 SDK** - Install .NET Core 6.0 SDK.

3. **Node.js** - Install the 16.x version of Node.js which includes npm. This is required to run the Angular front-end.

4. **Angular CLI** - Install Angular CLI globally using npm:
   ```
   npm install -g @angular/cli@16
   ```

## Setup Instructions

3. **Clone** - Open Visual Studio 2022 > Click on "Clone a repository" > Add Repository Location as: "https://github.com/virendra05/Rsystems.git" > Specify the folder path

2. **Open Backend Project**:
   - Open Visual Studio 2022.
   - Navigate to 'File > Open > Project/Solution' and select the .sln file from the cloned repository.

3. **Open Frontend Project**:
   - Open Visual Studio Code > File > Open Folder > from the cloned repository location Open CoreWebApp Angular Project
   - Open the terminal in Visual Studio Code
   - Run the following command to install dependencies:
     ```
     npm install
     ```

## How to Run the Application

1. **Running the .NET Core Backend**:
   - In Visual Studio 2022, set the Employee.API as the startup project.

2. **Running the Angular Frontend**:
   - In Visual Studio Code, open the terminal.
   - Navigate to the Angular project directory (CoreWebApp).
   - Start the Angular application by running:
     ```
     npm start
     ```
   - Access the frontend by navigating to `http://localhost:4200` in a web browser.

## Application Structure and Design Decisions

### `Employee.Api` Module
- **Connected Services**: Includes external services connected to the project.
- **Dependencies**: Lists all project dependencies.
- **Properties**: Contains configuration settings like launch settings for the API.
- **CommonUtility**: Utility classes like `Utility.cs` that provide common functionality across the project.
- **Controllers**: Includes `EmployeeController.cs` for handling API requests.
- **ViewModel**: Contains view models that define the data structure for API responses.
- **appsettings files**: Configuration files for different environments.
- **Program.cs**: Entry point of the .NET Core application.

### `BusinessManager` Module
- **Dependencies and Contract**: Interfaces like `EmployeeBusinessContract.cs` and services such as logging.
- **Manager**: Contains business logic implementation, e.g., `EmployeeBusinessManager.cs`.
- **Model**: Data models and validators for business logic validation.

### `DataManager` Module
- **Contract**: Interface `IEmployeeDataContract` defining data operations.
- **Manager**: `EmployeeDataManager.cs` implementing the data handling logic.
- **Model**: Entity models representing the database context, e.g., `Employee.cs`.

This structure ensures separation of concerns among different layers of the application, promoting maintainability and scalability.

   ```
 Employee.Api
├── Connected Services
├── Dependencies
├── Properties
├── CommonUtility
│   └── Utility.cs
├── Controllers
│   └── EmployeeController.cs
├── ViewModel
│   ├── AddEditDeleteResultViewModel.cs
│   ├── AddEmployeeViewRequestModel.cs
│   ├── BaseResultViewModel.cs
│   ├── EditEmployeeViewRequestModel.cs
│   ├── GetAllEmployeesResultViewModel.cs
│   ├── GetEmployeeRequestModel.cs
│   └── GetEmployeeResultViewModel.cs
├── appsettings.Development.json
├── appsettings.json
└── Program.cs


BusinessManager
├── Dependencies
│   ├── EmployeeBusinessContract.cs
│   └── ILoggingService.cs
├── Contract
│   ├── EmployeeBusinessContract.cs
│   └── ILoggingService.cs
├── Manager
│   ├── EmployeeBusinessManager.cs
│   └── SerilogLoggingService.cs
├── Model
│   ├── Enums
│   │   ├── ErrorTypeEnum.cs
│   │   └── HttpVerbCode.cs
│   ├── ErrorMessage
│   │   └── CommonErrorMessageForLogging.cs
│   ├── RequestResponse
│   │   ├── AddEditDeleteEmployeeResponse.cs
│   │   ├── AddEmployeeRequest.cs
│   │   ├── BaseRequest.cs
│   │   ├── BaseResponse.cs
│   │   ├── DeleteEmployeeRequest.cs
│   │   ├── EditEmployeeRequest.cs
│   │   ├── GetEmployeeListRequest.cs
│   │   ├── GetEmployeeResponse.cs
│   │   └── GetEmployeesListResponse.cs
│   ├── Validator
│       ├── AddEmployeeValidator.cs
│       ├── BaseRequestValidator.cs
│       ├── DeleteEmployeeValidator.cs
│       └── EditEmployeeValidator.cs

DataManager
├── Dependencies
├── Contract
│   └── IEmployeeDataContract.cs
├── Manager
│   └── EmployeeDataManager.cs
├── Model
│   ├── Employee.cs
│   └── EmployeeResult.cs

COREWEBAPP
├── .angular
├── src
│   ├── app
│   │   ├── app-routing.module.ts
│   │   ├── app.component.html
│   │   ├── app.component.scss
│   │   ├── app.component.ts
│   │   ├── app.module.ts
│   ├── assets
│   │   └── .gitkeep
│   ├── employeeList
│   │   ├── employeeList.component.html
│   │   ├── employeeList.component.scss
│   │   └── employeeList.component.ts
│   ├── models
│   │   └── employee.model.ts
│   ├── services
│   │   └── employee.service.ts
│   ├── favicon.ico
│   ├── index.html
│   ├── main.ts
│   ├── styles.scss
├── .editorconfig
├── .gitignore
├── angular.json
├── package-lock.json
├── package.json
├── tsconfig.app.json
├── tsconfig.json
└── tsconfig.spec.json

   ```
