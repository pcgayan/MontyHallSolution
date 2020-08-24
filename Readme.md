
## Monty Hall Game Simulator
![alt text] (https://github.com/pcgayan/MontyHallSolution/blob/master/Simulator.jpg?raw=true)

## Mandatory notes
1. System is completly devleoped on Visual studio 2019 Community edition.
2. All backend and client compnents are hosted as dokers on local docker instance.
3. Seperate auth server with JWT token support is added for authentication and session managment.
4. Seperate api-gateway add to encapulate docker instances of auth server, client web server, backend server. All traffic is initated via this.
5. React redux visual studio template is used with type scripting to develop entire solution on Visal studio 2019 Community edition.
6. Docker compse used to manage and hosting configurations for docker instances.
7. Backend serivce calls supported with api-gateway and JWT token security

## HOW TO Steps
1. Make sure you have dockerhub installed from https://hub.docker.com/ with a valid login.
2. Pull the root solution from https://github.com/pcgayan/MontyHallSolution
3. From Visal studio 2019 Community edition open https://github.com/pcgayan/MontyHallSolution/blob/master/MontyHallSolution.sln
4. Build the solution.
5. Run the docker-compose. This will create docker instances on local machine an host them on local docke hub instance. (all docker instance are linux based for low foot print)
6. Open brower window and brows to http://localhost:8001/
7. Once it is loaded with "Grant Player Access" where authenticated for a demo user (hardcoded on client) with JWT token click on "Simulate" link
8. Click on "Simulate" button

## Limitations
1. Add number of simulations and switch door is not accepted as user params from Client web. Instead you can edit https://github.com/pcgayan/MontyHallSolution/blob/master/ClientWebApplication/ClientApp/src/components/FetchMontyHallSimulatorData.tsx 
line 44, 45 to add those.
2. After trigger hit of Simulations button you have to refesh the page simulate again. 

## Architecture
![alt text] (https://github.com/pcgayan/MontyHallSolution/blob/master/Architecture.jpg?raw=true)

## Deployment
![alt text] (https://github.com/pcgayan/MontyHallSolution/blob/master/Deployment.jpg?raw=true)

## Implemented Use Cases
1. User Can start a new game and he ha/she have to complete the game before starting a new game with his/her id
2. USer can not go back on steps of the game. Only can move forward on game.
3. USer has limited time to complete game after that user is thrown out of the game.


## Used References
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

https://docs.microsoft.com/en-us/visualstudio/containers/container-tools-react?view=vs-2019

https://www.codeproject.com/Articles/3132485/CRUD-Operation-using-ASP-NET-CORE-2-2-and-React-Re

https://redux.js.org/introduction/getting-started
