using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        

        
        
            public String id { get; set; }
            public String bookName { get; set; }
            public String author { get; set; }
            public String publishYear { get; set; }
            public String originalLanguage { get; set; }
            public String genre { get; set; }
            public String coverImage { get; set; }

            
        
    }
}
