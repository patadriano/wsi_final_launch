using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace WSI_Launch
{
    public partial class Form2 : Form
    {
        Database dtb = new Database();
        byte[] imageBytes;
        byte[] imageByte;
        int id;
        public Form2()
        {
            InitializeComponent();
            GetImages();
            panel1.Visible = false;
            LoadImages();


        }
        private void LoadImages()
        {
            flowLayoutPanel1.Controls.Clear();
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
                    Tag = item.id

                };
                pictureBox.Click += (sender, e) =>
                {

                    button4.Visible = true;
                    button5.Visible = true;
                    panel1.Visible = true;
                    button2.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label1.Visible = false;
                    button1.Visible = false;
                    pictureBox1.Visible = false;

                    PictureBox clickedPictureBox = sender as PictureBox;
                    if (clickedPictureBox != null)
                    {
                        id = (int)clickedPictureBox.Tag; 
                        
                    }
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void GetImages()
        {
            List<Item> items = dtb.GetItems();

            foreach (Item item in items)
            {
                byte[] imageBytes = item.img;
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image itemImage = Image.FromStream(ms);
                    //pictureBox1.Image = itemImage;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Use 'using' to ensure the image is disposed of correctly
                using (Image img = Image.FromFile(openFileDialog.FileName))
                {
                    // Convert image to byte array
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, img.RawFormat); // Save image to memory stream
                        imageBytes = ms.ToArray();    // Store byte array
                    }

                    label1.Text = Path.GetFileName(openFileDialog.FileName);
                }
                    imageByte = imageBytes;
                    MemoryStream mus = new MemoryStream(imageBytes);
                    Image itemImage = Image.FromStream(mus);
                    pictureBox1.Image = itemImage;
                
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newItem = new Item
            {
                id = id,
                name = textBox1.Text,
                url = textBox2.Text,
                img = imageByte
            };
            ;
            if (button1.Text == "Add" )
            {
               
                dtb.InsertItems(newItem);
                
            }
            else if(button1.Text == "Edit")
            {
                if (imageByte == null)
                {
                    List<Item> items = dtb.RetrieveSpecific(id);

                    newItem.img = items[0].img;
                }
               
                
                dtb.UpdateItems(newItem);

            }
            
            LoadImages();
            panel1.Visible = false;


        }
        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            button2.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            label1.Visible = true;
            button1.Visible = true;
            button1.Text = "Add";
            button4.Visible = false;
            button5.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            label1.Text = "";
            pictureBox1.Image = null;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            label1.Visible = true;
            button1.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button1.Text = "Edit";
            List<Item> items = dtb.RetrieveSpecific(id);  

            if (items != null && items.Count > 0)
            {
                Item item = items[0]; 
                textBox1.Text = item.name;
                textBox2.Text = item.url;
                byte[] imageBytes = item.img;
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image itemImage = Image.FromStream(ms);
                        pictureBox1.Image = itemImage;

                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
                pictureBox1.Visible = true;
            }
            else
            {
                MessageBox.Show("Item not found!");
            }

            LoadImages();


        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dtb.Delete(id);
            LoadImages();
            panel1.Visible = false;
        }
    }
}
