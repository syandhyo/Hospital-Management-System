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

public partial class ASSET_color_mst : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Fill_Board();
            Clear();

        }
    }
    private void Clear()
    {
        try
        {
            txtcol.Text = string.Empty;
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
            using (BL_COLOUR obj = new BL_COLOUR())
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text.Equals("Submit"))
        {
            try
            {
                using (BL_COLOUR obj = new BL_COLOUR())
                {
                    BO_COLOUR ob = new BO_COLOUR
                    {

                        COLOR_Name = !string.IsNullOrEmpty(txtcol.Text) ? txtcol.Text.Trim() : null,
                    };
                    string msg = obj.Board_Insert(ob);
                    if (msg == "1")
                    {
                        //lblErrorMsg.Text = "Board Meeting Saved Sucessfully";
                        // lblErrorMsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
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
                using (BL_COLOUR obj = new BL_COLOUR())
                {
                    BO_COLOUR ob = new BO_COLOUR
                    {
                        COLOR_ID = Convert.ToInt32(hdfdUserTblId.Value),
                        COLOR_Name = !string.IsNullOrEmpty(txtcol.Text) ? txtcol.Text.Trim() : null,


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
    protected void GrdVw1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Clear();
        try
        {

            var id = Convert.ToInt32(GrdVw1.DataKeys[e.RowIndex].Value);
            BO_COLOUR ob = new BO_COLOUR();
            ob.COLOR_ID = id;
            using (BL_COLOUR blan = new BL_COLOUR())
            {
                DataTable obj = null;
                obj = blan.BoardView(ob);
                hdfdUserTblId.Value = obj.Rows[0]["COLOR_ID"] != DBNull.Value ? obj.Rows[0]["COLOR_ID"].ToString() : null;
                txtcol.Text = obj.Rows[0]["COLOR_Name"] != DBNull.Value ? obj.Rows[0]["COLOR_Name"].ToString() : null;

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
            using (BL_COLOUR obj = new BL_COLOUR())
            {
                string msg = obj.Board_Delete(new BO_COLOUR { COLOR_ID = id });
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