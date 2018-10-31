using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameServers
{
    class CheckCode
    {

        public string CreateRandomCode()
        {
            int CODELENGTH = 4;
            int number;
            string RandomCode = string.Empty;
            Random r = new Random();
            var builder = new StringBuilder();
            builder.Append(RandomCode);
            for (int i = 0; i < CODELENGTH; i++)
            {
                number = r.Next();
                number = number % 36;
                if (number < 10)
                    number += 48;
                else
                    number += 55;
                builder.Append(((char)number).ToString());
            }
            RandomCode = builder.ToString();
            return RandomCode;
        }

        public static void CreateCheckImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == string.Empty)
                return;
            int iWidth = (int)Math.Ceiling(checkCode.Length * 15m);
            int iHeight = 20;
            Bitmap image = new Bitmap(iWidth, iHeight);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random r = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 10; i++)
                {
                    int x1 = r.Next(image.Width);
                    int x2 = r.Next(image.Width);
                    int y1 = r.Next(image.Height);
                    int y2 = r.Next(image.Height);
                    using (var pen = new Pen(Color.Black))
                    {
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
                for (int i = 0; i < 100; i++)
                {
                    int x = r.Next(image.Width);
                    int y = r.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(r.Next()));
                }
                using (var pen = new Pen(Color.SaddleBrown))
                {
                    g.DrawRectangle(pen, 0, 0, image.Width - 1, image.Height - 1);
                }
                Font f = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Red, Color.Purple, 1.2f, true);
                g.DrawString(checkCode, f, brush, 2, 2);
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    //最终验证码图片
                    var img = Image.FromStream(ms);
                }
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
