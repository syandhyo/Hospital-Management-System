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

public partial class RECEPTION_admission_reciept : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1,da2;
    DataSet ds, ds1,ds3;
    DataRow dtr,dtr1;
    SqlDataReader dr,dr1,dr2;
    DataSet2 ds2 = new DataSet2();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ReportDocument rodc1 = new ReportDocument();
    ReportDocument rodc2 = new ReportDocument();
    RECEPTION_admission_reciept cr1 ;
    RECEPTION_admission_reciept cr2 ;
    RECEPTION_admission_reciept cr;
    string fr, to;
    protected void CloseReport()
    {
        try
        {
            if (rodc != null)
            {
                Sections objSections = rodc.ReportDefinition.Sections;
                foreach (Section objSection in objSections)
                {
                    ReportObjects objReports = objSection.ReportObjects;
                    foreach (ReportObject rptObj in objReports)
                    {
                        if (rptObj.Kind.Equals(CrystalDecisions.Shared.ReportObjectKind.SubreportObject))
                        {
                            SubreportObject subreportObject = (SubreportObject)rptObj;
                            ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                            subReportDocument.Close();
                        }
                    }
                }
                rodc.Close();
                rodc.Dispose();
                cr.rodc.Close();
                cr.rodc.Clone();
                cr.rodc.Dispose();
                rodc = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            if (CrystalReportViewer1 != null)
            {
                CrystalReportViewer1.ReportSource = null;
                CrystalReportViewer1.Dispose();
                cr.rodc.Close();
                cr.rodc.Clone();
                cr.rodc.Dispose();
                rodc = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

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
        lblorgid.Text = Session["ORGID"].ToString();
        //string S = Session["AID"].ToString();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();

        da = new SqlDataAdapter("select * from VISTOR_TABLE order by SLNO DESC", con);
        ds = new DataSet();
        DataTable dt1 = new DataTable();
        da.Fill(ds, "STOCK");
        dt1 = ds.Tables["STOCK"];
        int i = 0, k = 0;
        ds2.VR.Rows.Clear();
        while (i < dt1.Rows.Count)
        {
            dtr = dt1.Rows[i];
            ds2.VR.Rows.Add(new string[] { dtr["VNO"].ToString(), dtr["DATE"].ToString(), dtr["UHID"].ToString(), dtr["PNAME"].ToString(), dtr["VNAME"].ToString(), dtr["RELATION"].ToString() });
            k++;
            i += 1;
        }
        cr = new RECEPTION_admission_reciept();
        rodc.Load(Server.MapPath("~/MATREPORT/VR.rpt"));
        rodc.SetDataSource(dt1);
        CrystalReportViewer1.ReportSource = rodc;
        CrystalReportViewer1.DataBind();
        con.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
       
        CloseReport();
      
        cr.rodc.Close();
        cr.rodc.Dispose();
      
        rodc = null;
      
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
    }
   
}