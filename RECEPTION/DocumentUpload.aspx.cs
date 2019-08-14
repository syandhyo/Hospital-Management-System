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

public partial class RECEPTION_DocumentUpload : System.Web.UI.Page
{

   // string num1 = "SJ000";
    //SqlConnection con;
    //SqlDataAdapter da;
    DataSet ds = new DataSet();
    //SqlCommand com, cmd;
    SqlDataReader dr;
   
   // DataRow dtr;
   // string strimm;
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        //SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        //IDataReader dr = COM.ExecuteReader();
        //if (dr.Read())
        //{
        //    //Label1.Text = dr["FYEAR"].ToString();
        //    lblfyear.Text = dr["FYEAR"].ToString();
        //}
        //dr.Close();

        con.Close();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtspid.Text =="")
            {

                string message = "alert('Please!!Enter The IPD No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            using (SqlCommand cmd = new SqlCommand("sp_DocumentUpload", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtspid.Text;
                cmd.Parameters.Add("@PATIENTID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("@FILEUPLOAD", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@SYSDATE", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                //SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where VN='" + txtspid.Text + "' ORDER BY VN DESC", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    div2.Visible = true;
                    div1.Visible = false;
                    lblname.Text = dr["NAME"].ToString();
                    txtdate.Text = dr["DATE"].ToString();
                    lblip.Text = dr["VN"].ToString();

                }
                else
                {
                    string message = "alert('* Id is not found.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                    //dr.Close();
                }
            }
            dr.Close();

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        string s = "PatientDocument/" + lblip.Text.ToString()+fluImg.FileName;
           
        try
        {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
            using (SqlCommand cmd = new SqlCommand("sp_DocumentUpload", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@PATIENTID", SqlDbType.VarChar).Value = lblip.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblname.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                cmd.Parameters.Add("@FILEUPLOAD", SqlDbType.VarChar).Value = s.ToString();
                cmd.Parameters.Add("@SYSDATE", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                cmd.ExecuteNonQuery();
                fluImg.SaveAs(Server.MapPath(s));

                string message = "alert('Document Uploaded Sucessfully.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                // clearcontrol();
            }              

        }
            
        catch (Exception ex)
        {
            
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       
        // binddata();
        con.Close();
        div2.Visible = false;
        div1.Visible = true;
        txtspid.Text = "";
    }
    //---------File Upload---------
    public void Photo_Signature()
    {
       
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