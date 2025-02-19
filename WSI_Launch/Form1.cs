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
                        FlowDirection = FlowDirection.TopDown,
                        Padding = new Padding(0),
                        AutoSize = true
                    };

                byte[] imageBytes = item.img;
                MemoryStream ms = new MemoryStream(imageBytes);
                Image itemImage = Image.FromStream(ms);
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(100, 100);  // Set a standard size for your images (adjust as necessary)
                imageList.Images.Add(itemImage);

                Button button = new Button
                    {


                        Width = 100,
                        Height = 100,
                        Text = "",
                    ImageList = imageList,  // Assign the ImageList to the button
                    ImageIndex = 0,
                    ImageAlign = ContentAlignment.MiddleCenter,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent,
                        FlatAppearance = { BorderSize = 0 }
                    };
                    button.Click += (sender, e) =>
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
                    innerPanel.Controls.Add(button);
                    innerPanel.Controls.Add(label);
                    panel.Controls.Add(innerPanel);
                    flowLayoutPanel1.Controls.Add(panel);
                }
        }
    }
}
