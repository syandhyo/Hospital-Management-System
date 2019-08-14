using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Luminious.DataAcessLayer;
using Luminious.Connection;

/// <summary>
/// Summary description for BL_SBCAT
/// </summary>
public class BL_SBCAT:IDisposable
{
    private bool disposed = false;
    SqlParameter[] para = null;
    DataTable dtboard = new DataTable();
    DataTable dt = new DataTable();

	public BL_SBCAT()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Board_Insert(BO_SBCAT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@CAT_ID", ob.CAT_ID),
            new SqlParameter("@SBCAT_NAME", ob.SBCAT_NAME)
              
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_sbcat_mst_Insert]", para);

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
    public string Board_Update(BO_SBCAT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@SBCAT_ID",ob.SBCAT_ID),
            new SqlParameter("@CAT_ID", ob.CAT_ID),
            new SqlParameter("@SBCAT_NAME", ob.SBCAT_NAME)

            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_sbcat_mst_Update]", para);

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
    public string Board_Delete(BO_SBCAT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@SBCAT_ID", ob.SBCAT_ID)
            
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_sbcat_mst_Delete]", para);

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

        BO_SBCAT bo = new BO_SBCAT();
        dtboard = null;
        try
        {
            string Q = "select * from [Vw_sbcat]";
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
    public DataTable BoardView(BO_SBCAT ob)
    {

        BO_SBCAT bo = new BO_SBCAT();
        dtboard = null;
        try
        {
            string Q = "select * from [SUBCAT_MST] where  SBCAT_ID =@SBCAT_ID ";
            SqlParameter[] para = null;
            para = new SqlParameter[] { new SqlParameter("@SBCAT_ID", ob.SBCAT_ID) };
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

        ~BL_SBCAT()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}