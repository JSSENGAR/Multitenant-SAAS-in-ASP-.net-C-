using System;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using RestSharp;
using System.Configuration;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
namespace SP.LS
{
    public class MySqlC
    {
        SqlConnection con = new SqlConnection();      
        public string sqlcs()
        {

            string connectionString = "";

            if (HttpContext.Current.Session["ConString"].ToString() == "" && HttpContext.Current.Session["ConString"] != null && HttpContext.Current.Session.Contents.Count != 0 && HttpContext.Current.Session["usys"] != null)
            {
                int i = 0;
                string tenant = "";
                for (i = 1; i < HttpContext.Current.Session["usys"].ToString().Length; i++)
                {
                    if (HttpContext.Current.Session["usys"].ToString()[i].ToString() != "/")
                    {
                        tenant = tenant + HttpContext.Current.Session["usys"].ToString()[i].ToString();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                con.Close();
                con.ConnectionString = "Data Source = SERVER; Initial Catalog = tenant; User ID = XXXXXXXX; Password = YYYYYYYYYYYY; Max Pool Size=50000; Pooling=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("select IntCat, uid, pwd from Tin where Tenant='" + tenanti + "'", con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    HttpContext.Current.Session["ConString"] = "Data Source = SERVER; Initial Catalog = " + sdr["IntCat"].ToString() + "; User ID = " + sdr["uid"].ToString() + "; Password = " + Decrypt(sdr["pwd"].ToString()) + "; Max Pool Size=50000; Pooling=True";
                }
                else
                {
                    sdr.Close();
                    SqlCommand cmdd = new SqlCommand("select IntCat, uid, pwd from Tinfo where dfi=1", con);
                    SqlDataReader sdrd = cmdd.ExecuteReader();
                    if (sdrd.HasRows)
                    {
                        sdrd.Read();
                        HttpContext.Current.Session["ConString"] = "Data Source = SERVER; Initial Catalog = " + sdrd["IntCat"].ToString() + "; User ID = " + sdrd["uid"].ToString() + "; Password = " + Decrypt(sdrd["pwd"].ToString()) + "; Max Pool Size=50000; Pooling=True";
                    }
                    else
                    {
                        HttpContext.Current.Session["ConString"] = "Data Source = SERVER; Initial Catalog = dif; User ID = xxxxxxx; Password = yyyyyy; Max Pool Size=50000; Pooling=True";
                    }
                    sdrd.Close();
                }
                sdr.Close();
                con.Close();
                connectionString = HttpContext.Current.Session["ConString"].ToString();
            }
            else if (HttpContext.Current.Session["ConString"] != null && HttpContext.Current.Session.Contents.Count != 0 && HttpContext.Current.Session["usys"] != null)
            {
                connectionString = HttpContext.Current.Session["ConString"].ToString();
            }
            else
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Response.Redirect("~/LOGIN.aspx");
            }
            return connectionString;
        }
        public string Encrypt(string clearText)
        {
            string EncryptionKey = "XXXXXXXXXX";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "XXXXXXXXXX";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}