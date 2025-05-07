using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_project
{
    using System.Drawing;
    using System.Windows.Forms;

    public static class ThemeManager
    {
        public static bool IsDarkMode { get; set; } = false;

        public static void ApplyTheme(Form form)
        {
            if (IsDarkMode)
            {
                form.BackColor = Color.FromArgb(18, 18, 18);
                form.ForeColor = Color.White;

                foreach (Control control in form.Controls)
                {
                    if (control is Button button)
                    {
                        button.BackColor = Color.FromArgb(50, 50, 50);
                        button.ForeColor = Color.White;
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 70);
                    }

                    if (control is Label label)
                    {
                        label.ForeColor = Color.WhiteSmoke;
                       // label.BackColor = Color.Crimson;
                    }

                    if (control is TextBox textBox)
                    {
                        textBox.BackColor = Color.FromArgb(40, 40, 40);
                        textBox.ForeColor = Color.WhiteSmoke;
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }

                    if (control is ComboBox comboBox)
                    {
                        comboBox.BackColor = Color.FromArgb(40, 40, 40);
                        comboBox.ForeColor = Color.WhiteSmoke;
                        comboBox.FlatStyle = FlatStyle.Flat;
                    }

                    if (control is Panel panel)
                    {
                        panel.BackColor = Color.FromArgb(30, 30, 30);
                        panel.ForeColor = Color.WhiteSmoke;
                    }

                    if (control is CheckBox checkBox)
                    {
                        checkBox.BackColor = Color.FromArgb(50, 50, 50);
                        checkBox.ForeColor = Color.WhiteSmoke;
                    }

                    if (control is MenuStrip menuStrip)
                    {
                        menuStrip.BackColor = Color.FromArgb(30, 30, 30);
                        menuStrip.ForeColor = Color.WhiteSmoke;
                        menuStrip.Renderer = new ToolStripProfessionalRenderer(new DarkMenuColorTable()); 
                        foreach (ToolStripMenuItem item in menuStrip.Items)
                        {
                            item.BackColor = Color.FromArgb(30, 30, 30);
                            item.ForeColor = Color.WhiteSmoke;
                            foreach (ToolStripMenuItem subItem in item.DropDownItems)
                            {
                                subItem.BackColor = Color.FromArgb(40, 40, 40);
                                subItem.ForeColor = Color.WhiteSmoke;
                            }
                        }
                    }

                    // PictureBox
                    if (control is PictureBox pictureBox)
                    {
                        pictureBox.BackColor = Color.FromArgb(40, 40, 40);
                        pictureBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }
            else // Light Mode
            {
                form.BackColor = Color.White;
                form.ForeColor = Color.Black;

                foreach (Control control in form.Controls)
                {
                    if (control is Button button)
                    {
                        button.BackColor = SystemColors.Control;
                        button.ForeColor = Color.Black;
                        button.FlatStyle = FlatStyle.Standard;
                        button.FlatAppearance.BorderColor = SystemColors.ControlDark;
                    }

                    if (control is Label label)
                    {
                        label.ForeColor = Color.Black;
                        //label.BackColor = Color.Beige;
                    }

                    if (control is TextBox textBox)
                    {
                        textBox.BackColor = SystemColors.Window;
                        textBox.ForeColor = Color.Black;
                        textBox.BorderStyle = BorderStyle.Fixed3D;
                    }

                    if (control is ComboBox comboBox)
                    {
                        comboBox.BackColor = SystemColors.Window;
                        comboBox.ForeColor = Color.Black;
                        comboBox.FlatStyle = FlatStyle.Standard;
                    }

                    if (control is Panel panel)
                    {
                        panel.BackColor = SystemColors.Control;
                        panel.ForeColor = Color.Black;
                    }

                    if (control is CheckBox checkBox)
                    {
                        checkBox.BackColor = SystemColors.Control;
                        checkBox.ForeColor = Color.Black;
                    }

                    if (control is MenuStrip menuStrip)
                    {
                        menuStrip.BackColor = SystemColors.Control;
                        menuStrip.ForeColor = Color.Black;
                        menuStrip.Renderer = null; 
                        foreach (ToolStripMenuItem item in menuStrip.Items)
                        {
                            item.BackColor = SystemColors.Control;
                            item.ForeColor = Color.Black;
                            foreach (ToolStripMenuItem subItem in item.DropDownItems)
                            {
                                subItem.BackColor = SystemColors.Control;
                                subItem.ForeColor = Color.Black;
                            }
                        }
                    }

                    if (control is PictureBox pictureBox)
                    {
                        pictureBox.BackColor = SystemColors.Control;
                        pictureBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }
        }

        public static void ToggleTheme(List<Form> allForms)
        {
            IsDarkMode = !IsDarkMode;
            foreach (Form form in allForms)
            {
                if (form != null && !form.IsDisposed)
                {
                    ApplyTheme(form);
                }
            }
        }
    }

   
    public class DarkMenuColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(70, 70, 70); } 
        }

        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(70, 70, 70); } 
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(70, 70, 70); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(70, 70, 70); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(40, 40, 40); }
        }
    }

}
