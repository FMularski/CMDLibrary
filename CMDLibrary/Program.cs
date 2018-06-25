using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLibrary
{
    class Program
    {
        enum FormMode
        {
            New,
            Edit
        }

        static void Main(string[] args)
        {
            LibraryContext Context = new LibraryContext();

            //RemoveBook(Context);
            //AddNewBook(Context);
            //EditBook(Context);
            //RegisterUser(Context);
            LogIn(Context);
        }

        static Book BookForm(LibraryContext Context, FormMode mode)
        {
            Book book = new Book();

            string ISBN;
            bool uniqueISBN;

            if ( mode == FormMode.New)
                do
                {
                    Console.Write( "Enter ISBN of the new book: ");
                    ISBN = Console.ReadLine();
                    uniqueISBN = true;

                    foreach (var b in Context.Books)
                        if ( b.ISBN == ISBN)
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

                } while (!uniqueISBN || string.IsNullOrEmpty(ISBN));


            string title;

            do
            {
                Console.Write( mode == FormMode.New ? "Enter the title of the new book: " : "Enter new title of the editted book: ");
                title = Console.ReadLine();

                if ( string.IsNullOrEmpty(title))
                    Console.WriteLine("Error: Title is required! Try again.");
                
            } while (string.IsNullOrEmpty(title));

            book.Title = title;


            Console.Write( mode == FormMode.New ? "Enter the description of the new book: " : "Enter new description of the editted book: ");
            book.Description = Console.ReadLine();

            string authorsFirstName, authorsLastName;

            do
            {
                Console.Write( mode == FormMode.New ? "Enter author's firstname:" : "Enter new author's firstname: ");
                authorsFirstName = Console.ReadLine();

                if ( string.IsNullOrEmpty(authorsFirstName))
                    Console.WriteLine("Error: Author's firstname is required! Try again.");

            } while ( string.IsNullOrEmpty(authorsFirstName));

            do
            {
                Console.Write(mode == FormMode.New ? "Enter author's lastname:" : "Enter new author's lastname: ");
                authorsLastName = Console.ReadLine();

                if (string.IsNullOrEmpty(authorsLastName))
                    Console.WriteLine("Error: Author's lastname is required! Try again.");

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

            Console.WriteLine( mode == FormMode.New ? "Select genre of the new book:" : "Select new genre of the editted book:");
            Console.WriteLine("[1] Tragedy\n[2] Tragic comedy\n[3] Fantasy");
            Console.WriteLine("[4] Mythology\n[5] Adventure\n[6] Mystery");
            Console.WriteLine("[7] Science fiction\n[8] Drama\n[9] Romance");
            Console.Write("[10] Action / Adventure\n[11] Satire\n[12] Horror\n> ");

            int genreId;

            while (!int.TryParse(Console.ReadLine(), out genreId) || genreId < 1 || genreId > 12)
                Console.Write("Error: Invalid input.\nSelect a proper value.\n> ");

            book.GenreId = genreId;

            return book;
        }

        static void AddNewBook(LibraryContext context)
        {
            Book bookToAdd = BookForm(context, FormMode.New);
            context.Books.Add(bookToAdd);
            context.SaveChanges();

            Console.WriteLine("The book has been successfully added to the library.");
        }

        static void RemoveBook( LibraryContext Context)
        {
            foreach( var b in Context.Books.ToList())
            {
                Author author = Context.Authors.Single(a => a.Id == b.AuthorId);
                Console.WriteLine(b.ISBN + "\t\t| \"" + b.Title + "\" " + author.FirstName + " " + author.LastName);
            }

            Book booktoRemove;

            do
            {
                Console.Write("\nEnter ISBN of the book you want to remove: ");
                string ISBN = Console.ReadLine();

                booktoRemove = Context.Books.SingleOrDefault(b => b.ISBN == ISBN);

                if ( booktoRemove == null)
                    Console.WriteLine("Error: Invalid ISBN: " + ISBN + ". Try again.");

            } while (booktoRemove == null);

            Console.Write("Are you sure you want to remove \"" + booktoRemove.Title + "\"?\n[Y] Yes\t[N] No\n> ");
            if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")
            {
                Context.Books.Remove(booktoRemove);
                Console.WriteLine("\n\"" + booktoRemove.Title + "\" has been removed successfully.");
                Context.SaveChanges();
            }
            else
                Console.WriteLine("\nRemoval cancelled.");
        }

        static void EditBook( LibraryContext context)
        {
            foreach (var b in context.Books.ToList())
            {
                Author author = context.Authors.Single(a => a.Id == b.AuthorId);
                Console.WriteLine(b.ISBN + "\t\t| \"" + b.Title + "\" " + author.FirstName + " " + author.LastName);
            }

            Book bookToEdit;

            do
            {
                Console.Write("\nEnter ISBN of the book you want to edit: ");
                string ISBN = Console.ReadLine();

                bookToEdit = context.Books.SingleOrDefault(b => b.ISBN == ISBN);

                if (bookToEdit == null)
                    Console.WriteLine("Error: Invalid ISBN: " + ISBN + ". Try again.");

            } while (bookToEdit == null);

            Console.WriteLine("You are editting \"" + bookToEdit.Title + "\".\n");

            Book edittedBook = BookForm(context, FormMode.Edit);

            string oldTitle = bookToEdit.Title;
            bookToEdit.Title = edittedBook.Title;
            bookToEdit.Description = edittedBook.Description;
            bookToEdit.AuthorId = edittedBook.AuthorId;
            bookToEdit.GenreId = edittedBook.GenreId;

            Console.Write("Are you sure you want to apply changes for \"" + oldTitle + "\"?\n[Y] Yes\t[N] No\n> ");
            if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")
            {
                Console.WriteLine("\nChanges to \"" + oldTitle + "\" have been applied successfully.");
                context.SaveChanges();
            }
            else
                Console.WriteLine("\nEdittion cancelled.");
        }

        static void RegisterUser( LibraryContext context)
        {
            string login, pass, passConfirm;
            bool loginAvailable;

            do
            {
                loginAvailable = true;

                Console.Write("Enter login: ");
                login = Console.ReadLine();

                if (string.IsNullOrEmpty(login))
                {
                    Console.WriteLine("Error: Login is required! Try again.");
                    continue;
                }

                foreach (var user in context.Users)
                    if ( user.Login == login)
                    {
                        loginAvailable = false;
                        Console.WriteLine("Login \"" + login + "\" is already taken. Use different one.");
                    }

            } while (string.IsNullOrEmpty(login) || !loginAvailable);

            do
            {
                Console.Write("Enter password: ");
                pass = Console.ReadLine();

                if (string.IsNullOrEmpty(pass))
                    Console.WriteLine("Error: Password is required! Try again.");

            } while (string.IsNullOrEmpty(pass));

            do
            {
                Console.Write("Confirm password: ");
                passConfirm = Console.ReadLine();

                if (string.IsNullOrEmpty(passConfirm) || pass != passConfirm)
                    Console.WriteLine("Error: Password confirmation differs from password. Try again.");

            } while (string.IsNullOrEmpty(login) || pass != passConfirm);

            context.Users.Add(new User
            {
                Login = login,
                Password = pass,
                Role = User.RoleType.Regular
            });

            context.SaveChanges();
            Console.WriteLine("Account has been registered successfully.");
        }

        static bool LogIn( LibraryContext context)
        {
            string login, pass;
            bool match;

            Console.WriteLine("--== LOG IN ==--");

            do
            {
                match = false;

                do
                {
                    Console.Write("Login: ");
                    login = Console.ReadLine();

                    if (string.IsNullOrEmpty(login))
                        Console.WriteLine("Error: Login is required! Try again.");

                } while (string.IsNullOrEmpty(login));

                do
                {
                    Console.Write("Password: ");
                    pass = Console.ReadLine();

                    if (string.IsNullOrEmpty(pass))
                        Console.WriteLine("Error: Password is required! Try again.");

                } while (string.IsNullOrEmpty(pass));



                foreach (var user in context.Users)
                {
                    if (user.Login == login && user.Password == pass)
                    {
                        match = true;
                        break;
                    }
                }

                if( !match)
                {
                    Console.Write("Error: Login or password is incorrect.\nDo you want to try again?\n[Y]\t[N] No\n> ");
                    if (Console.ReadKey().KeyChar.ToString().ToLower() == "n") break;
                    else Console.WriteLine();
                }

               
            } while (!match);

            if (match)
            {
                Console.WriteLine("\nLogged in.");
                return true;
            }
            else
            {
                Console.WriteLine("\nLogging in cancelled.");
                return false;
            }
        }
    }


}
