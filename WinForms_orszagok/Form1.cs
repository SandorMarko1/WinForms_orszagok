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

            public List<string[]> orszagok = new List<string[]>();
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            if (checkBox1.Checked)
            {
                textBox1.Clear();
                foreach (var item in orszagok[listBox1.SelectedIndex])
                {
                    textBox1.AppendText(item + "; ");
                }
            }
            else
            {
                textBox1.Clear();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null && checkBox2.Checked)
            {
                string kivalasztottOrszag = listBox2.SelectedItem.ToString();
                MegjelenitAdatokat(kivalasztottOrszag);
            }
        }

        private void classos_feltoltes_Click(object sender, EventArgs e)
        {
            List<Europai> lista = new List<Europai>();
            var importaltOrszagok = beolvas.beolvasas("forras_europa.txt");
            lista.AddRange(importaltOrszagok);
            for (int i = 0; i < lista.Count; i++)
            {
                listBox2.Items.Add(lista[i].orszag);
            }
        }

        private void tabla_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null && checkBox2.Checked)
            {
                string kivalasztottOrszag = listBox2.SelectedItem.ToString();
                MegjelenitAdatokat(kivalasztottOrszag);
            }
        }
        private void MegjelenitAdatokat(string orszag)
        {
            Europai kivalasztott = beolvas.beolvasas("forras_europa.txt")
                .FirstOrDefault(o => o.orszag == orszag);

            if (kivalasztott == null) return;

            tabla1.Rows.Clear();
            tabla1.Columns.Clear();

            tabla1.ColumnCount = 5;
            tabla1.Columns[0].Name = "Ország";
            tabla1.Columns[1].Name = "Főváros";
            tabla1.Columns[2].Name = "Terület";
            tabla1.Columns[3].Name = "Népesség";
            tabla1.Columns[4].Name = "Népsűrűség";

            tabla1.Rows.Add(kivalasztott.orszag, kivalasztott.fovaros, kivalasztott.terulet, kivalasztott.nepesseg, kivalasztott.nepsuruseg);
        }

        private void tabla1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    class Europai
    {
        public string orszag { get; set; }
        public string fovaros { get; set; }
        public int terulet { get; set; }
        public int nepesseg { get; set; }
        public int nepsuruseg { get; set; }

        public Europai(string orszag, string fovaros, int terulet, int nepesseg, int nepsuruseg){
            this.orszag = orszag;
            this.fovaros = fovaros;
            this.terulet = terulet;
            this.nepesseg = nepesseg;
            this.nepsuruseg = nepsuruseg;
        }
    }

    class beolvas 
    {
        public static List<Europai> beolvasas(string file)
        {
            List<Europai> orszagok = new List<Europai>();
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine().Split(';');
                    if (sor.Length == 5 && int.TryParse(sor[2], out int terulet) && int.TryParse(sor[3], out int nepesseg) && int.TryParse(sor[4], out int nepsuruseg))
                    {
                        orszagok.Add(new Europai(sor[0], sor[1], terulet, nepesseg, nepsuruseg));
                    }
                }
            }
            return orszagok;
        }
    }

}
