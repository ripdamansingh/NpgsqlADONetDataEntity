

# ADO.Net.Npgsql.DataEntity

ADO.Net.Npgsql.DataEntity is the common library to do the CRUD operations, by using the library you can perform the INSERT, UPDATE, DELETE, SELECT, etc.

You can get the data tables from just a 1 line code.

## Your Support is Appreciated!
<a href="https://www.buymeacoffee.com/damanbedi2k" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" style="height: 37px !important;width: 170px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

<p dir="auto">Your ⭐ on <a href="https://github.com/ripdamansingh/NpgsqlADONetDataEntity">this repository</a> also helps! Thanks! 🖖🙂</p>



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



C# 　

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

            var data = dataEntity.ExecuteDataSetFN("fn_api_select_MasterDivision", "hjkj", "hgjkh");

        }
    }
}
 
```

Best practice to use PostgreSQL with .net

Sample Code 1
How to Insert, update, and delete data to the database.

Create a database function in the PostgreSQL

```PostgreSQL
CREATE OR REPLACE FUNCTION public.fn_insert_MasterEmployeeDetails(_empCode text, _firstName text,  _lastName text)
    RETURNS text
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
BEGIN

	insert into MasterEmployeeTable(empCode, firstName, lastName)
	select _empCode, _firstName, _lastName;
			
	RETURN "true";		
END;
$BODY$;
 
```

Use the PostgreSQL function in .net using c#

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

            var data = dataEntity.ExecuteDataSetFN("fn_insert_MasterEmployeeDetails", "Value_of__empCode", "Value_of__firstName", "Value_of_lastName");

        }
    }
}
 
```


Sample Code 2
How to select data from the PostgreSQL database table single or multiple.

Create a database function in the PostgreSQL

```PostgreSQL
-----Function with filter

CREATE OR REPLACE FUNCTION public.fn_select_MasterEmployeeDetails(_empCode text)
    RETURNS SETOF refcursor 
    LANGUAGE 'plpgsql'

AS $BODY$
DECLARE
_emptableDetails refcursor;  
BEGIN
	OPEN _emptableDetails FOR select * from MasterEmployeeTable where empCode=_empCode ;
	RETURN NEXT _emptableDetails;
END;
$BODY$;

----Function without filter
CREATE OR REPLACE FUNCTION public.fn_select_MasterEmployeeDetails()
    RETURNS SETOF refcursor 
    LANGUAGE 'plpgsql'

AS $BODY$
DECLARE
_emptableDetails refcursor;  
BEGIN
	OPEN _emptableDetails FOR select * from MasterEmployeeTable;
	RETURN NEXT _emptableDetails;
END;
$BODY$;

-----Function to return multiple tables

CREATE OR REPLACE FUNCTION public.fn_select_MasterEmployeeDetailsWithSalary()
    RETURNS SETOF refcursor 
    LANGUAGE 'plpgsql'

AS $BODY$
DECLARE
_emptableDetails refcursor;  
_empSalaryDetails refcursor;  
BEGIN
	OPEN _emptableDetails FOR select * from MasterEmployeeTable;
	RETURN NEXT _emptableDetails;
	
		OPEN _empSalaryDetails FOR select * from MasterEmployeeSalaryTable;
	RETURN NEXT _empSalaryDetails;
END;
$BODY$;

 
```

Use the PostgreSQL database function in .net using c#
```C#
using Npgsql.ADO.Net.DataEntity;
using System.Data;


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
			
			
            DataSet dataWithFilter = dataEntity.ExecuteDataSetFN("fn_select_MasterEmployeeDetails", "Value_of__empCode");
			
            DataTable _emp_dataTable = dataWithFilter.Tables[0];
			
	    DataSet dataWithOutFilter = dataEntity.ExecuteDataSetFN("fn_select_MasterEmployeeDetails");
			 
	    DataTable _emp_dataTable_WithoutFilter = dataWithOutFilter.Tables[0];
			 
  	    DataSet _multipleDataTables = dataEntity.ExecuteDataSetFN("fn_select_MasterEmployeeDetailsWithSalary");
			 
            DataTable _empDataTable= _multipleDataTables.Tables[0];
            DataTable _empSalaryDataTable= _multipleDataTables.Tables[1];
			 

        }
    }
}

 
```

