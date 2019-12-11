using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cube : System.Web.UI.Page
{

  
    SqlConnection con = new SqlConnection(clsLibrary.constr);
    clsLibrary objlib = new clsLibrary();
    string UserName = string.Empty;
    int EmpNO = 0;
    string strRole = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btnloginsubmit_Click(object sender, EventArgs e)
    {

        TimeSpan ts = new TimeSpan();
        byte[] data = System.Text.UnicodeEncoding.UTF8.GetBytes(txtPwd.Text);
        EncodeClass myEncoder = new EncodeClass(data);
        StringBuilder sb = new StringBuilder();
        sb.Append(myEncoder.GetEncoded());
        con.Open();
        if (con.State == ConnectionState.Closed) { con.Open(); }
        SqlCommand cmd = new SqlCommand("DBP_Craft_Get_UserMaster");
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@strUserName", txtempid.Text);
        cmd.Parameters.AddWithValue("@strPassword", Convert.ToString(sb.ToString()));
        SqlDataReader dr = cmd.ExecuteReader();     
        Session["UserCatg"] = "";
        Session["UserName"] = "";
        if (dr.Read())
        {
            Session["UserName"] = dr[1].ToString();
      
            UserName = Convert.ToString(Session["UserName"]);
            strRole = Convert.ToString(Session["UserCatg"]);
           
            if (dr[1].ToString() != "")
            {
                Session["UserName"] = txtempid.Text;
                Response.Redirect("ChEmNER.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Enter Login Credentials');", true);
            }
            dr.Close();  
          }
    else
        {   
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Invalid Emp id or Password');", true);
        }
        dr.Close();
       // con.Open();
    }
       
}
