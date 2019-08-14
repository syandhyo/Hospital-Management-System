using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

public partial class PHARMACYSTORE_StoreType : System.Web.UI.Page
{
    DataMathods OBJ_METHOD = new DataMathods();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
        [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string getData()
    {
        DataMathods OBJ_METHOD = new DataMathods();
        string setbranchesifany = "select distinct sl,NAME from PHARMACY_STORE_MASTER where ORGID='" + HttpContext.Current.Session["ORGID"] + "' and Branch_ID=" + HttpContext.Current.Session["Branch"] + "";
        bool isAuthenticated = false;
        DataSet dsbranch = OBJ_METHOD.Get_DataSet(setbranchesifany, false, false, null);
        int howmanyrows = 0;
        if (dsbranch.Tables[0].Rows.Count > 0)
        {
            howmanyrows = dsbranch.Tables[0].Rows.Count;
            isAuthenticated = true;
            if (howmanyrows == 1)
            {
                int branchId = Convert.ToInt32(dsbranch.Tables[0].Rows[0][0]);

                string getactivebranchfyid = "select ID,FYEAR,ORGID from  FYEAR1_TABLE where  STATUS='Active' and   BRANCH_ID=" + branchId.ToString();

                DataSet dsfyid = OBJ_METHOD.Get_DataSet(getactivebranchfyid, false, false, null);
                if (dsfyid.Tables[0].Rows.Count < 0)
                {
                    isAuthenticated = false;
                }
                else
                {
                    int BRANCH_FY = Convert.ToInt32(dsfyid.Tables[0].Rows[0][0]);
                    HttpContext.Current.Session["Branch"] = branchId;
                    HttpContext.Current.Session["BRANCH_FYR"] = BRANCH_FY;
                }
            }
            else
            {
              
            }
        }
        else
        {
            isAuthenticated = false;
        }

        return JsonConvert.SerializeObject(new { isAuthenticated = isAuthenticated, howmanyrows = howmanyrows, branchdata = dsbranch });
        //return rtn;
    }
        [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static string getbranchData(int branchid)
        {
            DataMathods OBJ_METHOD = new DataMathods();
            //string chkuser = "select BRANCH_ID from  LOGIN_TABLE where BRANCH_ID=" + branchid.ToString() + " and  ID ='" + HttpContext.Current.Session["UID"].ToString() + "'";
            bool isAuthenticated = false;
            //DataSet dsbranch = OBJ_METHOD.Get_DataSet(chkuser, false, false, null);
            //int howmanyrows = 0;
            //if (dsbranch.Tables[0].Rows.Count > 0)
            //{
            //    howmanyrows = dsbranch.Tables[0].Rows.Count;



            string getactivebranchfyid = "select ID,FYEAR,ORGID from  FYEAR1_TABLE where  STATUS='Active' and   BRANCH_ID=" + branchid.ToString();

            DataSet dsfyid = OBJ_METHOD.Get_DataSet(getactivebranchfyid, false, false, null);
            if (dsfyid.Tables[0].Rows.Count < 0)
            {
                isAuthenticated = false;
            }
            else
            {
                isAuthenticated = true;
                int BRANCH_FY = Convert.ToInt32(dsfyid.Tables[0].Rows[0][0]);
                HttpContext.Current.Session["Branch"] = branchid;
                HttpContext.Current.Session["BRANCH_FYR"] = BRANCH_FY;
                HttpContext.Current.Session["STOREID"] = branchid;
            }

            //}
            //else
            //{
            //    isAuthenticated = false;
            //}

            return JsonConvert.SerializeObject(new { isAuthenticated = isAuthenticated });
            //return rtn;
        }
}