--References 
https://dotnet.microsoft.com/learn/aspnet/microservice-tutorial/docker-file

https://www.codeproject.com/Articles/1276639/Microservice-using-ASP-NET-Core

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1, https://www.tutorialsteacher.com/core/dependency-injection-in-aspnet-core

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

--HOW TO Steps

dotnet new webapi -o montyHallService --no-https
dotnet run
docker build -t montyhallservice .
docker run -it --rm -p 3000:80 --name montyhallsolution montyhallservice
