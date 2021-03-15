using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodManagerApp
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        UserData ud = new UserData();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show(txtUsername.Text, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string Username = txtUsername.Text;
            string Pwd = txtPassword.Text;
            string Fullname = txtFullname.Text;
            string Addr = txtAddress.Text;
            string Email = txtEmail.Text;
            string Phone = txtPhone.Text;
            bool Role = true;
            if(chkRole.Checked == true)
            {
                Role = false;
            }
            
            //new user
            User user = new User
            {
                username = Username,
                password = Pwd,
                fullname = Fullname,
                address = Addr,
                email = Email,
                phone = Phone,
                role = Role,
                status = true
            };
            //call register method
            bool r = ud.RegisterNewAccount(user);
            string s = (r == true ? "successfull" : "failed");
            MessageBox.Show("Register new account " + s);
            this.Close();
        }

        //Must set Form: AutoValidate = EnableAllowFocusChange
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            Regex usernameFormat = new Regex("[a-zA-Z]{2,20}");
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                e.Cancel = true;
                txtUsername.Focus();
                errorProvider.SetError(txtUsername, "Please enter your Username!");
            } 
            else if(usernameFormat.IsMatch(txtUsername.Text) == false)
            {
                e.Cancel = true;
                txtUsername.Focus();
                errorProvider.SetError(txtUsername, "Username length [2,20], contains only a-z, A-Z character!");
            }
            else if (ud.checkUsernameExist(txtUsername.Text))
            {
                e.Cancel = true;
                txtUsername.Focus();
                errorProvider.SetError(txtUsername, "This username is existed!\nPlease choose another one!");
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(txtUsername, null);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            Regex passwordFormat = new Regex("[a-zA-Z0-9]{8,15}");
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider.SetError(txtPassword, "Please enter your Password!");
            }
            else if (passwordFormat.IsMatch(txtPassword.Text) == false)
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider.SetError(txtPassword, "Password length [8,15], contains only 0-9, a-z, A-Z character!");
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(txtPassword, null);
            }
        }

        private void txtFullname_Validating(object sender, CancelEventArgs e)
        {
            Regex fullnameFormat = new Regex("[a-zA-Z\\s]{5,50}");
            if (string.IsNullOrEmpty(txtFullname.Text))
            {
                e.Cancel = true;
                txtFullname.Focus();
                errorProvider.SetError(txtFullname, "Please enter your Fullname!");
            }
            else if (fullnameFormat.IsMatch(txtFullname.Text) == false)
            {
                e.Cancel = true;
                txtFullname.Focus();
                errorProvider.SetError(txtFullname, "Fullname length [5,50], contains only a-z, A-Z character!");
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(txtFullname, null);
            }
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            Regex addressFormat = new Regex("[a-zA-Z0-9\\s\\/\\,]{5,100}");
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                e.Cancel = true;
                txtAddress.Focus();
                errorProvider.SetError(txtAddress, "Please enter your Address!");
            }
            else if (addressFormat.IsMatch(txtAddress.Text) == false)
            {
                e.Cancel = true;
                txtAddress.Focus();
                errorProvider.SetError(txtAddress, "Address length [5,100], contains only a-z, A-Z, 0-9 character!");
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(txtAddress, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            Regex emailFormat = new Regex("([\\w]+)@([\\w]{2,8})(.([\\w]{2,8})){1}(.([\\w]{2,8}))?");
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider.SetError(txtEmail, "Please enter your Email!");
            }
            else if (emailFormat.IsMatch(txtEmail.Text) == false)
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider.SetError(txtEmail, "Email length [5,50], format xxx@xxx.xx(.xx)");
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(txtEmail, null);
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            Regex phoneFormat = new Regex("[0-9]{10}");
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                e.Cancel = true;
                txtPhone.Focus();
                errorProvider.SetError(txtPhone, "Please enter your Phone Number!");
            }
            else if (phoneFormat.IsMatch(txtPhone.Text) == false)
            {
                e.Cancel = true;
                txtPhone.Focus();
                errorProvider.SetError(txtPhone, "Phone number contains 10 digits 0-9!");
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(txtPhone, null);
            }
        }
    }
}
