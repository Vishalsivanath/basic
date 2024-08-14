using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CrudOperation
{
    public partial class Crud : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView();
            }
        }
        protected void btn_Sumbit_Click(object sender, EventArgs e)
        {
            string gender;
            if(RdFemale.Checked)
            {
                gender = "Female";
            }
            else if(RdMale.Checked)
            {
                gender = "Male";
            }
            else
            {
                gender = "others";
            }
            try
            {
                SqlConnection sc = new SqlConnection(conn);
                SqlCommand cmd = new SqlCommand("Emp_ins", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empName",txtName.Text);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@empRole", ddlROle.SelectedValue);
                sc.Open();
                cmd.ExecuteNonQuery();
                sc.Close();
                GridView();

            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        protected void GridView()
        {
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("Emp_se", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataSet dt = new DataSet();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        

        protected void Gridview1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridView();
        }

        protected void Gridview1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = e.RowIndex - 1;
            GridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string name = (row.FindControl("text2") as TextBox).Text;
            string gender = (row.FindControl("text3") as TextBox).Text;
            string role = (row.FindControl("text4") as TextBox).Text;
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("empse", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sc.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", id);
            cmd.Parameters.AddWithValue("@empName",name);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@empRole",role);
            cmd.ExecuteNonQuery();
            sc.Close();
            GridView1.EditIndex = -1;
            GridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("deleteemp", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", id);
            sc.Open();
            cmd.ExecuteNonQuery();
            sc.Close();
            GridView();
        }
    }
}

