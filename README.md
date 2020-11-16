
# RETAIL STORE BILLING API

A .NET Core WEBAPI application which performs retail store billing operations.

* This solution is implemented in ".NET CORE 3.1" Framework, EF Core, SQL Server 2019, Swagger and on "Microsoft Visual Studio Community 2019, Version 16.7.5" IDE.


PROJECT STRUCTURE
-----------------

This solution has Server side API & class library projects as listed below. Used all the possible latest .Net core features to implement this.

  Server Module: 
  -------------
  The server module is designed & implemented in much more loosely coupled design, to ensure the seperation of concerns. This module has 3 projects as listed below.
  
  1. RetailStoreAPI    -> A .NET Core restful API 
  2. StoreServices     -> Services layer between API &  Data access Layer 
  3. StoreDAC          -> The EF CORE Data access layer

  Client Module:
  --------------
  
  Currently, the Swagger UI is integregated for Unit testing this Application.
  
  
ARCHITECTURE & DESIGN DETAILS
-----------------------------

Followed 3-tier architecture for implementing the Server side module. Running this project automatically launches the Swagger UI as default page on Browser.


Highlights:
-----------

Below features are used for implementing this Application. 

  * N-Tier Architecture
  * .NET CORE RESTFUL API
  * Attribute Filters
  * Centralized Exception handling support
  * Input Model Validation configured at App level
  * Auto Mapper
  * ILogger
  * MVC pattern for Restful API
  * Attribute based Routing
  * Output Caching
  * Swagger Unit tests
  * Dependency Injection & IOC
  * Unit of Work design pattern
  * Repository Pattern
  * Normalized DB SQL Server Schema along with some basic records
  * Asynch Action methods
  * Linq queries  
  * String Interpolation
  * Server side validations
  and 
  * Several other features... 


Application Swagger UI snapshot:
-------------------------------

Click the below link to see the API's SwaggerUI image.

https://github.com/JagadeeshK786/RetailStoreBilling-.Net-Core-REST-API-with-Swagger/blob/main/RetailStoreBillingApp_SwaggerUI_Snapshot.jpg

Steps to build & run the Application:
-------------------------------------

1. Down load the source code & DB Scripts onto local folder
2. Run "~db_scripts\RetailStore.sql" script on SSMS to create the database
3. Open "~\source\RetailStoreAPP\RetailStoreAPP.sln" solution on VS2019
4. Build the solution
5. Run the application to launch the Swagger UI on the browser
6. The Swagger UI is self explanatory to proceed with API testing further


Please reach me @ "jagadeesh.kommineni@gmail.com" for further details on this Project implementation. Happy to have a conversation with you.

Thanks in Advance and, any comments or suggestions are very much appreciated.


Jagadeesh Kommineni
