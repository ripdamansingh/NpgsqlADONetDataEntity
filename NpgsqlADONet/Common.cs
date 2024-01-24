using Npgsql;
using System.Data;
using System.Reflection;

namespace Npgsql.ADO.Net.DataEntity
{
    public class Common
    {

        //public string pgConnection;

        private string[] _ParaName;

        #region ParaNameArray()
        public void ParaNameArray(params string[] paraname)
        {
            _ParaName = paraname;
        }
        #endregion

        #region Set_SelectSP
        public void Set_SelectSP(string SPName, object[] ParaArray, NpgsqlCommand cmd)
        {
            cmd.CommandTimeout = 1200;
            for (int i = 0; i < ParaArray.Length; i++)
            {
                cmd.Parameters.AddWithValue(_ParaName.GetValue(i).ToString(), ParaArray.GetValue(i));
            }
        }
        #endregion

        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public T GetItem<T>(DataRow dr)
        {

            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            try
            {
                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        //Convert.ChangeType(dr[column.ColumnName], pro.PropertyType);
                        if (pro.Name.ToLower() == column.ColumnName.ToLower())
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        else
                            continue;
                    }
                }
            }
            catch (Exception ex) { }
            return obj;
        }

    }
}
