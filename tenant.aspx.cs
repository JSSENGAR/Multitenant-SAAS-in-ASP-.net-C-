using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using SE.LS;

namespace SE
{
    public partial class Tenant : System.Web.UI.Page
    {
        MySqlC o = new MySqlC();
        SqlConnection con = new SqlConnection(); string ConS = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string CS = "Data Source = SERVER; Initial Catalog = tenant; User ID = XXXXXXXXXX; Password = YYYYYYYYYYYYYYY; Max Pool Size=50000; Pooling=True";
                using (SqlConnection con = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tin"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                            gvUsers.DataSource = dt;
                            gvUsers.DataBind();
                        }
                    }
                }
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // e.Row.Cells[4].Text = Decrypt(e.Row.Cells[4].Text);
            }
        }
        protected void Submit(object sender, EventArgs e)
        {
            string constr = "Data Source = SERVER; Initial Catalog = tenant; User ID = *************; Password = ***********; Max Pool Size = 50000; Pooling = True";
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Tinfo VALUES(@Tenant, @IntCat, @uid, @pwd, @dfi)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Tenant", txtTenant.Text);
                    cmd.Parameters.AddWithValue("@IntCat", txtIntCat.Text);
                    cmd.Parameters.AddWithValue("@uid", txtUserID.Text);
                    cmd.Parameters.AddWithValue("@pwd", o.Encrypt(txtPassword.Text));
                    cmd.Parameters.AddWithValue("@dfi", DropDownList1.SelectedIndex);
					cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}
//9589391358