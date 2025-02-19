using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSI_Launch
{
    public partial class Form1 : Form
    {
        Database dtb = new Database();
        public Form1()
        {
            InitializeComponent();
            LoadImages();
        }
        private void LoadImages()
        {
            List<Item> items = dtb.GetItems();
            foreach (var item in items)
                {

                    Panel panel = new Panel
                    {
                        Width = 120,
                        Height = 140,
                        Margin = new Padding(10)
                    };
                    FlowLayoutPanel innerPanel = new FlowLayoutPanel
                    {
                        Width = panel.Width,
                        Height = panel.Height,
                        Padding = new Padding(0),
                        AutoScroll = true,
                        FlowDirection = FlowDirection.LeftToRight,
                        //WrapContents = false

                    };
                byte[] imageBytes = item.img;
                MemoryStream ms = new MemoryStream(imageBytes);
                Image itemImage = Image.FromStream(ms);
                PictureBox pictureBox = new PictureBox
                {
                    Width = 100,
                    Height = 100,
                    Image = itemImage, 
                    SizeMode = PictureBoxSizeMode.StretchImage, 
                    BackColor = Color.Transparent,  
                    
                };

                
                pictureBox.Click += (sender, e) =>
                {
                  
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = item.url,
                        UseShellExecute = true
                    });
                };

                Label label = new Label
                    {
                        Width = 100,
                        Height = 20,
                        Text = item.name,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Padding = new Padding(0)
                    };
                    innerPanel.Controls.Add(pictureBox);
                    innerPanel.Controls.Add(label);
                    panel.Controls.Add(innerPanel);
                    flowLayoutPanel1.Controls.Add(panel);
                }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
