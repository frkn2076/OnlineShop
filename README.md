# OnlineShop

### OnlineShop is built with Microservices below: 
* Order
* Product
* Customer

#### Single endpoint of Api serves order information by including customer and product details.
<br>

![OnlineShop_Uml_Diagram](https://github.com/frkn2076/OnlineShop/assets/38755260/2cb04a13-350d-4ccd-be04-dee590f05846)

<br>

## Setup
* Install Docker
* Install .Net 8 SDK (For Https Certificate Creation)

<br>

## Create Https Certificates Locally
* **Windows:**
  * cd %USERPROFILE%\https
  * dotnet dev-certs https -ep ./OnlineShop.Order.Api.pfx -p sEcREtpaSsWord1
  * dotnet dev-certs https -ep ./OnlineShop.Customer.Grpc.pfx -p sEcREtpaSsWord2
  * dotnet dev-certs https -ep ./OnlineShop.Product.Grpc.pfx -p sEcREtpaSsWord3
    
* **Linux Or MacOS**
  * cd ~/https
  * dotnet dev-certs https -ep ./OnlineShop.Order.Api.pfx -p sEcREtpaSsWord1
  * dotnet dev-certs https -ep ./OnlineShop.Customer.Grpc.pfx -p sEcREtpaSsWord2
  * dotnet dev-certs https -ep ./OnlineShop.Product.Grpc.pfx -p sEcREtpaSsWord3


## Run
* **git clone https://github.com/frkn2076/OnlineShop**
* **cd .../OnlineShop**
* **docker-compose up** ('docker-compose down' to remove containers, networks, volumes, and images created by up)
  * *This might that a few minutes to complete.*
  * It will be running all tests and output the result.


## Api Call through Docker
* https://localhost:5001/order?from=2024-02-21T12:30:45.000Z&to=2024-04-25T12:30:45.000Z

#### DONE!

<br></br>

# Developer Path

### Setup
* Install your favorite IDE (VS, VSCode etc.)

### Initiate Database Instances through Docker
* docker run -p 5442:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=example -e POSTGRES_DB=orderdb -d postgres:13-alpine
* docker run -p 5443:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=example -e POSTGRES_DB=customerdb -d postgres:13-alpine
* docker run -p 5444:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=example -e POSTGRES_DB=productdb -d postgres:13-alpine

### Create Database Migration Files If You Have Any Changes
* Navigate the project that you have database changes: cd ../OnlineShop/source/OnlineShop.Order.Api
* dotnet ef migrations add {Your_Migration_Name} (will create Migration files)
* Projects will be applying these migrations during runtime once you start them.

### Run and Debug
* Run each microservices seperately according to 'https' profile under Properties/launchSettings.json
* You can use swagger documentation about calling API.
* Or [Prepared Http file](https://github.com/frkn2076/OnlineShop/blob/develop/source/OnlineShop.Order.Api/OnlineShop.Order.Api.http) to use sample call.

### Unit Tests
* **CMD:**:
  * cd .../OnlineShop.Customer.Grpc.Test
  * dotnet test ./OnlineShop.Customer.Grpc.Test.csproj
* Or **VS:** Test -> Run All Tests

### Integration Tests
* **VS:** Run explicitly since skipped (Ensure you have both Customer.Grpc and Product.Grpc running locally for integration tests since we don't have a server keeps grpc services up and running)
