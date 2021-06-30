# FoodCounter
Food Calorie Counter
  
Project using Dapper microORM and Dommel.  
DB available for now : Sqlite  
Migrations used with FluentMigration.  
Postman collection available for sharing data live tests with collaborators.  

This project coantains a CI configuration for GithubActions that test for now :  
- Builing project
- Run unit tests
- Run locally the API
- Run functional tests with Postman
- Count the lines of codes written (useless but essential ^^ )
  
Coming steps are :
- CI Check code covered by unit tests
- CI Analyse quality code (SonarQube)
- CD Deployment of API (from branches to environement "dev", "preprod" and "prod")
- FrontEnd developement (React or Blazor)
- Functionnal tests (Selenium)
- CD Deployment of FrontEnd (from branches to environement "dev", "preprod" and "prod")
- Doxygen documentation generation
