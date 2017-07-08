using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Login : System.Web.UI.Page
{
    string errorMsg = "";

    string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"]
        .ConnectionString;

    private static byte[] Key;
    private static byte[] IV;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        //data from form
        string email = TextBoxEmail.Text.Trim();
        string password = TextBoxPass.Text.Trim();


        //retrieve salt from DB
        string saltFromDB = getDBSalt(email);

        //retrieve hash from DB
        string hashFromDB = getDBHash(email);



       







        try
        {
            if (saltFromDB != null && hashFromDB != null && saltFromDB.Length > 0)
            {
                //hashing userinput
                string pwdWithSalt = password + saltFromDB;

                SHA512Managed hashing = new SHA512Managed();
                byte[] hashwithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                string finalHash = Convert.ToBase64String(hashwithSalt);

                //check userhash with dbHash
                if (finalHash.Equals(hashFromDB))
                {

                    //Decryption
                    //get Key and IV from db

                    Key = Convert.FromBase64String(getDBKey(email));
                    System.Diagnostics.Debug.WriteLine(Key);

                    IV = Convert.FromBase64String(getDBIv(email));

                    lbl_Error.Text = "";
                    LabelWelcome.Text = "Welcome, " + decryptData(getDBName(email));
                }
                else
                {
                    lbl_Error.Text = "Email or Password is Invalid";
                }
            }
            else
            {
                lbl_Error.Text = "Email or Password is Invalid";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }


    protected string decryptData(string cipherText)
    {
        byte[] cipherTextByte =Convert.FromBase64String(cipherText);

        try
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.Key = Key;
            cipher.IV = IV;
            ICryptoTransform deCryptoTransform = cipher.CreateDecryptor();
            byte[] plainText = deCryptoTransform.TransformFinalBlock(cipherTextByte, 0, cipherTextByte.Length);

            //encode decrypted data to utf8
            return Encoding.UTF8.GetString(plainText);


        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }



    }

    protected string getDBSalt(string userid)
    {
        string h = null;
        SqlConnection connection = new SqlConnection(MYDBConnectionString);
        string sql = "select PasswordSalt FROM Account WHERE Email=@USERID";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@USERID", userid);
        try
        {
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["PasswordSalt"] != null)
                    {
                        if (reader["PasswordSalt"] != DBNull.Value)
                        {
                            h = reader["PasswordSalt"].ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            connection.Close();
        }
        return h;
    }


    protected string getDBHash(string userid)
    {
        string h = null;
        SqlConnection connection = new SqlConnection(MYDBConnectionString);
        string sql = "select PasswordHash FROM Account WHERE Email=@USERID";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@USERID", userid);
        try
        {
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["PasswordHash"] != null)
                    {
                        if (reader["PasswordHash"] != DBNull.Value)
                        {
                            h = reader["PasswordHash"].ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            connection.Close();
        }
        return h;
    }


    protected string getDBName(string userid)
    {
        string h = null;
        SqlConnection connection = new SqlConnection(MYDBConnectionString);
        string sql = "select Name FROM Account WHERE Email=@USERID";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@USERID", userid);
        try
        {
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["Name"] != null)
                    {
                        if (reader["Name"] != DBNull.Value)
                        {
                            h = reader["Name"].ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            connection.Close();
        }
        return h;
    }


    protected string getDBKey(string userid)
    {
        string h = null;
        SqlConnection connection = new SqlConnection(MYDBConnectionString);
        string sql = "select keyy FROM Account WHERE Email=@USERID";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@USERID", userid);
        try
        {
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["keyy"] != null)
                    {
                        if (reader["keyy"] != DBNull.Value)
                        {
                            h = reader["keyy"].ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            connection.Close();
        }
        return h;
    }

    protected string getDBIv(string userid)
    {
        string h = null;
        SqlConnection connection = new SqlConnection(MYDBConnectionString);
        string sql = "select iv FROM Account WHERE Email=@USERID";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@USERID", userid);
        try
        {
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["iv"] != null)
                    {
                        if (reader["iv"] != DBNull.Value)
                        {
                            h = reader["iv"].ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            connection.Close();
        }
        return h;
    }

}