using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Luminious.DataAcessLayer;
using Luminious.Connection;
using System.Data;
using System.Data.SqlClient;

public partial class ASSET_Sbcat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            binddrop();
            Fill_Board();
            Clear();

        }
    }
    public void binddrop()
    {
        SqlHelper.DropDownListPopulate(Luminious.Connection.Configuration.ConnectionString, "select CAT_ID,CAT_NAME FROM CAT_MASTER", CommandType.Text, ddl_CatName, "---Select Division---");
    }
    private void Clear()
    {
        try
        {
            txtsbcat.Text = string.Empty;
            ddl_CatName.SelectedValue = "0";
            btnSubmit.Text = "Submit";

        }
        catch (Exception ex)
        {
            ExceptionHandler.WriteException(ex.Message, true);
        }
    }
    private void Fill_Board()
    {
        try
        {
            using (BL_SBCAT obj = new BL_SBCAT())
            {
                GrdVw1.DataSource = obj.BoardView();
                GrdVw1.DataBind();

            }
        }
        catch (Exception ex)
        {
            ExceptionHandler.WriteException(ex.Message, true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text.Equals("Submit"))
        {
            try
            {
                using (BL_SBCAT obj = new BL_SBCAT())
                {
                    BO_SBCAT ob = new BO_SBCAT
                    {
                        CAT_ID = (ddl_CatName.SelectedValue != "" ? long.Parse(ddl_CatName.SelectedValue) : (long?)null),
                        SBCAT_NAME = !string.IsNullOrEmpty(txtsbcat.Text) ? txtsbcat.Text.Trim() : null,
                    };
                    string msg = obj.Board_Insert(ob);
                    if (msg == "1")
                    {
                        //lblErrorMsg.Text = "Board Meeting Saved Sucessfully";
                        // lblErrorMsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        //lblErrorMsg.Text = "Error in data saving";
                        //lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }

                Clear();
                Fill_Board();
            }
            catch (Exception ex)
            {
                ExceptionHandler.WriteException(ex.Message, true);
            }

        }
        else
        {
            try
            {
                using (BL_SBCAT obj = new BL_SBCAT())
                {
                    BO_SBCAT ob = new BO_SBCAT
                    {
                        SBCAT_ID = Convert.ToInt32(hdfdUserTblId.Value),
                        CAT_ID = (ddl_CatName.SelectedValue != "" ? long.Parse(ddl_CatName.SelectedValue) : (long?)null),
                        SBCAT_NAME = !string.IsNullOrEmpty(txtsbcat.Text) ? txtsbcat.Text.Trim() : null,


                    };
                    string msg = obj.Board_Update(ob);
                    if (msg == "1")
                    {
                        //lblErrorMsg.Text = "Board Meeting Updated Sucessfully";
                        //lblErrorMsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        //lblErrorMsg.Text = "Error in data Updating";
                        //lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    Clear();
                    Fill_Board();

                }

            }


            catch (Exception ex)
            {
                ExceptionHandler.WriteException(ex.Message, true);
            }
        }
    }

    protected void GrdVw1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdVw1.PageIndex = e.NewPageIndex;
            Fill_Board();
        }
        catch (Exception ex)
        {
            ExceptionHandler.WriteException(ex.Message, true);
        }
    }

    protected void GrdVw1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Clear();
        try
        {

            var id = Convert.ToInt32(GrdVw1.DataKeys[e.RowIndex].Value);
            BO_SBCAT ob = new BO_SBCAT();
            ob.SBCAT_ID = id;
            using (BL_SBCAT blan = new BL_SBCAT())
            {
                DataTable obj = null;
                obj = blan.BoardView(ob);
                hdfdUserTblId.Value = obj.Rows[0]["SBCAT_ID"] != DBNull.Value ? obj.Rows[0]["SBCAT_ID"].ToString() : null;
                ddl_CatName.SelectedValue = obj.Rows[0]["CAT_ID"] != DBNull.Value ? obj.Rows[0]["CAT_ID"].ToString() : "0";
                txtsbcat.Text = obj.Rows[0]["SBCAT_NAME"] != DBNull.Value ? obj.Rows[0]["SBCAT_NAME"].ToString() : null;

                btnSubmit.Text = "Update";
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
    }

    protected void GrdVw1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            var id = Convert.ToInt32(GrdVw1.DataKeys[e.RowIndex].Value);
            using (BL_SBCAT obj = new BL_SBCAT())
            {
                string msg = obj.Board_Delete(new BO_SBCAT { SBCAT_ID = id });
                if (msg == "1")
                {
                    //lblErrorMsg.Text = "Board Deleted Sucessfully";
                    //lblErrorMsg.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    //lblErrorMsg.Text = "Error in data Deleting";
                    //lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                }
                Fill_Board();
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
    }
}