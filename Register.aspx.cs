using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;

using System.Data.SqlClient;
using System.Security.Cryptography;


public partial class Register : System.Web.UI.Page
{

    string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
    static string finalHash;
    static string salt;
    private static byte[] Key;
    private static byte[] IV;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {


        //get from form
        string email = TextBoxEmail.Text.Trim();
        string password = TextBoxPassword.Text.Trim();
        string nameMember = TextBoxNameOfMember.Text.Trim();



        //generate salt
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[]saltByte = new byte[8];
        //fills array of bytes 
        rng.GetBytes(saltByte);
        salt = Convert.ToBase64String(saltByte);


        //hashing
        SHA512Managed hashing = new SHA512Managed();
        string pwdWithSalt = password + salt;
        byte[] hashwithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
        finalHash = Convert.ToBase64String(hashwithSalt);


        //Encryption generation of Random Key and IV
        RijndaelManaged cipher = new RijndaelManaged();
        cipher.GenerateKey();
        Key = cipher.Key;
        IV = cipher.IV;





        //save to db
        try
        {
            using (SqlConnection con = new SqlConnection(MYDBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@Email,@Name,@PasswordHash,@PasswordSalt,@DateTimeRegistered,@key,@iv,@attempt)"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Name", encryptData(nameMember));
                        cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                        cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                        cmd.Parameters.AddWithValue("@DateTimeRegistered", DateTime.Now);
                        cmd.Parameters.AddWithValue("@key", Convert.ToBase64String(Key));
                        cmd.Parameters.AddWithValue("@iv",Convert.ToBase64String(IV));
                        cmd.Parameters.AddWithValue("@attempt", 0);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }






    }


    protected string encryptData(string data)
    {
        //assigning Key and IV to RijndaelManaged
       string cipherSave = null;

        try
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.Key = Key;
            cipher.IV = IV;


            //transform cipher
            ICryptoTransform enCryptoTransform = cipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(data);
            byte[] cipherText = enCryptoTransform.TransformFinalBlock(plainText, 0, plainText.Length);

            cipherSave = Convert.ToBase64String(cipherText);



        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        return cipherSave;

    }








}