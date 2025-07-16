# Image Processing 

https://github.com/user-attachments/assets/9aba42ba-45c5-4657-bddd-6de583eb663e

## Overview
This project is a Windows Forms application built to perform various image processing tasks, including histogram equalization, grayscale conversion, RGB channel separation, Gaussian blur, sharpening, brightness adjustment, and negative effect.

## Technologies Used
- **Programming Language**: C#
- **Framework**: .NET Windows Forms
- **Libraries**:
  - System.Drawing for image manipulation
  - System.Windows.Forms.DataVisualization.Charting for histogram visualization
  - OpenCV (via openCV wrapper) for image loading and resizing
  - AForge.Imaging for image processing utilities

## Features Implemented
1. **Image Loading**: Load images (JPEG, BMP) via OpenFileDialog.
2. **Histogram Equalization**: Compute and display histograms for RGB channels, equalize image histograms, and visualize the equalized histogram.
3. **Grayscale Conversion**: Convert images to grayscale using weighted RGB values.
4. **RGB Channel Separation**: Extract and display individual Red, Green, and Blue channels.
5. **Gaussian Blur**: Apply Gaussian blur using a generated kernel.
6. **Sharpening**: Apply a sharpening filter using a convolution kernel.
7. **Brightness Adjustment**: Increase image brightness by a fixed value.
8. **Negative Effect**: Invert image colors to create a negative effect.
9. **Theme Switching**: Toggle between light and dark themes for the UI.
10. **Image Saving**: Save processed images in PNG, JPEG, or BMP formats.

## How to Run
1. Clone the repository.
2. Open the solution in Visual Studio.
3. Ensure OpenCV and AForge.Imaging dependencies are correctly referenced.
4. Build and run the project.
5. Use the menu to load an image and apply desired filters.

## Notes
- The project assumes the presence of OpenCV and AForge.Imaging libraries.
- Ensure the image format is compatible (24-bit RGB) for certain operations like histogram creation.
