using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BL_loginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DBservices dbs = new DBservices();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        DataTable UserDetails = new DataTable();


        if (Request.QueryString == null)
        {
            Response.End();
            return;
        }

        NameValueCollection workerDetailQS = Request.QueryString;


        string UserNameString = workerDetailQS["id"];
        string passwordString = workerDetailQS["password"];


        try
        {
            UserDetails = dbs.getUserDedails(UserNameString, passwordString);

            if (passwordString != UserDetails.Rows[0].ItemArray[0].ToString())
            {
                Response.StatusCode = 1;   // user name exists BUT password is not correct
            }


        }


        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :loginPage.aspx.cs, the exeption message is : " + ex.Message);
            Response.StatusCode = 3;  // empty pass and user name 
        }

        UserDetails.Columns[1].ColumnName = "id";
        string jsonStringUserDetails = serializer.Serialize(SerializeTable(UserDetails));
        Response.Write(jsonStringUserDetails);
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