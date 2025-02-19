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

namespace WSI_Launch
{
    public partial class Form2 : Form
    {
        Database dtb = new Database();
        byte[] imageBytes;
        public Form2()
        {
            InitializeComponent();
            GetImages();
            
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
                    pictureBox1.Image = itemImage;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog.FileName);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
                imageBytes = ms.ToArray();

                img.Dispose();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newItem = new Item
            {
                name = textBox1.Text,
                url = textBox2.Text,
                img = imageBytes
            };

            dtb.InsertItems(newItem);
        }
    }
}
