using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Luminious.DataAcessLayer;
using Luminious.Connection;
/// <summary>
/// Summary description for BL_UNIT
/// </summary>
public class BL_UNIT:IDisposable
{
    private bool disposed = false;
    SqlParameter[] para = null;
    DataTable dtboard = new DataTable();
    DataTable dt = new DataTable();

	public BL_UNIT()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Board_Insert(BO_UNIT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
 
            new SqlParameter("@UOM_Name", ob.UOM_Name)
              
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_Unit_Insert]", para);

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
    public string Board_Update(BO_UNIT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@UOM_ID",ob.UOM_ID),
            new SqlParameter("@UOM_Name", ob.UOM_Name)

            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_Unit_Update]", para);

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
    public string Board_Delete(BO_UNIT ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@UOM_ID", ob.UOM_ID)
            
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_Unit_Delete]", para);

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

        BO_UNIT bo = new BO_UNIT();
        dtboard = null;
        try
        {
            string Q = "select * from [UOM_MST]";
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
    public DataTable BoardView(BO_UNIT ob)
    {

        BO_UNIT bo = new BO_UNIT();
        dtboard = null;
        try
        {
            string Q = "select * from [UOM_MST] where  UOM_ID =@UOM_ID ";
            SqlParameter[] para = null;
            para = new SqlParameter[] { new SqlParameter("@UOM_ID", ob.UOM_ID) };
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

     ~BL_UNIT()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}