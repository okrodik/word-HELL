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
        int highlightPosition = 0;

        int indexer = 0;

        string fileText;

        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text files(*.txt)|*.txt| All files(*.*)|*.*";

            label1.KeyPress += label1_KeyPress;
            label1.Paint += LblCustomLabel_Paint;
            label1.Text = "";

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
        }
        private void UpdateLabel()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Math.Min(chars.Count, 10); i++)
            {
                sb.Append(chars[i]);
            }
            label1.Text = sb.ToString();
            label1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!gameRunning)
            {
                gameRunning = true;
                label1.Focus(); // Переключаемся на label для возможности захвата событий KeyPress
            }
        }

        private void LblCustomLabel_Paint(object sender, PaintEventArgs e)
        {
            if (highlightPosition < label1.Text.Length)
            {
                // Полупрозрачная красная кисть
                var redTransparentBrush = new SolidBrush(Color.FromArgb(80, Color.Red));

                // Получаем текст до текущего символа
                string beforeText = label1.Text.Substring(0, highlightPosition);
                float offsetX = e.Graphics.MeasureString(beforeText, label1.Font).Width;

                // Измеряем точный размер символа
                SizeF charSize = e.Graphics.MeasureString(label1.Text[highlightPosition].ToString(), label1.Font);

                // Настройка коррекции размеров прямоугольника
                float paddingLeft = -1f;     // Немного уменьшим левую границу
                float paddingRight = 1f;      // Увеличим правую границу
                float paddingTop = -1f;       // Немного увеличим верхнюю границу
                float paddingBottom = 1f;     // Увеличим нижнюю границу

                // Координаты и размеры прямоугольника с поправками
                int x = e.ClipRectangle.X + (int)Math.Round(offsetX + paddingLeft);
                int y = e.ClipRectangle.Y + (int)Math.Round(paddingTop);
                int width = (int)Math.Round(charSize.Width + paddingRight - paddingLeft);
                int height = (int)Math.Round(charSize.Height + paddingBottom - paddingTop);

                // Прямоугольник для подсветки
                Rectangle rect = new Rectangle(x, y, width, height);
                e.Graphics.FillRectangle(redTransparentBrush, rect);
            }
        }

        private void label1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gameRunning)
            {
                // Проверяем правильность ввода символа
                char pressedChar = Convert.ToChar(e.KeyChar);
                char currentChar = label1.Text[highlightPosition];

                if (pressedChar == currentChar)
                {
                    // Перемещаем подсветку вперед
                    Dvizenie();
                }
            }
        }

       
        private void Dvizenie()
        {
            if (highlightPosition < 4 || fileText.Length - 5 <= indexer) // Перемещаем подсветку максимум до 5 символа и в конце
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
        }

        private void Zamena()
        {
            if (index < chars.Count)
            {
                // Убираем первый символ и добавляем новый
                string currentText = label1.Text.Remove(0, 1);
                currentText += chars[index++];

                // Обновляем лейбл
                label1.Text = currentText;
                label1.Invalidate();
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