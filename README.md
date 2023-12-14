# Mini-Twitter
Welcome to the Mini-Twitter System repository based on the Clean Architecture. The application's primary functions include user management, tweeting, following/followers functionality, retweets, replies, likes, and a timeline displaying tweets from a user's followers.

## About the Project
The Mini-Twitter System is designed using the principles of Clean Architecture, which emphasizes separation of concerns, maintainability, and scalability. The architecture is divided into distinct layers, each with a specific responsibility:

Presentation Layer: This layer includes the user interface components, such as the web application or mobile app. It interacts with the Application layer to provide a user-friendly interface for restaurant staff and customers, I'll be using ASP.NET Core Web API as presentation Layer In this case but since we are working using Clean Architecture we could add UI like Angular and it will work just perfect.

Application Layer: The Application layer contains the use cases and business logic. It orchestrates the interactions between the Presentation and Domain layers, ensuring that business rules are followed and use cases are executed.

Domain Layer: The heart of the system, the Domain layer, encapsulates the core business logic and entities. It defines the fundamental concepts of the restaurant domain, such as menus, orders, reservations, and staff roles.

Infrastructure Layer: The Infrastructure layer deals with external concerns, such as database access, API integrations, and external services. It supports the higher layers without being tied to specific implementation details.

## Technologies Used

Technologies utilized in building the API include ASP.NET Core Web API, EF Core, Fluent API, Mapster, Mediator, CQRS pattern, Repository pattern, Fluent Validation, Serilog, Output Cache, OData, JWT for authentication and authorization, and PostgreSQL. The API is structured based on the Clean architecture principles.

## API Documentation

I have included Postman API documentation within the repository for testing purposes. While Swagger is available, please note that complex OData queries may not run unless Authorization is disabled. Therefore, I recommend using Postman for testing.
