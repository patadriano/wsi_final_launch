using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Data;

namespace WSI_Launch
{
    public class Database
    {
        string connectionString = ConfigurationManager.ConnectionStrings["AppLaunch"].ConnectionString;

        public void InsertItems(Item item)
        {
            string query = "INSERT INTO Websites (Name, Url, Image) VALUES (@Name, @Url, @Image)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", item.name);
                    command.Parameters.AddWithValue("@Url", item.url);
                    command.Parameters.AddWithValue("@Image", item.img);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record inserted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Insert failed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        public void UpdateItems(Item item)
        {
            string query = "UPDATE Websites SET Name = @Name, Url = @Url,  Image = @Image WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.id);
                    command.Parameters.AddWithValue("@Name", item.name);
                    command.Parameters.AddWithValue("@Url", item.url);
                    command.Parameters.AddWithValue("@Image", item.img);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record inserted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Insert failed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        public List<Item> GetItems()
        {
            List<Item> items = new List<Item>();
            string query = "SELECT * FROM Websites";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] imageBytes = reader["Image"] as byte[];

                                if (imageBytes != null)
                                {
                                    Item item = new Item
                                    {
                                        id = (int)reader["Id"],
                                        name = reader["Name"].ToString(),
                                        url = reader["Url"].ToString(),
                                        img = imageBytes
                                    };
                                    items.Add(item);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                return items;
            }
        }
        public List<Item> RetrieveSpecific(int id)
        {
            List<Item> items = new List<Item>();
            string query = "SELECT * FROM Websites where Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] imageBytes = reader["Image"] as byte[];

                                if (imageBytes != null)
                                {
                                    Item item = new Item
                                    {
                                        id = (int)reader["Id"],
                                        name = reader["Name"].ToString(),
                                        url = reader["Url"].ToString(),
                                        img = imageBytes
                                    };
                                    items.Add(item);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                return items;
            }
        }

        public void Delete(int id)
        {
            List<Item> items = new List<Item>();
            string query = "Delete FROM Websites where Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }

        }
    }
}
