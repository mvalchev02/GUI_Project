using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    internal class Shape9 : Shape
    {
        public Shape9()
        {
        }

        public Shape9(RectangleF rect) : base(rect)
        {
        }

        public Shape9(Shape shape) : base(shape)
        {
        }

        public override RectangleF Rectangle { get => base.Rectangle; set => base.Rectangle = value; }
        public override float Width { get => base.Width; set => base.Width = value; }
        public override float Height { get => base.Height; set => base.Height = value; }
        public override PointF Location { get => base.Location; set => base.Location = value; }
        public override Color FillColor { get => base.FillColor; set => base.FillColor = value; }
        public override Color StrokeColor { get => base.StrokeColor; set => base.StrokeColor = value; }
        public override int FillOpacity { get => base.FillOpacity; set => base.FillOpacity = value; }
        public override int BorderWidth { get => base.BorderWidth; set => base.BorderWidth = value; }
        public override int BorderOpacity { get => base.BorderOpacity; set => base.BorderOpacity = value; }
        public override float Angle { get => base.Angle; set => base.Angle = value; }

        public override bool Contains(PointF point)
        {
            return base.Contains(point);
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            base.Rotate(grfx);

            float centerX = Rectangle.X + Rectangle.Width / 2;
            float centerY = Rectangle.Y + Rectangle.Height / 2;

            PointF point1 = new PointF(Rectangle.X, centerY);
            PointF point2 = new PointF(centerX, Rectangle.Top);
            PointF point3 = new PointF(Rectangle.Right, centerY);
            PointF point4 = new PointF(centerX, Rectangle.Bottom);

            PointF[] points = { point1, point2, point3, point4 };

            using (Brush brush = new SolidBrush(Color.FromArgb(FillOpacity, FillColor)))
            {
                grfx.FillPolygon(brush, points);
            } 

            using (Pen pen = new Pen(Color.FromArgb(BorderOpacity, StrokeColor), BorderWidth))
            { 
                             
                grfx.DrawLine(pen, point1, point2);
                grfx.DrawLine(pen, point2, point3);
                grfx.DrawLine(pen, point3, point4);
                grfx.DrawLine(pen, point4, point1);

                grfx.DrawLine(pen, Rectangle.X, centerY, Rectangle.Right, centerY);
                grfx.DrawLine(pen, centerX, centerY, centerX, Rectangle.Bottom);
            }

            grfx.ResetTransform();

        }

        public override RectangleF GetBoundingBox()
        {
            return base.GetBoundingBox();
        }

        public override void Rotate(Graphics grfx)
        {
            base.Rotate(grfx);
        }
    }
}
