## EF Core Bug with Application Insights on Azure Functions

After upgrading Microsoft.EntityFrameworkCore dependecy to 3.1.4 or higher on an Azure Function application, the SqlDependency in Application Insights stopped working.

Repo contains 4 scenario:

* Working SqlDependency without EF Core reference - ADO.NET access  
  Branch [master](/../../tree/master)
* Working SqlDependency with EF Core (3.1.3) reference - ADO.NET access  
  Branch [efcore3-1-3](/../../tree/efcore3-1-3)
* Not working SqlDependency with EF Core (3.1.8) reference - ADO.NET access  
  Branch [efcore3-1-8](/../../tree/efcore3-1-8)
* Not working SqlDependency with EF Core (3.1.8) reference - EF Core access  
  Branch [using-efcore](/../../tree/using-efcore)
   
  
  
