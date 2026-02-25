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
        bool gameRunning = false;
        SolidBrush redBrush = new SolidBrush(Color.Red);
        int highlightPosition = 0;
        int maxHighlightPosition = 5;

        int highlightIndex = 0;

        int indexxx = 0;

        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text files(*.txt)|*.txt| All files(*.*)|*.*";

            label1.KeyPress += label1_KeyPress;
            label1.Paint += LblCustomLabel_Paint;

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
            label1.Refresh();
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
            if (!gameRunning)
            {
                RemoveAndAddNewChar();
                gameRunning = true;
                label1.Focus(); // Переключаемся на label для возможности захвата событий KeyPress
            }
        }

        private void LblCustomLabel_Paint(object sender, PaintEventArgs e)
        {
            // Получаем размер шрифта первого символа
            //SizeF size = e.Graphics.MeasureString(label1.Text[highlightPosition].ToString(), label1.Font);

            //// Рассчитываем координаты прямоугольника для подсветки
            //int x = e.ClipRectangle.X + (int)(size.Width * highlightPosition);
            //int y = e.ClipRectangle.Y;
            //int width = (int)size.Width;
            //int height = e.ClipRectangle.Height;

            //// Рисуем красный фон вокруг выделенного символа
            //Rectangle rect = new Rectangle(x, y, width, height);
            //e.Graphics.FillRectangle(redBrush, rect);
        }

        private void label1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gameRunning)
            {
                if (indexxx < 5)
                {
                    char enteredChar = e.KeyChar;
                    char firstChar = label1.Text[indexxx];

                    if (enteredChar == firstChar)
                    {
                        RemoveAndAddNewChar();
                    }
                }
                else
                {
                    char enteredChar = e.KeyChar;
                    char firstChar = label1.Text[5];

                    if (enteredChar == firstChar)
                    {
                        RemoveAndAddNewChar();
                    }
                }
                indexxx++;
            }
        }

        private void RemoveAndAddNewChar()
        {
            if (index >= chars.Count || chars.Count <= 0)
            {
                return; // Завершаем процесс, если больше нечего брать
            }


            // Далее ограничиваем максимум
            if (highlightPosition < maxHighlightPosition)
            {
                highlightPosition++;
                label1.Text = MakeCapital(label1.Text, highlightPosition);

            }

            if (highlightPosition == 4 && label1.Text.Length > 5 && label1.Text[5] == ' ')
            {
                highlightPosition++;
            }

            if (highlightPosition >= maxHighlightPosition)
            {
                highlightPosition = 5;
                var currentText = label1.Text.Remove(0, 1);
                currentText += chars[index++];

                // Обновляем лейбл

                label1.Text = MakeCapital(currentText, highlightPosition);
            }
        }
    }
}


