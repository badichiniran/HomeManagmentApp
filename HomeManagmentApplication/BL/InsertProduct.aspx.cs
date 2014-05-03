using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BL_InsertProduct : System.Web.UI.Page
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
            Product product = new Product()
            {

                Product_amount = requestQuery["Prodect_amount"],
                Product_id = requestQuery["Product_id"],
                Units = requestQuery["Units"],
                Comment = requestQuery["Comment"],
                UserId = requestQuery["UserId"],

            };
            dbs.insertProductToList(product);


        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("AnimelType_id_FK1"))
                Response.StatusCode = 1; // must choose AnimelType
            else
            {
                Response.StatusCode = 999;  //general error
                Logger.writeToLog(LoggerLevel.ERROR, "page :InjuredPet_Incident.aspx.cs, the exeption message is : " + ex.Message);
                //send mail to systemsupport 
            }
        }

    }
}