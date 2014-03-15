using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegisterUser : System.Web.UI.Page
{
    DataTable NewUserID = new DataTable();
    JavaScriptSerializer serializer = new JavaScriptSerializer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString == null)
        {
            Response.End();
            return;
        }

        NameValueCollection workerDetailQS = Request.QueryString;

        List<string> UserDetailsList = new List<string>();
        UserDetailsList.Add(workerDetailQS["NewUNTB"]);
        UserDetailsList.Add(workerDetailQS["NewPassTB"]);


        try
        {

            DBservices dbs = new DBservices();

            NewUserID = dbs.CreateNewUser(UserDetailsList); //create new user and returns the New User ID
        }


        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :loginPage.aspx.cs, the exeption message is : " + ex.Message);
            Response.StatusCode = 3;  // empty pass and user name 
        }

        NewUserID.Columns[0].ColumnName = "id";
        string jsonStringuserID = serializer.Serialize(SerializeTable(NewUserID));
        Response.Write(jsonStringuserID);
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