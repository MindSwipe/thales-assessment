# Thales Assessment
This repository holds the code for my implementation of the assessment for my Thales Group application

## Building and Running
Before being able to run the applications you must first apply the EF Core migrations, to do that you must have the .NET 7 cli installed (comes with the .NET 7 SDK found [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)) as well as the .NET EF Core Tools installed, to do that you must open a PowerShell window and enter

```dotnet tool install --global dotnet-ef```

After that is done create a new (empty) SQLite DB file and then open the `appsettings.json` file from the `ThalesAssessment.Server` project and set the `SqlitePath` to your newly created file. Now open a PowerShell window and navigate to the directory containting `ThalesAssessment.DataAccess.csproj` and execute the following command:

```dotnet ef database update --startup-project ..\ThalesAssessment.Server\ThalesAssessment.Server.csproj```

The required projects should build and EF Core should successfully setup your DB file for use. It is now possible to run the Server and use the Swagger interface to explore the API. Alternatively you can run both the Server and Client at the same time and use the Client to interact with the server, make sure that the Client is pointing at the correct `ApiUrl` in it's `appsettings.json`, by default the Server will bind to `https://localhost:7000` and the Client will try to connect to it. If for some reason HTTPS doesn't work you can also try switching to HTTP since neither the Client nor the Server have the explicit need to run on HTTPS (something that should be implemented if the application were to be used in a productive environment, alongside authentication/ authorization)

## .NET 7
Currently the entire Solution targets .NET 7, this is because I oversaw the line stating that you are currently using .NET Framework 4.7.1. Porting the code itself to .NET Framework 4.7.1 now would be quite some work, since the entire codebase uses C# features not available on .NET Framework 4.7.1 (namely file scoped namespaces and nullable reference types), and also uses .NET Platform Extensions which are not entirely available in .NET Framework 4.7.1. Also, the switch to "legacy" Entity Framework from EF Core would require some re-architecting of the data access project.

## Reflection
The overall structure and implementation details of the solution should showcase my C#/ .NET abilities, at the same time I purposefully did not "overdo" the implementation to an extreme degree where I'm playing "Tech Jargon Bingo", which means there are some things I would do different in a "real" enterprise application as well as some things missing which I would consider a necessity in a production application. I will now list some points and go further in depth.

1. Introduce IOC into the Client  
The client currently does not use IOC nor does it use Dependcy Injection. In an application which is to be used productively I would definitely use IOC and DI to further decouple individual views and their viewmodels from each other, as well as use actual constructor injection to supply services (namely `ApiService` here).

2. Further abstract things away  
Currently quite a bit of logic is done in the controllers themselves, I would preferably like to get that logic out of there and move it to a service layer, these services would be implementations of interfaces, not only for DI, but also to aid in UnitTesting and mocking business logic. Same goes for the `ApiService` and any future services used in the Client. Furthermore using something like `AutoMapper` or even source generators to implement the API models would greatly reduce the potential maintenance required when extending domain entities.

3. Testing  
A definitve must before this application could be used are automated tests, I purposefully left these out since I don't believe they are required to show my C#/ .NET skills, but I will elaborate on how I'd implement them here. I'm a huge fan of DI, even in the automated tests I'd inject (potentially mocked) services needed. For example, the `AssessmentContext` would be injected in a way so each automated test gets its own DB so that collisions in test data between tests cannot happen. Furthermore the eventual services (as described in #2) would also be injected into tests for testing.

## Images
![Empty client](/images/v0.0.1/emptyClient.PNG)

![Create New Person dialog](/images/v0.0.1/createNewPerson.PNG)

![Client with users and role](/images/v0.0.1/clientWithUsersAndRole.PNG)