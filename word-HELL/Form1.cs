using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace word_HELL
{
    public partial class Form1 : Form
    {
        List<char> chars = new List<char>();

        int index = 10;
        int indexer = 0;

        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text files(*.txt)|*.txt| All files(*.*)|*.*";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = openFileDialog1.FileName;
            string fileText = File.ReadAllText(fileName);

            foreach (char ch in fileText)
            {
                chars.Add(ch);
            }

            UpdateLabelWithFirstTenChars();
        }
        private void UpdateLabelWithFirstTenChars()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Math.Min(chars.Count, 10); i++)
            {
                sb.Append(chars[i]);
            }
            label1.Text = sb.ToString();
            label1.Text = MakeCapital(sb.ToString(), 0);
        }

                private string MakeCapital(string input, int position)
        {
            if (position >= input.Length || position < 0)
                return input;

            char[] array = input.ToCharArray();
            array[position] = Char.ToUpper(array[position]); // преобразование символа в верхний регистр
            return new string(array);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (indexer >= 5)
            {
                if (index >= chars.Count || chars.Count <= 0)
                {
                    return; // Выходим, если достигли конца списка или список пуст
                }

                // Формируем новую строку, удаляя первый символ и добавляя следующий
                var currentText = label1.Text.Remove(0, 1);
                currentText += chars[index++];

                // Обновляем лейбл
                label1.Text = currentText;
            }
            indexer++;
        }
    }
}
