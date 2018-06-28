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

        enum Selection : byte
        {
            ListOfBooks,
            Register,
            LogIn,
            LogOut,
            MyAccount,
            MyBooks,
            AddBook,
            EditBook,
            RemoveBook,
            RentBook,
            Exit
        }

        static void Main(string[] args)
        {
            LibraryContext Context = new LibraryContext();
            while (MainMenu(Context) != (byte)Selection.Exit) ;
        }

        static Book BookForm(LibraryContext Context, FormMode mode)
        {
            Book book = new Book();

            string ISBN;
            bool uniqueISBN;

            if (mode == FormMode.New)
                do
                {
                    Console.Write("Enter ISBN of the new book: ");
                    ISBN = Console.ReadLine();
                    uniqueISBN = true;

                    foreach (var b in Context.Books)
                        if (b.ISBN == ISBN)
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
                Console.Write(mode == FormMode.New ? "Enter the title of the new book: " : "Enter new title of the editted book: ");
                title = Console.ReadLine();

                if (string.IsNullOrEmpty(title))
                    Console.WriteLine("Error: Title is required! Try again.");

            } while (string.IsNullOrEmpty(title));

            book.Title = title;


            Console.Write(mode == FormMode.New ? "Enter the description of the new book: " : "Enter new description of the editted book: ");
            book.Description = Console.ReadLine();

            string authorsFirstName, authorsLastName;

            do
            {
                Console.Write(mode == FormMode.New ? "Enter author's firstname:" : "Enter new author's firstname: ");
                authorsFirstName = Console.ReadLine();

                if (string.IsNullOrEmpty(authorsFirstName))
                    Console.WriteLine("Error: Author's firstname is required! Try again.");

            } while (string.IsNullOrEmpty(authorsFirstName));

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

            Console.WriteLine(mode == FormMode.New ? "Select genre of the new book:" : "Select new genre of the editted book:");
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
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static void RemoveBook(LibraryContext Context)
        {
            foreach (var b in Context.Books.ToList())
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

                if (booktoRemove == null)
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

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static void EditBook(LibraryContext context)
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

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static void ListOfBooks(LibraryContext context)
        {
            Console.WriteLine("╔═══════════════╗");
            Console.WriteLine("║ LIST OF BOOKS ║");
            Console.WriteLine("╚═══════════════╝");

            foreach (var b in context.Books.ToList())
            {
                Author author = context.Authors.Single(a => a.Id == b.AuthorId);
                Console.Write(b.ISBN + "\t\t| \"" + b.Title + "\" " + author.FirstName + " " + author.LastName + "\t\tAvailable: ");
                Console.WriteLine(b.Rented ? "No" : "Yes");
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static void RegisterUser(LibraryContext context)
        {
            Console.WriteLine("╔══════════════╗");
            Console.WriteLine("║ REGISTRATION ║");
            Console.WriteLine("╚══════════════╝");

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
                    if (user.Login == login)
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
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static User LogIn(LibraryContext context)
        {
            Console.WriteLine("╔════════════╗");
            Console.WriteLine("║ LOGGING IN ║");
            Console.WriteLine("╚════════════╝");

            string login, pass;
            User loggedUser = null;

            do
            {
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
                        loggedUser = user;
                        break;
                    }
                }

                if (loggedUser == null)
                {
                    Console.Write("Error: Login or password is incorrect.\nDo you want to try again?\n[Y]\t[N] No\n> ");
                    if (Console.ReadKey().KeyChar.ToString().ToLower() == "n") break;
                    else Console.WriteLine();
                }


            } while (loggedUser == null);

            if (loggedUser != null)
            {
                Console.WriteLine("\nLogged in.");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
                Console.Clear();
                return loggedUser;
            }
            else
            {
                Console.WriteLine("\nLogging in cancelled.");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
                Console.Clear();
                return null;
            }
        }

        static void PrintMainScreen()
        {
            Console.WriteLine("╔═════════════╗");
            Console.WriteLine("║ CMD LIBRARY ║");
            Console.WriteLine("╚═════════════╝");
            Console.WriteLine("[1] List of books");
            Console.WriteLine("[2] Register");
            Console.WriteLine("[3] Log in");
            Console.WriteLine("[4] Exit");
        }

        static byte MainMenu(LibraryContext context)
        {
            PrintMainScreen();

            switch (MainScreenChoice(4))
            {
                case 1:
                    ListOfBooks(context);
                    return (byte)Selection.ListOfBooks;
                case 2:
                    RegisterUser(context);
                    return (byte)Selection.Register;
                case 3:
                    User loggedUser = LogIn(context);
                    if (loggedUser != null)
                    {
                        if ( loggedUser.Role == User.RoleType.Regular)
                            while (LoggedInMainMenu(loggedUser, context) != (byte)Selection.LogOut) ;

                        if (loggedUser.Role == User.RoleType.Administrator)
                            while (AdminMainMenu(loggedUser, context) != (byte)Selection.LogOut) ;
                    }
                    return (byte)Selection.LogIn;
                case 4:
                    return (byte)Selection.Exit;
                default:
                    return 0;
            }

        }

        static int MainScreenChoice( int choiceCounter)
        {
            int choice;

            Console.Write("> ");

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > choiceCounter)
            {
                Console.WriteLine("Error: Invalid input. Try again.");
                Console.Write("> ");
            }

            Console.Clear();

            return choice;
        }

        static void LoggedUserScreen(User loggedUser)
        {
            Console.WriteLine("╔═════════════╗");
            Console.WriteLine("║ CMD LIBRARY ║\t\t\tLogged in as: " + loggedUser.Login);
            Console.WriteLine("╚═════════════╝");
            Console.WriteLine("[1] List of books");
            Console.WriteLine("[2] My account");
            Console.WriteLine("[3] My books");
            Console.WriteLine("[4] Log out");
        }

        static byte LoggedInMainMenu(User loggedUser, LibraryContext context)
        {
            LoggedUserScreen(loggedUser);

            switch (MainScreenChoice(4))
            {
                case 1:
                    ListOfBooks(context);
                    return (byte)Selection.ListOfBooks;
                case 2:
                    MyAccount(loggedUser);
                    return (byte)Selection.MyAccount;
                case 3:
                    MyBooks(loggedUser, context);
                    return (byte)Selection.MyBooks;
                case 4:
                    return (byte)Selection.LogOut;
                default:
                    return 0;
            }
        }

        static void MyAccount(User loggedUser)
        {
            Console.WriteLine("╔════════════╗");
            Console.WriteLine("║ MY ACCOUNT ║");
            Console.WriteLine("╚════════════╝");

            Console.WriteLine("User ID: " + loggedUser.Id);
            Console.WriteLine("Login: " + loggedUser.Login);
            Console.WriteLine("Password: " + loggedUser.Password);
            Console.WriteLine(loggedUser.Role == User.RoleType.Regular ? "Role: Regular user" : "Role: Administrator");

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static void MyBooks(User loggedUser, LibraryContext context)
        {
            Console.WriteLine("╔══════════╗");
            Console.WriteLine("║ MY BOOKS ║");
            Console.WriteLine("╚══════════╝");

            if ( loggedUser.RentedBooks == null)
            {
                Console.WriteLine("You don't have any rented books.");
                return;
            }

            foreach (var b in loggedUser.RentedBooks.ToList())
            {
                Author author = context.Authors.Single(a => a.Id == b.AuthorId);
                Console.WriteLine(b.ISBN + "\t\t| \"" + b.Title + "\" " + author.FirstName + " " + author.LastName);
            }
        }

        static void AdminScreen(User loggedUser)
        {
            Console.WriteLine("╔═════════════╗");
            Console.WriteLine("║ CMD LIBRARY ║\t\t\tLogged in as: " + loggedUser.Login);
            Console.WriteLine("╚═════════════╝");
            Console.WriteLine("[1] List of books");
            Console.WriteLine("[2] My account");
            Console.WriteLine("[3] Add a new book");
            Console.WriteLine("[4] Edit a book");
            Console.WriteLine("[5] Remove a book");
            Console.WriteLine("[6] Rent a book");
            Console.WriteLine("[7] Log out");
        }

        static byte AdminMainMenu(User loggedUser, LibraryContext context)
        {
            AdminScreen(loggedUser);

            switch (MainScreenChoice(7))
            {
                case 1:
                    ListOfBooks(context);
                    return (byte)Selection.ListOfBooks;
                case 2:
                    MyAccount(loggedUser);
                    return (byte)Selection.MyAccount;
                case 3:
                    AddNewBook(context);
                    return (byte)Selection.AddBook;
                case 4:
                    EditBook(context);
                    return (byte)Selection.EditBook;
                case 5:
                    RemoveBook(context);
                    return (byte)Selection.RemoveBook;
                case 6:
                    //RentBook(context);
                    return (byte)Selection.RentBook;
                case 7:
                    return (byte)Selection.LogOut;
                default:
                    return 0;
            }
        }
    }
}
