## To run app in VS:
* Choose Docker Compose as startup project.
* Run Application

## To run app from command line:
* Open terminal in main directory
* ```docker-compose -f ./docker-compose.yaml up -d```

## To make new migration:
* Install dotnet ef tools globally
* dotnet ef migrations <<MigrationName>> --project "KleskaProject.Infrastructure" --startup-project "KleskaProject.API"
