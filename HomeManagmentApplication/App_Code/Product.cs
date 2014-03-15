using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Product
/// </summary>
public class Product
{
    public Product()
    {
        //
        // TODO: Add constructor logic here
        //
    }

  


    string product_id;
    public string Product_id
    {
        get { return product_id; }
        set { product_id = value; }
    }


    string product_desc;
    public string Product_desc
    {
        get { return product_desc; }
        set { product_desc = value; }
    }

    string units;
    public string Units
    {
        get { return units; }
        set { units = value; }
    }

    string product_amount;
    public string Product_amount
    {
        get { return product_amount; }
        set { product_amount = value; }
    }

    string comment;
    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }

    string userId;
    public string UserId
    {
        get { return userId; }
        set { userId = value; }
    }




    //public void RemoveProduct()
    //{
    //    try
    //    {
    //        dbs.RemoveProduct(this);
    //    }
    //    catch
    //    {
    //        throw;
    //    }

    //}
}