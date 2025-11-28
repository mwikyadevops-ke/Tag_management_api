Vehicle Tagging and Weight Event Management System
This project is an ASP.NET Core Web API for managing vehicle 
weigh events and automatic/manual tagging. 
It supports PostgreSQL for storage and Swagger UI for API exploration.

Features
1.Record vehicle weight events from external systems
2.Automatic tagging for overloaded vehicles
3.Manual tagging by users with reason and image
4.Close tags with notes
5.Swagger documentation for all endpoints
6.PostgreSQL support for production-ready database storage

Project Structure
TagManagement/
├── Controllers/      # API controllers
├── Data/             # DbContext
├── Dtos/             # Data Transfer Objects
├── Enums/            # Enum types
├── Models/           # Database models
├── Services/         # Business logic services
├── Program.cs        # Entry point
├── appsettings.json  # Configuration
└── .gitignore

Installation
git clone https://github.com/username/TagManagement.git
cd TagManagement

