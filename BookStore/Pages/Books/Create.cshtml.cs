using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BookStore.Pages.Books
{
    public class CreateModel : PageModel
    {
        public Book bookInfo = new Book();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        { 
            bookInfo.bookName = Request.Form["name"];
            bookInfo.author = Request.Form["author"];
            bookInfo.publishYear = Request.Form["year"];
            bookInfo.originalLanguage = Request.Form["language"];
            bookInfo.genre = Request.Form["genre"];
            bookInfo.coverImage = Request.Form["image"];
            

            if (bookInfo.bookName.Length==0 || bookInfo.author.Length == 0 ||
                bookInfo.publishYear.Length == 0 || bookInfo.originalLanguage.Length == 0
                || bookInfo.genre.Length == 0 || bookInfo.coverImage.Length ==0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save data into database

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=bookStore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO books " +
                                 "(bookName, author, publishYear, originalLanguage, genre, coverImage) VALUES " +
                                 "(@bookName, @author, @publishYear, @originalLanguage, @genre, @coverImage);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@bookName", bookInfo.bookName);
                        command.Parameters.AddWithValue("@author", bookInfo.author);
                        command.Parameters.AddWithValue("@publishYear", bookInfo.publishYear);
                        command.Parameters.AddWithValue("@originalLanguage", bookInfo.originalLanguage);
                        command.Parameters.AddWithValue("@genre", bookInfo.genre);
                        command.Parameters.AddWithValue("@coverImage", bookInfo.coverImage);
                        

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            bookInfo.bookName = "";
            bookInfo.author = "";
            bookInfo.publishYear = "";
            bookInfo.originalLanguage = "";
            bookInfo.genre = "";
            bookInfo.coverImage = "";
            
            successMessage = "New book Added Correctly";

            Response.Redirect("/Books/Index");
        }

    }
}
