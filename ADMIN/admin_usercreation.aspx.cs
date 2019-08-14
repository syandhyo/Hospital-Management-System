using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

public partial class ADMIN_admin_usercreation : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    DataMathods dataobj = new DataMathods();
    string brc = "";
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from LOGIN_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("US{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        lblusid.Text = num1;

        dr.Close();
        con.Close();
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
        //lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            Session["SortedView"] = null;
            binddata();
        }
        //clear_control();
    }
    public void binddata()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        DataSet ds = dataobj.Get_DataSet("SELECT a.slno as slno,a.ID as ID,b.sNAME as NAME,a.STATUS FROM LOGIN_TABLE a join tblStaff b on a.NAME=b.id WHERE  a.BRANCH_ID='" + Session["Branch"] + "' ORDER BY ID DESC");
        //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,STATUS FROM LOGIN_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        //da.Fill(dt);
        dt = ds.Tables[0];
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "slno" };
        GridView1.DataBind();


        //try
        //{
        //    using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
        //    {
        //        COM.CommandType = CommandType.StoredProcedure;
        //        COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //        dr = COM.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            lblfyear.Text = dr["FYEAR"].ToString();
        //        }
        //        dr.Close();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);

        //}
        //SqlDataAdapter da1 = new SqlDataAdapter("select distinct sname+' '+EMPID AS SNAME ,id FROM tblstaff where ORGID='" + lblorgid.Text + "'", con);
        binddropdown();


        string sqlstr = "select BRANCH_USER_CODE from  BRANCH_MST where BRANCH_ID=" + Session["Branch"];
        
        DataSet dst = dataobj.Get_DataSet(sqlstr);
        if (dst.Tables[0].Rows.Count > 0)
        {
            brc = dst.Tables[0].Rows[0][0].ToString();
            Label6.Text = brc+"-";
        }



        //con.Close();
    }
    protected void binddropdown(){
        DataSet dsEmp = dataobj.Get_DataSet("select distinct sname+' '+EMPID AS SNAME ,id FROM tblstaff where Branch_ID='" + Session["Branch"] + "'");
        DataTable dtEmp = dsEmp.Tables[0];
       // da1.Fill(ds1);
        Dropempname.DataSource = dtEmp;
        Dropempname.DataTextField = "SNAME";
        Dropempname.DataValueField = "id";
        Dropempname.DataBind();
        Dropempname.Items.Insert(0, new ListItem("Select", "0"));
        Dropempname.SelectedValue = "0";
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this User ?');";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string MESSAGE1=string.Empty;
        DataMathods OBJ_METHOD=new DataMathods();
        try
        {
            if (Dropempname.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtusername.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpassword.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtcnfrmpassword.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 50, "INSERT");
            // SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);//AUTO()
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@BRANCH_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));//AUTO()
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, Dropempname.SelectedValue);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@USERNAME", SqlDbType.VarChar, 500, Label6.Text + txtusername.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@PASSWORD", SqlDbType.VarChar, 500, txtpassword.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@STATUS", SqlDbType.VarChar, 500, dropstatus.SelectedValue);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@STORE_ID", SqlDbType.Int, 0, 0);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@USERTYPE", SqlDbType.VarChar, 50, "nonpharma");

            OBJ_METHOD.ExecuteProceedure("ADMIN_USER_CREATION", "", "@Id", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                int themasterId = Convert.ToInt32(OBJ_METHOD._objOut.ToString().Split(':')[0]);
                string themasterautoinc = OBJ_METHOD._objOut.ToString().Split(':')[1];

                if (themasterId == -1)
                {
                    
                    MESSAGE1 = "alert('" + themasterautoinc + "')";
                }
                else if (themasterId == -2)
                {
                    
                    MESSAGE1 = "alert('" + themasterautoinc + "')";
                }
                else
                {


                    int cnt = 0;
                    foreach (ListItem item in CheckBoxList1.Items)
                    {
                        //PERMISSIONSTR.Add(item.Text + ":" + item.Selected);

                        SQL_PARAMS = new SqlParameter[6];


                        //insert into USER_PERMISSION_TABLE (ID,ORGID,PER,selected)values(@autoincID,@ORGID,@PER,@selected)
                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 50, "INSERTUPT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@USER_LOGIN_SLNO", SqlDbType.Int, 0, themasterId);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@PER", SqlDbType.VarChar, 500, item.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@selected", SqlDbType.Bit, 0, item.Selected);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@MasterName", SqlDbType.VarChar, 500, themasterautoinc);
                        OBJ_METHOD.ExecuteProceedure("ADMIN_USER_CREATION", "", "@Id", SqlDbType.VarChar, SQL_PARAMS, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            cnt++;
                        }

                    }
                    //string strper = string.Join(",", PERMISSIONSTR);

                    if (cnt == CheckBoxList1.Items.Count)
                    {
                        int cnt2 = 0;
                        foreach (ListItem item in CheckBoxList2.Items)
                        {
                            //PERMISSIONSTR.Add(item.Text + ":" + item.Selected);

                            SQL_PARAMS = new SqlParameter[5];


                            //        cmd.CommandText = "insert into USER_MODULE_TABLE (ID,MODULE,selected)values(@ID,@MODULE,@selected) ";
                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 50, "INSERTUMT");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@USER_LOGIN_SLNO", SqlDbType.Int, 0, themasterId);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@MODULE", SqlDbType.VarChar, 500, item.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@selected", SqlDbType.Bit, 500, item.Selected);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@MasterName", SqlDbType.VarChar, 500, themasterautoinc);
                            OBJ_METHOD.ExecuteProceedure("ADMIN_USER_CREATION", "", "@Id", SqlDbType.VarChar, SQL_PARAMS, true);
                            if (OBJ_METHOD._RESULT > 0)
                            {
                                cnt2++;
                            }

                        }
                        if (cnt2 == CheckBoxList2.Items.Count)
                        {
                            OBJ_METHOD.commitOrRollbackTran("commit");
                            MESSAGE1 = "alert('User saved successfully')";
                            clearcontrol();
                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            MESSAGE1 = "alert('Error ocurred, Data can not be saved right now.')";
                        }
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        MESSAGE1 = "alert('Error ocurred, Data can not be saved right now.')";
                    }
                }


            }

            else
            {
                
                //MESSAGE1 = "alert('Error ocurred, Data can not be saved right now.')";
                int themasterId = Convert.ToInt32(OBJ_METHOD._objOut.ToString().Split(':')[0]);
                string themasterautoinc = OBJ_METHOD._objOut.ToString().Split(':')[1];

                if (themasterId == -1)
                {

                    MESSAGE1 = "alert('" + themasterautoinc + "')";
                }
                else if (themasterId == -2)
                {

                    MESSAGE1 = "alert('" + themasterautoinc + "')";
                }
                OBJ_METHOD.commitOrRollbackTran("rollback");
            }

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            MESSAGE1 = "alert('Due to some issues, Data not inserted.')";
        }
        finally
        {
         binddata();
         ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", MESSAGE1, true);
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string MESSAGE1 = string.Empty;
        DataMathods OBJ_METHOD = new DataMathods();
        try
        {
            if (txtusername.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //else if (txtpassword.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}


            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();

            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 50, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, Dropempname.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@USERNAME", SqlDbType.VarChar, 500, Label6.Text + txtusername.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PASSWORD", SqlDbType.VarChar, 500, txtpassword.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@STATUS", SqlDbType.VarChar, 500, dropstatus.SelectedValue);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@UPDATEID", SqlDbType.Int, 0, lblusid.Text);

            OBJ_METHOD.ExecuteProceedure("ADMIN_USER_CREATION", "", "@Id", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                //int themasterId = Convert.ToInt32(OBJ_METHOD._objOut.ToString().Split(':')[0]);
                //string themasterautoinc = OBJ_METHOD._objOut.ToString().Split(':')[1];

                string themasterautoinc = OBJ_METHOD._objOut.ToString();
                int cnt = 0;
                foreach (ListItem item in CheckBoxList1.Items)
                {
                    //PERMISSIONSTR.Add(item.Text + ":" + item.Selected);

                    SQL_PARAMS = new SqlParameter[6];


                    //insert into USER_PERMISSION_TABLE (ID,ORGID,PER,selected)values(@autoincID,@ORGID,@PER,@selected)
                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 50, "INSERTUPT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@USER_LOGIN_SLNO", SqlDbType.Int, 0, lblusid.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@PER", SqlDbType.VarChar, 500, item.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@selected", SqlDbType.Bit, 0, item.Selected);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@MasterName", SqlDbType.VarChar, 500, themasterautoinc);
                    OBJ_METHOD.ExecuteProceedure("ADMIN_USER_CREATION", "", "@Id", SqlDbType.VarChar, SQL_PARAMS, true);
                    if (OBJ_METHOD._RESULT > 0)
                    {
                        cnt++;
                    }

                }
                //string strper = string.Join(",", PERMISSIONSTR);

                if (cnt == CheckBoxList1.Items.Count)
                {
                    int cnt2 = 0;
                    foreach (ListItem item in CheckBoxList2.Items)
                    {
                        //PERMISSIONSTR.Add(item.Text + ":" + item.Selected);

                        SQL_PARAMS = new SqlParameter[5];


                        //        cmd.CommandText = "insert into USER_MODULE_TABLE (ID,MODULE,selected)values(@ID,@MODULE,@selected) ";
                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 50, "INSERTUMT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@USER_LOGIN_SLNO", SqlDbType.Int, 0, lblusid.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@MODULE", SqlDbType.VarChar, 500, item.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@selected", SqlDbType.Bit, 500, item.Selected);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@MasterName", SqlDbType.VarChar, 500, themasterautoinc);
                        OBJ_METHOD.ExecuteProceedure("ADMIN_USER_CREATION", "", "@Id", SqlDbType.VarChar, SQL_PARAMS, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            cnt2++;
                        }

                    }
                    if (cnt2 == CheckBoxList2.Items.Count)
                    {
                        clearcontrol();
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        MESSAGE1 = "alert('User updated successfully')";
                        
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        MESSAGE1 = "alert('Error ocurred, Data can not be updated right now.')";
                    }
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    MESSAGE1 = "alert('Error ocurred, Data can not be updated right now.')";
                }

            }
            else
            {
                int themasterId = Convert.ToInt32(OBJ_METHOD._objOut.ToString().Split(':')[0]);
                string themasterautoinc = OBJ_METHOD._objOut.ToString().Split(':')[1];

                if (themasterId == -1)
                {

                    MESSAGE1 = "alert('" + themasterautoinc + "')";
                }
                else if (themasterId == -2)
                {

                    MESSAGE1 = "alert('" + themasterautoinc + "')";
                }
                else
                {

                    MESSAGE1 = "alert('Error ocurred, Data can not be updated right now.')";
                }

                OBJ_METHOD.commitOrRollbackTran("rollback");
            }
   
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            MESSAGE1 = "alert('Error ocurred, Data can not be updated right now.')";
        }
        finally
        {
            
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", MESSAGE1, true);
        }
       // Response.Redirect("~/ADMIN/admin_usercreation.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/ADMIN/admin_usercreation.aspx");
        clearcontrol();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        DataMathods OBJ_METHOD=new DataMathods();
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["slno"].ToString();
            
            string str="select * from LOGIN_TABLE where slno='" + slno + "'";
            DataSet ds = OBJ_METHOD.Get_DataSet(str);
            if (ds.Tables[0].Rows.Count>0)
            {
                DataTable dt = ds.Tables[0];
                btncreate.Visible = false;
                btnupdate.Visible = true;
                lblusid.Text =dt.Rows[0]["slno"].ToString();
                Dropempname.Text = dt.Rows[0]["NAME"].ToString();
                txtusername.Text = dt.Rows[0]["USERNAME"].ToString().Substring(dt.Rows[0]["USERNAME"].ToString().IndexOf('-')+1);
                txtpassword.Text = dt.Rows[0]["PASSWORD"].ToString();
                txtcnfrmpassword.Text = dt.Rows[0]["PASSWORD"].ToString();
                dropstatus.Text = dt.Rows[0]["STATUS"].ToString();
            }
            
            
                str= "select * from USER_PERMISSION_TABLE where USER_LOGIN_SLNO='" + lblusid.Text + "'";
                

                ds = OBJ_METHOD.Get_DataSet(str);
            if (ds.Tables[0].Rows.Count>0)
            {
                    CheckBoxList1.Items.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = ds.Tables[0].Rows[i]["PER"].ToString();
                        item.Value = ds.Tables[0].Rows[i]["PER"].ToString();
                        item.Selected = Convert.ToBoolean(ds.Tables[0].Rows[i]["selected"]);

                        CheckBoxList1.Items.Add(item);
                    }
                }

            str = "select * from USER_MODULE_TABLE where USER_LOGIN_SLNO='" + lblusid.Text + "'";


            ds = OBJ_METHOD.Get_DataSet(str);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CheckBoxList2.Items.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = ds.Tables[0].Rows[i]["MODULE"].ToString();
                    item.Value = ds.Tables[0].Rows[i]["MODULE"].ToString();
                    item.Selected = Convert.ToBoolean(ds.Tables[0].Rows[i]["selected"]);

                    CheckBoxList2.Items.Add(item);
                }
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataMathods OBJ_METHOD = new DataMathods();
        string slno = GridView1.DataKeys[e.RowIndex].Values["slno"].ToString();
        string MESSAGE1 = "";
        try
        {
            
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 50, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DELETEID", SqlDbType.Int, 0, Convert.ToInt32(slno));

            OBJ_METHOD.ExecuteProceedure("ADMIN_USER_CREATION", "", "@Id", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                MESSAGE1 = "alert('" + OBJ_METHOD._objOut + "')";

            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("commit");
            MESSAGE1 = "alert('Data could not be deleted due to some issues.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", MESSAGE1, true);
        }
        //Response.Redirect("~/ADMIN/admin_usercreation.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,STATUS FROM LOGIN_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clearcontrol()
    {
        //binddropdown();
        
        txtusername.Text = "";
        txtpassword.Text = "";
        txtcnfrmpassword.Text = "";
        foreach (ListItem item in CheckBoxList1.Items)
        {
            item.Selected = false;
        }

        foreach (ListItem item in CheckBoxList2.Items)
        {
            item.Selected = false;
        }
        dropstatus.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
        btncancel.Visible = true;

    }
    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }
    public DataTable getdata()
    {
        
        DataSet ds = dataobj.Get_DataSet("SELECT a.slno as slno,a.ID as ID,b.sNAME as NAME,a.STATUS FROM LOGIN_TABLE a join tblStaff b on a.NAME=b.id WHERE  a.BRANCH_ID='" + Session["Branch"] + "' ORDER BY ID DESC");
        DataTable dt = ds.Tables[0];
        return dt;

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";

        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";

        }
        DataView sortedView = new DataView(getdata());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        GridView1.DataSource = sortedView;
        GridView1.DataBind();
    }
}