using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Image_project
{
    public partial class Grayscale : Form
    {
        Bitmap newimage;
        public Grayscale(Bitmap image)
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            newimage = image;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            int width = newimage.Width;
            int height = newimage.Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = newimage.GetPixel(x, y);

                    int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);

                    newimage.SetPixel(x, y, Color.FromArgb(pixelColor.A, grayValue, grayValue, grayValue));
                }
            }


            pictureBox1.Image = new Bitmap(newimage, pictureBox1.Size);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
