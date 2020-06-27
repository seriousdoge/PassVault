using System;
using System.IO;
using System.Windows.Forms;


namespace PassVault
{
    public partial class Vault : MetroFramework.Forms.MetroForm
    {
        string line = "";
        string file = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PassVault");
        public Vault()
        {          
            InitializeComponent();

            if (!File.Exists(file))
            {
                System.IO.FileStream f = System.IO.File.Create(file);
                f.Close();              
            }
        }             
        private void textBox1_TextChanged(object sender, EventArgs e)
        {}
        private void textBox2_TextChanged(object sender, EventArgs e)
        {}
        private void Label2_Click(object sender, EventArgs e)
        {}
        private void button1_Click(object sender, EventArgs e)
        {
            var txt = new StreamWriter(file, true);
            var inp = AesEncDec.Encrypt(textBox1.Text);
            txt.WriteLine(inp);           
            txt.Close();
            textBox1.Clear();
        }  
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(file);
                while ((line = sr.ReadLine()) != null)
                {
                    listBox1.Items.Add(AesEncDec.Decrypt(line));
                    if(line == "")
                    {
                        continue;
                    }
                }
                sr.Close();
            }

            catch (FileNotFoundException)
            {
                MessageBox.Show("File already deleted, please set key again !");
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {}

        private void Vault_Load(object sender, EventArgs e)
        {}

        private void button3_Click(object sender, EventArgs e)
        {
            var confirmAction = MessageBox.Show("Are you sure you want to permanenty delete the file ?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmAction == DialogResult.Yes)
            {
                File.Delete(file);
                MessageBox.Show("Data permanently erased !");
            }
            else
            {
                
            }
        }
    }
}