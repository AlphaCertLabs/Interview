# AlphaCert Technical Screening
# Candidate

## Overview

In this folder are two solutions `CanWeFixIt` and `YesWeCan`. The general idea is
that you update the `CanWeFixIt` solution so that it conforms to the 
requirements as stated in this document below.

The `YesWeCan` solution contains a console application that will call the 
`CanWeFixItApi`, an ASP.NET API, and check the various endpoints for expected
behaviours.

The simplest way to achieve this is to just run them both at the same time 
using your preferred development tooling (Visual Studio, Rider, dotnet cli). 
Ensure the api project is up and then run the console application.

The projects target `net5.0` so ensure you have the .NET 5 SDK installed.

If the api returns the correct payloads at the appropriate endpoints then you
will see console output with passed tests:

```
=== MarketData Verification ===
Active Test: Passed
Content Test: Passed
Count Test: Passed
========================================
=== Instrument Verification ===
Active Test: Passed
Content Test: Passed
Count Test: Passed
========================================
=== MarketValuation Verification ===
Active Test: Passed
Content Test: Passed
Count Test: Passed
========================================
Press any key to exit...
```

If the api is up but returns incorrect payload responses you might see 
`Failed` instead of `Passed` for some tests.
If the api is not up or there is some other communication issue you will see a
exception message and stack trace in the console output.

```
No connection could be made because the target machine actively refused it. (localhost:5010)
   at System.Net.Http.ConnectHelper.ConnectAsync(Func`3 callback, DnsEndPoint endPoint, HttpRequestMessage requestMessage, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.ConnectAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
...
```

## Actions and General Guidelines

The `CanWeFixItApi` project is currently incomplete and contains a broken 
implementation of the final requirements. There are also various kinds of bugs
and only partial features.

Fix as much as possible and implement features according to the requirements.

You may refactor anything in the `CanWeFixIt` projects, including completely 
deleting and replacing them. You may use and consume any appropriately licensed
third party libraries you are comfortable with. If you do go this route, make
note of the url and port that is expected in the console tester application 
(`http://localhost:5010`).

The provided solution creates an in-memory SQLite database with test data and 
an example Dapper based DatabaseService. You may fix this or swap this out with
any other database and ORM that you are comfortable with. 

You are welcome to check the code in the `YesWeCan` console application and
even encouraged to replicate the test criteria in your own unit tests.

## Requirements

The api should have three endpoints:

* `GET v1/instruments`
* `GET v1/marketdata`
* `GET v1/valuations`

All endpoints should return json payloads matching the following criteria.

`v1/instruments`: A list of `Instruments` from the database that are currently
`active`.
Example:
```json
[
  {
    "id": 2,
    "sedol": "Sedol2",
    "name": "Name2",
    "active": true
  },
  ...
]

``` 

`v1/marketdata`: A list of `MarketData` from the database that are currently
`active` and also have a calculated `instrumentId`. The `instrumentId` should 
be calculated by looking up the `sedol` of the marketData against the `sedol` of 
the instrument. So "Sedol2" would be mapped against InstrumentId 2 since that 
instrument has the `sedol` of "Sedol2". If there is no matching instrument it
shouldn't be returned by this endpoint.
Example:
```json
[
  {
    "id": 2,
    "dataValue": 2222,
    "instrumentId": 2,
    "active": true
  },
  ...
]

``` 

`v1/valuations`: A list of `MarketValuation` with a single item in the list.
This item should have a `name` of "DataValueTotal" and a `total` of the sum
of all currently `active` `MarketData`.
Example:
```json
[
  {
    "name": "DataValueTotal",
    "total": XXXXX
  }
]

``` 
