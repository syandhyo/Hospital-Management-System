using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Luminious.DataAcessLayer;
using Luminious.Connection;

/// <summary>
/// Summary description for BL_COLOUR
/// </summary>
public class BL_COLOUR:IDisposable
{
    private bool disposed = false;
    SqlParameter[] para = null;
    DataTable dtboard = new DataTable();
    DataTable dt = new DataTable();

	public BL_COLOUR()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable BoardView()
    {

        BO_COLOUR bo = new BO_COLOUR();
        dtboard = null;
        try
        {
            string Q = "select * from [COLOR_MST]";
            dtboard = Luminious.DataAcessLayer.SqlHelper.ExecuteDataTable(Luminious.Connection.Configuration.ConnectionString, CommandType.Text, Q);
            if (dtboard != null && dtboard.Rows.Count > 0)
            {
                return dtboard;
            }

        }
        catch (SqlException ex)
        {
            ExceptionHandler.WriteException(ex.Message, true);
        }
        catch (Exception ex)
        {
            ExceptionHandler.WriteException(ex.Message, true);
        }
        finally
        {

        }
        return null;
    }
    public DataTable BoardView(BO_COLOUR ob)
    {

        BO_COLOUR bo = new BO_COLOUR();
        dtboard = null;
        try
        {
            string Q = "select * from [COLOR_MST] where  COLOR_ID =@COLOR_ID ";
            SqlParameter[] para = null;
            para = new SqlParameter[] { new SqlParameter("@COLOR_ID", ob.COLOR_ID) };
            dtboard = Luminious.DataAcessLayer.SqlHelper.ExecuteDataTable(Luminious.Connection.Configuration.ConnectionString, CommandType.Text, Q, para);

            if (dtboard != null && dtboard.Rows.Count > 0)
            {
                return dtboard;
            }

        }
        catch (SqlException ex)
        {
            ExceptionHandler.WriteException(ex.Message, true);
        }
        catch (Exception ex)
        {
            ExceptionHandler.WriteException(ex.Message, true);
        }
        finally
        {

        }
        return null;
    }
    public string Board_Insert(BO_COLOUR ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
 
            new SqlParameter("@COLOR_Name", ob.COLOR_Name)
              
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_color_Insert]", para);

            if (res > 0)
            {
                Result = "Record Saved Sucessfully...";
                count = res.ToString();
            }
            else
            {
                Result = "Error in data saving";
                count = res.ToString();
            }
        }
        catch (SqlException ex)
        {
            Result = ex.Message;
            ExceptionHandler.WriteException(ex.Message, true);
        }
        catch (Exception ex)
        {
            Result = ex.Message;
            ExceptionHandler.WriteException(ex.Message, true);
        }
        finally
        {

        }
        return count;
    }
    public string Board_Update(BO_COLOUR ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@COLOR_ID",ob.COLOR_ID),
            new SqlParameter("@COLOR_Name", ob.COLOR_Name)

            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_color_Update]", para);

            if (res > 0)
            {
                Result = "Record Saved Sucessfully...";
                count = res.ToString();
            }
            else
            {
                Result = "Error in data saving";
                count = res.ToString();
            }
            //  ca.Certifying_agency_id = parm[1].Value != null && parm[1].Value != "" ? Convert.ToInt32(parm[1].Value) : 0;
        }
        catch (SqlException ex)
        {
            Result = ex.Message;
            ExceptionHandler.WriteException(ex.Message, true);
        }
        catch (Exception ex)
        {
            Result = ex.Message;
            ExceptionHandler.WriteException(ex.Message, true);
        }
        finally
        {

        }
        return count;
    }
    public string Board_Delete(BO_COLOUR ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@COLOR_ID", ob.COLOR_ID)
            
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_color_Delete]", para);

            if (res > 0)
            {
                Result = "Record Deleted Sucessfully...";
                count = res.ToString();
            }
            else
            {
                Result = "Error in data Deleting";
                count = res.ToString();
            }
            //  ca.Certifying_agency_id = parm[1].Value != null && parm[1].Value != "" ? Convert.ToInt32(parm[1].Value) : 0;
        }
        catch (SqlException ex)
        {
            Result = ex.Message;
            ExceptionHandler.WriteException(ex.Message, true);
        }
        catch (Exception ex)
        {
            Result = ex.Message;
            ExceptionHandler.WriteException(ex.Message, true);
        }
        finally
        {

        }
        return count;
    }
     protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                if (dt != null)
                {
                    dt.Dispose();
                }

            }

            // shared cleanup logic
            disposed = true;
        }
    }

     ~BL_COLOUR()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}