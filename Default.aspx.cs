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

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        Image1.Height= 30;
        Image1.Width = 150;
        Image1.ImageUrl = "images/" + TextBox1.Text + ".png";
    }
}
