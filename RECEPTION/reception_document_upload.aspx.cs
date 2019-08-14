using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

using System.IO;
using System.Collections;
using System.Drawing.Drawing2D;

public partial class RECEPTION_reception_document_upload : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();

    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["out"] == "INACTIVE")
        {
            Response.Redirect("~/index.aspx");
        }
        Response.Buffer = true;

        Response.CacheControl = "no-cache";
        if (Session["NAME"] == null)
        {
            Response.Redirect("~/index.aspx");
        }
        lblid.Text = Session["NAME"].ToString();
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
            div1.Visible = true;
            div2.Visible = false;
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtspid.Text == "")
            {
                string message = "alert('Please!!Enter The IPD No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtspid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("sp_DocumentUpload", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                div2.Visible = true;
                div1.Visible = false;
                lblname.Text = DS1.Tables[0].Rows[0]["NAME"].ToString();
                txtdate.Text = Convert.ToDateTime(DS1.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                lblip.Text = DS1.Tables[0].Rows[0]["VN"].ToString();
              
            }
            else
            {
                string message = "alert('* Id is not found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
               
            }
  
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
     {
       string message1 = string.Empty;

       string s = "PatientDocument/" + lblip.Text.ToString() + fluImg.FileName;

          //Photo_Signature(); 
            try
            {
                //string s = "PatientDocument/" + lblip.Text.ToString() + fluImg.FileName;
                // lblip.Text= Request.QueryString["xsho"].ToString();
                // AdminBLL.rollno = Convert.ToString(txtrollno.Text.Trim());
                #region Photo Upload Start.....
                string filename = "";

                if ((fluImg.PostedFile != null) && (fluImg.PostedFile.ContentLength > 0))
                {
                    int hpf = fluImg.PostedFile.ContentLength;
                    if ((hpf > 0) && (hpf < 2097152))
                    {
                        string fileExt = System.IO.Path.GetExtension(fluImg.FileName);
                        string ContntType = fluImg.PostedFile.ContentType;

                        if ((fileExt == ".gif") || (fileExt == ".GIF") || (fileExt == ".jpeg") || (fileExt == ".JPEG") || (fileExt == ".jpg") || (fileExt == ".JPG") || (fileExt == ".png") || (fileExt == ".PNG") || (fileExt == ".PDF") || (fileExt == ".txt ") || (fileExt == ".TXT ") || (fileExt == ".CPP ") || (fileExt == ".docx") || (fileExt == ".xlsx") || (fileExt == ".doc") || (fileExt == ".xls") || (fileExt == ".xltx") || (fileExt == ".xltm"))
                        {
                            //Get the file name
                            //filename = txtregistrationno.Text + "_" + Session.SessionID + "_" + Path.GetFileName(FileUpload1.FileName);


                            filename = "PatientDocument/" + lblip.Text.ToString() + fluImg.FileName;
                            //Save in to temp folder
                            //FileUpload1.PostedFile.SaveAs(MapPath("../Upload_Gallary/EmployeePhoto/" + filename));
                            string targetPath = Server.MapPath("" + filename);

                            Stream FromStream = default(Stream);
                            FromStream = fluImg.PostedFile.InputStream;
                            //Generate Thumnails
                            GenerateThumbnails(0.5, FromStream, targetPath);
                            //AdminBLL.Photo = Convert.ToString(filename);
                            string s1 = Convert.ToString(filename);
                            s1 = fluImg.FileName.ToString();

                        }
                        else
                        {
                            string message = "alert('not valid file..')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            //filename = "not valid file..";
                            return;
                        }
                    }
                    else
                    {
                        string message = "alert('Size Is More Than 2MB..')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //filename = "Size Is More Than 2MB..";
                        return;

                    }
                }
                else
                {
                    string message = "alert('Please!!Select The File..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    //filename = "Please!!Select The File..";
                    return;
                }
                #endregion Photo Upload End.........


                SqlParameter[] SQL_PARAMS = new SqlParameter[6];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@PATIENTID", SqlDbType.VarChar, 500, lblip.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 200, lblname.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@FILEUPLOAD", SqlDbType.VarChar, 500, s.ToString());
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@SYSDATE", SqlDbType.DateTime, 0, DateTime.Now.ToString());

                OBJ_METHOD.ExecuteProceedure("sp_DocumentUpload", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    fluImg.SaveAs(Server.MapPath(s));
                    message1 = "alert('Document Uploaded Sucessfully...')";
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Due to some issues, Data not saved.')";
                }

               
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            finally
            {
                //reset();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            }

        
        // binddata();

            div2.Visible = false;
            div1.Visible = true;
            txtspid.Text = "";
    }
 
    private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = (int)(image.Width * scaleFactor);
            var newHeight = (int)(image.Height * scaleFactor);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }
}