
# Npgsql.ADO.Net.DataEntity

Npgsql.ADO.Net.DataEntity is the common library to do the CRUD operations, by using the library you can perform the INSERT, UPDATE, DELETE, SELECT, etc.

You can get the data tables from just a 1 line code.

## NuGet
[![NuGet](https://img.shields.io/nuget/v/ADO.Net.Npgsql.DataEntity.svg)](https://www.nuget.org/packages/ADO.Net.Npgsql.DataEntity)



## Installation

Install Npgsql.ADO.Net.DataEntity with .NET CLI

```bash
 dotnet add package ADO.Net.Npgsql.DataEntity --version 1.0.1
```

Install Npgsql.ADO.Net.DataEntity with Package Manager

```bash
 NuGet\Install-Package ADO.Net.Npgsql.DataEntity -Version 1.0.1
 ```




####C# 　

```C#
using Npgsql.ADO.Net.DataEntity;

namespace SampleCode
{
    public class Program
    {
        private static DataEntity dataEntity = new DataEntity();
        static void Main(string[] args)
        {
            YourMainClass();
        }

        static void YourMainClass()
        {
            //Declare your PostgreSQL database connection string globally.
            //Then you can create a Common Class or use it separately in the classes.
       
	   //Database connection string declared in YourMainClass globally.___START
            dataEntity.pgConnection = "Server=pg_server_IP;User Id=pg_user_iid;Pwd=pg_passwrd;Database=pg_password";
            //Database connection string declared in YourMainClass globally.___END

            var data = dataEntity.ExecuteDataSetFN("fn_api_select_pg_function", "pram1", "pram2");

        }
    }
}
 
```

