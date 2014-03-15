using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for SQLQueries
/// </summary>
public class SQLQueries
{
    public SQLQueries()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static String getUserDedails(string UserName, string passwordString)
    {
        return "SELECT UserPassword,UserId FROM dbo.Users WHERE UserName = '" + UserName + "' and UserPassword = '" + passwordString + "'";
    }



    public static String ProductCategories()
    {
        return "SELECT * FROM .dbo.Product_categories";
    }

    public static String getProducts()
    {
        return "SELECT Product_id,Product_desc FROM Products where IsApproved=1";
    }

    public static String insertProductToList(Product p)
    {
        String command;
        string getUserList = "DECLARE @tmpList_id int SET @tmpList_id = (select List_id from List where UserId='" + p.UserId + "' and  Is_Active=1 )";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Values({0}, '{1}', '{2}' , '{3}' , '{4}'  )", "@tmpList_id", p.Product_id, p.Product_amount, p.Units, p.Comment);
        String prefix = "INSERT INTO Product_list " + "( List_id, Product_id,Product_amount,Unit_id,Comment) ";

        command = getUserList + prefix + sb.ToString();

        return command;
    }

    public static String insertNewProductToList(Product p)
    {
        String command;

        string insertNewPdoduct = " INSERT INTO Products (Product_desc,Product_category_id,IsApproved,UserId) VALUES ('" + p.Product_desc + "',10,1," + p.UserId + ")";
        string getNewPdoductId = " DECLARE @tmpProduct_id int SET @tmpProduct_id = (select top 1 Product_id from Products order by time_stmp desc) ";
        string getUserList = " DECLARE @tmpList_id int SET @tmpList_id = (select List_id from List where UserId='" + p.UserId + "' and  Is_Active=1 ) ";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Values({0}, {1}, '{2}' , '{3}' , '{4}'  )", "@tmpList_id", "@tmpProduct_id", p.Product_amount, p.Units, p.Comment);
        String prefix = "INSERT INTO Product_list " + "( List_id, Product_id,Product_amount,Unit_id,Comment) ";

        command = insertNewPdoduct + getNewPdoductId + getUserList + prefix + sb.ToString();

        return command;
    }
    public static String ShowShoppingList()
    {
        string showProducts = "SELECT     dbo.products.product_id,dbo.ProductTypes.ProductType_desc, dbo.products.product_name, dbo.products.product_amount FROM         dbo.products INNER JOIN  dbo.ProductTypes ON dbo.products.ProductType_id = dbo.ProductTypes.ProductType_id WHERE products.isPurchesd=0 ORDER BY dbo.ProductTypes.ProductType_desc";
        return showProducts;
    }

    public static String ShowShoppingList_byUserId(string UserId)
    {

        return "SELECT Product_list_id,Product_desc,Product_category_desc,Unit_desc,Comment,Is_purchased,Product_amount FROM View_ShowProductList  where UserId='" + UserId + "' order by Product_category_desc ";
    }

    public static String RemoveProduct(string Product_list_id)
    {
        string removeProduct = "UPDATE [Product_list] SET [Is_purchased] = 1 WHERE Product_list_id in (" + Product_list_id + ")";
        return removeProduct;
    }


    public static String DeleteProduct(string Product_list_id)
    {
        string deleteProduct = "DELETE FROM Product_list WHERE Product_list_id in (" + Product_list_id + ")";
        return deleteProduct;
    }
    public static String FinishShopping(string UserId)
    {
        String command;
        string getOldList = " DECLARE @OldList_id int SET @OldList_id = (select List_id from List where UserId='" + UserId + "' and  Is_Active=1 )";
        string updateList = " UPDATE [List] SET [Is_Active] = 0, Purchasing_time_stmp=GETDATE() WHERE UserId='" + UserId + "' and Is_Active=1 ";
        string insertNewList = " INSERT INTO [List] (UserId) VALUES(" + UserId + ") ";
        string getNewListId = " DECLARE @NewList_id int SET @NewList_id = (select List_id from List where UserId='" + UserId + "' and  Is_Active=1 )";
        string updateProductListId = " UPDATE [Product_list] SET [List_id] = @NewList_id WHERE List_id=@OldList_id  and Is_purchased=0 ";
        command = getOldList + updateList + insertNewList + getNewListId + updateProductListId;

        return command;
    }



    public static String CreateNewUser(List<string> UserDetailsList)
    {
        String command;

        string insertNewUser = " INSERT INTO Users (UserName,UserPassword) VALUES ('" + UserDetailsList[0] + "'," + UserDetailsList[1] + ")";
        string getNewUserId = " DECLARE @tmpUserId int SET @tmpUserId = (select top 1 UserId from Users where UserName='" + UserDetailsList[0] + "' order by Register_time_stmp desc ) ";
        string insertNewUserList = " INSERT INTO List (List_name,UserId,Is_Active) VALUES ('רשימת קניות לסופר',@tmpUserId,1)";
        string insertNew_Constant_UserList = " INSERT INTO List (List_name,UserId,Is_Active,Is_CosntantList) VALUES ('רשימת קניות קבועה',@tmpUserId,0,1)";
        string returnNewUserId = " select UserId from Users where UserId=@tmpUserId ";
        command = insertNewUser + getNewUserId + insertNewUserList + insertNew_Constant_UserList + returnNewUserId;

        return command;
    }

    public static String GetLastPurchesd(Product p)
    {


        string LastPurchesd = "select top 1 convert (varchar,Purchasing_time_stmp, 103)as Purchasing_time_stmp  from list where list_id in (select list_id from Product_list where Product_id ='" + p.Product_id + "') and UserId='" + p.UserId + "' and Is_Active=0 order by Purchasing_time_stmp asc";

        return LastPurchesd;
    }
}