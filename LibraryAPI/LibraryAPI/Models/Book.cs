using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryAPI.Models
{
    public partial class Book
    {
        public Book()
        {
            ReadBooks = new HashSet<ReadBook>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double? Rating { get; set; }
        public string ImageUrl { get; set; }
        public string PdfUrl { get; set; }

        public virtual ICollection<ReadBook> ReadBooks { get; set; }
    }
}
