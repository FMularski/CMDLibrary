using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLibrary
{
    class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description  { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public bool Rented { get; set; }
        public User UserWhoRented { get; set; }
        public int? UserWhoRentedId { get; set; }
    }
}
