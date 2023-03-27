# Improvements made for this technical challenge

In order to present a technical proposal that could serve as the basis for a real service, I've decided to make the following improvements:
- In the Backend project. TechChallenge.Api I've migrated the original .net core version from 3.1 (without support) to 6.0 because it's a LTS version
- I've added database persistence, using EF using Code First approach
- The DB will be generated automatically when starting the service. To deactivate the automatic management of the DB, the value of the "AutoMigrate" variable must be changed to "false"
- Within the User entity, the following improvements have been made:
  - Added ID and basic traceability data
  - The "UserType" attribute now uses an Enum data type which makes it easier to type actions and minimizes errors
- Implementation of Repository, UnitOfWork, Dependency injection and MVVM design patterns
- Improvement of the architecture to decouple the presentation, application and infrastructure layers
- Creating base classes for the data models used in the presentation layer and the entity data that is managed in the infrastructure layer


For the tests:
- Implementation of a version of the in-memory database for set used in all tests
- Creation of tests for the infrastructure, application and presentation layers taking into account the scope of each one of them 


# Assumpiotns to execute the service

This technical challenge as been done keeping a account following assumpiotns:
- The environment where the solution is run has an instance of SQL Server "(localdb)\\MSSQLLocalDB". Otherwise, you must change the connection string in the corresponding appsettings file


# Recommended improvements that have not been implemented

Due to lack of time, the improvements detailed below have not been implemented
- Logger
- Use of environment variables, in order to comply with the 12factors principles


# Backend.TechChallenge

A developer went on vacation and several issues arose in the project that needed to be resolved.

The webAPI works, but it has many flaws in architecture, code quality, testing and etc.

We need you to refactor the code of this project.

Remember to treat it as a refactoring of a final code, which will go to production and has to be as good as possible.

## What we expect to find in the Challenge

In the result of the refactoring we would like to find:

- Object-oriented programming.

- An architectural model. The one that you consider most applicable or that you have more experience.

- The Clean Code concepts that you consider important.

- The best unit tests you can do and with the code coverage you consider important.

- A polymorphic system or some design pattern. The one that fits the most or that you like the most.

- Transversal/crosscutting concepts that you consider important to a webAPI in production such as logging, validation, exception handling...

- REST concepts, SOLID principles and good practices applied.

- And you want to take more time in the challenge you can change the type of persistence (currently TXT file), but consider that your new implementation should be working.

Do the best you can.


## How much time do you have for the challenge

It is a small WebAPI and normally a good refactoring can be done in about 2 hours.

But we know that each one has its speed and in general we prefer to prioritize the quality of delivery, so there is no time limit.


## As you must deliver the challenge once finished

For you to do the challenge you must create a branch or a fork from this one (main).

And once you have finished the refactoring you can send us:

* The link of your branch on Github
* The link of a PR from your branch to the original repo
