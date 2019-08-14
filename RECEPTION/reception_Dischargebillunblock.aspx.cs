using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Drawing.Printing;

public partial class RECEPTION_reception_Dischargebillunblock : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    string fr, to;
    DataMathods OBJ_METHOD = new DataMathods();

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
        lblorgid.Text = Session["ORGID"].ToString();
        string S = Session["DID"].ToString();

        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.ADVI,A.BEDNO,A.CSUMM,A.DATE,A.DIAG,A.DISCHARGE,A.DISCU,A.ID,A.INVS,A.NAME,A.NOTE,A.PID,A.STATUS,A.TREAT,A.WARD,B.DATE AS ADDATE,C.NAME AS ORGNAME,C.ADDRESS,C.EMAIL,C.GSTNO,C.PHONE+','+C.PHONE2 AS PHONE,C.REGNO,b.AGE,B.PADDRESS AS PADDRESS,A.PAST,A.COMPLAINS,A.REMARKS  FROM DISCHARGE_TABLE A,ADMISSION_TABLE B,ORG_TABLE C where A.ID='" + S.ToString() + "' AND B.VN=A.PID", false,false);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt1 = new DataTable();
            Ds.Tables[0].TableName = "STOCK";
            dt1 = Ds.Tables["STOCK"];
            int i = 0, k = 0;
            ds2.DISCHARGERE.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.DISCHARGERE.Rows.Add(new string[] { dtr["ADVI"].ToString(), dtr["BEDNO"].ToString(), dtr["CSUMM"].ToString(), dtr["DATE"].ToString(), dtr["DIAG"].ToString(), dtr["DISCHARGE"].ToString(), dtr["DISCU"].ToString(), dtr["ID"].ToString(), dtr["INVS"].ToString(), dtr["NAME"].ToString(), dtr["NOTE"].ToString(), dtr["PID"].ToString(), dtr["STATUS"].ToString(), dtr["TREAT"].ToString(), dtr["WARD"].ToString(), dtr["ADDATE"].ToString(), dtr["ORGNAME"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["PAST"].ToString(), dtr["COMPLAINS"].ToString(), dtr["REMARKS"].ToString() });
                k++;
                i += 1;
            }
            RECEPTION_reception_Dischargebillunblock cr = new RECEPTION_reception_Dischargebillunblock();
            rodc.Load(Server.MapPath("~/REPORTS/dischargebillreportunblock.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();
            con.Close();

        }
 
    }
    string GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            settings.PrinterName = printer;
            if (settings.IsDefaultPrinter)
                return printer;
        }
        return string.Empty;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            rodc.PrintOptions.PrinterName = GetDefaultPrinter();
            rodc.PrintToPrinter(1, false, 0, 0);
            //rodc1.PrintOptions.PrinterName = GetDefaultPrinter();
            // rodc1.PrintToPrinter(1, false, 0, 0);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //return;
            // Response.Write("<script LANGUAGE='JavaScript' >alert('connect printer settings')</script>");
        }
    }
}