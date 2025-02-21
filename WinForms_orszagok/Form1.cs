using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinForms_orszagok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                List<string[]> orszagok = new List<string[]>();

                using (StreamReader sr = new StreamReader("forras_azsia.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        orszagok.Add(sr.ReadLine().Split(';'));
                    }
                }

                if (checkBox1.Checked)
                {
                    if (listBox1.SelectedItem != null)
                    {
                        string selectedItem = listBox1.SelectedItem.ToString();

                        for (int i = 0; i < listBox1.Items.Count; i++)
                        {
                            if (listBox1.Items[i].ToString() == selectedItem)
                            {
                                textBox1.Clear();
                                foreach (var item in orszagok[i])
                                {
                                    textBox1.AppendText(item + "; ");
                                }
                                break;
                            }
                        }
                    }
                }
        }

        public void feltolt_gomb_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<string[]> orszagok = new List<string[]>();
            using (StreamReader sr = new StreamReader("forras_azsia.txt"))
            {
                while (!sr.EndOfStream)
                {
                    orszagok.Add(sr.ReadLine().Split(';'));
                }
            }
            for (int i = 0; i < orszagok.Count; i++)
            {
                listBox1.Items.Add(orszagok[i][0]);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }



    }
}
