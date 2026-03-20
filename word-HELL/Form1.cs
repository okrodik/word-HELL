using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace word_HELL
{
    public partial class Form1 : Form
    {
        Label[] labels = new Label[10];
        List<char> chars = new List<char>();

        int index = 10;
        bool gameRunning = false;
        int highlightPosition = 0;

        int indexer = 0;

        string fileText;

        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text files(*.txt)|*.txt| All files(*.*)|*.*";

            label1.KeyPress += label1_KeyPress;
            label1.Text = "";

            CreateLabel();
        }

        private void CreateLabel()
        {
            for (int i = 0; i < 10; i++)
            {
                labels[i] = new Label();
                labels[i].Location = new Point(150 + 50 * i, 100);
                labels[i].AutoSize = true;
                labels[i].Text = "";
                labels[i].Font = new Font("Arial", 14f, FontStyle.Bold);

                this.Controls.Add(labels[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = openFileDialog1.FileName;
            fileText = File.ReadAllText(fileName);

            foreach (char ch in fileText)
            {
                chars.Add(ch);
            }

            UpdateLabel();
            paintLabel(0);
        }

        private void UpdateLabel()
        {
            for (int i = 0; i < Math.Min(chars.Count, 10); i++)
            {
                labels[i].Text = chars[i].ToString();           
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!gameRunning)
            {
                gameRunning = true;
                label1.Focus();
            }
        }

        private void paintLabel(int x)
        {
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].ForeColor = Color.Black;
            }

            labels[x].ForeColor = Color.Red;
        }

        private void label1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gameRunning)
            {
                char pressedChar = Convert.ToChar(e.KeyChar);
                char currentChar = Convert.ToChar(labels[highlightPosition].Text);

                if (pressedChar == currentChar)
                {
                    Dvizenie();
                }
            }
        }

       
        private void Dvizenie()
        {
            if (highlightPosition < 4 || fileText.Length - 5 <= indexer)
            {
                highlightPosition++;
                label1.Invalidate(); 
                indexer++;
            }
            else 
            {
                Zamena();
                indexer++;
            }

            if (fileText.Length < indexer)
            {
                WINResult();
            }

            paintLabel(highlightPosition);
        }

        private void Zamena()
        {
            if (index < chars.Count)
            {
                for (int i = 0; i < labels.Length - 1; i++)
                {
                    labels[i].Text = labels[i + 1].Text;
                }

                labels[9].Text = chars[index++].ToString();
            }
        }

        private void WINResult()
        {
            label1.Text = "";
            label1.Invalidate();
            indexer = 0;
            gameRunning = false;
            highlightPosition = 0;
            MessageBox.Show("YOU WIM");
        }
    }
}