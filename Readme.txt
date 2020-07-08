https://dotnet.microsoft.com/learn/aspnet/microservice-tutorial/docker-file

dotnet new webapi -o montyHallService --no-https

dotnet run

docker build -t montyhallservice .

docker run -it --rm -p 3000:80 --name montyhallsolution montyhallservice

https://www.codeproject.com/Articles/1276639/Microservice-using-ASP-NET-Core

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1, https://www.tutorialsteacher.com/core/dependency-injection-in-aspnet-core

    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": false,
      "launchUrl": "api/montyHall",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "MontyHallSolution": {
      "commandName": "Project",
      "launchBrowser": false,
      "launchUrl": "api/montyHall",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "http://localhost"
    },

services:
  montyhallapi:
    image: ${DOCKER_REGISTRY-}montyhallapi
    build:
      context: .
      dockerfile: MontyHallAPI/Dockerfile
    ports:
        - "8002:80"
    environment:
    - ASPNETCORE_ENVIRONMENT=Development
    - ASPNETCORE_URLS=http://0.0.0.0:80

  apigateway:
    image: ${DOCKER_REGISTRY-}apigateway
    build:
      context: .
      dockerfile: APIGateway/Dockerfile
     ports:
        - "8001:80"
    environment:
    - ASPNETCORE_ENVIRONMENT=Development
    - ASPNETCORE_URLS=http://0.0.0.0:80

        "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "weatherforecast",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },

    https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot

    https://microservices.io/patterns/security/access-token.html

    https://microservices.io/patterns/apigateway.html -
    
    https://fullstack.app/posts/2019/08/20/api-gateway-with-aspnet-core-and-ocelot.html

    https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/

    https://www.c-sharpcorner.com/article/building-api-gateway-using-ocelot-in-asp-net-core-part-two/

    https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/

    https://medium.com/swlh/building-net-core-api-gateway-with-ocelot-6302c2b3ffc5

    
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer