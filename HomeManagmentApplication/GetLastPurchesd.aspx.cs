using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetLastPurchesd : System.Web.UI.Page
{
    DBservices dbs = new DBservices();
    DataTable LastPurchesDate = new DataTable();
    JavaScriptSerializer serializer = new JavaScriptSerializer();

    protected void Page_Load(object sender, EventArgs e)
    {



        if (Request.QueryString == null)
        {
            Response.End();
            return;
        }
        NameValueCollection requestQuery = Request.QueryString;

        try
        {
            Product product = new Product()
            {
                Product_id = requestQuery["Product_id"],
                UserId = requestQuery["UserId"],
            };
            LastPurchesDate = dbs.GetLastPurchesd(product);
        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :loginPage.aspx.cs, the exeption message is : " + ex.Message);
        }

        LastPurchesDate.Columns[0].ColumnName = "LastPurchesDate";
        string jsonStringLastPurchesDate = serializer.Serialize(SerializeTable(LastPurchesDate));
        Response.Write(jsonStringLastPurchesDate);
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
