using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace allNames
{
    public partial class UserControl1: UserControl
    {
        TextBox tb = new TextBox();

        private bool parLangValue = false;
        public bool parLang
        {
            get
            {
                return parLangValue;
            }
            set
            {
                parLangValue = value;
            }
        }
        public override string Text
        {
            get
            {
                return tb.Text;
            }
        }

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            tb.KeyDown += new KeyEventHandler(tb_KeyDown);
            tb.TextChanged += new EventHandler(tb_TextChanged);
            tb.Resize += new EventHandler(tb_Resize);
            tb.ForeColorChanged += new EventHandler(tb_ForeColorChanged);

            tb.Dock = DockStyle.Fill;

            ContextMenu blank = new ContextMenu();
            tb.ContextMenu = blank;

            tb.Width = this.Width;
            this.Height = tb.Height;

            this.Controls.Add(tb);
        }

        private void UserControl1_FontChanged(object sender, EventArgs e)
        {
            
        }

        private void UserControl1_Resize(object sender, EventArgs e)
        {
            tb.Width = this.Width;
            this.Height = tb.Height;
        }

        private void tb_Resize(Object sender, EventArgs e)
        {
            this.Width = tb.Width;
            this.Height = tb.Height;

            
        }

        private void tb_KeyPress(Object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if(tb.Text.Length >= 20 && char.IsControl(c) == false)
            {
                MessageBox.Show("Името може да има до 20 символа!", "Внимание", MessageBoxButtons.OK);
                e.Handled = true;
            }
            if (!(c >= 'а' && c <= 'я' || c >= 'А' && c <= 'Я' || c == '-') && !(c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '-') && !char.IsControl(c))
            {
                MessageBox.Show("Символите са забранени!", "Внимание", MessageBoxButtons.OK);
                e.Handled = true;
            }
            else
            {
                if (parLangValue)
                {
                    if (c >= 'а' && c <= 'я' || c >= 'А' && c <= 'Я' || c == '-' || char.IsControl(c))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Може да въвеждате само на Български!", "Внимание", MessageBoxButtons.OK);
                        e.Handled = true;
                    }
                }
                else
                {
                    if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '-' || char.IsControl(c))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Може да въвеждате само на Английски!", "Внимание", MessageBoxButtons.OK);
                        e.Handled = true;
                    }
                }
            }
        }


        private void tb_TextChanged(Object sender, EventArgs e)
        {
            string s = tb.Text;

            if (s.Length == 1)
            {
                s.ToUpper();
            }
            else if (s.Length >= 2)
            {
                //===========UPPER CASE LETTERS
                string temp;

                if (Char.IsUpper(s[1]))
                {
                    temp = s.Substring(0, 1);

                    for (int i = 1; i < s.Length; i++)
                    {
                        temp = temp + s.Substring(i, 1).ToUpper();
                    }

                    s = temp;
                }
                else
                {
                    temp = s.Substring(0, 1);

                    for (int i = 1; i < s.Length; i++)
                    {
                        if (s.Substring(i - 1, 1) == "-")
                        {
                            temp = temp + s.Substring(i, 1).ToUpper();
                        }
                        else
                        {
                            temp = temp + s.Substring(i, 1).ToLower();
                        }
                    }

                    s = temp;
                }

                //===========MIDDLE LINES
                temp = s.Substring(0, 1);

                for (int i = 1; i < s.Length; i++)
                {
                    if (s.Substring(i - 1, 1) == "-" && s.Substring(i, 1) == "-")
                    {
                        MessageBox.Show("Не може да слагате повече от 2 тирета едно след друго!", "Внимание", MessageBoxButtons.OK);
                    }
                    else if(s.Substring(i - 1, 1) == "-")
                    {
                        if (s.Substring(1, 1) == s.Substring(i, 1))
                        {
                            temp = temp + s.Substring(i, 1);
                        }
                        else
                        {
                            temp = temp + s.Substring(i, 1).ToUpper();
                        }
                    }
                    else
                    {
                        temp = temp + s.Substring(i, 1);
                    }
                }

                s = temp;

                //===========FIRST LINES
                temp = s;
                int n = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    if (s.Substring(i, 1) == "-")
                    {
                        n++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (n >= 1) 
                {
                    MessageBox.Show("Името не може да започва с тире!", "Внимание", MessageBoxButtons.OK);
                }

                temp = temp.Remove(0, n);
                s = temp;
            }

            tb.Text = s;
            tb.SelectionStart = tb.Text.Length;
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string s = tb.Text;

                if (s.Length >= 2)
                {
                    //===========LAST LINES
                    string temp = s;

                    while (temp.EndsWith("-"))
                    {
                        int size = temp.Length;
                        temp = temp.Substring(0, size - 1);
                    }

                    s = temp;

                    MessageBox.Show("Името не може да завършва с тире!", "Внимание", MessageBoxButtons.OK);
                }

                tb.Text = s;
            }
        }

        private void tb_ForeColorChanged(object sender, EventArgs e)
        {
            tb.ForeColor = this.ForeColor;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys key)
        {
            Keys PasteKeys = Keys.Control | Keys.V;

            if (key == PasteKeys)
            {
                MessageBox.Show("Комбинацията за поставяне е забранена!", "Внимание", MessageBoxButtons.OK);
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
