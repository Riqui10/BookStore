using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BookStore.Pages.Books
{
    public class EditModel : PageModel
    {
        public Book bookInfo = new Book();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bookStore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM books WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                bookInfo.id = "" + reader.GetInt32(0);
                                bookInfo.bookName = reader.GetString(1);
                                bookInfo.author = reader.GetString(2);
                                bookInfo.publishYear = reader.GetDateTime(3).Year.ToString();
                                bookInfo.originalLanguage = reader.GetString(4);
                                bookInfo.genre = reader.GetString(5);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            bookInfo.id = Request.Form["id"];
            bookInfo.bookName = Request.Form["name"];
            bookInfo.author = Request.Form["author"];
            bookInfo.publishYear = Request.Form["year"];
            bookInfo.originalLanguage = Request.Form["language"];
            bookInfo.genre = Request.Form["genre"];
            

            if (bookInfo.bookName.Length == 0 || bookInfo.author.Length == 0 ||
                bookInfo.publishYear.Length == 0 || bookInfo.originalLanguage.Length == 0
                || bookInfo.genre.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bookStore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE books " +
             "SET bookName=@bookName, author=@author, publishYear=@publishYear, originalLanguage=@originalLanguage, genre=@genre " +
             "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@bookName", bookInfo.bookName);
                        command.Parameters.AddWithValue("@author", bookInfo.author);
                        command.Parameters.AddWithValue("@publishYear", bookInfo.publishYear);
                        command.Parameters.AddWithValue("@originalLanguage", bookInfo.originalLanguage);
                        command.Parameters.AddWithValue("@genre", bookInfo.genre);
                        
                        command.Parameters.AddWithValue("@id", bookInfo.id);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Books/Index");

        }

    }
}
