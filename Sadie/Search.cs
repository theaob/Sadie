using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sadie
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
            
        }

        public string Literal
        {
         get 
         { 
             return textBox1.Text; 
         }
         set 
         {
             textBox1.Text = value;
         }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Literal = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Literal = textBox1.Text;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
