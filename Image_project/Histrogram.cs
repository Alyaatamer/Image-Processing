using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using openCV;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using AForge.Imaging;
using System.Drawing.Imaging;

namespace Image_project
{
    public partial class Histrogram : Form
    {
        Bitmap newimage, processedBitmap;

        public Histrogram(Bitmap image)
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            newimage = image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmpImg = (Bitmap)newimage;
            Bitmap newImage = bmpImg;
            int width = newimage.Width;
            int hieght = newimage.Height;

            //******************* Calculate N(i) **************//

            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];





            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hieght; j++)
                {
                    Color pixelColor = bmpImg.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;
                }
            }

            //******************* Calculate P(Ni) **************//
            decimal[] prob_ni_Red = new decimal[256];
            decimal[] prob_ni_Green = new decimal[256];
            decimal[] prob_ni_Blue = new decimal[256];

            for (int i = 0; i < 256; i++)
            {
                prob_ni_Red[i] = (decimal)ni_Red[i] / (decimal)(width * hieght);
                prob_ni_Green[i] = (decimal)ni_Green[i] / (decimal)(width * hieght);
                prob_ni_Blue[i] = (decimal)ni_Blue[i] / (decimal)(width * hieght);
            }

            //******************* Calculate CDF **************//

            decimal[] cdf_Red = new decimal[256];
            decimal[] cdf_Green = new decimal[256];
            decimal[] cdf_Blue = new decimal[256];

            cdf_Red[0] = prob_ni_Red[0];
            cdf_Green[0] = prob_ni_Green[0];
            cdf_Blue[0] = prob_ni_Blue[0];

            for (int i = 1; i < 256; i++)
            {
                cdf_Red[i] = prob_ni_Red[i] + cdf_Red[i - 1];
                cdf_Green[i] = prob_ni_Green[i - 1] + cdf_Green[i - 1];
                cdf_Blue[i] = prob_ni_Blue[i - 1] + cdf_Blue[i - 1];
            }


            //******************* Calculate CDF(L-1) **************//


            int red, green, blue;
            int constant = 255;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hieght; j++)
                {
                    Color pixelColor = bmpImg.GetPixel(i, j);

                    red = (int)(cdf_Red[pixelColor.R] * constant);
                    green = (int)(cdf_Red[pixelColor.G] * constant);
                    blue = (int)(cdf_Red[pixelColor.B] * constant);

                    Color newColor = Color.FromArgb(red, green, blue);
                    newImage.SetPixel(i, j, newColor);

                }
            }

            pictureBox2.Image = new Bitmap(newimage,pictureBox2.Size);

            /*   MessageBox.Show("MN= "+width*hieght+"\n"
                   +"N(i)= "+ ni_Red[0]+"\n"
                   +"P(Ni)= "+prob_ni_Red[0]+"\n"
                   +"CDF= "+cdf_Red[255]);*/
            //btnGetEqualizedHistogram.Enabled = true;
            //btnEqualizeImage.Enabled = false;

        }


        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmpImg = new Bitmap(pictureBox2.Image);
            int width = bmpImg.Width;
            int hieght = bmpImg.Height;


            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hieght; j++)
                {
                    Color pixelColor = bmpImg.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;

                }
            }


            for (int i = 0; i < 256; i++)
            {
                histoChart.Series["Red"].Points.AddY(ni_Red[i]);
                histoChart.Series["Green"].Points.AddY(ni_Green[i]);
                histoChart.Series["Blue"].Points.AddY(ni_Blue[i]);
            }
           // btnGetEqualizedHistogram.Enabled = false;

        }

        private void Histrogram_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmpImg = (Bitmap)newimage;
            int width = newimage.Width;
            int hieght = newimage.Height;


            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hieght; j++)
                {
                    Color pixelColor = bmpImg.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;

                }
            }


            for (int i = 0; i < 256; i++)
            {
                chart1.Series["Red"].Points.AddY(ni_Red[i]);
                chart1.Series["Green"].Points.AddY(ni_Green[i]);
                chart1.Series["Blue"].Points.AddY(ni_Blue[i]);
            }
            //button2.Enabled = true;
            //button3.Enabled = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void histoChart_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click_2(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
