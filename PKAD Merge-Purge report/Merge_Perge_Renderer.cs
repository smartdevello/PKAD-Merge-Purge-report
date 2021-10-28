using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace PKAD_Merge_Purge_report
{
    public class Merge_Perge_Renderer
    {
        private int width = 0, height = 0;
        private double totHeight = 1200;
        private Bitmap bmp = null;
        private Graphics gfx = null;
        private List<Merge_Purge_Model> data = null;
        Image logoImg = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo.png"));
        Image redFingerImg = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "red_finger.png"));
        public Merge_Perge_Renderer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public int getDataCount()
        {
            if (this.data == null) return 0;
            else return this.data.Count;
        }
        public List<Merge_Purge_Model> getData()
        {
            return this.data;
        }
        public void setData(List<Merge_Purge_Model> data)
        {
            this.data = data;
        }
        public void setRenderSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public Point convertCoord(Point a)
        {
            double px = height / totHeight;

            Point res = new Point();
            res.X = (int)(a.X * px);
            res.Y = (int)((totHeight - a.Y) * px);
            return res;
        }
        public PointF convertCoord(PointF p)
        {
            double px = height / totHeight;
            PointF res = new PointF();
            res.X = (int)(p.X * px);
            res.Y = (int)((totHeight - p.Y) * px);
            return res;
        }
        public Bitmap getBmp()
        {
            return this.bmp;
        }
        public void drawFilledCircle(Brush brush, Point o, Size size)
        {
            double px = height / totHeight;
            size.Width = (int)(size.Width * px);
            size.Height = (int)(size.Height * px);

            Rectangle rect = new Rectangle(convertCoord(o), size);

            gfx.FillEllipse(brush, rect);
        }
        public void fillRectangle(Color color, Rectangle rect)
        {
            rect.Location = convertCoord(rect.Location);
            double px = height / totHeight;
            rect.Width = (int)(rect.Width * px);
            rect.Height = (int)(rect.Height * px);

            Brush brush = new SolidBrush(color);
            gfx.FillRectangle(brush, rect);
            brush.Dispose();

        }
        public void drawRectangle(Pen pen, Rectangle rect)
        {
            rect.Location = convertCoord(rect.Location);
            double px = height / totHeight;
            rect.Width = (int)(rect.Width * px);
            rect.Height = (int)(rect.Height * px);
            gfx.DrawRectangle(pen, rect);
        }

        public void drawImg(Image img, Point o, Size size)
        {
            double px = height / totHeight;
            o = convertCoord(o);
            Rectangle rect = new Rectangle(o, new Size((int)(size.Width * px), (int)(size.Height * px)));
            gfx.DrawImage(img, rect);

        }
        public void drawString(Color color, Point o, string content, int font = 15)
        {

            o = convertCoord(o);

            // Create font and brush.
            Font drawFont = new Font("Calibri", font);
            SolidBrush drawBrush = new SolidBrush(color);

            gfx.DrawString(content, drawFont, drawBrush, o.X, o.Y);

            drawFont.Dispose();
            drawBrush.Dispose();

        }

        public void drawCenteredString_withBorder(string content, Rectangle rect, Brush brush, Font font, Color borderColor)
        {

            //using (Font font1 = new Font("Calibri", fontSize, FontStyle.Bold, GraphicsUnit.Point))

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            gfx.DrawString(content, font, brush, rect, stringFormat);

            Pen borderPen = new Pen(new SolidBrush(borderColor), 2);
            gfx.DrawRectangle(borderPen, rect);
            borderPen.Dispose();
        }
        public void drawCenteredImg_withBorder(Image img, Rectangle rect, Brush brush, Font font, Color borderColor)
        {
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            //gfx.DrawString(content, font, brush, rect, stringFormat);
            //drawImg(logoImg, new Point(20, 60), new Size(150, 50));
            gfx.DrawImage(img, rect);
            Pen borderPen = new Pen(new SolidBrush(borderColor), 2);
            gfx.DrawRectangle(borderPen, rect);
            borderPen.Dispose();
        }
        public void drawCenteredString(string content, Rectangle rect, Brush brush, Font font)
        {

            //using (Font font1 = new Font("Calibri", fontSize, FontStyle.Bold, GraphicsUnit.Point))

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            gfx.DrawString(content, font, brush, rect, stringFormat);
            //gfx.DrawRectangle(Pens.Black, rect);

        }
        private void fillPolygon(Brush brush, PointF[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = convertCoord(points[i]);
            }
            gfx.FillPolygon(brush, points);
        }
        public void drawLine(Point p1, Point p2, Color color, int linethickness = 1)
        {
            if (color == null)
                color = Color.Gray;

            p1 = convertCoord(p1);
            p2 = convertCoord(p2);
            gfx.DrawLine(new Pen(color, linethickness), p1, p2);

        }
        public void drawString(Font font, Color brushColor, string content, Point o)
        {
            o = convertCoord(o);
            SolidBrush drawBrush = new SolidBrush(brushColor);
            gfx.DrawString(content, font, drawBrush, o.X, o.Y);
        }
        public void drawString(Point o, string content, int font = 15)
        {

            o = convertCoord(o);

            // Create font and brush.
            Font drawFont = new Font("Calibri", font);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            gfx.DrawString(content, drawFont, drawBrush, o.X, o.Y);

        }

        public void drawPie(Color color, Point o, Size size, float startAngle, float sweepAngle)
        {
            // Create location and size of ellipse.
            double px = height / totHeight;
            size.Width = (int)(size.Width * px);
            size.Height = (int)(size.Height * px);

            Rectangle rect = new Rectangle(convertCoord(o), size);
            // Draw pie to screen.            
            Brush grayBrush = new SolidBrush(color);
            gfx.FillPie(grayBrush, rect, startAngle, sweepAngle);
        }


        public void draw(int pageID = 1)
        {
            if (bmp == null)
                bmp = new Bitmap(width, height);
            else
            {
                if (bmp.Width != width || bmp.Height != height)
                {
                    bmp.Dispose();
                    bmp = new Bitmap(width, height);

                    gfx.Dispose();
                    gfx = Graphics.FromImage(bmp);
                    gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                }
            }

            if (gfx == null)
            {
                gfx = Graphics.FromImage(bmp);
                gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            }
            else
            {
                gfx.Clear(Color.Transparent);
            }


            if (data == null) return;
            int baseIndex = 12 * (pageID - 1);

            if (baseIndex >= data.Count ) return;

            Pen blackBorderPen2 = new Pen(Color.Black, 2);
            Font percentFont = new Font("Calibri", 28, FontStyle.Bold, GraphicsUnit.Point);
            Font percentFont1 = new Font("Calibri", 28, FontStyle.Regular, GraphicsUnit.Point);
            Font numFont = new Font("Calibri", 45, FontStyle.Bold, GraphicsUnit.Point);
            Font h3Font = new Font("Calibri", 24, FontStyle.Bold, GraphicsUnit.Point);
            Font h4Font = new Font("Calibri", 20, FontStyle.Bold, GraphicsUnit.Point);


            Font headertitle = new Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point);
            Font headerText = new Font("Calibri", 18, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont10 = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont8 = new Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont7 = new Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont6 = new Font("Calibri", 6, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont5 = new Font("Calibri", 5, FontStyle.Regular, GraphicsUnit.Point);

            double px = height / totHeight;
            int totWidth = (int)(this.width / px);
            drawCenteredString("PKAD MERGE/PURGE REPORT", new Rectangle(0, 1200, 750, 80), Brushes.Black, percentFont);
            fillRectangle(Color.Black, new Rectangle(720, 1200, totWidth - 720, 80));
            drawCenteredString(string.Format("R-{0}", data[baseIndex].registered), new Rectangle(720, 1200, 270, 80 ), Brushes.White, percentFont);
            fillRectangle(Color.White, new Rectangle(1050, 1195, 670, 58));
            drawCenteredString(string.Format("{0,4:0000.##} {1}", data[baseIndex].precinct, data[baseIndex].precinct_name), new Rectangle(1050, 1200, 670, 80), Brushes.Black, percentFont);
            fillRectangle(Color.Green, new Rectangle(1640, 1195, totWidth - 1650, 58));
            drawCenteredString(string.Format("V-{0}", data[baseIndex].voted), new Rectangle(1640, 1200, (totWidth - 1650) / 2, 75), Brushes.White, percentFont);
            drawCenteredString(Math.Round(data[baseIndex].voted * 100 / (double)data[baseIndex].registered, 2).ToString() + "%", new Rectangle(totWidth - (totWidth - 1650) / 2, 1200, (totWidth - 1650) / 2, 75), Brushes.Yellow, h3Font);


            drawLine(new Point(0, 600), new Point(totWidth, 600), Color.Black, 4);
            
            drawCenteredString("REGISTERED VOTER GROWTH INDEX", new Rectangle(0, 1040, 750, 75), Brushes.Black, percentFont);
            drawCenteredString("(5 months PRIOR 2020 General Election)", new Rectangle(750, 1040, 800, 75), Brushes.Black, percentFont1);
            drawCenteredString("REGISTERED VOTER GROWTH INDEX", new Rectangle(0, 580, 750, 75), Brushes.Black, percentFont);
            drawCenteredString("(7 months POST 2020 General Election)", new Rectangle(750, 580, 800, 75), Brushes.Black, percentFont1);

            //Draw First Row section

            //Draw A
            int index = baseIndex;
            int baseTop = 980;
            int baseLeft = 0;
            float percent = 0, angle = 0;

            percent = (data[index].additions + data[index + 1].additions + data[index + 2].additions) / (float)data[index].registered;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Green, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("3 MONTH\nGROWTH", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);

            //Draw B

            baseLeft = 270 ;
            percent = (data[index + 3].additions + data[index + 4].additions) / (float)data[index].registered;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Green, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("LAST 2 MONTH\nGROWTH", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);

            //Draw C

            baseLeft = 270 * 2;
            percent = (data[index].additions + data[index + 1].additions + data[index + 2].additions + data[index + 3].additions + data[index + 4].additions) / 5.0f / (float)data[index].registered;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Green, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("AVG MONTH\nGROWTH", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);


            //Draw D
            int totadditions = data[index].additions + data[index + 1].additions + data[index + 2].additions + data[index + 3].additions + data[index + 4].additions;
            baseLeft = 270 * 3;
            drawCenteredString(totadditions.ToString(), new Rectangle(baseLeft, baseTop - 80, 270, 80), Brushes.Black, numFont);
            fillRectangle(Color.Green, new Rectangle(baseLeft + 40, baseTop - 160, 190, 40));
            drawCenteredString("TOTAL NEW\nREGISTRATIONS", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);



            ////Draw E
            int totadditions_voted = data[index].additions_voted + data[index + 1].additions_voted + data[index + 2].additions_voted + data[index + 3].additions_voted + data[index + 4].additions_voted;

            baseLeft = 270 * 4;
            percent = totadditions_voted / (float)totadditions;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Green, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("%\nACTIVATION", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);

            ////Draw F
            int tot_removals = data[index].removals + data[index + 1].removals + data[index + 2].removals + data[index + 3].removals + data[index + 4].removals;

            baseLeft = 270 * 5;
            percent = tot_removals / (float) data[index].registered;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Red, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("5 MONTH\nPURGE INDEX", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);


            ////Draw G
            baseLeft = 270 * 6;
            percent = tot_removals / (float)data[index].registered;
            angle = 360 * percent;
            string gdisplayed = Math.Round((percent * 20), 2).ToString() + "%";
            if (gdisplayed.StartsWith("0")) gdisplayed = gdisplayed.Substring(1);

            drawCenteredString(gdisplayed, new Rectangle(baseLeft, baseTop - 80, 270, 80), Brushes.Black, numFont);
            drawCenteredString("5 MONTH\nPURGE AVG", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);


            ////Draw H
            baseLeft = 270 * 7;
            percent = tot_removals / (float)data[index].registered;

            drawCenteredString(tot_removals.ToString(), new Rectangle(baseLeft, baseTop - 80, 270, 80), Brushes.Black, numFont);
            fillRectangle(Color.Red, new Rectangle(baseLeft + 40, baseTop - 160, 190, 40));
            drawCenteredString("TOTAL\nPURGED", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);



            //Draw I
            baseTop = 520;
            baseLeft = 0;

            percent = (data[index +5].additions + data[index + 6].additions + data[index + 7].additions) / (float)data[index].registered;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Green, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("3 MONTH\nGROWTH", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);


            //Draw J

            baseLeft = 270;
            percent = (data[index + 8].additions + data[index + 9].additions + data[index + 10].additions + data[index + 11].additions) / (float)data[index].registered;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Green, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("LAST 4 MONTH\nGROWTH", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);



            //Draw K

            baseLeft = 270 * 2;
            percent = (data[index + 8].additions + data[index + 9].additions + data[index + 10].additions + data[index + 11].additions) / 4.0f / (float)data[index].registered;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Green, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("AVG MONTH\nGROWTH", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);


            //Draw L
            totadditions = data[index + 5].additions + data[index + 6].additions + data[index + 7].additions + data[index + 8].additions + data[index + 9].additions + data[index + 10].additions + data[index + 11].additions;
            baseLeft = 270 * 3;
            drawCenteredString(totadditions.ToString(), new Rectangle(baseLeft, baseTop - 80, 270, 80), Brushes.Black, numFont);
            fillRectangle(Color.Green, new Rectangle(baseLeft + 40, baseTop - 160, 190, 40));
            drawCenteredString("TOTAL NEW\nREGISTRATIONS", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);


            ////Draw M
            int tot_removals1 = data[index + 5].removals + data[index + 6].removals + data[index + 7].removals + data[index + 8].removals + data[index + 9].removals + data[index + 10].removals + data[index + 11].removals;

            baseLeft = 270 * 4;
            percent = tot_removals1 / (float)data[index].registered;
            angle = 360 * percent;

            drawPie(Color.LightGray, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 0, 360);
            drawPie(Color.Red, new Point(baseLeft + 25, baseTop - 25), new Size(220, 220), 270, angle);
            drawPie(Color.White, new Point(baseLeft + 60, baseTop - 60), new Size(150, 150), 0, 360);
            drawCenteredString(string.Format("{0}%", Math.Round(percent * 100, 2).ToString()), new Rectangle(baseLeft + 60, baseTop - 60, 150, 150), Brushes.Black, h3Font);
            drawCenteredString("7 MONTH\nPURGE INDEX", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);


            ////Draw N
            baseLeft = 270 * 5;
            percent = tot_removals1 / 7.0f /  (float)data[index].registered;
            angle = 360 * percent;
            gdisplayed = Math.Round((percent * 100), 2).ToString() + "%";
            if (gdisplayed.StartsWith("0")) gdisplayed = gdisplayed.Substring(1);

            drawCenteredString(gdisplayed, new Rectangle(baseLeft, baseTop - 80, 270, 80), Brushes.Black, numFont);
            drawCenteredString("7 MONTH\nPURGE AVG", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);

            ////Draw O
            baseLeft = 270 * 6;
            drawCenteredString(tot_removals1.ToString(), new Rectangle(baseLeft, baseTop - 80, 270, 80), Brushes.Black, numFont);
            fillRectangle(Color.Red, new Rectangle(baseLeft + 40, baseTop - 160, 190, 40));
            drawCenteredString("TOTAL\nPURGED", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);

            ////Draw P
            baseLeft = 270 * 7;
            if (tot_removals1 > tot_removals)
            {
                string pdisplayed = string.Format("+{0}%", tot_removals1 * 100 / tot_removals);
                drawCenteredString(pdisplayed, new Rectangle(baseLeft, baseTop + 20, 270, 80), Brushes.Black, numFont);
                drawImg(redFingerImg, new Point(baseLeft, baseTop - 40), new Size(270, 220));
            } else
            {
                string pdisplayed = string.Format("+{0}%", tot_removals1 * 100 / tot_removals);
                drawCenteredString(pdisplayed, new Rectangle(baseLeft, baseTop - 80, 270, 80), Brushes.Black, numFont);
            }
            drawCenteredString("INCREASED\nPURGED", new Rectangle(baseLeft, baseTop - 240, 270, 120), Brushes.Black, h4Font);


            drawImg(logoImg, new Point(30, 120), new Size(150, 80));
            string copyright = "©2021 Tesla Laboratories, llc & JHP";
            drawCenteredString(copyright, new Rectangle(1600, 100, 500, 50), Brushes.Black, headerText);
            percentFont.Dispose();
            numFont.Dispose();

            h3Font.Dispose();
            h4Font.Dispose();
            blackBorderPen2.Dispose();
            headertitle.Dispose();
            headerText.Dispose();
            textFont10.Dispose();
            textFont8.Dispose();
            textFont7.Dispose();
            textFont6.Dispose();
            textFont5.Dispose();
        }
    }
}
