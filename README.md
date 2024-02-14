# Running the Application

To run the application, follow these steps:

## .NET Project:

1. In the NuGet Package Console, choose project `Infrastructure` and type `update-database`.
2. To seed some data use `/Seeder/SeedContacts` and `/Seeder/SeedUser` endpoints.

## Angular Project:

1. Open terminal in Angular folder and Type:
   ```
   npm install
   npm start
   ```

## Sign in Information:

To sign in, use the following credentials:
- **Login:** user
- **Password:** zaq1@WSX

# Project Information:

The .NET Project consists of 2 projects: Contacts and Infrastructure.

## Infrastructure:

- Contains the entire database logic, including repositories, models, migrations, and enums.

## Contacts:

- Contains the business logic, including controllers, services, and files for JWT authentication.

### Controllers:

- **Identity Controller:** Configured endpoints for authentication.
- **Contacts Controller:** Configured CRUD operations for the main application table.
- **Seeder Controller:** Contains endpoints for seeding data.

The Angular project has 2 services:

## Services:

- **Contact Service:** Contains methods for sending HTTP requests to CRUD endpoints.
- **Identity Service:** Contains methods to send HTTP requests to get authentication tokens, logout, and check if the user is signed in.

To send authentication tokens in HTTP requests, the application uses an auth interceptor.

## Components:

- **Top Bar Component:** Always displayed at the top of the screen.
- **Contact List Component:** The first component displayed in the application, showing a list of contacts.
- **Login Component:** Accessible by clicking "login" on the top bar.
- **Details Component:** Displays information about a selected contact. Accessed by clicking the "Details" button in the contact list.
- **Update Component:** Allows editing contact details.

# Used Technologies and Libraries:

- .NET Core 6
- Entity Framework Core 6
- Microsoft.AspNetCore.Authentication.JwtBearer
- Angular Router
- RxJS
