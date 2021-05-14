using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToolFunction.Utils
{
    class PictureZoom
    {
        private static bool isMove = false;
        private static Point mouseDownPoint;
        private static PictureBox pbox;

        /// <summary>
        /// 加载IMG，路径形式
        /// </summary>
        /// <param name="pb">PictureBox</param>
        /// <param name="path"></param>
        public static void LoadImg(PictureBox pb, string path)
        {
            pbox = pb;

            pbox.MouseDown += new MouseEventHandler(MouseDown);
            pbox.MouseMove += new MouseEventHandler(MouseMove);
            pbox.MouseUp += new MouseEventHandler(MouseUp);
            pbox.MouseWheel += new MouseEventHandler(MouseWheel);

            GC.Collect();

            Image img = Image.FromFile(path);
            Bitmap bmp = new Bitmap(img);
            img.Dispose();

            pbox.Image = bmp;
        }

        /// <summary>
        /// 加载IMG，Bitmap形式
        /// </summary>
        /// <param name="pb">PictureBox</param>
        /// <param name="bm">Bitmap</param>
        public static void LoadImg(PictureBox pb, Bitmap bm)
        {
            pbox = pb;

            pbox.MouseDown += new MouseEventHandler(MouseDown);
            pbox.MouseMove += new MouseEventHandler(MouseMove);
            pbox.MouseUp += new MouseEventHandler(MouseUp);
            pbox.MouseWheel += new MouseEventHandler(MouseWheel);

            pbox.Image = bm;

            GC.Collect();
        }

        private static void MouseWheel(object sender, MouseEventArgs e)
        {
            double x = (double)e.X / pbox.Width;
            double y = (double)e.Y / pbox.Height;

            var p = pbox.Size;

            //太窄了，宽度加个10
            if (p.Width + e.Delta < 100)
            {
                p.Width += 10;
            }
            else
                p.Width += e.Delta;

            //太矮或太高，高度加（减）个10
            int temp = p.Height + e.Delta * pbox.Height / pbox.Width;
            if (temp < 100)
            {
                p.Height += 10;
            }
            else if (p.Height > 5000)
            {
                p.Height -= 10;
            }
            else
                p.Height = temp;

            pbox.Size = p;

            double x2 = (double)e.X / pbox.Width;
            double y2 = (double)e.Y / pbox.Height;

            double x3 = x2 - x;
            double y3 = y2 - y;

            int x4 = (int)(pbox.Width * x3);
            int y4 = (int)(pbox.Height * y3);
            Point location = new Point();
            location.X = pbox.Location.X + x4;
            location.Y = pbox.Location.Y + y4;
            pbox.Location = location;
        }

        private static void MouseMove(object sender, MouseEventArgs e)
        {
            pbox.Focus(); //鼠标在picturebox上时才有焦点，此时可以缩放
            if (isMove)
            {
                int x, y;   //新的pictureBox1.Location(x,y)
                int moveX, moveY; //X方向，Y方向移动大小。
                moveX = Cursor.Position.X - mouseDownPoint.X;
                moveY = Cursor.Position.Y - mouseDownPoint.Y;
                x = pbox.Location.X + moveX;
                y = pbox.Location.Y + moveY;
                pbox.Location = new Point(x, y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }

        }

        private static void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = false;
            }
        }

        private static void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X; //记录鼠标左键按下时位置
                mouseDownPoint.Y = Cursor.Position.Y;
                isMove = true;
                pbox.Focus(); //鼠标滚轮事件(缩放时)需要picturebox有焦点
            }
        }
    }
}
