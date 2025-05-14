# Helmes back-end

### Prerequisites
- .NET 9 SDK
- PostgreSQL
- Docker


### To build:
1. Open docker
2. In the terminal, navigate to the root directory of the project (.../HelmesTask), and run the following command:
~~~ sh
docker-compose up -d
~~~


### To use the application:
Run the WebApp project.
Navigate to http://localhost:5091/api/forms
Start the front-end application (follow front-end README instructions).


dotnet tool install --global dotnet-ef

dotnet ef migrations add --project DAL --startup-project WebApp --context AppDbContext InitialCreate
dotnet ef migrations --project DAL --startup-project WebApp remove

dotnet ef database --project DAL --startup-project WebApp update
dotnet ef database --project DAL --startup-project WebApp drop

docker-compose up --build
