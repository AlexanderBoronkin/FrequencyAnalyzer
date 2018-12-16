using System;
using System.Windows.Forms;
using System.IO;

namespace FrequencyAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string all_data;
        string[] lines;
        OpenFileDialog openfile1 = new OpenFileDialog();
        SaveFileDialog savefile1 = new SaveFileDialog();

        //общий алфавит
        char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й',
            'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц',
            'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'};
        //частота в процентах
        double[] freq = new double[32];
        //табличное значение частот
        double[] tab_freq= { 7.998, 1.592, 4.533, 1.687, 2.977,//а б в г д 
                               8.483, 0.94, 1.641, 7.367, 1.208, //е ж з и й
                               3.486, 4.343,3.203, 6.7, 10.983,  //к л м н о
                               2.804, 4.764, 5.473, 6.318, 2.615,//п р с т у
                               0.267, 0.966, 0.486, 1.45, 0.718, //ф х ц ч ш
                               0.361, 0.037, 1.898, 1.735, 0.331,//щ ъ ы ь э
                               0.638, 2.001 };//ю я
        //шифровальный алфавит, берется из текстбоксов
        char[] alph_mod;

        private void ШифровкаТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 new_form = new Form2();
            new_form.Left = this.Left;
            new_form.Top = this.Top;
            new_form.Show();
            this.Hide();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //загрузка шифротекста
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            openfile1.Filter = "Текстовые файлы|*.txt";
            try
            {
                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i < openfile1.FileNames.Length; i++)
                    {
                        lines = File.ReadAllLines(openfile1.FileNames[i], System.Text.Encoding.UTF8);
                    }
                }
                for (int i = 0; i < lines.Length && i < 1000; i++)
                {
                    richTextBox1.Text += lines[i] + "\n";
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файла");
            }
        }


        //анализ
        private void button2_Click(object sender, EventArgs e)
        {
            all_data = richTextBox1.Text;
            //считаем частоту каждого символа
            for (int i = 0; i < all_data.Length; i++)
            {
                char c = all_data[i];
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (c == alphabet[j])
                        freq[j]++;
                }
            }
            for (int i = 0; i < freq.Length; i++)
                freq[i] = freq[i] * 100 / all_data.Length;

            double[] tab_freq_sort = tab_freq;
            double[] freq_sort = freq;

            for (int i = 0; i < tab_freq.Length; i++)
            {
                for (int j = tab_freq.Length - 1; j > i; j--)
                {
                    if (tab_freq_sort[j - 1] > tab_freq_sort[j])
                    {
                        double x = tab_freq_sort[j - 1];
                        tab_freq_sort[j - 1] = tab_freq_sort[j];
                        tab_freq_sort[j] = x;
                    }
                    if (freq_sort[j - 1] > freq_sort[j])
                    {
                        double x = freq_sort[j - 1];
                        freq_sort[j - 1] = freq_sort[j];
                        freq_sort[j] = x;
                    }
                }
            }
            ;
        }

        //обновить
        private void button3_Click(object sender, EventArgs e)
        {
            all_data = richTextBox1.Text;
        }

        //сохранить в файл
        private void button4_Click(object sender, EventArgs e)
        {
            savefile1.Filter = "Текстовые файлы|*.txt";
            savefile1.ShowDialog();
            try
            {
                using (StreamWriter sw = new StreamWriter(savefile1.FileName, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(richTextBox1.Text);
                }
                MessageBox.Show("Запись прошла успешно!");
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так при записи!");
            }
        }

        private void Make_key()
        {
            alph_mod = new char[alphabet.Length];
            alph_mod[0] = Convert.ToChar(textА.Text); alph_mod[1] = Convert.ToChar(textБ.Text);
            alph_mod[2] = Convert.ToChar(textВ.Text); alph_mod[3] = Convert.ToChar(textГ.Text);
            alph_mod[4] = Convert.ToChar(textД.Text); alph_mod[5] = Convert.ToChar(textЕ.Text);
            alph_mod[6] = Convert.ToChar(textЖ.Text); alph_mod[7] = Convert.ToChar(textЗ.Text);
            alph_mod[8] = Convert.ToChar(textИ.Text); alph_mod[9] = Convert.ToChar(textЙ.Text);
            alph_mod[10] = Convert.ToChar(textК.Text); alph_mod[11] = Convert.ToChar(textЛ.Text);
            alph_mod[12] = Convert.ToChar(textМ.Text); alph_mod[13] = Convert.ToChar(textН.Text);
            alph_mod[14] = Convert.ToChar(textО.Text); alph_mod[15] = Convert.ToChar(textП.Text);
            alph_mod[16] = Convert.ToChar(textР.Text); alph_mod[17] = Convert.ToChar(textС.Text);
            alph_mod[18] = Convert.ToChar(textТ.Text); alph_mod[19] = Convert.ToChar(textУ.Text);
            alph_mod[20] = Convert.ToChar(textФ.Text); alph_mod[21] = Convert.ToChar(textХ.Text);
            alph_mod[22] = Convert.ToChar(textЦ.Text); alph_mod[23] = Convert.ToChar(textЧ.Text);
            alph_mod[24] = Convert.ToChar(textШ.Text); alph_mod[25] = Convert.ToChar(textЩ.Text);
            alph_mod[26] = Convert.ToChar(textЪ.Text); alph_mod[27] = Convert.ToChar(textЫ.Text);
            alph_mod[28] = Convert.ToChar(textЬ.Text); alph_mod[29] = Convert.ToChar(textЭ.Text);
            alph_mod[30] = Convert.ToChar(textЮ.Text); alph_mod[31] = Convert.ToChar(textЯ.Text);
        }

    }
}
