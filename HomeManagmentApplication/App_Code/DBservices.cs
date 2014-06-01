using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for DBservices
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;
    public string connectionString;
    public DBservices()
    {
        connectionString = WebConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
    }

    public SqlConnection connect()
    {
        try
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: connect(), exeption message: " + ex.Message);
            throw;
        }
    }

    public void disconnect(SqlConnection con)
    {
        if (con != null)
        {
            try
            {
                con.Close();
            }
            catch (Exception ex)
            {
                Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: disconnect(), exeption message: " + ex.Message);
                throw;
            }
        }
    }
    public DataTable getUserDedails(string UserName, string passwordString)
    {
        SqlConnection con;
        try
        {
            con = connect(); // open the connection to the database/
            da = new SqlDataAdapter(SQLQueries.getUserDedails(UserName, passwordString), con); // create the data adapter
            DataSet ds = new DataSet();
            da.Fill(ds);       // Fill the datatable (in the dataset), using the Select command 
            dt = ds.Tables[0];     // point to the cars table , which is the only table in this case

        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: getVolonteerPass(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;
    }
    public DataTable GetProductCategories()
    {
        SqlConnection con;

        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.ProductCategories(), con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: getAnimalTypes(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;

    }
    public DataTable getProducts()
    {
        SqlConnection con;

        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.getProducts(), con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: getAnimalTypes(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;

    }
    public void insertNewProductToList(Product p)
    {
        SqlConnection con;

        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.insertNewProductToList(p), con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables["HM_Products"];

        }
        catch (Exception ex)
        {

            throw ex;
        }
        disconnect(con);


    }
    public void insertProductToList(Product p)
    {
        SqlConnection con;

        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.insertProductToList(p), con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables["HM_Products"];

        }
        catch (Exception ex)
        {

            throw ex;
        }
        disconnect(con);


    }
    public DataTable ShowShoppingList_byUserId(string UserId)
    {
        SqlConnection con;
        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.ShowShoppingList_byUserId(UserId), con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: getVeg_ShoppingList(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;
    }
    public void RemoveProduct(string Product_list_id)
    {

        SqlConnection con;
        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.RemoveProduct(Product_list_id), con);
            DataSet ds = new DataSet();
            da.Fill(ds);

        }
        catch (Exception ex)
        {

            throw ex;
        }
        disconnect(con);
    }
    public void DeleteProduct(string Product_list_id)
    {

        SqlConnection con;
        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.DeleteProduct(Product_list_id), con);
            DataSet ds = new DataSet();
            da.Fill(ds);

        }
        catch (Exception ex)
        {

            throw ex;
        }
        disconnect(con);
    }
    public void FinishShopping(string UserName)
    {

        SqlConnection con;
        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.FinishShopping(UserName), con);
            DataSet ds = new DataSet();
            da.Fill(ds);

        }
        catch (Exception ex)
        {

            throw ex;
        }
        disconnect(con);
    }
    public DataTable CreateNewUser(List<string> UserDetailsList)
    {
        SqlConnection con;
        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.CreateNewUser(UserDetailsList), con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        disconnect(con);
        return dt;
    }
    public DataTable GetLastPurchesd(Product p)
    {
        SqlConnection con;
        try
        {
            con = connect();
            da = new SqlDataAdapter(SQLQueries.GetLastPurchesd(p), con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        disconnect(con);
        return dt;
    }
    public void AddPurches(string amount,string  comment,string  Category_id,string  PurchesDate,string  paymentMethod,string  UserId)
    {
        string InsertQuery = "INSERT INTO [HM_Expenses] ([amount],[comment],[ExpenseCategory_id],[PurchesDate],[paymentMethod],[UserId]) VALUES ('" + amount + "','" + comment + "','" + Category_id + "','" + PurchesDate + "','" + paymentMethod + "','" + UserId + "')";
        SqlConnection con;
        try
        {
            con = connect();
            da = new SqlDataAdapter(InsertQuery, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        disconnect(con);
    }
    public DataTable UserExpences(string id)
    {
        SqlConnection con;
        DataTable dt = new DataTable();
        try
        {
            con = connect();
            SqlCommand cmd = new SqlCommand("HM_GetExpences", con);

            SqlParameter p1 = new SqlParameter("Userid", id);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(p1);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);


        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: ActivityShow(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;
    }  
    public DataTable UserExpencesByMonth(string id)
    {
        SqlConnection con;
        DataTable dt = new DataTable();
        try
        {
            con = connect();
            SqlCommand cmd = new SqlCommand("HM_GetExpencesByMonth", con);
            
            SqlParameter p1 = new SqlParameter("Userid", id);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(p1);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
          
            adapter.Fill(dt);


        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: ActivityShow(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;
    }
    public DataTable getUserName(string id)
    {
        SqlConnection con;
        DataTable dt = new DataTable();
        try
        {
            con = connect();
            SqlCommand cmd = new SqlCommand("HM_GetUserName", con);        
            SqlParameter p1 = new SqlParameter("Userid", id);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(p1);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
          
            adapter.Fill(dt);


        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: ActivityShow(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;
    }

    public DataTable GetExpencesByCategoryHead(string id)
    {
        SqlConnection con;
        DataTable dt = new DataTable();
        try
        {
            con = connect();
            SqlCommand cmd = new SqlCommand("HM_GetExpencesByHeadCategory", con);        
            SqlParameter p1 = new SqlParameter("Userid", id);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(p1);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
          
            adapter.Fill(dt);


        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: ActivityShow(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;
    }
    public DataTable GetExpencesByCategoryId(string id)
    {
        SqlConnection con;
        DataTable dt = new DataTable();
        try
        {
            con = connect();
            SqlCommand cmd = new SqlCommand("HM_GetExpencesByCategoryId", con);
            SqlParameter p1 = new SqlParameter("Userid", id);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(p1);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);


        }
        catch (Exception ex)
        {
            Logger.writeToLog(LoggerLevel.ERROR, "page :DBServicesAPP.cs, function: ActivityShow(), exeption message: " + ex.Message);
            throw ex;
        }
        disconnect(con);
        return dt;
    }
}