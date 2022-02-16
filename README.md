# UNIXstore.Api
A .NET REST API using C# built to store UNIX commands
[![api-swagger.png](https://i.postimg.cc/KYyhkVLy/api-swagger.png)](https://postimg.cc/MntFg9ds)

technologies used: 
C#, Docker, Kubernetes, Postman, xunit (unit tests), MongoDB

Features and concepts: 

1) 
Dependency injection:
[![dependency-injection.png](https://i.postimg.cc/59HJrwfT/dependency-injection.png)](https://postimg.cc/9zjsRwRY)
The dependency inversion principle states that our controller should depend on an abstraction/interface that all other dependencies will implement.
This feature will help in scalability as the controller here has no idea what dependencies it is working with. In the future, i can simply "inject"
the dependency and do not need to change the implementation of controller. 

Decoupling implementations from each other makes testing, upgrades easier. 

2) 
DTOs (Data transferable objects):
[![dtos.png](https://i.postimg.cc/9QDyRg21/dtos.png)](https://postimg.cc/WdPdxnhJ)
We need a contract between the clients and out API and DTOs are that contract. This is almost like type safety/checking where both the api and client must respect the contract. 
We do not want anything other than a string for command for example.

3) 
What makes this API RESTful:
[![rest.png](https://i.postimg.cc/50Qx8v0h/rest.png)](https://postimg.cc/BLs90j2p)
A REST api must support at minimum 4 calls:
PUT: modify an existing entry
POST: add a new entry
GET: get an existing entry
DELETE: delete an existing entry

4) 
Persistence of entities using MongoDB:
MongoDB hosted on docker was used to store the unix commands. 

5)
Deployment to Docker:
public docker pull command: docker pull deepan19/unixstore
link to docker hub page: https://hub.docker.com/r/deepan19/unixstore

[![docker.png](https://i.postimg.cc/761qB439/docker.png)](https://postimg.cc/1ntxtLYV)

Why docker? advantages of docker:
Deployment is challenging as the client's PC may not have the OS, environment, dependencies, .NET runtime, mongodb driver that are required for the api to run in production.
Also we have a MongoDB database and its own dependencies that needs to run in production. 
A docker container contains everything our RESTapi needs to run in another PC
Our RESTapi -> docker file -> build a docker image using docker engine -> push a docker container

6)
Kubernetes:
the kubernetes control plane schedules, load balances, selfheals, allocates pods (collection of containers) to nodes based on cpu, scales up containers:
turns desired state into actual state

self heal capability of API container:
[![kubernetes-self-heal.png](https://i.postimg.cc/mrxB3kvJ/kubernetes-self-heal.png)](https://postimg.cc/nX1ypFcG)

self-heal capability of MongoDB service:
[![Screenshot-2022-02-14-233257.png](https://i.postimg.cc/k4wgJ924/Screenshot-2022-02-14-233257.png)](https://postimg.cc/Hccgzq4D)

7) unit testing using xunit and postman
unit testing is  to make sure individual methods work in isolation without external dependencies. Can prevent expensive bug fixes later on as we scale. 
[![unit-tests.png](https://i.postimg.cc/13HmcsKK/unit-tests.png)](https://postimg.cc/WddcsRCD)

testing all requests using postman:
[![test-postman.png](https://i.postimg.cc/fW9MqxKy/test-postman.png)](https://postimg.cc/grG9nL1b)

TDD(test driven development) was used for testing: write a test before writing enough production code to make the failting test pass. write a failing test->write code to make it pass -> Refactor

