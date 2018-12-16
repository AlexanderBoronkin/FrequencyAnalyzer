using System;
using System.Windows.Forms;
using System.IO;

namespace FrequencyAnalyzer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string[] lines;
        OpenFileDialog openfile1 = new OpenFileDialog();
        SaveFileDialog savefile1 = new SaveFileDialog();

        //общий алфавит
        char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й',
            'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц',
            'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'};
        //шифровальный алфавит, берется из текстбоксов
        char[] alph_mod;

        //заполнение шифровального алфавита
        //знаю, что топорно и в лоб, но по другому не знаю, как сделать
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

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form main_form = Application.OpenForms[0];
            main_form.StartPosition = FormStartPosition.Manual;
            main_form.Left = this.Left;
            main_form.Top = this.Top;
            main_form.Show();        
        }
        //ограничение не более 1 тысячи строк в файле
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
                for (int i = 0; i < lines.Length&&i<1000; i++)
                {
                    richTextBox1.Text += lines[i]+"\n";
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файла");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Make_key();
            string new_data = "";
            string data = richTextBox1.Text.ToLowerInvariant();
            for(int i=0;i<data.Length;i++)
            {
                char c = data[i];
                for(int j=0;j<alphabet.Length;j++)
                {
                    if (c == alphabet[j])
                        c=alph_mod[j];
                }
                new_data += c;
            }
            richTextBox1.Text = new_data.ToLowerInvariant();
        }
    }
}
