If you have Mongo in your local then use the same connection string
but if you don't have then run docker-compose


"MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    //"ConnectionString": "mongodb://root:password@localhost:27018/?authSource=admin",  ////RUN Docker-compomse then use this connection string
    "DatabaseName": "mytempdb"
  },




 ////DOCKER-COMPOSE
 
//docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
//docker-compose -f docker-compose.yml -f docker-compose.override.yml down -fv


version: '3.4'

services:
  tt.deliveries.web.api:
    image: ${DOCKER_REGISTRY-}ttdeliverieswebapi
    build:
      context: .
      dockerfile: Web/TT.Deliveries.Web.Api/Dockerfile
  
  Mongo:
    image: mongo
    environment:
      - MONGO_INITDB_ROOT_USERNAME=root
      - MONGO_INITDB_ROOT_PASSWORD=password
      - MONGO_INITDB_DATABASE=mytempdb
    restart: always
    ports:
      - 27018:27017
    
  tt.deliveries.scheduler.functions:
    image: ${DOCKER_REGISTRY-}ttdeliveriesschedulerfunctions
    build:
      context: .
      dockerfile: TT.Deliveries.Scheduler.Functions/Dockerfile

