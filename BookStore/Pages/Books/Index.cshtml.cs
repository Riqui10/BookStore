using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List<Book> listBooks = new List<Book>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        public void OnGet()
        {
            
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bookStore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM books";
                    if(!string.IsNullOrEmpty(SearchTerm))
                    {
                        sql += " WHERE bookName LIKE @SearchTerm OR author LIKE @SearchTerm";
                    }
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (!string.IsNullOrEmpty(SearchTerm))
                        {
                            command.Parameters.AddWithValue("@SearchTerm", "%" + SearchTerm + "%");
                        }
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

    /*public class BookInfo
    {
        public String id;
        public String bookName;
        public String author;
        public String publishYear;
        public String originalLanguage;
        public String genre;
        public String coverImage;
    }*/
}