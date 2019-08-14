using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Diagnostics;


public partial class ADMIN_Barcode2 : System.Web.UI.Page
{
    public static string UserData;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        //    InstalledFontCollection fontList = new InstalledFontCollection();
        //    foreach (FontFamily family in fontList.Families)
        //    {
        //        drpFontType.Items.Add(family.Name);
        //    }
        //    foreach (System.Reflection.PropertyInfo prop in typeof(Color).GetProperties())
        //    {
        //        if (prop.PropertyType.FullName == "System.Drawing.Color")
        //            drpFontcolor.Items.Add(prop.Name);
        //    }
        //}
        //string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        //SqlConnection con = new SqlConnection(cs);
        //SqlCommand cmd2 = new SqlCommand("Select * from barcodeprint", con);
        //DataSet ds = new DataSet();
        //SqlDataAdapter sda = new SqlDataAdapter(cmd2);
        //sda.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    gvBarcode.DataSource = ds.Tables[0];
        //    gvBarcode.DataBind();
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (btnAdd.Text == "Add")
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        //    SqlConnection con = new SqlConnection(cs);
        //    SqlCommand cmd2 = new SqlCommand("Select * from barcodeprint", con);
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd2);
        //    sda.Fill(ds);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //      ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Only one record can be inserted');", true);
        //        return;
                
        //    }


        //    //SqlConnection con = new SqlConnection(cs);
        //    con.Open();
        //    int x = 1;
        //    SqlCommand cmd = new SqlCommand("insert into barcodeprint (slno,xmargin,ymargin,width,height,fonttype,fontsize,fontcolor,linegap) values ('" + Convert.ToInt32(x) + "','" + Convert.ToInt32(txtXmargin.Text) + "','" + txtYmargin.Text + "','" + Convert.ToInt32(txtWidth.Text) + "','" + Convert.ToInt32(txtHeight.Text) + "','" + drpFontType.SelectedValue.ToString() + "','" + drpFontsize.SelectedValue.ToString() + "','" + drpFontcolor.SelectedValue.ToString() + "','" + drpLineGap.SelectedValue.ToString() + "')", con);
        //    int i = cmd.ExecuteNonQuery();
        //    con.Close();
        //    if (i > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Record inserted Successfully');", true);
        //    }
        //}
        //if (btnAdd.Text == "Update")
        //{
        //    btnAdd.Text = "Add";
        //    string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        //    SqlConnection con = new SqlConnection(cs);
        //    con.Open();
        //    int x = 1;
        //    SqlCommand cmd = new SqlCommand("update barcodeprint set xmargin ='" + Convert.ToInt32(txtXmargin.Text) + "',ymargin='" + txtYmargin.Text + "',width='" + Convert.ToInt32(txtWidth.Text) + "',height= '" + Convert.ToInt32(txtHeight.Text) + "',fonttype ='" + drpFontType.SelectedValue.ToString() + "',fontsize='" + drpFontsize.SelectedValue.ToString() + "',fontcolor='" + drpFontcolor.SelectedValue.ToString() + "',linegap = '" + drpLineGap.SelectedValue.ToString() + "' where id ='"+UserData+"'", con);
        //    int i = cmd.ExecuteNonQuery();
        //    con.Close();
        //    if (i > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Record Updated Successfully');", true);
        //    }
        //}
    }

    protected void gvBarcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridViewRow g = gvBarcode.SelectedRow;
        //Label self = (Label)gvBarcode.SelectedRow.FindControl("lblId");
        //UserData = self.Text;
        //txtXmargin.Text = "" + g.Cells[1].Text + "";
        //txtYmargin.Text = "" + g.Cells[2].Text + "";
        //txtWidth.Text = "" + g.Cells[3].Text + "";
        //txtHeight.Text = "" + g.Cells[4].Text + "";
        //drpFontType.SelectedIndex = drpFontType.Items.IndexOf(drpFontType.Items.FindByText(g.Cells[5].Text));
        //drpFontsize.SelectedIndex = drpFontsize.Items.IndexOf(drpFontsize.Items.FindByText(g.Cells[6].Text));
        //drpFontcolor.SelectedIndex = drpFontcolor.Items.IndexOf(drpFontcolor.Items.FindByText(g.Cells[7].Text));
        //drpLineGap.SelectedIndex = drpLineGap.Items.IndexOf(drpLineGap.Items.FindByText(g.Cells[8].Text));
        ////Btnsubmit.Text = "Update";
        //btnAdd.Text = "Update";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
       // Response.Redirect("~/ADMIN/Barcode2.aspx");
    }
    protected void btnGeneratebarcode_Click(object sender, EventArgs e)
    {
        // string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        //SqlConnection con = new SqlConnection(cs);
        //SqlCommand cmd2 = new SqlCommand("Select * from barcodeprint", con);
        //DataSet ds1 = new DataSet();
        //SqlDataAdapter sda = new SqlDataAdapter(cmd2);
        //sda.Fill(ds1);
        //if (ds1.Tables[0].Rows.Count > 0)
        //{
        //    string cmpnynm, addrs, phone, barcodeautognrt;
        //    string fnttype, fntcolor, pcs, mrp, oth, mcgmpc;
        //    int x, y, width, height, fntsize, lingap, grswt, netwt, purity, mc, cmpnycode, h, itmnm;
        //    x = int.Parse(ds1.Tables[0].Rows[0]["xmargin"].ToString());
        //    y = int.Parse(ds1.Tables[0].Rows[0]["ymargin"].ToString());
        //    width = int.Parse(ds1.Tables[0].Rows[0]["width"].ToString());
        //    height = int.Parse(ds1.Tables[0].Rows[0]["height"].ToString());
        //    fnttype = ds1.Tables[0].Rows[0]["fonttype"].ToString();
        //    fntsize = int.Parse(ds1.Tables[0].Rows[0]["fontsize"].ToString());
        //    fntcolor = ds1.Tables[0].Rows[0]["fontcolor"].ToString();
        //    lingap = int.Parse(ds1.Tables[0].Rows[0]["linegap"].ToString());
        //    grswt = int.Parse(ds1.Tables[0].Rows[0]["grosswt"].ToString());
        //    netwt = int.Parse(ds1.Tables[0].Rows[0]["netwt"].ToString());
        //    //barcodeautognrt = txt_strmbarcode.Text;

        //    //Font fnt1 = new Font("Arial Black", 6, FontStyle.Bold);
        //    //Font fnt2 = new Font("Arial Black", 5, FontStyle.Bold);
        //    //Font fnt = new Font(fnttype, fntsize, FontStyle.Regular);

        //    //System.Drawing.Image myimage = default(System.Drawing.Image);
        //    //myimage = Code128Rendering.MakeBarcodeImage(barcodeautognrt, 1, true);
        //    //barcodepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        //    //barcodepicture.Size = new Size(width, height);
        //    //barcodepicture.Image = myimage;

        //    //Bitmap myBitmap1 = new Bitmap(barcodepicture.Width, barcodepicture.Height);
        //    //barcodepicture.DrawToBitmap(myBitmap1, new Rectangle(x, y, barcodepicture.Width, barcodepicture.Height));

        //    //e.Graphics.DrawString(cmpnynm, fnt2, Brushes.Black, x, y);
        //    //e.Graphics.DrawImage(barcodepicture.Image, x - 6, y + 12, barcodepicture.Width, barcodepicture.Height);
        //    //e.Graphics.DrawString(txt_strmbarcode.Text, fnt, Brushes.Black, x, y + 25);
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == "")
        {
            return;
        }
        if (File.Exists(Server.MapPath("Barcode.txt")))
        {
            File.Delete(Server.MapPath("BarCode.txt"));
        }
        //File.Create(Server.MapPath("BarCode.txt"));
        File.WriteAllText(Server.MapPath("BarCode.txt"), TextBox1.Text);

        Process.Start(Server.MapPath("BarCodeGenerate.exe"));
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Image1.Height = 30;
        Image1.Width = 150;
        Image1.ImageUrl = "~images/" + TextBox1.Text + ".png";
    }
}