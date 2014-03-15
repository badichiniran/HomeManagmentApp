using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for Logger
/// </summary>
/// 
public enum LoggerLevel
{
    INFO, WARNING, ERROR, DEBUG
}
public class Logger
{

    static private StreamWriter SR = null;
    static string physicalPath = HttpContext.Current.Server.MapPath("/") + @"\" + WebConfigurationManager.AppSettings["logFolderName"].ToString() + @"\";

    static Logger()
    {
    }
    static public void writeToLog(LoggerLevel level, string text)
    {
        try
        {

            string path = Logger.physicalPath;

            if (!Directory.Exists(path))    //DirectoryServices object
                Directory.CreateDirectory(path);

            //normalize date format to 00/00/0000
            string day = (DateTime.Today.Day < 10) ? "0" + DateTime.Today.Day.ToString() : DateTime.Today.Day.ToString();
            string month = (DateTime.Today.Month < 10) ? "0" + DateTime.Today.Month.ToString() : DateTime.Today.Month.ToString();
            string year = DateTime.Today.Year.ToString();


            Environment.CurrentDirectory = path;
            SR = File.AppendText(day + "_" + month + "_" + year + ".log");   // if exists ->add     else -> create and add

            if (SR != null)
            {
                SR.WriteLine(DateTime.Now + " " + level.ToString() + " [" + System.Threading.Thread.CurrentThread.ManagedThreadId + "]" + "\t " + text);
                SR.Close();
            }
            //DBservicesA dbs = new DBservices();
            //string CleanText = text.Replace("'", "''");
            //dbs.insertExaption(CleanText);
        }
        catch
        {

        }
    }
}