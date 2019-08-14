using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;


public class ExceptionHandler
{
	public ExceptionHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void WriteException(string errMessage)
    {
        string logFile = String.Empty;
        try
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(0);
            MethodBase methodBase = stackFrame.GetMethod();
            string ss = System.Web.HttpContext.Current.Request.Url.AbsolutePath;

            FileInfo oInfo = new FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            string pagename = oInfo.Name;




            logFile = ConfigurationManager.AppSettings["ErrorLog"].ToString();
            string path = logFile + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                sw.WriteLine("______________________________________________________________________________________");
                string remoteIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                sw.WriteLine("\r\n Error Log Entry : ");
                sw.WriteLine("Error Date & Time: {0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Error in " + System.Web.HttpContext.Current.Request.Url.ToString() + "\r\nError Message : " + errMessage;
                sw.WriteLine("Error Message  " + err);
                sw.WriteLine("Page Name:" + pagename);
                sw.WriteLine("Method Name: " + methodBase.Name);
                
                sw.WriteLine("Error Line Number: " + stackFrame.GetFileLineNumber().ToString());
                sw.WriteLine("Column Number :" + stackFrame.GetFileColumnNumber());
                sw.WriteLine("IP Address" + remoteIP);
                sw.WriteLine("______________________________________________________________________________________");
                sw.Flush();
                sw.Close();
            }
        }
        catch (Exception)
        {
            //ExceptionHandler.WriteException(errMessage);
        }
    } 

    //public static void WriteException(string errMessage)
    //{
    //    string logFile = String.Empty;
    //    try
    //    {
    //        logFile = ConfigurationManager.AppSettings["ErrorLog"].ToString();
    //        string path = logFile + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
    //        if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
    //        {
    //            File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
    //        }
    //        using (StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
    //        {
    //            string remoteIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
    //            sw.WriteLine("\r\nLog Entry : ");
    //            sw.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
    //            string err = "Error in " + System.Web.HttpContext.Current.Request.Url.ToString() + "\r\nError Message : " + errMessage;
    //            sw.WriteLine(err);
    //            //
    //            sw.WriteLine(remoteIP);
    //            //
    //            sw.WriteLine("_____________________");
    //            sw.Flush();
    //            sw.Close();
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //ExceptionHandler.WriteException(errMessage);
    //    }
    //}


    /// <summary>
    /// Save log into database or file 
    /// </summary>
    /// <param name="errMessage">User Exceptions</param>
    /// <param name="IsSaveintoDatabase">true:save into database, False: save into filename</param>

    public static void WriteException(string errMessage, bool IsSaveintoDatabase)
    {
        string logFile = String.Empty;
        try
        {

            if (IsSaveintoDatabase)
            {

                string remoteIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);


                string fullError = "ERROR:- " + "(" + errMessage + ")" + "IN IP ADDRESS:-" + remoteIP;
                Luminious.DataAcessLayer.SqlHelper.ExecuteNonQuery(Luminious.Connection.Configuration.ConnectionString, CommandType.Text, "INSERT INTO MST_ERRORPORG_LOG VALUES(@datetimelog,@IPADDR,@errmsg)", new SqlParameter[] { new SqlParameter("@datetimelog", DateTime.Now), new SqlParameter("@IPADDR", remoteIP.Trim()), new SqlParameter("@errmsg", HttpUtility.HtmlEncode(fullError.Trim())) });
            }
            else
            {
                logFile = ConfigurationManager.AppSettings["ErrorLog"].ToString();
                string path = logFile + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }
                using (StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    string remoteIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                    sw.WriteLine("\r\nLog Entry : ");
                    sw.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in " + System.Web.HttpContext.Current.Request.Url.ToString() + "\r\nError Message : " + errMessage;
                    sw.WriteLine(err);
                    //
                    sw.WriteLine(remoteIP);
                    //
                    sw.WriteLine("_____________________");
                    sw.Flush();
                    sw.Close();
                }
            }


        }


        catch (Exception)
        {
            //ExceptionHandler.WriteException(errMessage);
        }
    }
   



   





  




    public static void WriteEx(Exception exception)
    {
        string logFile = String.Empty;
        StreamWriter logWriter;
        try
        {
            logFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ErrorLog"].ToString()) + "ExTraceLog.txt";
            if (File.Exists(logFile))
                logWriter = File.AppendText(logFile);
            else
                logWriter = File.CreateText(logFile);
            logWriter.WriteLine("=>" + DateTime.Now + " " + " An Error occured : " + exception.StackTrace + " Message : " + exception.Message + "\n\n");
            logWriter.Close();
        }
        catch (Exception)
        {
            ExceptionHandler.WriteException(exception.Message);
        }
    }

    public static string CreateErrorMessage(Exception ApplException)
    {
        StringBuilder message = new StringBuilder();
        try
        {
            message.Append("Exception :: " + ApplException.ToString());
            if (ApplException.InnerException != null)
            {
                message.Append("Inner Exception :: " + ApplException.InnerException.ToString());
            }
            return message.ToString();
        }
        catch
        {
            message.Append("Exception :: Unknown Exception");
            return message.ToString();
        }
    }

    public static void WriteLog(string message)
    {
        FileStream fileStream = null;
        StreamWriter streamWriter = null;
        string logFile = String.Empty;
        try
        {
            logFile = ConfigurationManager.AppSettings["ErrorLog"].ToString();
            string path = logFile + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
            if (path.Equals(""))            // RETURN IF PATH DOES NOT EXIST
                return;
            // CREATE LOG FILE DIRECTORY IF IT DOES NOT EXIST
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo = new FileInfo(path);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) 
                logDirInfo.Create();
            //
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                string remoteIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                sw.WriteLine("\r\nLog Entry : ");
                sw.WriteLine("Remote IP : " + remoteIP);
                sw.WriteLine("{0}", "Date/Time Error Occured : " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                sw.WriteLine(message);
                sw.WriteLine("_____________________");
                sw.Flush();
                sw.Close();
            }
        }
        finally
        {
            if (streamWriter != null)
                streamWriter.Close();
            if (fileStream != null)
                fileStream.Close();
        }
    }

    public static void ClearBrowserData()
    {
        System.Web.HttpContext.Current.Response.Buffer = true;
        System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        System.Web.HttpContext.Current.Response.Expires = -15000;
        System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
        System.Web.HttpContext.Current.Response.Cache.SetNoStore();
        System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.Web.HttpContext.Current.Response.AppendHeader("Cache-Control", "no-store");
        System.Web.HttpContext.Current.Response.AppendHeader("Cache-Control", "Must-revalidate");
        System.Web.HttpContext.Current.Response.AppendHeader("Cache-Control", "Proxy-revalidate");

    }


    ~ExceptionHandler()
    {
        GC.SuppressFinalize(this);
    }
}
