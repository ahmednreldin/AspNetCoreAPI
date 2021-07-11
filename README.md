
# The Core Project
The Core project is the center of best practice design,
 and all other project dependencies should point toward it. 
 As such, it has very few external dependencies.
The one exception in this case is the System.Reflection.TypeExtensions package,
 which is used by ValueObject to help implement 
 its IEquatable<> interface.
 The Core project should include things like:

* Entities
* DTOs
* Logger
* Automapper
* ContentNegotation

## Technologies implemented:
* ASP.NET 5.0 (with .NET Core 5.0)
* ASP.NET MVC Core
* ASP.NET WebApi Core with JWT Bearer Authentication
* ASP.NET Identity Core
* Entity Framework Core 5.0
* AutoMapper
* Swagger UI with JWT support


## Architecture:
Full architecture with responsibility separation concerns, SOLID and Clean Code
Domain Driven Design (Layers and Domain Model Pattern)
Domain Events
Domain Validations
Repository



 ## output format 
 * Content nagation 
 * serilize to json , xml , CSV 

 ## Logger
 configure logger service using NLog library

 ## Host and deployment 
 * Configure IIS Integration 

 ## Global Error handling 
 using Custom middleware and output error in json format 
