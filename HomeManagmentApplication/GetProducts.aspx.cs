using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBservices dbs = new DBservices();

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        //DataTable getCities;
        // DataTable getAnimalTypes;
        DataTable Products;

        try
        {


            Products = dbs.getProducts();
        }

        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :InjuredPet.aspx.cs, the exeption message is : " + ex.Message);
            throw;
        }

        Products.Columns[0].ColumnName = "id";
        Products.Columns[1].ColumnName = "name";


        // string jsonStringProducts = "{\"Products\":" + serializer.Serialize(SerializeTable(Products)) + "}";
        string jsonStringProducts = serializer.Serialize(SerializeTable(Products));

        Response.Write(jsonStringProducts);
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