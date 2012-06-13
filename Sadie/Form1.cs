using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sadie
{
    public partial class Form1 : Form
    {
        bool changes = false;
        Font font;
        bool saved = false;
        bool opened = false;
        String openPath;

        public Form1(String[] args)
        {
            InitializeComponent();
            Program.loadConfiguration();
            applyConfigurations();
            try
            {
                if (args[0] != null)
                {
                    TextReader tr = new StreamReader(args[0]);
                    textBox1.Text = tr.ReadToEnd();
                    tr.Close();
                    opened = true;
                    openPath = args[0];
                    textBox1.ScrollBars = ScrollBars.Vertical;
                    changes = false;
                }
            }
            catch (Exception)
            {
                try
                {
                    TextReader tr = new StreamReader(Program.headerPath);
                    textBox1.Text = tr.ReadToEnd();
                    textBox1.Text = textBox1.Text.Replace("{{timestamp}}", DateTime.Now.ToString(Program.timestamp));
                    tr.Close();
                    tr = new StreamReader(Program.footerPath);
                    textBox1.Text += "\r\n\r\nYou can write your post here\r\n\r\n";
                    textBox1.Text += tr.ReadToEnd();
                    tr.Close();
                    changes = false;
                }
                catch (Exception)
                {

                }
            }
            textBox1.Select(textBox1.Text.Length, 0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            textBox1.Select(0, 0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab1 = new AboutBox1();
            ab1.ShowDialog();
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void configurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration conf = new Configuration(font);
            conf.ShowDialog();
        }

        private void cut()
        {
            textBox1.Cut();
        }

        private void paste()
        {
            textBox1.Paste();
        }

        private void copy()
        {
            textBox1.Copy();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cut();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void showLineNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showLineNumbersToolStripMenuItem.Checked = !showLineNumbersToolStripMenuItem.Checked;
            if (showLineNumbersToolStripMenuItem.Checked)
            {
                textBox1.WordWrap = true;
                Program.wordWrap = true;
                Program.saveConfiguration();
            }
            else
            {
                textBox1.WordWrap = false;
                Program.wordWrap = false;
                Program.saveConfiguration();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            changes = true;
            Size textSize = TextRenderer.MeasureText(textBox1.Text, this.textBox1.Font);

            if (textSize.Height > textBox1.Height)
            {
                textBox1.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                textBox1.ScrollBars = ScrollBars.None;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changes)
            {
                DialogResult dr = MessageBox.Show("Save post?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dr)
                {
                    case DialogResult.Yes:
                        {
                            //Save file here!
                            saveToolStripMenuItem_Click(sender, e);
                            break;
                        }
                    case DialogResult.No:
                        {
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            e.Cancel = true;
                            break;
                        }
                }
            }
        }

        private void editPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == null)
            {
                return;
            }
            TextReader tr = new StreamReader(openFileDialog1.FileName);
            textBox1.Text = tr.ReadToEnd();
            tr.Close();
        }

        private void fontDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            Program.fontFamily = fontDialog1.Font.FontFamily.Name;
            Program.emSize = fontDialog1.Font.Size;
            textBox1.Font = fontDialog1.Font;
            Program.saveConfiguration();
        }

        private void applyConfigurations()
        {
            textBox1.WordWrap = Program.wordWrap;
            showLineNumbersToolStripMenuItem.Checked = textBox1.WordWrap;
            font = new Font(Program.fontFamily, Program.emSize);
            textBox1.Font = font;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opened)
            {
                TextWriter tw = new StreamWriter(openPath);

                tw.Write(textBox1.Text);

                tw.Close();

                saved = true;
                changes = false;
                return;
            }
            
            if (!saved)
            {
                saveFileDialog1.Reset();
                saveFileDialog1.Filter = "Markdown | *.md";
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName == null || saveFileDialog1.FileName.Length < 1)
                {
                    return;
                }
                opened = true;
                openPath = saveFileDialog1.FileName;

                TextWriter tw = new StreamWriter(saveFileDialog1.FileName);

                tw.Write(textBox1.Text);

                tw.Close();

                changes = false;
                saved = true;
            }
            
           
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "Search")
            {
                toolStripTextBox1.Text = "";
            }
        }

        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                toolStripTextBox1.Text = "Search";
            }
        }


        int locale = 0;
        //int at = 0;
        String lastSearch = "";
        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                String search = toolStripTextBox1.Text;
                
                if (textBox1.Text.Contains(search))
                {
                    if (lastSearch != search)
                    {
                        locale = 0;
                    }

                    try
                    {
                        int index = textBox1.Text.IndexOf(search, locale);
                        locale = index + search.Length;
                        lastSearch = search;
                        textBox1.Select(index, search.Length);
                        //textBox1.Focus();
                        textBox1.ScrollToCaret();
                    }
                    catch (Exception)
                    {
                        locale = 0;
                        int index = textBox1.Text.IndexOf(search, locale);
                        locale = index + search.Length;
                        lastSearch = search;
                        textBox1.Select(index, search.Length);
                        //textBox1.Focus();
                        textBox1.ScrollToCaret();
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Reset();
            saveFileDialog1.Filter = "Markdown (.md)|*.md|Markdown (.markdown)|*.markdown|Textile (.textile)|*.textile|HTML (.html)|*.html|Text File (.txt)|*.txt";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == null || saveFileDialog1.FileName.Length < 1)
            {
                return;
            }

            TextWriter tw = new StreamWriter(saveFileDialog1.FileName);

            tw.Write(textBox1.Text);

            tw.Close();

            openPath = saveFileDialog1.FileName;

            changes = false;

            saved = true;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void countOccurrenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Enter string to be counted", "Count Occurrence",MessageBoxButtons.OKCancel,MessageBoxOptions.
            //DialogResult.
            //MessageBox.Show(Mess,
            //DialogResult
            //Search _ara = new Search();
            //_ara.Owner = this;

            Search arama = new Search();

            DialogResult = arama.ShowDialog();

            if (DialogResult == DialogResult.OK)
            {

                MessageBox.Show(CountStringOccurrences(arama.Literal) + " occurrence(s) for " + arama.Literal,"Occurrence Count");
                //MessageBox.Show(arama.Literal);
            }
            else if (DialogResult == DialogResult.Yes)
            {
                toolStripTextBox1.Text = arama.Literal;
                toolStripTextBox1_KeyPress(this, new KeyPressEventArgs('\r'));
                //toolStripTextBox1.
            }

            //_ara.ShowDialog();
        }

        private String CountStringOccurrences(string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = textBox1.Text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count.ToString();
        }


        
    }
}
