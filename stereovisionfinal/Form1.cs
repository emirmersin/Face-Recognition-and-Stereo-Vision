using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace stereovisionfinal
{
    public partial class Form1 : Form
    {
        Capture capture1;
        Capture capture2;
        Rectangle faceL;
        float xr, yr, xl, yl,y,x,F,D, Z;
        Rectangle faceR;
        CascadeClassifier classifierFace = new CascadeClassifier("C:/Users/Emir/Downloads/stereovisionfinal/stereovisionfinal/stereovisionfinal/haarcascade_frontalface_default.xml");
       
        
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (capture1==null)
            {
                capture1 = new Capture(0);
            }
            capture1.ImageGrabbed += timer1_Tick;
            capture1.Start();
            if (capture2 == null)
            {
                capture2 = new Capture(1);
            }
            capture2.ImageGrabbed += timer1_Tick;
            capture2.Start();
            xr = (faceL.Y - faceR.Y);
            yr = (faceL.X - faceR.X);
            F = 800;
            D = 1500;
            Z = (F * D) / (xr * yr);

            
            

            label1.Text = "Distance is:" + Z + "Y is: " + xr + "X is: " + yr;

           

            
        }

        

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                
                Mat Frame1 = new Mat();
                capture1.Retrieve(Frame1);
                
                var ImageFrame1 = Frame1.ToImage<Bgr, Byte>();
                if (ImageFrame1 != null)
                {
                    var grayFrame = ImageFrame1.Convert<Gray, byte>();
                    Rectangle[] faces = classifierFace.DetectMultiScale(grayFrame, 1.3, 5);
                    foreach (var face in faces)
                    {
                        ImageFrame1.Draw(face, new Bgr(Color.BurlyWood), 3);
                        faceL = face;
                    }
                    
                    pictureBox1.Image = ImageFrame1.ToBitmap();
                   
                    var grayFace = ImageFrame1.Convert<Gray, byte>();
                    pictureBox3.Image = grayFace.ToBitmap();
                    

                }
                Mat Frame2 = new Mat();
                capture2.Retrieve(Frame2);
                
                var ImageFrame2 = Frame2.ToImage<Bgr, Byte>();
                if (ImageFrame2 != null)
                {
                    var grayFrame = ImageFrame2.Convert<Gray, byte>();
                    var faces = classifierFace.DetectMultiScale(grayFrame, 1.3, 5);




                    foreach (var face in faces)
                    {
                        ImageFrame2.Draw(face, new Bgr(Color.BurlyWood), 3);
                        faceR = face;
                    }
                    
                     
                    
                    

                    pictureBox2.Image = ImageFrame2.ToBitmap();
                
                }
                
            }
            catch (Exception)
            {

            }
        }
    }
}
