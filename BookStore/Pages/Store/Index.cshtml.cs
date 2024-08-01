using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace BookStore.Pages.Books
{
    public class StoreModel : PageModel
    {
        public List<Book> listBooks = new List<Book>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bookStore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM books";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book();
                                book.id = "" + reader.GetInt32(0);
                                book.bookName = reader.GetString(1);
                                book.author = reader.GetString(2);
                                book.publishYear = reader.GetDateTime(3).Year.ToString();
                                book.originalLanguage = reader.GetString(4);
                                book.genre = reader.GetString(5);
                                book.coverImage = reader.GetString(6);
                                listBooks.Add(book);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }


    }

    public class StoreInfo
    {
        public String id;
        public String bookName;
        public String author;
        public String publishYear;
        public String originalLanguage;
        public String genre;
        public String coverImage;

        
    }
}
