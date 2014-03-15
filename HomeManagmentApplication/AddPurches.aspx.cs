using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddPurches : System.Web.UI.Page
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

        try
        {
            string amount = requestQuery["amount"];
            string comment = requestQuery["comment"];
            string Category_id = requestQuery["Category_id"];
            string PurchesDate = requestQuery["PurchesDate"];
            string paymentMethod = requestQuery["paymentMethod"];
            string UserId = requestQuery["UserId"];

            dbs.AddPurches(amount, comment, Category_id, PurchesDate, paymentMethod, UserId);

        }
        catch (Exception)
        {


        }



        Response.End();

    }
}