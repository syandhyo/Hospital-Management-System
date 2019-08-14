using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Luminious.DataAcessLayer;
using Luminious.Connection;
/// <summary>
/// Summary description for BL_ITEM
/// </summary>
public class BL_ITEM:IDisposable
{
    private bool disposed = false;
    SqlParameter[] para = null;
    DataTable dtboard = new DataTable();
    DataTable dt = new DataTable();

	public BL_ITEM()
	{
		//
		// TODO: Add constructor logic here
		//
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

         ~BL_ITEM()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public string Board_Insert(BO_ITEM ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
          
            new SqlParameter("@ASSET_NAME", ob.ASSET_NAME),
            new SqlParameter("@CAT_ID", ob.CAT_ID),
            new SqlParameter("@SBCAT_ID", ob.SBCAT_ID),
            new SqlParameter("@UOM_ID", ob.UOM_ID),
            new SqlParameter("@COLOR_ID", ob.COLOR_ID),
            new SqlParameter("@QNTY", ob.QNTY),
            new SqlParameter("@PRICE", ob.PRICE),  
            new SqlParameter("@DEPRICT", ob.DEPRICT),
              new SqlParameter("@BRANCH_ID", ob.BRANCH_ID),  
            new SqlParameter("@BRANCH_FY", ob.BRANCH_FY)

            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_item_Insert]", para);

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
    public string Board_Update(BO_ITEM ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@ASSET_ID", ob.ASSET_ID),
            new SqlParameter("@ASSET_NAME", ob.ASSET_NAME),
            new SqlParameter("@CAT_ID", ob.CAT_ID),
            new SqlParameter("@SBCAT_ID", ob.SBCAT_ID),
            new SqlParameter("@UOM_ID", ob.UOM_ID),
            new SqlParameter("@COLOR_ID", ob.COLOR_ID),
            new SqlParameter("@QNTY", ob.QNTY),
            new SqlParameter("@PRICE", ob.PRICE),  
            new SqlParameter("@DEPRICT", ob.DEPRICT)
            };

            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_item_Update]", para);

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
    public DataTable BoardView()
    {

        BO_ITEM bo = new BO_ITEM();
        dtboard = null;
        try
        {
            string Q = "select * from [Vw_assetentry]";
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
    public DataTable BoardView(BO_ITEM ob)
    {

        BO_ITEM bo = new BO_ITEM();
        dtboard = null;
        try
        {
            string Q = "select * from [ASSET_MST] where  ASSET_ID =@ASSET_ID ";
            SqlParameter[] para = null;
            para = new SqlParameter[] { new SqlParameter("@ASSET_ID", ob.ASSET_ID) };
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
    public string Board_Delete(BO_ITEM ob)
    {
        string Result = null;
        string count = null;
        try
        {
            para = new SqlParameter[] { 
            new SqlParameter("@ASSET_ID", ob.ASSET_ID)
            
            };


            int res = SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, CommandType.StoredProcedure, "[sp_item_Delete]", para);

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
}