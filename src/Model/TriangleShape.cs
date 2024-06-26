using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    internal class TriangleShape : Shape
    {
        public TriangleShape()
        {
        }

        public TriangleShape(RectangleF rect) : base(rect)
        {
        }

        public TriangleShape(Shape shape) : base(shape)
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

            // Център
            float centerX = Rectangle.X + Rectangle.Width / 2;
            float centerY = Rectangle.Y + Rectangle.Height / 2;

            PointF point1 = new PointF(Rectangle.X, Rectangle.Bottom);
            PointF point2 = new PointF(centerX, Rectangle.Top);
            PointF point3 = new PointF(Rectangle.Right, Rectangle.Bottom);

            // Масив от точки на триъгълника
            PointF[] points = { point1, point2, point3 };

            // Запълващ цвят
            using (Brush brush = new SolidBrush(Color.FromArgb(FillOpacity, FillColor)))
            {
                grfx.FillPolygon(brush, points);
            }

            using (Pen pen = new Pen(Color.FromArgb(BorderOpacity, StrokeColor), BorderWidth))
            {

                grfx.DrawLine(pen, Rectangle.X, Rectangle.Bottom, centerX, Rectangle.Top);

                grfx.DrawLine(pen, centerX, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);

                grfx.DrawLine(pen, Rectangle.Left, Rectangle.Bottom, Rectangle.Right, Rectangle.Bottom);
            }

            grfx.ResetTransform();

        }
      
        public override void Rotate(Graphics grfx)
        {
            base.Rotate(grfx);
        }

    }
}
