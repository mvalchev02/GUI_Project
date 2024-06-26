using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor

		public DialogProcessor()
		{
			Selection = new List<Shape>();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Избран елемент.
		/// </summary>
		private List<Shape> selection;
		public List<Shape> Selection
		{
			get { return selection; }
			set { selection = value; }
		}

		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging
		{
			get { return isDragging; }
			set { isDragging = value; }
		}

		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation
		{
			get { return lastLocation; }
			set { lastLocation = value; }
		}

		#endregion




		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			RectangleShape rect = new RectangleShape(new Rectangle(x, y, 100, 200));
			rect.FillColor = Color.White;
			rect.StrokeColor = Color.Black;

			ShapeList.Add(rect);
		}

		public void AddRandomEllipse()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 200));
			ellipse.StrokeColor = Color.Blue;

			ellipse.FillColor = Color.White;

			ShapeList.Add(ellipse);
		}
		public void AddRandomCircle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			CircleShape circlee = new CircleShape(new Rectangle(x, y, 100, 100));
			circlee.FillColor = Color.White;

			ShapeList.Add(circlee);
		}

		public void AddRandomSquare()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			Square sq = new Square(new Rectangle(x, y, 100, 100));
			sq.FillColor = Color.White;

			ShapeList.Add(sq);
		}
		public void AddRandomLine()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 500);

			Line ln = new Line(new Rectangle(x, y, 100, 250));

			ShapeList.Add(ln);
		}
        public void AddRandomTriangle()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 500);

            TriangleShape ts = new TriangleShape(new Rectangle(x, y, 100, 100));
            ts.FillColor = Color.White;

            ShapeList.Add(ts);
        }
        public void AddRandomShapeNine()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 500);

            Shape9 sn = new Shape9(new Rectangle(x, y, 150, 100));
            sn.FillColor = Color.White;

            ShapeList.Add(sn);
        }

        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
        public Shape ContainsPoint(PointF point)
		{
			for (int i = ShapeList.Count - 1; i >= 0; i--)
			{
				if (ShapeList[i].Contains(point))
				{
					//ShapeList[i].FillColor = Color.Red;

					return ShapeList[i];
				}
			}
			return null;
		}

        /// <summary>
        /// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
        /// </summary>
        /// <param name="p">Вектор на транслация.</param>
        public void TranslateTo(PointF p)
        {
            if (Selection.Count > 0)
            {
                float coordinateX = p.X - lastLocation.X;
                float coordinateY = p.Y - lastLocation.Y;

                foreach (var shape in Selection)
                {
                    if (shape is GroupShape group)
                    {
                        // Транслирай само обхващащия правоъгълник на групата
                        group.Rectangle = new RectangleF(
                            group.Rectangle.X + coordinateX,
                            group.Rectangle.Y + coordinateY,
                            group.Rectangle.Width,
                            group.Rectangle.Height
                        );
                    }
                    else
                    {
                        // Транслирай единичен елемент
                        shape.Location = new PointF(
                            shape.Location.X + coordinateX,
                            shape.Location.Y + coordinateY
                        );
                    }
                }

                lastLocation = p;
            }
        }

        public void Remove()
        {
            if (Selection == null || Selection.Count == 0)
                return;

            // Премахване на всички избрани елементи от ShapeList
            ShapeList.RemoveAll(shape => Selection.Contains(shape));

            // Изчистване на Selection
            Selection.Clear();
        }


        public void Rotate(float angle)
		{
			foreach (Shape shape in Selection)
			{
				if (selection != null)
				{
					shape.Angle = angle;
				}
			}

		}
		
		      

        public override void DrawShape(Graphics grfx, Shape item)
        {
            base.DrawShape(grfx, item);

            if (Selection != null && Selection.Contains(item))
            {
                // Оцветяване на рамката на селектираните примитиви
                if (item is GroupShape)
                {
                    grfx.DrawRectangle(Pens.Green, item.Rectangle.X, item.Rectangle.Y, item.Rectangle.Width, item.Rectangle.Height);
                }
                else
                {
                    grfx.DrawRectangle(Pens.Red, item.GetBoundingBox().X, item.GetBoundingBox().Y, item.GetBoundingBox().Width, item.GetBoundingBox().Height);
                }
            }
        }


        public void GroupShapes()
        {
            if (Selection.Count < 2) return;

            // Изчисляване на обхващащия правоъгълник
            float minimalX = Selection.Min(shape => shape.Location.X);
            float minimalY = Selection.Min(shape => shape.Location.Y);
            float maximalX = Selection.Max(shape => shape.Location.X + shape.Width);
            float maximalY = Selection.Max(shape => shape.Location.Y + shape.Height);

            // Създаване на обхващащия правоъгълник
            var groupRectangle = new RectangleF(minimalX, minimalY, maximalX - minimalX, maximalY - minimalY);

            // Създаване на нова група с фигурите от Selection
            var group = new GroupShape(groupRectangle);

            // Добавяне на старите фигури към групата
            group.shapees.AddRange(Selection);

            // Добавяне на групата към списъка с фигури
            ShapeList.Add(group);

            // Премахване на фигурите от списъка с фигури, които сега са част от групата
            foreach (var item in group.shapees)
            {
                ShapeList.Remove(item);
            }

            // Добавяне на групата и новия елемент към Selection
            Selection.Add(group);
        }

        public void UnGroupShapes()
        {
            if (Selection.Count == 0) return;

            List<Shape> groupedShapes = new List<Shape>();

            // Преминаване през всички избрани фигури
            foreach (var shape in Selection.ToList())
            {
                // Проверка дали фигурата е група
                if (shape is GroupShape groupShape)
                {
                    // Добавяне на фигурите от групата към списъка с групирани фигури
                    groupedShapes.AddRange(groupShape.shapees);

                    // Премахване на групата от списъка с фигури
                    ShapeList.Remove(groupShape);

                    // Премахване на групата от Selection
                    Selection.Remove(shape);
                }
            }

            // Добавяне на групираните фигури към списъка с фигури
            ShapeList.AddRange(groupedShapes);

            // Добавяне на групираните фигури към Selection
            Selection.AddRange(groupedShapes);
        }




    }
}
