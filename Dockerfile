FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore \
    && dotnet test ./test/OnlineShop.Customer.Grpc.Test/OnlineShop.Customer.Grpc.Test.csproj >> /app/test_results.txt \
    && dotnet test ./test/OnlineShop.Order.Api.Test/OnlineShop.Order.Api.Test.csproj >> /app/test_results.txt \
    && dotnet test ./test/OnlineShop.Product.Grpc.Test/OnlineShop.Product.Grpc.Test.csproj >> /app/test_results.txt 
CMD cat /app/test_results.txt