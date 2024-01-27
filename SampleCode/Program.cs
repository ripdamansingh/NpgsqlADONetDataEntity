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

            var data = dataEntity.ExecuteDataSetFN("fn_api_select_YourPostgreSQl_Database_function", "pram1", "pram2");



            DataSet dataWithFilter = dataEntity.ExecuteDataSetFN("fn_select_MasterEmployeeDetails", "Value_of__empCode");

            DataTable _emp_dataTable = dataWithFilter.Tables[0];

            DataSet dataWithOutFilter = dataEntity.ExecuteDataSetFN("fn_select_MasterEmployeeDetails");

            DataTable _emp_dataTable_WithoutFilter = dataWithOutFilter.Tables[0];

            DataSet _multipleDataTables = dataEntity.ExecuteDataSetFN("fn_select_MasterEmployeeDetailsWithSalary");

            DataTable _empDataTable = _multipleDataTables.Tables[0];
            DataTable _empSalaryDataTable = _multipleDataTables.Tables[1];
        }
    }
}