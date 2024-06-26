using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    public class GroupShape : Shape
    {
        

        public GroupShape(RectangleF rect) : base(rect)
        {
        }

        public GroupShape(RectangleShape rectangle) : base(rectangle)
        {
        }

        public GroupShape(RectangleF rect, List<Shape> shapes) : base(rect)
        {
            this.shapees = shapes;
        }

        public List<Shape> shapees = new List<Shape>();

        public override bool Contains(PointF point)
        {

            if (shapees.Count > 0)
            {
                foreach (Shape item in shapees)
                {
                    if (item.Contains(point))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }

            else
            {
                return false;
            }

        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            foreach (Shape item in shapees)
            {
                item.DrawSelf(grfx);
            }

        }


    }
}
