# ASP.NET Core 6 - Currency Exchange Trade Web API

This project provides storing exchange trade by clients integrated with the latest currency exchange rate API.


## Authentication

It uses basic authentication. It has swagger UI and to test can use the swagger UI or postman with username: admin and password: admin credentials.

Credential information is in appsetting.json if want can change it from there.

```c
  "Credentials": {
    "UserName": "admin",
    "Password": "admin"
  }
```

## Exchange API 

Currency exchange rate information gets from exchangeratesapi.io. It has a registered API Key in appsetting.json.

```c
  "ExchangeAPI": {
    "Url": "https://api.apilayer.com/exchangerates_data",
    "Key": "*****"
  }

```

## Database

Entity Framework Core is used for MSSql connection and queries. The initial SQL script is in the repository (Initial SQL Query.sql) for creating tables. The connection string can change in appsetting.json for use (shown below).

```c
  "ConnectionStrings": {
    "DefaultConnection": "*****"
  }

```
## Sample Usage

There are two different types of API usage:

### Trade with symbol
The first usage is the version that uses common currency symbols as input. Currency symbols are used for this API "from" and "to" inputs. These symbols can be controlled from the symbol API ("/api/CurrencySymbol" GET method -symbol parameter).
Usage: "/api/Trade" POST method, sample request is shown below.

### Trade with Id
Another usage is the version of currency IDs saved as input. The saved currency IDs are used for this API  "from" and "to" inputs. These IDs can be checked from the symbol API ("/api/CurrencySymbol" GET method - id parameter).
Usage: "/api/Trade/withId" POST method, sample request is shown below.

Both APIs use the same procedures in the backend. The only difference is mapping the symbol id and symbol name. ID version can be used for less data transfer with external systems.

The sample requests are shown below. Also seen in the swagger UI. If successful API saves the trade in the database and returns exchanged amount and exchange rate.

### Currency symbol API
To get information about currency symbols for using in from/to parameters, can use the "/api/CurrencySymbol" GET method (that is integrated with exchangeratesapi.io). In that API there is a symbol value or Id value. Another value is the definition.

```c

# Sample Request for symbol usage:
POST "/api/Trade"
{
    "clientId": 1,
    "tradeDate": "2022-10-09T13:01:31.814Z",
    "from": "EUR",
    "to": "USD",
    "amount": 10,
    "tradeType": "Buy"
}
//from and to symbol values get from api/CurrencySymbol - symbol value.
//"tradeType": "Buy" or "Sell"

 # Sample Request for Id usage:
POST "/api/Trade/withId"
{
    "clientId": 1,
    "tradeDate": "2022-10-09T13:01:31.814Z",
    "from": 1,
    "to": 2,
    "amount": 10,
    "type": 0
}  
//from and to Id values get from api/CurrencySymbol - id value.
//"tradeType": Sell=0 Buy=1 (Default 0)

# Success response message:
10 EUR was exchanged for 9.71246 USD at a rate of 0.971246.


# Currency symbol output:
GET "/api/CurrencySymbol"
{
    "id": 1,
    "symbol": "EUR",
    "definition": "Euro"
}
```

## Logging
For logging, Serilog is used. And logs are stored MSSql database that the same database for using Trade APIs.
Log can configure in appsettings.json. First usage the log table automatically creates.

```c
"Serilog": {
      ....
      "Args": {
          "connectionString": "****"
      ......
}
```

## Caching
For caching, MemoryCache Library is used. Currency exchange rates keep in the cache for 30 minutes. 

Currency symbols are stored in the database (It also has a cache, too). A task job checks the exchange API for new currencies daily. 

## Bussiness Rules
For client trade has a limit of an hour, using business logic with stored data.

If clients will use the API on different servers, the project has IP Rate Limiting. It blocks requests from the same IP an hour (after ten times). 

If API uses the same server with different clients, that feature must disable with the below parameter saved in appsetting.json. Business logic with stored data has already run in service middleware.

At the same time, the feature has an IP white list. Local IP has been saved in the white list for don't block local usage.

 ```c
"IpRateLimiting": {
      "EnableEndpointRateLimiting": true,
      ....
      "IpWhitelist": [ "::1" ],
      ......
}
```

## Input Validator
A fluent validator is used for an input validator. It checks null, empty, or length rules. Any input except that rules automatically return an error response message.

## Unit Test
N-unit is used for unit tests. It tests null, not null, successful, null input, and invalid input test cases.

And also a MOQ library is used for mock tests.


## Error Message

When there is something goes wrong, some error message return. Except for automatically returning error codes (400, 401, etc.), application errors (500) return as a user-friendly exception. 
Return errors like :
```c
NoValueException: "There is no value for this trade!"  - when some problem with the exchange rate.
NoSuccessExceptionException: "Trade didn't succeed!"  - when trade didn't save in the database.
NoValidValueException: "There is no valid value for this trade!"  - when the exchange rate didn't get the API. 
ReachedMaximumException: "This client reached the maximum trades per hour!" - when a client trades 10 times an hour. (If the IP limit rule is disable)
NoSymbolException: "Symbol not found!" - when there is no currency symbol.
InvalidCurrencyException: "Currency value is invalid!" - when to or from inputs are null or of different lengths (the validator blocks those inputs, it is used for unit tests).

```

