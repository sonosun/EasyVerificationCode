using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace EasyVerificationCode.Common
{
    /// <summary>
    /// 创建校验码图片
    /// </summary>
    public class RandomCodeImageCreator
    {
        //背景颜色
        static readonly Color[] BackgroudColors = { Color.LightBlue, Color.LightCyan, Color.LightGray, Color.LightGreen, Color.LightPink, Color.LightSalmon };

        //字体颜色
        static readonly Color[] FontColors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

        //字体
        static readonly string[] Fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

        //间隔
        const int SPACE = 5;

        /// <summary>  
        /// 生成图像
        /// </summary>  
        /// <param name="code"></param>
        /// <param name="fontSize">基准字体大小，实际生成的字体大小以此为基准进行缩放</param>
        public static byte[] Create(string code, int fontSize = 18)
        {
            if (string.IsNullOrEmpty(code)) return new byte[0];

            Random random = new Random();
            byte[] imageBuffer = null;

            //定义图像的大小，生成图像的实例  
            using (Bitmap image = new Bitmap(code.Length * (fontSize + SPACE), fontSize * 2))
            using (Graphics graphics = Graphics.FromImage(image))
            {
                //背景设为白色  
                graphics.Clear(Color.White);

                //在随机位置画背景点  
                for (int i = 0; i < fontSize * 2; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    int colorIndex = random.Next(BackgroudColors.Length);
                    //graphics.DrawRectangle(new Pen(BackgroudColors[colorIndex], 0), x, y, 1, 1);

                    Font font = new Font(Fonts[0], 6, FontStyle.Bold);
                    Brush brush = new SolidBrush(BackgroudColors[colorIndex]);
                    graphics.DrawString(i.ToString(), font, brush, x, y);
                }

                char[] codeArr = code.ToCharArray();
                for (int i = 0; i < codeArr.Length; i++)
                {
                    int characterFontSize = random.Next(fontSize, (int)(fontSize * 1.6));
                    int fontIndex = random.Next(Fonts.Length);
                    Font font = new Font(Fonts[fontIndex], characterFontSize, FontStyle.Bold);

                    int colorIndex = random.Next(FontColors.Length);
                    Brush brush = new SolidBrush(FontColors[colorIndex]);

                    int x = i == 0 ? random.Next(SPACE) : (random.Next(SPACE) + fontSize) * i;
                    int y = random.Next(fontSize / 4);
                    //绘制一个验证字符  
                    graphics.DrawString(codeArr[i].ToString(), font, brush, x, y);
                }
                graphics.Save();

                using (Stream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);
                    imageBuffer = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(imageBuffer, 0, imageBuffer.Length);
                    stream.Close();
                }
            }

            return imageBuffer;
        }

    }
}
