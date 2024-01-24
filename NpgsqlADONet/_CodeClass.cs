using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;
using System.Text;


public class _CodeClass
{
    #region GetJSONString
    public static string GetJSONString(DataTable Dt)
    {
        string[] StrDc = new string[Dt.Columns.Count];
        string HeadStr = string.Empty;
        for (int i = 0; i < Dt.Columns.Count; i++)
        {
            StrDc[i] = Dt.Columns[i].Caption;
            HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
        }
        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
        StringBuilder Sb = new StringBuilder();
        Sb.Append("{\"" + Dt.TableName + "\" : [");
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            string TempStr = HeadStr;
            Sb.Append("{");

            for (int j = 0; j < Dt.Columns.Count; j++)
            {
                TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().Trim());
            }
            Sb.Append(TempStr + "},");
        }

        if (Sb.ToString().Substring(Sb.ToString().Length - 1) == ",")
            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
        Sb.Append("]}");

        return Sb.ToString();
    }
    #endregion
}
