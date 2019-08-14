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

public partial class ASSET_cat_mst : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            //CreateRandomHashing();
            //CrossSiteRequestForgery();
            Fill_Board();
            Clear();

        }
    }
    private void Clear()
    {
        try
        {
            txtcat.Text = string.Empty;
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
            using (BL_CAT obj = new BL_CAT())
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
                using (BL_CAT obj = new BL_CAT())
                {
                    BO_CAT ob = new BO_CAT
                      {

                          CAT_NAME = !string.IsNullOrEmpty(txtcat.Text) ? txtcat.Text.Trim() : null,
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
                using (BL_CAT obj = new BL_CAT())
                {
                    BO_CAT ob = new BO_CAT
                    {
                        CAT_ID = Convert.ToInt32(hdfdUserTblId.Value),
                        CAT_NAME = !string.IsNullOrEmpty(txtcat.Text) ? txtcat.Text.Trim() : null,


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
            BO_CAT ob = new BO_CAT();
            ob.CAT_ID = id;
            using (BL_CAT blan = new BL_CAT())
            {
                DataTable obj = null;
                obj = blan.BoardView(ob);
                hdfdUserTblId.Value = obj.Rows[0]["CAT_ID"] != DBNull.Value ? obj.Rows[0]["CAT_ID"].ToString() : null;
                txtcat.Text = obj.Rows[0]["CAT_NAME"] != DBNull.Value ? obj.Rows[0]["CAT_NAME"].ToString() : null;

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
            using (BL_CAT obj = new BL_CAT())
                {
                    string msg = obj.Board_Delete(new BO_CAT { CAT_ID = id });
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