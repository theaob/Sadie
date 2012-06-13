using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Sadie
{
    public partial class Configuration : Form
    {
        public Configuration(Font font)
        {
            InitializeComponent();
            textBox1.Font = font;
            textBox2.Font = font;
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            TextReader tr = new StreamReader(Program.headerPath);
            textBox1.Text = tr.ReadToEnd();
            tr.Close();
            tr = new StreamReader(Program.footerPath);
            textBox2.Text = tr.ReadToEnd();
            tr.Close();
            textBox3.Text = Program.generatePath;
            textBox4.Text = Program.headerPath;
            textBox5.Text = Program.footerPath;
            textBox6.Text = Program.timestamp;
            label5.Text = DateTime.Now.ToString(Program.timestamp);
            button8.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextWriter tw = new StreamWriter(Program.headerPath,false);
            tw.Write(textBox1.Text);
            tw.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextWriter tw = new StreamWriter(Program.footerPath, false);
            tw.Write(textBox2.Text);
            tw.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox3.Text;
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != textBox3.Text && folderBrowserDialog1.SelectedPath != null)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = textBox4.Text;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != Program.headerPath && openFileDialog1.FileName != null)
            {
                textBox4.Text = openFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = textBox5.Text;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != Program.footerPath && openFileDialog1.FileName != null)
            {
                textBox5.Text = openFileDialog1.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Program.generatePath != textBox3.Text && textBox3.Text != null)
            {
                Program.generatePath = textBox3.Text;
            }
            if (Program.headerPath != textBox4.Text && textBox4.Text != null)
            {
                Program.headerPath = textBox4.Text;
            }
            if (Program.footerPath != textBox5.Text && textBox5.Text != null)
            {
                Program.footerPath = textBox5.Text;
            }
            Program.saveConfiguration();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                label5.Text = DateTime.Now.ToString(textBox6.Text);
            }
            catch (Exception)
            {
                label5.Text = "Expression was invalid";
                button8.Enabled = false;
                return;
            }
            if (textBox6.Text != Program.timestamp)
            {
                button8.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != Program.timestamp && textBox6.Text != null)
            {
                Program.timestamp = textBox6.Text;
                Program.saveConfiguration();
                button8.Enabled = false;
            }
            else
            {
                button8.Enabled = false;
            }
        }
    }
}
