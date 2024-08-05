using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPaletteGenerator
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
        }

        private void flp_Paint(object sender, PaintEventArgs e)
        {
        }
        public bool isgen = false;
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            GeneratePallet();
            isgen = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && isgen == false)
            {
                GeneratePallet();
            }
        }



        private void GeneratePallet()
        {
            flp.Controls.Clear();
            Random rand = new Random();
            List<Color> colors = new List<Color>();
            this.copy.Location = new Point(405, 494);
            copy.ForeColor = Color.White;
            copy.Text = "Click the hex code to copy it!";

            while (colors.Count < 6)
            {
                Color randomColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                bool isDistinct = true;

                foreach (Color color in colors)
                {
                    if (ColorControl(randomColor, color))
                    {
                        isDistinct = false;
                        break;
                    }
                }

                
                if (isDistinct)
                {
                    colors.Add(randomColor);
                }
            }

            foreach (Color color in colors)
            {
                string hexColor = ColorTranslator.ToHtml(color);

                // main panel
                Panel colorPanel = new Panel
                {
                    BackColor = color,
                    Size = new Size(300, 600),
                    Margin = new Padding(5) 
                };

                // label for hex codes
                Label colorLabel = new Label
                {
                    Text = $"       {hexColor}",
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Fill,
                    ForeColor = SystemColors.ControlLightLight,
                    BackColor = ColorTranslator.FromHtml("#21435A"),
                    Font = new Font("Bahnschrift", 10, FontStyle.Regular),
                    Cursor = Cursors.Hand
                };

                colorLabel.Click += (s, evt) =>
                {
                    Clipboard.SetText(hexColor);
                    this.copy.Location = new Point(400, 494);
                    copy.ForeColor = Color.LimeGreen;
                    copy.Text = $"Successfully Copied {hexColor}!";
                };

                TableLayoutPanel colorTable = new TableLayoutPanel
                {
                    ColumnCount = 1,
                    RowCount = 2,
                    Dock = DockStyle.Fill,
                    AutoSize = true
                };
                colorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 400));
                colorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); 

                colorTable.Controls.Add(colorPanel, 0, 0);
                colorTable.Controls.Add(colorLabel, 0, 1);

                flp.Controls.Add(colorTable);
                
            }
        }

        private bool ColorControl(Color c1, Color c2)
        {
            const int threshold = 50; 

            int deltaR = c1.R - c2.R;
            int deltaG = c1.G - c2.G;
            int deltaB = c1.B - c2.B;
            int distance = (int)Math.Sqrt(deltaR * deltaR + deltaG * deltaG + deltaB * deltaB);

            return distance < threshold;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            GeneratePallet();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/audriuxsmik/color-pallete-generator";
            Process.Start(url);
        }
    }
}
