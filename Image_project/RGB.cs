using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_project
{
    public enum Colors
    {
        Red, Green, Blue
    };

    public partial class RGB : Form
    {
        Bitmap newimage;
        public RGB(Bitmap image)
        {
            InitializeComponent();         
            ThemeManager.ApplyTheme(this);
            newimage = image;
        }
        
        private void FormRGB_Load(object sender, EventArgs e) { }




        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            newimage = new Bitmap(newimage, pictureBox1.Size);
            pictureBox1.Image = IMG.GetColor(newimage, Colors.Red);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            newimage = new Bitmap(newimage, pictureBox2.Size);
            pictureBox2.Image = IMG.GetColor(newimage, Colors.Green);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            newimage = new Bitmap(newimage, pictureBox3.Size);
            pictureBox3.Image = IMG.GetColor(newimage, Colors.Blue);
        }
    }
    public static class IMG
    {
        public static Bitmap GetColor(Bitmap src, Colors c)
        {
            int w = src.Width, h = src.Height;

            Bitmap ret = new Bitmap(w, h);

            for (int i = 0; i < w; ++i)
            {
                for (int j = 0; j < h; ++j)
                {
                    Color pixel = src.GetPixel(i, j);

                    int r = 0, g = 0, b = 0;
                    if (c == Colors.Red)
                    {
                        r = pixel.R;
                    }
                    else if (c == Colors.Green)
                    {
                        g = pixel.G;
                    }
                    else if (c == Colors.Blue)
                    {
                        b = pixel.B;
                    }


                    ret.SetPixel(i, j, Color.FromArgb(r, g, b));

                }
            }
            return ret;
        }



    }

}
