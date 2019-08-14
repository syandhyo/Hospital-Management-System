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

public partial class RECEPTION_Finalbill_reciept : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    string fr, to;
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
       IDataReader dr = COM.ExecuteReader();
        if (dr.Read())
        {
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
      
        con.Close();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

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
            string S = Session["PAYMENT"].ToString();


            da = new SqlDataAdapter("select A.NAME AS HNAME,A.PHONE AS HPHONE,A.ADDRESS AS HADDRESS,A.GSTNO AS HGSTNO,B.NAME AS PNAME,B.DATE AS ADDATE,B.AGE AS PAGE,B.GENDER AS PGENDER,B.BEDNO AS PBEDNO,B.WARD AS PWARD,B.PADDRESS AS PADDRESS,B.PHONE AS PHONE,C.VOUCHERNO AS VOUCHERNO,C.MODE AS MODE,C.CHARGES AS DEBIT,C.CREDITS AS CREDITS,C.DATETIME AS CDATE,C.DESCRIPTION AS DESCP,D.DISCAMT AS DISCAMT,D.ID AS ID,D.DATE AS DDATE,C.PID AS PID from ORG_TABLE A,ADMISSION_TABLE B,PA_TRANS C,DISCHARGE_PAYMENT D WHERE B.ID=C.PID AND D.PID=C.PID AND C.PID='"+S.ToString()+"'", con);
            ds = new DataSet();
            DataTable dt1 = new DataTable();
            da.Fill(ds, "STOCK");
            dt1 = ds.Tables["STOCK"];
            int i = 0, k = 0;
            ds2.FINALBILL.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.FINALBILL.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["HNAME"].ToString(), dtr["HPHONE"].ToString(), dtr["HADDRESS"].ToString(), dtr["HGSTNO"].ToString(), dtr["PNAME"].ToString(), dtr["ADDATE"].ToString(), dtr["PAGE"].ToString(), dtr["PGENDER"].ToString(), dtr["PBEDNO"].ToString(), dtr["PWARD"].ToString(), dtr["PADDRESS"].ToString(), dtr["PHONE"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["MODE"].ToString(), dtr["DEBIT"].ToString(), dtr["CREDITS"].ToString(), dtr["CDATE"].ToString(), dtr["DESCP"].ToString(), dtr["DISCAMT"].ToString(), dtr["DDATE"].ToString(), dtr["PID"].ToString() });
                k++;
                i += 1;
            }
            RECEPTION_Finalbill_reciept cr = new RECEPTION_Finalbill_reciept();
            rodc.Load(Server.MapPath("~/REPORTS/provisionalbill.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();
            cr.rodc.Close();
            cr.rodc.Dispose();
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