# AmericasRoutes.API

AmericasRoutesAPI calculates multiple routes for Truck drivers starting from USA to specified destination (country code)

To Test simply send the request below:
`https://usroutes.azurewebsites.net/{location}` where location is the Country code for one of the countries in Americas

    List of Countries Registered in the Database:
    - CAN
    - USA
    - MEX
    - BLZ
    - GTM
    - SLV
    - HND
    - NIC
    - CRI
    - PAN

## Presentation
For Presenting the data to the end user running the query minimal API was built using .NET 6.0, this api has single  end-point which is taking validated Location Code, running query to GremlinServer and returning a JSON in the form

```
{
  "destination": "BLZ",
  "routes": [
    [
      "USA",
      "MEX",
      "BLZ"
    ],
    [
      "USA",
      "MEX",
      "GTM",
      "BLZ"
    ]
  ]
}
```
above is the result of the query https://usroutes.azurewebsites.net/BLZ

Endpoint returns three shortest Traversals from USA --> Destination. In case there are less than 3 It does not return empty array inside Routes

```
{
  "destination": "PAN",
  "routes": [
    [
      "USA",
      "MEX",
      "GTM",
      "HND",
      "NIC",
      "CRI",
      "PAN"
    ],
    [
      "USA",
      "MEX",
      "GTM",
      "SLV",
      "HND",
      "NIC",
      "CRI",
      "PAN"
    ],
    [
      "USA",
      "MEX",
      "BLZ",
      "GTM",
      "HND",
      "NIC",
      "CRI",
      "PAN"
    ]
  ]
}
```

## Infrastructure

For Infrastructure Azure CosmosDB with Gremlin was used, Using graph database was very suitable for projecting political map of Americas.
