using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Draw.src.Model
{
    public class Square : Shape
    {
        public Square(RectangleF rect) : base(rect)
        {
        }

        public Square(RectangleShape rectangle) : base(rectangle)
        {
        }

        public override RectangleF Rectangle { get => base.Rectangle; set => base.Rectangle = value; }
        public override float Width { get => base.Width; set => base.Width = value; }
        public override float Height { get => base.Height; set => base.Height = value; }
        public override PointF Location { get => base.Location; set => base.Location = value; }
        public override Color FillColor { get => base.FillColor; set => base.FillColor = value; }
        public int[] Vertices
        {
            get { return Vertices; }
            set { Vertices = value; }
        }
        public bool Contains(List<Point> Vertices,PointF point)
        {
            int intersectCount = 0;
            for (int i = 0; i < Vertices.Count; i++)
            {
                int next = (i + 1) % Vertices.Count;
                if (((Vertices[i].Y <= point.Y && point.Y < Vertices[next].Y) ||
                    (Vertices[next].Y <= point.Y && point.Y < Vertices[i].Y) &&
                    (point.X < (Vertices[next].X - Vertices[i].X) * (point.Y - Vertices[i].Y) / (Vertices[next].X - Vertices[i].X) + Vertices[i].X)))
                {
                    intersectCount++;
                }
            }
                
                return intersectCount % 2 == 1;


            if(base.Contains(point)) return true;
            return false;
        }

    /*    public bool isPointinConvexPolygon(List<Point> polygon, PointF point)
        {
            int n = Vertices.Length;
            float sign = 0;
            for(int i=0;i<n-1;i++)
            {
               float dx1 = polygon[(i+1)%n].X - polygon[i].X;
               float dy1 = polygon[(i+1)%n].Y - polygon[i].Y;
               float dx2 = point.X - polygon[i].X;
               float dy2 = point.Y - polygon[i].Y;

                float crossProduct = dx1 * dy2 - dx2 * dy1;
                if(i==0)
                {
                    sign = crossProduct;
                }
                else if(sign*crossProduct < 0)
                {
                    return false;
                }
            }
            return true;
        }*/

        public override void DrawSelf(Graphics grfx)
        {

            Color c = Color.FromArgb(FillOpacity, FillColor);
            Color col = Color.FromArgb(BorderOpacity, StrokeColor);

            base.DrawSelf(grfx);
            base.Rotate(grfx);

            grfx.FillRectangle(new SolidBrush(c),Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawRectangle(new Pen(col,BorderWidth),Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.ResetTransform();

        }
    }
}
