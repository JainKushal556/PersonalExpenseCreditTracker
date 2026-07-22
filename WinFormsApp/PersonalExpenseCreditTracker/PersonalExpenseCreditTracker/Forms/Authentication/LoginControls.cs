using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public partial class LoginControls : Form
    {
        public LoginControls()
        {
            InitializeComponent();
        }
        private void textBox2_Event(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter your email address")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
            {
                textBox2.Text = "Enter your email address";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Event(object sender, EventArgs e)
        {
            if (textBox3.Text == "Enter your password")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == "")
            {
                textBox3.Text = "Enter your password";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void LoginControls_Load(object sender, EventArgs e)
        {
            textBox2.Text = "Enter your email address";
            textBox2.ForeColor = Color.Gray;
            textBox2.BackColor = Color.White;
            textBox2.SelectionStart = 0;
            textBox2.SelectionLength = 0;

            textBox3.Text = "Enter your password";
            textBox3.ForeColor = Color.Gray;
            textBox3.BackColor = Color.White;
            textBox3.SelectionStart = 0;
            textBox3.SelectionLength = 0;


            tableLayoutPanel1.Left = (this.ClientSize.Width - tableLayoutPanel1.Width) / 2;
            tableLayoutPanel1.Top = (this.ClientSize.Height - tableLayoutPanel1.Height) / 2;

        }

        private void LoginControls_Resize(object sender, EventArgs e)
        {

            tableLayoutPanel1.Left = (this.ClientSize.Width - tableLayoutPanel1.Width) / 2;
            tableLayoutPanel1.Top = (this.ClientSize.Height - tableLayoutPanel1.Height) / 2;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

       
    }
}
