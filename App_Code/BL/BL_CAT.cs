using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Luminious.DataAcessLayer;
using Luminious.Connection;
/// <summary>
/// Summary description for BL_CAT
/// </summary>
public class BL_CAT:IDisposable
{
     private bool disposed = false;
        SqlParameter[] para = null;
    DataTable dtboard = new DataTable();
    DataTable dt = new DataTable();
	public BL_CAT()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Board_Insert(BO_CAT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
 
            new SqlParameter("@CAT_NAME", ob.CAT_NAME)
              
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_cat_mst_Insert]", para);

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
    public string Board_Update(BO_CAT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@CAT_ID",ob.CAT_ID),
            new SqlParameter("@CAT_NAME", ob.CAT_NAME)

            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_cat_mst_Update]", para);

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
    public string Board_Delete(BO_CAT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@CAT_ID", ob.CAT_ID)
            
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_cat_mst_Delete]", para);

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
    public DataTable BoardView()
    {

        BO_CAT bo = new BO_CAT();
        dtboard = null;
        try
        {
            string Q = "select * from [CAT_MASTER]";
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
    public DataTable BoardView(BO_CAT ob)
    {

        BO_CAT bo = new BO_CAT();
        dtboard = null;
        try
        {
            string Q = "select * from [CAT_MASTER] where  CAT_ID =@CAT_ID ";
            SqlParameter[] para = null;
            para = new SqlParameter[] { new SqlParameter("@CAT_ID", ob.CAT_ID) };
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

        ~BL_CAT()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}