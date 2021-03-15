using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodManagerApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        User user;
        UserData udt = new UserData();
        int count = 0;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = txtUsername.Text;
            string Password = txtPassword.Text;
            bool valid = true;
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                valid = false;
                MessageBox.Show("Empty Username or Password!");
            }
            
            if (valid == true)
            {

                user = udt.CheckLogin(Username, Password);
                if (user != null)
                {
                    if (user.status == true)
                    {
                        if (user.role == true)
                        {
                            //admin view
                            frmHome adminHomepage = new frmHome();
                            adminHomepage.Show();
                        }
                        else
                        {
                            //staff view
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your account has been locked!\nPlease contact admin to unlock!");
                    }
                }
                else
                {
                    count++;
                    MessageBox.Show("User does not exist");
                    if (count > 2)
                    {
                        udt.LockUser(Username);
                        MessageBox.Show("Your account has been locked!\nPlease contact admin to unlock!");
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegister registerAcc = new frmRegister();
            registerAcc.Show();
        }
    }
}
