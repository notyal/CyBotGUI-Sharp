using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cybot_GUI
{
    public partial class CyBotGUI : Form
    {
        public CyBotGUI()
        {
            InitializeComponent();
        }

        //Connect
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            logBox.Items.Add("Attempting connection to " + maskedTextBox1.Text);
        }

        private void logBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //
        //Text boxes for the movement controls.
        //
        private void rightValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void leftValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void forwardValue_TextChanged(object sender, EventArgs e)
        {

        }

        //Forward Button
        private void forwardButton_Click(object sender, EventArgs e)
        {
            try
            {
                int fV = Convert.ToInt16(forwardValue.Text);
                if (fV > 0)
                {
                    logBox.Items.Add("Moving forward " + fV + "mm");
                }
            }
            catch (Exception a)
            {
                logBox.Items.Add("INVALID FORMAT");
                logBox.Items.Add(a.Message);
                throw;
            }

        }
        private void addObject(int x, int y)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 200, 300));
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        //Left Button
        private void leftButton_Click(object sender, EventArgs e)
        {
            addObject(0, 0);
        }

        //Right Button
        private void rightButton_Click(object sender, EventArgs e)
        {

        }

        private void clearGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logBox.Items.Clear();
        }

        private void Window_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
