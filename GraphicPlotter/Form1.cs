using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicPlotter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            one();
            line();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            one();
            line();
        }

        private void one()
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            Pen a = new Pen(Color.Blue, 1);
            Pen b = new Pen(Color.Green, 2);
            Font drawFont = new Font("Arial", 12);
            Font signatureFont = new Font("Arial", 7);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            int sizeWidth = 900;
            int sizeHeight = 600;

            Point center = new Point(((int)(sizeWidth / 2) - 8), (int)((sizeHeight / 2) - 19));
            g.DrawLine(a, 10, center.Y, center.X, center.Y);
            g.DrawLine(a, center.X, center.Y, 2 * center.X - 10, center.Y);
            g.DrawLine(a, center.X, 10, center.X, center.Y);
            g.DrawLine(a, center.X, center.Y, center.X, 2 * center.Y - 10);
            g.DrawString("X", drawFont, drawBrush, new PointF(2 * center.X - 5, center.Y + 10), drawFormat);
            g.DrawString("Y", drawFont, drawBrush, new PointF(center.X + 30, 5), drawFormat);
            g.DrawString("0", signatureFont, drawBrush, new PointF(center.X, center.Y), drawFormat);


            g.DrawLine(a, center.X, 10, center.X + 5, 20);
            g.DrawLine(a, center.X, 10, center.X - 5, 20);
            g.DrawLine(a, sizeWidth - 25, center.Y, sizeWidth - 30, center.Y + 5);
            g.DrawLine(a, sizeWidth - 25, center.Y, sizeWidth - 30, center.Y - 5);
            int stepForAxes = 25;
            int lenghtShtrih = 3;
            int maxValueForAcesX = 12;
            int maxValueForAcesY = 10;
            float oneDelenieX = (float)maxValueForAcesX / ((float)center.X / (float)stepForAxes);
            float oneDelenieY = (float)maxValueForAcesY / ((float)center.Y / (float)stepForAxes);


            for (int i = center.X, j = center.X, k = 1; i < 2 * center.X - 30; j -= stepForAxes, i += stepForAxes, k++)
            {

                g.DrawLine(a, i, center.Y - lenghtShtrih, i, center.Y + lenghtShtrih);

                g.DrawLine(a, j, center.Y - lenghtShtrih, j, center.Y + lenghtShtrih);
                if (i < 2 * center.X - 55)
                {
                    g.DrawString((k * oneDelenieX).ToString("0.0"), signatureFont, drawBrush, new PointF(i + stepForAxes + 9, center.Y + 6), drawFormat);
                    g.DrawString(((k * oneDelenieX).ToString("0.0").ToString() + "‐"), signatureFont, drawBrush, new PointF(j - stepForAxes + 9, center.Y + 6), drawFormat);
                }
            }

            for (int i = center.Y, j = center.Y, k = 1; i < 2 * center.Y - 30; j -= stepForAxes, i += stepForAxes, k++)
            {

                g.DrawLine(a, center.X - lenghtShtrih, i, center.X + lenghtShtrih, i);

                g.DrawLine(a, center.X - lenghtShtrih, j, center.X + lenghtShtrih, j);
                if (i < 2 * center.Y - 55)
                {
                    g.DrawString((k * oneDelenieY).ToString("0.0"), signatureFont, drawBrush, new PointF(center.X + 22, j + stepForAxes + 9), drawFormat);
                    g.DrawString(((k * oneDelenieY).ToString("0.0").ToString() + "‐"), signatureFont, drawBrush, new PointF(center.X + 22, i - stepForAxes + 9), drawFormat);
                }
            }
            int numOfPoint = 200;
            float[] first = new float[numOfPoint];
            for (int i = 0; i < numOfPoint; i++)
            {
                first[i] = (float)maxValueForAcesX / (float)numOfPoint * (i + 1) - (float)(maxValueForAcesX / 2);
            }
            float[] second = new float[numOfPoint];

            for (int i = 0; i < numOfPoint; i++)
            {
                second[i] = (float)(Math.Exp(first[i]) * Math.Pow(first[i], 3));
            }
            Point[] pointOne = new Point[numOfPoint];
            float tempX = 1 / oneDelenieX * stepForAxes;
            float tempY = 1 / oneDelenieY * stepForAxes;
            for (int i = 0; i < numOfPoint; i++)
            {
                pointOne[i].X = center.X + (int)(first[i] * tempX);

                pointOne[i].Y = center.Y - (int)(second[i] * tempY);
            }

            g.DrawCurve(b, pointOne);
            g.DrawLines(b, pointOne);
        }
        private void line()
        {
            chart1.Series[0].Points.Clear();
            for (double x = -6; x <= 2; x += 0.01)
            {
                chart1.Series[0].Points.AddXY(x, Math.Exp(x) * Math.Pow(x, 3));
            }
        }
    }
}
