using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            groupBox1.Visible = false;

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        /// <summary>
        /// Изход от програмата. Затваря главната форма, а с това и програмата.
        /// </summary>
        void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}

        /// <summary>
        /// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
        /// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
        /// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
        /// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
        /// </summary>
        void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (pickUpSpeedButton.Checked)
            {
                Shape temp = dialogProcessor.ContainsPoint(e.Location);
                if (temp != null)
                {
                    if (dialogProcessor.Selection.Contains(temp))
                    {
                        dialogProcessor.Selection.Remove(temp);
                    }
                    else
                    {
                        dialogProcessor.Selection.Add(temp);
                    }
                }

                if (dialogProcessor.Selection != null)
                {
                    statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
                    dialogProcessor.IsDragging = true;
                    dialogProcessor.LastLocation = e.Location;

                    viewPort.Invalidate();
                }
            }
        }



        /// <summary>
        /// Прихващане на преместването на мишката.
        /// Ако сме в режм на "влачене", то избрания елемент се транслира.
        /// </summary>
        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllipse();

            statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

            viewPort.Invalidate();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomSquare();

            statusBar.Items[0].Text = "Последно действие: Рисуване на квадрат";

            viewPort.Invalidate();
        }

        private void toolStripButtonCircleDraw_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomCircle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на кръг";

            viewPort.Invalidate();
        }

        private void toolStripButtonDrawLine_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomLine();

            statusBar.Items[0].Text = "Последно действие: Рисуване на линия";

            viewPort.Invalidate();
        }



        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    if (shape is GroupShape group)
                    {
                        // Наследява настройките на групата към всички фигури в нея
                        foreach (var item in group.shapees)
                        {
                            item.FillColor = colorDialog1.Color;
                        }
                    }
                    else
                    {
                        shape.FillColor = colorDialog1.Color;
                    }
                }

                viewPort.Invalidate();
            }
        }


        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            // Променя прозрачността на запълване

            foreach (Shape shape in dialogProcessor.Selection)
            {
                if (shape is GroupShape group)
                {
                    // Наследява настройките на групата към всички фигури в нея
                    foreach (var item in group.shapees)
                    {
                        item.FillOpacity = trackBar1.Value;
                    }
                }
                else
                {
                    shape.FillOpacity = trackBar1.Value;
                }
            }

            viewPort.Invalidate();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            // Променя ширината на контур

            foreach (Shape shape in dialogProcessor.Selection)
            {
                if (shape is GroupShape group)
                {
                    // Наследява настройките на групата към всички фигури в нея
                    foreach (var item in group.shapees)
                    {
                        item.BorderWidth = trackBar2.Value;
                    }
                }
                else
                {
                    shape.BorderWidth = trackBar2.Value;
                }
            }

            viewPort.Invalidate();
        }


        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            // Избор на цвят за контур

            DialogResult result = colorDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    if (shape is GroupShape group)
                    {
                        // Наследяване на настройките на групата към всички фигури в нея
                        foreach (var item in group.shapees)
                        {
                            item.StrokeColor = colorDialog2.Color;
                        }
                    }
                    else
                    {
                        shape.StrokeColor = colorDialog2.Color;
                    }
                }

                viewPort.Invalidate();
            }
        }


        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            // Променя прозрачността на контур

            foreach (Shape shape in dialogProcessor.Selection)
            {
                if (shape is GroupShape group)
                {
                    // Наследява настройките на групата към всички фигури в нея
                    foreach (var item in group.shapees)
                    {
                        item.BorderOpacity = trackBar3.Value;
                    }
                }
                else
                {
                    shape.BorderOpacity = trackBar3.Value;
                }
            }

            viewPort.Invalidate();
        }


        private void trackBar3_Scroll(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            if (groupBox1.Visible)
            {
                groupBox1.Visible = false;
            }
            else
            {
                groupBox1.Visible = true;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            dialogProcessor.Rotate((float)trackBar4.Value);
            statusBar.Items[0].Text = "Последно действие: Ротация";
            viewPort.Invalidate();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (groupBox2.Visible)
            {
                groupBox2.Visible = false;
            }
            else
            {
                groupBox2.Visible = true;
            }
        }

        private void groupBox1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            dialogProcessor.GroupShapes();
            statusBar.Items[0].Text = "Последно действие: Групиране на избраните примитиви";
            viewPort.Invalidate();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            dialogProcessor.UnGroupShapes();
            statusBar.Items[0].Text = "Последно действие: Разгрупиране на избраните примитиви";
            viewPort.Invalidate();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            dialogProcessor.Remove();
            statusBar.Items[0].Text = "Последно действие: Изтриване на фигури";
            viewPort.Invalidate();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            
                dialogProcessor.AddRandomTriangle();

                statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

                viewPort.Invalidate();
            
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomShapeNine();

            statusBar.Items[0].Text = "Последно действие: Рисуване на форма девет";

            viewPort.Invalidate();
        }
    }

}
