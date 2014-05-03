using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BL_GetProductCategories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBservices dbs = new DBservices();

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        DataTable ProductCategories;


        try
        {
            ProductCategories = dbs.GetProductCategories();   // 1 is shabat imutz

        }

        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :ActivityShabatDog.aspx.cs, the exeption message is : " + ex.Message);
            throw;
        }

        ProductCategories.Columns[0].ColumnName = "Code";
        ProductCategories.Columns[1].ColumnName = "Name";

        string jsonStringCategories = serializer.Serialize(SerializeTable(ProductCategories));


        string jsonString = "{\"Categories\":" + jsonStringCategories + "}";
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