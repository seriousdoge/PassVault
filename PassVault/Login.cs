using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace PassVault
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {}
        
        private void Login_Load(object sender, EventArgs e)
        {}
              
        public static string Crypt(string text)
        {
            return Convert.ToBase64String(ProtectedData.Protect(Encoding.Unicode.GetBytes(text), null, DataProtectionScope.CurrentUser));
        }

        public static string Decrypt(string text)
        {

            return Encoding.Unicode.GetString(ProtectedData.Unprotect(Convert.FromBase64String(text), null, DataProtectionScope.CurrentUser));
        }        
        private void button1_Click(object sender, EventArgs e)
        {
            
            RegistryKey rkey1 = Registry.CurrentUser.OpenSubKey("Passvault");
            var regvalue = rkey1.GetValue("Secret").ToString();

            if (String.Equals(Decrypt(regvalue), TextBx.Text) == true)
            {
                this.Hide();
                Vault v2 = new Vault();
                v2.Closed += (s, args) => this.Close();
                v2.Show();
            }

            else
            {
                MessageBox.Show("Wrong Pin!");
            }            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (TextBx.Text.Length < 5)
            {
                MessageBox.Show("Password too short!");
            }
            else
            {
                RegistryKey rkey = Registry.CurrentUser.CreateSubKey("PassVault");
                rkey.SetValue("Secret", Crypt(TextBx.Text));
                TextBx.Clear();
                MessageBox.Show("Password set!");
            }
            
        }
    }
}