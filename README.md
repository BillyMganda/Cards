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

- Clone the Repository: Clone the repository containing the API code to your local machine: ```git clone https://github.com/BillyMganda/Cards.git```
- Database Configuration: Ensure you have a Microsoft SQL database installed. Update the database connection string in appsettings.json to point to your database.
- Install Dependencies: Install the necessary dependencies for the API. This includes .NET Core SDK(v6) and any additional packages specified in the project's csproj files.
- Database Setup: Run the following Microsoft SQL server scripts to create Users and Cards tables.
```
-- CREATE Users Table
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

-- CREATE Cards Table
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
Users can interact with the Cards RESTful API to manage tasks efficiently. Here's how they can use the API:

- User Registration: The register endpoint(https://localhost:7236/api/Authentication/register) is used to register a user with role as memeber(0) or admin(1)
```json
{
  "email": "admin@example.com",
  "password": "string",
  "role": 1
}
```
- Authentication: Users need to authenticate themselves using their email and password to access the API endpoints(https://localhost:7236/api/Authentication/login). Upon successful authentication, a JSON Web Token (JWT) is generated, which should be included in the Authorization header of subsequent API requests.
```json
{
  "email": "admin@example.com",
  "password": "string"
}
```
- Create Cards: Users can create new cards by providing a name for the task. Optionally, they can include a description and specify a color for the card. Upon creation, the status of the card is set to "To Do"(Status 0) by default. Use the following endpoint to create a card: https://localhost:7236/api/Cards
```json
{
  "name": "card name",
  "description": "card description",
  "color": "silver"
}
```
- Search Cards: Users can search through cards they have access to by specifying various filters such as name, color, status, and date of creation. Additionally, they can limit the number of results and sort them based on different criteria. Use the following endpoint to search for cards: https://localhost:7236/api/Cards/search?Name=card&Color=white&Status=0&Page=1&Size=1&Offset=0&Limit=0
- Retrieve a Card: Users can request a single card they have access to by providing the card's ID. This allows them to view the details of a specific task.
Use the following endpoint to retrieve a card: https://localhost:7236/api/Cards/cardId?cardId=80b2c1e6-7783-4a39-3ea7-08dc2c59dc16
```json response
{
        "id": "38fc170b-3076-4ae3-ea02-08dc2c20761a",
        "name": "card name",
        "description": "card description",
        "color": "white",
        "status": 0,
        "createdAt": "2024-02-12T23:16:01.75",
        "userId": "94764e44-54b1-4e94-b5e4-cf82704eb35b"
    }
```
- Retrieve a list of cards: Users can request a list of cards they have access to. This allows them to view the details of cards.
Use the following endpoint to retrieve a list of cards: https://localhost:7236/api/Cards
```json response
[    
    {
        "id": "d2bc1857-a108-442a-3ea5-08dc2c59dc16",
        "name": "card name",
        "description": "card description",
        "color": "red",
        "status": 0,
        "createdAt": "2024-02-13T06:06:09.993",
        "userId": "06c95e9e-a283-4315-ab21-a2f36c9ba239"
    },    
    {
        "id": "8c027657-d315-47bd-3ea9-08dc2c59dc16",
        "name": "card name",
        "description": "card description",
        "color": "blue",
        "status": 0,
        "createdAt": "2024-02-13T06:07:04.82",
        "userId": "9522870c-e8f8-48cc-83de-cdacf7a73f52"
    }
]
```
- Update Cards: Users can update the name, description, color of a card they have access to. They can also clear out the contents of the description and color fields if needed.
Use the following endpoint to update a card by using PUT command: https://localhost:7236/api/Cards/92b45f06-c2eb-4202-3eaa-08dc2c59dc16
```json
{
  "name": "stringY",
  "description": "stringY",
  "color": "stringY"
}
```
- Update Cards Status: Users can update the status of a card they have access to by passing a request body in JSON format.
Use the following endpoint to update a card status by using PUT command: https://localhost:7236/api/Cards/92b45f06-c2eb-4202-3eaa-08dc2c59dc16/status
```json
{
  "status": 1
}
```
- Delete Cards: Users can delete a card they have access to, removing the task from the system. On successful card deletion, a 204 NoContent status code is returned.
Use the following endpoint to delete a card by using DELETE command: https://localhost:7236/api/Cards/92b45f06-c2eb-4202-3eaa-08dc2c59dc16

By following these steps, users can effectively create, manage, and track tasks using the Cards RESTful API.

## Documentation

You can access swagger documentation by following the link: https://localhost:7236/swagger/index.html
