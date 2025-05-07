using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using openCV;
using System.Drawing.Imaging;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms.DataVisualization.Charting;
using AForge.Imaging;


namespace Image_project
{

    public partial class Form1 : Form
    {
        private List<Form> allForms = new List<Form>();
        private int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        IplImage image1;
        Bitmap img;
        public Form1()
        {
            InitializeComponent();
            allForms.Add(this);
            ThemeManager.ApplyTheme(this);
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = " ";
            openFileDialog1.Filter = "JPEG|*.JPG|Bitmap|*.bmp|All|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    img = new Bitmap(openFileDialog1.FileName);
                    image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
                    CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
                    IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
                    cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);
                    pictureBox1.Image = (System.Drawing.Image)resized_image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


      
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RGB rgb = new RGB(img);
            allForms.Add(rgb);
            rgb.Show();
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grayscale gray = new Grayscale(img);
            allForms.Add(gray);
            gray.Show();
        }

       

        private void convertToToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void histrogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Histrogram h = new Histrogram(img);
            allForms.Add(h);
            h.Show();

        }


        private double[,] GenerateGaussianKernel(int size, double sigma)
        {
            double[,] kernel = new double[size, size];
            double sum = 0;
            int offset = size / 2;

            for (int y = -offset; y <= offset; y++)
            {
                for (int x = -offset; x <= offset; x++)
                {
                    double exponent = -(x * x + y * y) / (2 * sigma * sigma);
                    kernel[y + offset, x + offset] = Math.Exp(exponent) / (2 * Math.PI * sigma * sigma);
                    sum += kernel[y + offset, x + offset];
                }
            }

            // Normalize the kernel
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    kernel[y, x] /= sum;
                }
            }

            return kernel;
        }
        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            try
            {
                Bitmap inputImage = new Bitmap(pictureBox1.Image);
                int width = inputImage.Width;
                int height = inputImage.Height;

                Bitmap outputImage = new Bitmap(width, height);

                int kernelSize = 7; // Gaussian kernel size
                double[,] kernel = GenerateGaussianKernel(kernelSize, 1.5);

                // Apply Gaussian Blur
                for (int y = kernelSize / 2; y < height - kernelSize / 2; y++)
                {
                    for (int x = kernelSize / 2; x < width - kernelSize / 2; x++)
                    {
                        double r = 0, g = 0, b = 0;

                        for (int ky = -kernelSize / 2; ky <= kernelSize / 2; ky++)
                        {
                            for (int kx = -kernelSize / 2; kx <= kernelSize / 2; kx++)
                            {
                                Color pixel = inputImage.GetPixel(x + kx, y + ky);
                                double weight = kernel[ky + kernelSize / 2, kx + kernelSize / 2];

                                r += pixel.R * weight;
                                g += pixel.G * weight;
                                b += pixel.B * weight;
                            }
                        }

                        r = Clamp((int)r, 0, 255);
                        g = Clamp((int)g, 0, 255);
                        b = Clamp((int)b, 0, 255);

                        outputImage.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                    }
                }

                pictureBox2.Image = outputImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying Gaussian Blur: {ex.Message}");
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

       
        private Bitmap ApplySharpeningFilter(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);

                // Sharpening kernel
                int[,] kernel = {
            {  0, -1,  0 },
            { -1,  5, -1 },
            {  0, -1,  0 }

            };

            int kernelSize = 3;
            int offset = kernelSize / 2;

            for (int y = offset; y < source.Height - offset; y++)
            {
                for (int x = offset; x < source.Width - offset; x++)
                {
                    double r = 0, g = 0, b = 0;

                    for (int ky = -offset; ky <= offset; ky++)
                    {
                        for (int kx = -offset; kx <= offset; kx++)
                        {
                            Color pixel = source.GetPixel(x + kx, y + ky);
                            int weight = kernel[ky + offset, kx + offset];

                            r += pixel.R * weight;
                            g += pixel.G * weight;
                            b += pixel.B * weight;
                        }
                    }

                    r = Clamp((int)r, 0, 255);
                    g = Clamp((int)g, 0, 255);
                    b = Clamp((int)b, 0, 255);

                    result.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }

            return result;
        }

        private void sharpeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            try
            {
                Bitmap inputImage = new Bitmap(pictureBox1.Image);
                Bitmap outputImage = ApplySharpeningFilter(inputImage);
                pictureBox2.Image = outputImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying sharpening filter: {ex.Message}");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Filterd_Image_Click(object sender, EventArgs e)
        {

        }

       

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ThemeManager.ToggleTheme(allForms);
        }

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap source = new Bitmap(pictureBox1.Image);
            int width = source.Width;
            int height = source.Height;
            Bitmap newImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color px = source.GetPixel(x, y);

                    int r = 255 - px.R;
                    int g = 255 - px.G;
                    int b = 255 - px.B;

                    Color inverted = Color.FromArgb(px.A, r, g, b);
                    newImage.SetPixel(x, y, inverted);
                }
            }

            pictureBox2.Image = newImage;

           
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            pictureBox2.Image = null;
            
        }

        private void brightnessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap source = new Bitmap(pictureBox1.Image);
            Bitmap result = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);

                    int r = Math.Max(0, Math.Min(255, pixel.R + 50));
                    int g = Math.Max(0, Math.Min(255, pixel.G + 50));
                    int b = Math.Max(0, Math.Min(255, pixel.B + 50));

                    result.SetPixel(x, y, Color.FromArgb(pixel.A, r, g, b));
                }
            }

            pictureBox2.Image = result;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                SaveFileDialog save = new SaveFileDialog
                {
                    Filter = "PNG Files|*.png|JPEG Files|*.jpg;*.jpeg|BMP Files|*.bmp",
                    Title = "Save Filtered Image",
                    DefaultExt = "png"
                };

                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox2.Image.Save(save.FileName);
                        MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No filtered image to save. Apply a filter first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
