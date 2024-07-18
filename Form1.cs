using System;
using System.Drawing;
using System.Windows.Forms;

namespace GridDragDropApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Assign the MouseDown event handler to each control object (Label, Textbox, etc.)
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button)
                {
                    control.MouseDown += Button_MouseDown;
                }
                if (control is Label)
                {
                    control.MouseDown += Label_MouseDown;
                }
                if (control is TextBox)
                {
                    control.MouseDown += TextBox_MouseDown;
                }
            }

            // Assign the DragEnter and DragDrop event handlers to the TableLayoutPanel
            tableLayoutPanel1.DragEnter += TableLayoutPanel1_DragEnter;
            tableLayoutPanel1.DragDrop += TableLayoutPanel1_DragDrop;
            tableLayoutPanel1.AllowDrop = true;
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && e.Button == MouseButtons.Left)
            {
                button.DoDragDrop(button, DragDropEffects.Move);
            }
        }

        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            if (label != null && e.Button == MouseButtons.Left)
            {
                label.DoDragDrop(label, DragDropEffects.Move);
            }
        }

        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null && e.Button == MouseButtons.Left)
            {
                textbox.DoDragDrop(textbox, DragDropEffects.Move);
            }
        }

        private void TableLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Button)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else if (e.Data.GetDataPresent(typeof(Label)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else if (e.Data.GetDataPresent(typeof(TextBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TableLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Button)))
            {
                Button button = (Button)e.Data.GetData(typeof(Button));
                Label label = (Label)e.Data.GetData(typeof(Label));
                TextBox textbox = (TextBox)e.Data.GetData(typeof(TextBox));

                Point clientPoint = tableLayoutPanel1.PointToClient(new Point(e.X, e.Y));

                int row = GetRow(clientPoint.Y);
                int column = GetColumn(clientPoint.X);

                if (row != -1 && column != -1)
                {
                    tableLayoutPanel1.Controls.Remove(button);
                    tableLayoutPanel1.Controls.Add(button, column, row);
                }
            }

            if (e.Data.GetDataPresent(typeof(Label)))
            {
                Label label = (Label)e.Data.GetData(typeof(Label));

                Point clientPoint = tableLayoutPanel1.PointToClient(new Point(e.X, e.Y));

                int row = GetRow(clientPoint.Y);
                int column = GetColumn(clientPoint.X);

                if (row != -1 && column != -1)
                {
                    tableLayoutPanel1.Controls.Remove(label);
                    tableLayoutPanel1.Controls.Add(label, column, row);
                }
            }

            if (e.Data.GetDataPresent(typeof(TextBox)))
            {
                TextBox textbox = (TextBox)e.Data.GetData(typeof(TextBox));

                Point clientPoint = tableLayoutPanel1.PointToClient(new Point(e.X, e.Y));

                int row = GetRow(clientPoint.Y);
                int column = GetColumn(clientPoint.X);

                if (row != -1 && column != -1)
                {
                    tableLayoutPanel1.Controls.Remove(textbox);
                    tableLayoutPanel1.Controls.Add(textbox, column, row);
                }
            }
        }

        private int GetRow(int y)
        {
            int totalHeight = 0;
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                totalHeight += tableLayoutPanel1.GetRowHeights()[i];
                if (y < totalHeight)
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetColumn(int x)
        {
            int totalWidth = 0;
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                totalWidth += tableLayoutPanel1.GetColumnWidths()[i];
                if (x < totalWidth)
                {
                    return i;
                }
            }
            return -1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeactivateDragDrop();
        }

        private void DeactivateDragDrop()
        {
            // Detach the DragEnter and DragDrop event handlers from the TableLayoutPanel
            tableLayoutPanel1.DragEnter -= TableLayoutPanel1_DragEnter;
            tableLayoutPanel1.DragDrop -= TableLayoutPanel1_DragDrop;
            tableLayoutPanel1.AllowDrop = false;

            // Detach the MouseDown event handlers from the buttons
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button)
                {
                    control.MouseDown -= Button_MouseDown;
                    control.MouseDown += Label_MouseDown;
                    control.MouseDown += TextBox_MouseDown;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Assign and activate the DragEnter and DragDrop event handlers to the TableLayoutPanel in Designer
            tableLayoutPanel1.DragEnter += TableLayoutPanel1_DragEnter;
            tableLayoutPanel1.DragDrop += TableLayoutPanel1_DragDrop;
            tableLayoutPanel1.AllowDrop = true;

            // Attach the MouseDown event handlers from the buttons
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button)
                {
                    control.MouseDown += Button_MouseDown;
                }
                if (control is Label)
                {
                    control.MouseDown += Label_MouseDown;
                }
                if (control is TextBox)
                {
                    control.MouseDown += TextBox_MouseDown;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
