# Cards API

The Cards RESTful web service is a powerful tool built on C#/.NET, designed for efficient task management through intuitive card creation and manipulation. With support for user roles, authentication via JSON Web Tokens, and comprehensive filtering options, it offers a seamless experience for both members and administrators.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)  
- [Usage](#usage)
  - [Endpoints](#endpoints)
  - [Authentication](#authentication)
  - [Request and Response Examples](#request-and-response-examples)
- [Documentation](#documentation)
  - [API Reference](#api-reference)
  - [Models](#models)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The Cards RESTful web service is a C#/.NET application designed for managing tasks in the form of cards. Users are uniquely identified by their email address and have roles of either Member or Admin, requiring authentication via password to access cards.

Members can create cards with mandatory names and optional descriptions and colors. Upon creation, cards default to the "To Do" status. The service supports searching through cards with filters for name, color, status, and creation date, and allows for pagination and sorting options. Users can request single cards, update card details including name, description, color, and status, and delete cards they have access to.

Authentication is implemented using JSON Web Tokens (JWT), allowing users to include the token in the Authorization header for subsequent API calls. The service provides a robust and flexible solution for task management, catering to both individual users and administrators with varying access levels.

## Features

Key features of the API.

- User Authentication: Users are authenticated using email and password, with support for JSON Web Tokens (JWT).
- User Roles: Users have roles (Member or Admin) determining their access levels to cards.
- Card Creation: Users can create cards by specifying a mandatory name along with optional description and color.
- Card Status: Upon creation, cards default to a "To Do"(0) status, with options to update status to "In Progress"(1) or "Done"(2).
- Access Control: Members have access to cards they created, while Admins have access to all cards.
- Search and Filtering: Users can search cards by name, color, status, and date of creation, with optional pagination and sorting features.
- Single Card Retrieval: Users can request a single card they have access to.
- Card Modification: Users can update the name, description, color, and status of cards they have access to, with support for clearing description and color fields.
- Card Deletion: Users can delete cards they have access to, providing efficient management of task lists.

## Getting Started

- Clone the Repository: Clone the repository containing the API code to your local machine.
- Database Configuration: Ensure you have a Microsoft SQL database installed. Update the database connection string in appsettings.json to point to your database.
- Install Dependencies: Install the necessary dependencies for the API. This includes .NET Core SDK and any additional packages specified in the project's csproj files.
- Database Setup: Run the following Microsoft SQL server scripts to create Users and Cards tables.
```
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[UserRole] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Cards](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Color] [nvarchar](7) NULL,
	[Status] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cards]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
```

## Usage

Explain how users can use your API.

### Endpoints

List and describe each endpoint available in your API.

### Authentication

Explain how authentication works in your API.

### Request and Response Examples

Provide examples of requests and responses for each endpoint.

## Documentation

Provide links to additional documentation or resources.

### API Reference

Link to the detailed API reference documentation.

### Models

Link to the documentation for data models used in your API.

## Contributing

Explain how others can contribute to your project.

## License

Include licensing information for your project.
