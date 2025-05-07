# ASP.NET Core WebApi - Flag Explorer API

## About The Project setup: 

At a root folder of **flagExplorer/FlagExplorer** we have a docker-compose yaml file for running the API in DOCKER using the following command and a Dockerfile under this path **flagExplorer/FlagExplorer**, also runs
Tests for the API:

```bash
docker-compose up -d --build flagexplorerapi
```

Then hit this link on your browser, you should see the API Swagger pages:

```bash
http://localhost:35001/swagger/index.html
```


# React JS WEB APP - Flag Explorer WEB

## About The Project setup: 

At a root folder of **flagExplorer/flagexplorer.web** we have a docker-compose yaml file for running the React WEB APP in DOCKER using the following command:

```bash
docker-compose up -d --build
```

Then hit this link on your browser, you should see the WEB HOME PAGE, with a grid of different country flags:

```bash
http://localhost:3000/
```


