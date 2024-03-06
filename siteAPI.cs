using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for siteAPI
/// </summary>
public class siteAPI
{
    config config = new config();
    SqlConnection sqlcon;
    SqlCommand sqlcmd;
    SqlDataReader sqlreader;
    SqlDataAdapter sqldataadapter;
    public siteAPI()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public SqlDataReader getSiteData(string query)
    {
        sqlcon = new SqlConnection(config.connectionstring);
        if (sqlcon != null) { sqlcon.Close(); SqlConnection.ClearAllPools(); }
        sqlcon.Open();
        sqlcmd = new SqlCommand(query, sqlcon);
        sqlreader = sqlcmd.ExecuteReader();
        return sqlreader;
    }

    public string cmdQuery(string query)
    {
        string result;
        int returnedCount;
        sqlcon = new SqlConnection(config.connectionstring);
        if (sqlcon != null) { sqlcon.Close(); SqlConnection.ClearAllPools(); }
        sqlcon.Open();
        sqlcmd = new SqlCommand(query, sqlcon);
        returnedCount = sqlcmd.ExecuteNonQuery();
        if (returnedCount == 1)
        {
            result = "1";
        }
        else if (returnedCount == 2)
        {
            result = "2";
        }
        else
        {
            result = "0";
        }
        return result;
    }
    public string cmdQueryWDataTable(string procedure, string typeValue, DataTable dt)
    {
        string result;
        int returnedCount;
        sqlcon = new SqlConnection(config.connectionstring);
        if (sqlcon != null) { sqlcon.Close(); SqlConnection.ClearAllPools(); }
        sqlcon.Open();
        sqlcmd = new SqlCommand(procedure, sqlcon);
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Parameters.AddWithValue(typeValue, dt);
        returnedCount = sqlcmd.ExecuteNonQuery();
        if (returnedCount == 1)
        {
            result = "1";
        }
        else if (returnedCount == 2)
        {
            result = "2";
        }
        else
        {
            result = "0";
        }
        return result;
    }

    public SqlDataAdapter getItemWAdapt(string query)
    {
        sqlcon= new SqlConnection(config.connectionstring);
        if (sqlcon != null)
        {
            sqlcon.Close();
            SqlConnection.ClearAllPools();
        }
        sqlcon.Open();
        string getItem = query;
        sqlcmd = new SqlCommand(getItem, sqlcon);
        sqldataadapter = new SqlDataAdapter(sqlcmd);
        return sqldataadapter;
    }

    public SqlDataAdapter loadTable(string query)
    {
        sqlcon = new SqlConnection(config.connectionstring);
        if (sqlcon != null) { sqlcon.Close(); SqlConnection.ClearAllPools(); }
        sqlcon.Open();
        sqldataadapter = new SqlDataAdapter(query, sqlcon);
        return sqldataadapter;

    }

    public SqlDataReader UserLogin(string userName, string password)
    {
        sqlcon = new SqlConnection(config.connectionstring);
        if (sqlcon != null) { sqlcon.Close(); SqlConnection.ClearAllPools(); }
        sqlcon.Open();
        string sql = "adminLogin'" + userName + "','" + password + "'";
        sqlcmd = new SqlCommand(sql, sqlcon);
        sqlreader = sqlcmd.ExecuteReader();
        return sqlreader;
    }
}
public static class StringExt
{
    public static string Truncate(this string value, int maxLength, string truncationSuffix = "…")
    {
        return value?.Length > maxLength
            ? value.Substring(0, maxLength) + truncationSuffix
            : value;
    }
    public static string RemoveHTMLTags(string value)
    {
        return Regex.Replace(value, "<.*?>", string.Empty);

    }
}