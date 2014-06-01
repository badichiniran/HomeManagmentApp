using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BL_GetExpencesByCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBservices dbs = new DBservices();
        if (Request.QueryString == null)
        {
            Response.End();
            return;
        }
        NameValueCollection requestQuery = Request.QueryString;

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        DataTable UserExpencesByCategoryHead;
        DataTable UserExpencesByCategoryId;
        string User_id = requestQuery["UserId"];

        try
        {
            UserExpencesByCategoryHead = dbs.GetExpencesByCategoryHead(User_id);
            UserExpencesByCategoryId = dbs.GetExpencesByCategoryId(User_id);


        }


        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :ActivityShow.aspx.cs, the exeption message is : " + ex.Message);
            throw;
        }
        string jsonStringUserExpencesByCategoryHead = serializer.Serialize(SerializeTable(UserExpencesByCategoryHead));
        string jsonStringUserExpencesByCategoryId = serializer.Serialize(SerializeTable(UserExpencesByCategoryId));
        string jsonString = "{\"UserExpencesByCategoryHead\":" + jsonStringUserExpencesByCategoryHead + ",\"UserExpencesByCategoryId\":" + jsonStringUserExpencesByCategoryId + "}";

        Response.Write(jsonString);
        Response.End();


    }


    private IEnumerable<Dictionary<string, object>> SerializeTable(DataTable table)
    {
        return table.DefaultView.OfType<DataRowView>().Select(row =>
        {
            var result = new Dictionary<string, object>();
            foreach (DataColumn column in table.Columns)
            {
                result.Add(column.ColumnName, Convert.ToString(row.Row[column.ColumnName]));
            }

            return result;
        });
    }
}