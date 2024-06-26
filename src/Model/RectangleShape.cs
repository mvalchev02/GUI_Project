using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	public class RectangleShape : Shape
	{
		#region Constructor
		
		public RectangleShape(RectangleF rect) : base(rect)
		{
		}
		
		public RectangleShape(RectangleShape rectangle) : base(rectangle)
		{
		}
		
		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// В случая на правоъгълник този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>

       

        public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			Color c = Color.FromArgb(FillOpacity,FillColor);
			Color col = Color.FromArgb(BorderOpacity,StrokeColor);

            base.Rotate(grfx);


            /*PointF point1 = new PointF(Location.X, Location.Y);
			PointF point2 = new PointF(Location.X + Width, Location.Y + Height);

			Color gradient1 = Color.FromArgb(FillOpacity,Color.Blue);
			Color gradient2 = Color.FromArgb(FillOpacity,Color.Red);*/

            //grfx.FillRectangle(new SolidBrush(FillColor),Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

            //grfx.FillRectangle(new LinearGradientBrush(point1,point2, gradient1, gradient2),Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.FillRectangle(new SolidBrush(c),Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

			grfx.DrawRectangle(new Pen(col,BorderWidth),Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.ResetTransform();

        }

    }
}
