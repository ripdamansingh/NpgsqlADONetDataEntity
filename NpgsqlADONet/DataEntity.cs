using Npgsql;
using System.Data;

namespace Npgsql.ADO.Net.DataEntity
{
    public class DataEntity : Common
    {
        public string pgConnection { get; set; }
        
        public DataTable ExecuteDataTableFN(string fn_Name, params object[] ParaArray)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlConnection conn = new NpgsqlConnection(pgConnection);
            conn.Open();
            NpgsqlTransaction trans = conn.BeginTransaction();
            var strPrams = String.Join(",", ParaArray.Select(p => string.Format("'{0}'", string.Format("{0}", p).Replace('\'', ' '))));
            cmd.CommandText = "select * from " + fn_Name + "(" + strPrams + ");";
            cmd.Connection = conn;
            cmd.CommandTimeout = 300000;
            NpgsqlDataReader dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            trans.Commit();
            conn.Close();
            return dt;
        }

        public DataTable ExecuteDataTableFN(string fn_Name)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlConnection conn = new NpgsqlConnection(pgConnection);
            conn.Open();
            cmd.CommandTimeout = 300000;
            NpgsqlTransaction trans = conn.BeginTransaction();
            cmd.CommandText = "select * from " + fn_Name + "();";
            cmd.Connection = conn;
            NpgsqlDataReader dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            trans.Commit();
            conn.Close();
            return dt;
        }

        public async Task<DataTable> ExecuteDataTableFNAsync(string fn_Name, params object[] ParaArray)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlConnection conn = new NpgsqlConnection(pgConnection);
            conn.Open();
            NpgsqlTransaction trans = await conn.BeginTransactionAsync();
            var strPrams = String.Join(",", ParaArray.Select(p => string.Format("'{0}'", string.Format("{0}", p).Replace('\'', ' '))));
            cmd.CommandText = "select * from " + fn_Name + "(" + strPrams + ");";
            cmd.Connection = conn;
            cmd.CommandTimeout = 300000;
            NpgsqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            dt.Load(dataReader);
            trans.Commit();
            conn.Close();
            return dt;
        }

        public DataTable ExecuteDataTableSP(string sp_Name, params object[] ParaArray)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlConnection conn = new NpgsqlConnection(pgConnection);
            conn.Open();
            cmd.CommandTimeout = 300000;
            NpgsqlTransaction trans = conn.BeginTransaction();
            var strPrams = String.Join(",", ParaArray.Select(p => string.Format("'{0}'", string.Format("{0}", p).Replace('\'', ' '))));
            cmd.CommandText = "Call " + sp_Name + "(" + strPrams + ");";
            cmd.Connection = conn;
            NpgsqlDataReader dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            trans.Commit();
            conn.Close();
            return dt;
        }

        public async Task<DataTable> ExecuteDataTableSPAsync(string sp_Name, params object[] ParaArray)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlConnection conn = new NpgsqlConnection(pgConnection);
            conn.Open();
            NpgsqlTransaction trans = await conn.BeginTransactionAsync();
            var strPrams = String.Join(",", ParaArray.Select(p => string.Format("'{0}'", string.Format("{0}", p).Replace('\'', ' '))));
            cmd.CommandText = "Call " + sp_Name + "(" + strPrams + ");";
            cmd.Connection = conn;
            cmd.CommandTimeout = 300000;
            NpgsqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            dt.Load(dataReader);
            trans.Commit();
            conn.Close();
            return dt;
        }

        public DataSet ExecuteDataSetFN(string fn_Name)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlConnection conn = new NpgsqlConnection(pgConnection);
            conn.Open();
            cmd.CommandTimeout = 300000;
            NpgsqlTransaction trans = conn.BeginTransaction();
            try
            {
                cmd.CommandText = "select * from " + fn_Name + "();";
                cmd.Connection = conn;
                NpgsqlDataReader dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                foreach (DataRow row in dt.Rows)
                {
                    dt1 = new DataTable();
                    string cursor = row[fn_Name].ToString();
                    cmd.CommandText = "fetch all in \"" + cursor + "\"";
                    cmd.CommandType = CommandType.Text;
                    NpgsqlDataReader cursorDataReader = cmd.ExecuteReader();
                    dt1.Load(dataReader);
                    ds.Tables.Add(dt1);
                }
                trans.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                conn.Close();
            }
            return ds;
        }

        public DataSet ExecuteDataSetFN(string fn_Name, params object[] ParaArray)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlConnection conn = new NpgsqlConnection(pgConnection);
            conn.Open();
            NpgsqlTransaction trans = conn.BeginTransaction();
            try
            {
                var strPrams = String.Join(",", ParaArray.Select(p => string.Format("'{0}'", string.Format("{0}", p).Replace('\'', ' '))));
                cmd.CommandText = "select * from " + fn_Name + "(" + strPrams + ");";
                cmd.Connection = conn;
                cmd.CommandTimeout = 300000;
                NpgsqlDataReader dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                foreach (DataRow row in dt.Rows)
                {
                    dt1 = new DataTable();
                    string cursor = row[fn_Name].ToString();
                    cmd.CommandText = "fetch all in \"" + cursor + "\"";
                    cmd.CommandType = CommandType.Text;
                    NpgsqlDataReader cursorDataReader = cmd.ExecuteReader();
                    dt1.Load(dataReader);
                    ds.Tables.Add(dt1);
                }
                trans.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                conn.Close();
            }
            return ds;
        }

    }
}
