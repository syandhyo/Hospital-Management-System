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
using QRCoder;
using System.IO;
using System.Drawing;

public partial class ASSET_item_entry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binddrop();
            Fill_Board();
            Clear();

        }
    }
    public void binddrop()
    {
        SqlHelper.DropDownListPopulate(Luminious.Connection.Configuration.ConnectionString, "select CAT_ID,CAT_NAME FROM CAT_MASTER", CommandType.Text, DDL_Cat, "---Select Division---");
        //SqlHelper.DropDownListPopulate(Luminious.Connection.Configuration.ConnectionString, "select SBCAT_ID,SBCAT_NAME FROM SUBCAT_MST", CommandType.Text, DDL_Sbcat, "---Select Division---");
        SqlHelper.DropDownListPopulate(Luminious.Connection.Configuration.ConnectionString, "select UOM_ID,UOM_Name FROM UOM_MST", CommandType.Text, DDL_UOM, "---Select Unit---");
        SqlHelper.DropDownListPopulate(Luminious.Connection.Configuration.ConnectionString, "select COLOR_ID,COLOR_Name FROM COLOR_MST", CommandType.Text, DDL_clr, "---Select Colour---");
    }
    private void Clear()
    {
        try
        {
            txtitem.Text = string.Empty;
            DDL_Cat.SelectedValue = "0";
            DDL_Cat_SelectedIndexChanged(null, null);
            DDL_Sbcat.SelectedValue = "0";
            //DDL_Sbcat_SelectedIndexChanged(null, null);
            DDL_UOM.SelectedValue = "0";
            //DDL_UOM_SelectedIndexChanged(null, null);
            DDL_clr.SelectedValue = "0";
            
            txtqty.Text = " ";
            txtprc.Text = " ";
            txtdpt.Text = " ";
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
            using (BL_ITEM obj = new BL_ITEM())
            {
                GrdVw3.DataSource = obj.BoardView();
                GrdVw3.DataBind();

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
                using (BL_ITEM obj = new BL_ITEM())
                {
                    BO_ITEM ob = new BO_ITEM
                    {

                        CAT_ID = (DDL_Cat.SelectedValue != "" ? long.Parse(DDL_Cat.SelectedValue) : (long?)null),
                        SBCAT_ID = (DDL_Sbcat.SelectedValue != "" ? long.Parse(DDL_Sbcat.SelectedValue) : (long?)null),
                        ASSET_NAME = !string.IsNullOrEmpty(txtitem.Text) ? txtitem.Text.Trim() : null,
                        UOM_ID = (DDL_UOM.SelectedValue != "" ? long.Parse(DDL_UOM.SelectedValue) : (long?)null),
                        COLOR_ID = (DDL_clr.SelectedValue != "" ? long.Parse(DDL_clr.SelectedValue) : (long?)null),
                        QNTY = !string.IsNullOrEmpty(txtqty.Text) ? Convert.ToDecimal(txtqty.Text.Trim()) : (decimal?)null,
                        PRICE = !string.IsNullOrEmpty(txtprc.Text) ? Convert.ToDecimal(txtprc.Text.Trim()) : (decimal?)null,
                        DEPRICT = !string.IsNullOrEmpty(txtdpt.Text) ? Convert.ToDecimal(txtdpt.Text.Trim()) : (decimal?)null,
                        BRANCH_ID = Convert.ToInt32(Session["BRANCH_FYR"]) ,
                        BRANCH_FY = Convert.ToInt32(Session["Branch"]),
                        //BRANCH_ID = (Convert.ToInt32(Session["BRANCH_FYR"]) != "" ? long.Parse((Session["BRANCH_FYR"])) : (long?)null),
                        //BRANCH_FY = (DDL_Cat.SelectedValue != "" ? long.Parse(DDL_Cat.SelectedValue) : (long?)null),

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
                using (BL_ITEM obj = new BL_ITEM())
                {
                    BO_ITEM ob = new BO_ITEM
                    {
                        ASSET_ID = Convert.ToInt32(hdfdUserTblId.Value),
                        CAT_ID = (DDL_Cat.SelectedValue != "" ? long.Parse(DDL_Cat.SelectedValue) : (long?)null),
                        SBCAT_ID = (DDL_Sbcat.SelectedValue != "" ? long.Parse(DDL_Sbcat.SelectedValue) : (long?)null),
                        ASSET_NAME = !string.IsNullOrEmpty(txtitem.Text) ? txtitem.Text.Trim() : null,
                        UOM_ID = (DDL_UOM.SelectedValue != "" ? long.Parse(DDL_UOM.SelectedValue) : (long?)null),
                        COLOR_ID = (DDL_clr.SelectedValue != "" ? long.Parse(DDL_clr.SelectedValue) : (long?)null),
                        QNTY = !string.IsNullOrEmpty(txtqty.Text) ? Convert.ToDecimal(txtqty.Text.Trim()) : (decimal?)null,
                        PRICE = !string.IsNullOrEmpty(txtprc.Text) ? Convert.ToDecimal(txtprc.Text.Trim()) : (decimal?)null,
                        DEPRICT = !string.IsNullOrEmpty(txtdpt.Text) ? Convert.ToDecimal(txtdpt.Text.Trim()) : (decimal?)null,

                    };
                    string msg = obj.Board_Update(ob);
                    if (msg == "1")
                    {
                        //lblErrorMsg.Text = "Section Update Sucessfully";
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
    protected void DDL_Cat_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
        {
            if (DDL_Cat.SelectedValue != "0")
            {
                DDL_Sbcat.Items.Clear();
        SqlHelper.DropDownListPopulate(Luminious.Connection.Configuration.ConnectionString, "select SBCAT_ID,SBCAT_NAME FROM SUBCAT_MST where CAT_ID = '"+DDL_Cat.SelectedValue +"'", CommandType.Text, DDL_Sbcat, "---Select Division---");
            }
            else
            {
                DDL_Sbcat.Items.Clear();
            }
        }
         catch (Exception ex)
         {
             ExceptionHandler.WriteException(ex.Message, true);
         }
    }


    protected void GrdVw3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdVw3.PageIndex = e.NewPageIndex;
            Fill_Board();
        }
        catch (Exception ex)
        {
            ExceptionHandler.WriteException(ex.Message, true);
        }
    }

    protected void GrdVw3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Clear();
        try
        {

            var id = Convert.ToInt32(GrdVw3.DataKeys[e.RowIndex].Value);
            BO_ITEM ob = new BO_ITEM();
            ob.ASSET_ID = id;
            using (BL_ITEM blan = new BL_ITEM())
            {
                DataTable obj = null;
                obj = blan.BoardView(ob);
                hdfdUserTblId.Value = obj.Rows[0]["ASSET_ID"] != DBNull.Value ? obj.Rows[0]["ASSET_ID"].ToString() : null;
                DDL_Cat.SelectedValue = obj.Rows[0]["CAT_ID"] != DBNull.Value ? obj.Rows[0]["CAT_ID"].ToString() : "0";
                DDL_Cat_SelectedIndexChanged(null, null);
                DDL_Sbcat.SelectedValue = obj.Rows[0]["SBCAT_ID"] != DBNull.Value ? obj.Rows[0]["SBCAT_ID"].ToString() : "0";
                //DDL_Sbcat_SelectedIndexChanged(null, null);
                txtitem.Text = obj.Rows[0]["ASSET_NAME"] != DBNull.Value ? obj.Rows[0]["ASSET_NAME"].ToString() : null;
                DDL_UOM.SelectedValue = obj.Rows[0]["UOM_ID"] != DBNull.Value ? obj.Rows[0]["UOM_ID"].ToString() : "0";
                //DDL_UOM_SelectedIndexChanged(null, null);
                DDL_clr.SelectedValue = obj.Rows[0]["COLOR_ID"] != DBNull.Value ? obj.Rows[0]["COLOR_ID"].ToString() : "0";
                txtqty.Text = obj.Rows[0]["QNTY"] != DBNull.Value ? obj.Rows[0]["QNTY"].ToString() : null;
                txtprc.Text = obj.Rows[0]["PRICE"] != DBNull.Value ? obj.Rows[0]["PRICE"].ToString() : null;
                txtdpt.Text = obj.Rows[0]["DEPRICT"] != DBNull.Value ? obj.Rows[0]["DEPRICT"].ToString() : null;
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
    protected void GrdVw3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            var id = Convert.ToInt32(GrdVw3.DataKeys[e.RowIndex].Value);
            using (BL_ITEM obj = new BL_ITEM())
            {
                string msg = obj.Board_Delete(new BO_ITEM { ASSET_ID = id });
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
    protected void GrdVw3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string id = GrdVw3.Rows[e.NewSelectedIndex].Cells[1].Text;
        string name = GrdVw3.Rows[e.NewSelectedIndex].Cells[2].Text;
        string address = GrdVw3.Rows[e.NewSelectedIndex].Cells[3].Text;
        string phone = GrdVw3.Rows[e.NewSelectedIndex].Cells[8].Text;
       // string code = id + "," + name + "," + address + "," + phone;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(phone, QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
           // plBarCode.Controls.Add(imgBarCode);
        }
    }
}