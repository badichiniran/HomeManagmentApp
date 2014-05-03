using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BL_RemoveProduct : System.Web.UI.Page
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
            string Product_list_id = requestQuery["Product_list_id"];
            string RemoveOrDelete = requestQuery["RemoveOrDelete"];

            Product_list_id = Product_list_id.Replace("ID", "");

            //remove the purchesd products id's
            int index = Product_list_id.IndexOf(",P");
            if (index > 0)
                Product_list_id = Product_list_id.Substring(0, index);

            if (RemoveOrDelete == "1")  // remove product  from list - update IsPurchesd=true
            {

                dbs.RemoveProduct(Product_list_id);
            }
            else if (RemoveOrDelete == "2" && Product_list_id.Contains("P") == false) // delete product from list 
                dbs.DeleteProduct(Product_list_id);

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                Response.StatusCode = 999;
            else
            {
                Response.StatusCode = 1;  //general error
                Logger.writeToLog(LoggerLevel.ERROR, "page :ActivityRequestDogTour.aspx.cs, Faild to insertRequest. the exeption message is : " + ex.Message);
                //send mail to systemsupport + log
            }
        }

    }
}