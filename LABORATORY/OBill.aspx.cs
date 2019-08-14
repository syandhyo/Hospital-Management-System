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
      


public partial class LABORATORY_Bill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    SqlDataReader dr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    //ReportDocument rodc = new ReportDocument();
    ReportDocument rodc1 = new ReportDocument();
    string fr, to;
    LABORATORY_Bill cr;
    protected void CloseReport()
    {
        try
        {
            if (rodc1 != null)
            {
                Sections objSections = rodc1.ReportDefinition.Sections;
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
                rodc1.Close();
                rodc1.Dispose();
                cr.rodc1.Close();
                cr.rodc1.Clone();
                cr.rodc1.Dispose();
                rodc1 = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            if (CrystalReportViewer2 != null)
            {
                CrystalReportViewer2.ReportSource = null;
                CrystalReportViewer2.Dispose();
                cr.rodc1.Close();
                cr.rodc1.Clone();
                cr.rodc1.Dispose();
                rodc1 = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }
        catch
        {

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
        string S = Session["LABID"].ToString();

        try
        {
            SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
            dr = COM.ExecuteReader();
            if (dr.Read())
            {
                //Label1.Text = dr["FYEAR"].ToString();
                lblfyear.Text = dr["FYEAR"].ToString();
            }
            dr.Close();

            //da = new SqlDataAdapter("SELECT A.ID AS ID,A.LINDID AS LIND,A.PID+' '+A.PNAME AS PID,A.PRICE AS PRICE,A.DATE AS DATE,B.NAME AS ORGNAME,B.PHONE AS ORGPHONE,B.GSTNO AS GSTNO,B.ADDRESS AS ADDRESS,D.INV AS INV,C.PRICE AS PPRICE,A.UNAME AS PREPAREDBY FROM LABRES_TABLE A,LABRESULT_TABLE C,TEST_COMPONENT_TABLE D, ORG_TABLE B WHERE A.ID='" + S.ToString() + "' AND A.ORGID=B.ID AND C.ID=A.ID AND C.INV=D.slno AND C.VALUE!='0'", con);
            //ds = new DataSet();
            //DataTable dt1 = new DataTable();
            //da.Fill(ds, "STOCK");
            //dt1 = ds.Tables["STOCK"];
            //int i = 0, k = 0;
            //ds2.LBILL.Rows.Clear();
            //while (i < dt1.Rows.Count)
            //{
            //    dtr = dt1.Rows[i];
            //    ds2.LBILL.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["LIND"].ToString(), dtr["PID"].ToString(), dtr["PRICE"].ToString(), dtr["DATE"].ToString(), dtr["ORGNAME"].ToString(), dtr["ORGPHONE"].ToString(), dtr["GSTNO"].ToString(), dtr["ADDRESS"].ToString(), dtr["INV"].ToString(), dtr["PPRICE"].ToString(), dtr["PREPAREDBY"].ToString() });
            //    k++;
            //    i += 1;
            //}


            da1 = new SqlDataAdapter("SELECT TSTCAT.NAME AS CATEGORY, TSTCOMP.NAME AS TESTNAME,TSTCOMP.INV AS INV,LABRESULT.REF AS REF,LABRESULT.UNIT AS UNIT, LABRESULT.VALUE AS VALUE ,RES.ID AS ID,RES.DATE AS DATE,RES.PID+' '+RES.PNAME AS PNAME,'' AS BEDNO,'' AS WARD,'' AS AGE,RES.REFBY AS REFBY,ORG.NAME AS HNAME,ORG.ADDRESS AS HADDRESS,ORG.GSTNO AS GSTNO,ORG.PHONE AS HPHONE,ORG.PHONE2 AS PHONE2,ORG.REGNO AS REGNO,ORG.EMAIL AS EMAIL FROM LABRESULT_TABLE LABRESULT,TEST_COMPONENT_TABLE TSTCOMP,TEST_CATEGORY_TABLE TSTCAT,TEST_NAME_TABLE TSTNAME ,LABRES_TABLE RES,ORG_TABLE ORG WHERE LABRESULT.INV=TSTCOMP.slno AND TSTNAME.CATEGORY=TSTCAT.ID AND TSTCOMP.ID=TSTNAME.ID AND RES.ID=LABRESULT.ID AND LABRESULT.ID='" + S.ToString() + "' AND LABRESULT.VALUE !='0'", con);
            ds1 = new DataSet();
            DataTable dt11 = new DataTable();
            da1.Fill(ds1, "STOCK");
            dt11 = ds1.Tables["STOCK"];
            int i1 = 0, k1 = 0;
            ds2.LABRESULT.Rows.Clear();
            while (i1 < dt11.Rows.Count)
            {
                dtr = dt11.Rows[i1];
                ds2.LABRESULT.Rows.Add(new string[] { dtr["CATEGORY"].ToString(), dtr["TESTNAME"].ToString(), dtr["INV"].ToString(), dtr["REF"].ToString(), dtr["UNIT"].ToString(), dtr["VALUE"].ToString(), dtr["ID"].ToString(), dtr["DATE"].ToString(), dtr["PNAME"].ToString(), dtr["BEDNO"].ToString(), dtr["WARD"].ToString(), dtr["AGE"].ToString(), dtr["REFBY"].ToString(), dtr["HNAME"].ToString(), dtr["HADDRESS"].ToString(), dtr["GSTNO"].ToString(), dtr["HPHONE"].ToString(), dtr["PHONE2"].ToString(), dtr["REGNO"].ToString(), dtr["EMAIL"].ToString() });
                k1++;
                i1 += 1;
            }
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT ID,INVES,A140,A160,A180,A320 FROM LAB_WIDALRESULT_TABLE WHERE ID='" + S.ToString() + "'", con);
            DataSet ds12 = new DataSet();
            DataTable dt111 = new DataTable();
            da2.Fill(ds12, "STOCK");
            dt111 = ds12.Tables["STOCK"];
            int i12 = 0, k12 = 0;
            ds2.WIDAL.Rows.Clear();
            while (i12 < dt111.Rows.Count)
            {
                dtr = dt111.Rows[i12];
                ds2.WIDAL.Rows.Add(new string[] { dtr["ID"].ToString(), dtr["INVES"].ToString(), dtr["A140"].ToString(), dtr["A160"].ToString(), dtr["A180"].ToString(), dtr["A320"].ToString() });
                k12++;
                i12 += 1;
            }
            cr = new LABORATORY_Bill();
            // rodc.Load(Server.MapPath("~/REPORTS/labbill.rpt"));
            // rodc.SetDataSource(dt1);
            rodc1.Load(Server.MapPath("~/REPORTS/labreport.rpt"));
            rodc1.SetDataSource(dt11);
            rodc1.Subreports[0].SetDataSource(ds2.Tables["WIDAL"]);
            // CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer2.ReportSource = rodc1;
            // CrystalReportViewer1.DataBind();
            CrystalReportViewer2.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new LABORATORY_Bill();
        cr.rodc1.Close();
        cr.rodc1.Clone();
        cr.rodc1.Dispose();
        rodc1 = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
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
          
            //rodc.PrintOptions.PrinterName = GetDefaultPrinter();
            //rodc.PrintToPrinter(1, false, 0, 0);
            rodc1.PrintOptions.PrinterName = GetDefaultPrinter();
            rodc1.PrintToPrinter(1, false, 0, 0);
        }
        catch(Exception ex)
        {
            string message = "alert('"+ex.Message+"')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //return;
           // Response.Write("<script LANGUAGE='JavaScript' >alert('connect printer settings')</script>");
        }
    }

  
    
}