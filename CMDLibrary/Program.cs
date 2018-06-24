using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryContext Context = new LibraryContext();

            AddNewBook(Context);
        }

        static void AddNewBook(LibraryContext Context)
        {
            Book book = new Book();

            for(; ;)
            {
                Console.Write("Enter ISBN of the new book: ");

                string ISBN = Console.ReadLine();
                bool uniqueISBN = true;

                foreach (var b in Context.Books)
                    if (ISBN == b.ISBN)
                    {
                        uniqueISBN = false;
                        break;
                    }

                if (uniqueISBN && !string.IsNullOrEmpty(ISBN))
                {
                    book.ISBN = ISBN;
                    break;
                }
                else
                    Console.WriteLine("Error: Entered ISBN is invalid or already taken. Try again.");
            }

            string title;

            do
            {
                Console.Write("Enter the title of the new book: ");
                title = Console.ReadLine();

                if ( string.IsNullOrEmpty(title))
                    Console.WriteLine("Error: Title is required! Try again.");
                
            } while (string.IsNullOrEmpty(title));

            book.Title = title;


            Console.Write("Enter the description of the new book: ");
            book.Description = Console.ReadLine();

            string authorsFirstName, authorsLastName;

            do
            {
                Console.Write("Enter author's firstname:");
                authorsFirstName = Console.ReadLine();

                if ( string.IsNullOrEmpty(authorsFirstName))
                    Console.WriteLine("Erroe: Author's firstname is required! Try again.");

            } while ( string.IsNullOrEmpty(authorsFirstName));

            do
            {
                Console.Write("Enter author's lastname:");
                authorsLastName = Console.ReadLine();

                if (string.IsNullOrEmpty(authorsLastName))
                    Console.WriteLine("Erroe: Author's lastname is required! Try again.");

            } while (string.IsNullOrEmpty(authorsLastName));


            Author author = Context.Authors.SingleOrDefault(a => a.FirstName == authorsFirstName && a.LastName == authorsLastName);

            if (author == null)
            {
                author = new Author
                {
                    FirstName = authorsFirstName,
                    LastName = authorsLastName
                };

                Context.Authors.Add(author);
                Context.SaveChanges();
            }

            book.AuthorId = author.Id;

            Console.Write("Select genre of the new book:\n");
            Console.WriteLine("[1] Tragedy\n[2] Tragic comedy\n[3] Fantasy");
            Console.WriteLine("[4] Mythology\n[5] Adventure\n[6] Mystery");
            Console.WriteLine("[7] Science fiction\n[8] Drama\n[9] Romance");
            Console.Write("[10] Action / Adventure\n[11] Satire\n[12] Horror\n> ");

            int genreId = new int();

            while (!int.TryParse(Console.ReadLine(), out genreId) || genreId < 1 || genreId > 12)
                Console.Write("Error: Invalid input.\nSelect a proper value.\n> ");
            book.GenreId = genreId;

            Context.Books.Add(book);
            Context.SaveChanges();

            Console.WriteLine("The book has been successfully added to the library.");
        }
    }
}
