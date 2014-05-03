using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BL_ShowShoppingList : System.Web.UI.Page
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

        DataTable ShoppingList;


        try
        {
            string UserId = requestQuery["UserId"];
            ShoppingList = dbs.ShowShoppingList_byUserId(UserId);

        }

        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :ShowShoppingList.aspx.cs, the exeption message is : " + ex.Message);
            throw;
        }
        string jsonStringShoppingList = serializer.Serialize(SerializeTable(ShoppingList));

        Response.Write(jsonStringShoppingList);
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