using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BL_GetUserName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DBservices dbs = new DBservices();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        DataTable UserName = new DataTable();


        if (Request.QueryString == null)
        {
            Response.End();
            return;
        }

        try
        {
        NameValueCollection UserIdQS = Request.QueryString;


       
            UserName = dbs.getUserName(UserIdQS["id"]);

        }


        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :loginPage.aspx.cs, the exeption message is : " + ex.Message);
            Response.StatusCode = 3;  // empty pass and user name 
        }

        UserName.Columns[0].ColumnName = "id";
        string jsonStringUserName = serializer.Serialize(SerializeTable(UserName));
        Response.Write(jsonStringUserName);
        Response.End();
    }

    private IEnumerable<Dictionary<string, object>> SerializeTable(DataTable table)
    {
        return table.DefaultView.OfType<DataRowView>().Select(row =>
        {
            var result = new Dictionary<string, object>();
            foreach (DataColumn column in table.Columns)
            {
                result.Add(column.ColumnName, row.Row[column.ColumnName]);
            }

            return result;
        });
    }
}