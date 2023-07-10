using MovieDatabase.Databases;
using MovieDatabase.Movies;

namespace MovieDatabase.App
{
    public static class App
    {
        private enum ActionType
        {
            None,
            Add,
            Remove,
            EditMovie,
            EditStatus,
            Purchase,
            DisplayDatabase,
            DisplayLibrary,
            DisplayStats,
            Exit
        }
        
        private enum MovieActionType
        {
            None,
            Title,
            Year,
            Director,
            Genre,
            Length,
            Price,
            Status
        }

        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("MOVIE DATABASE\n");
                Console.WriteLine("1. Add movie to database.\n" +
                                  "2. Delete movie from database.\n" +
                                  "3. Edit movie info.\n" +
                                  "4. Edit movie watch status.\n" +
                                  "5. Purchase a movie.\n" +
                                  "6. Display movie database.\n" +
                                  "7. Display your library.\n" +
                                  "8. Display movie statistics.\n" +
                                  "9. Exit the program.\n");
                Console.Write("What do you want to do?: ");
                try
                {
                    var action = (ActionType)int.Parse(Console.ReadLine() ?? string.Empty);
                    if (action == ActionType.Exit) Environment.Exit(0);
                    Console.Clear();
                    switch (action)
                    {
                        case ActionType.Add:
                            AddMovie();
                            break;
                        case ActionType.Remove:
                            RemoveMovie();
                            break;
                        case ActionType.EditMovie:
                            EditMovie();
                            break;
                        case ActionType.EditStatus:
                            EditStatus();
                            break;
                        case ActionType.Purchase:
                            PurchaseMovie();
                            break;
                        case ActionType.DisplayDatabase:
                            DisplayDatabase();
                            DisplayFilterMenu(true);
                            break;
                        case ActionType.DisplayLibrary:
                            DisplayLibrary();
                            DisplayFilterMenu(false);
                            break;
                        case ActionType.DisplayStats:
                            DisplayStats();
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                finally
                {
                    Console.Write("\nPress any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void AddMovie()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine() ?? string.Empty;
            Console.Write("Year of release: ");
            int year = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write("Director: ");
            string director = Console.ReadLine() ?? string.Empty;
            Console.Write("Genre: ");
            string genre = Console.ReadLine() ?? string.Empty;
            Console.Write("Movie length (in minutes): ");
            int length = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write("Price: ");
            float price = float.Parse(Console.ReadLine() ?? string.Empty);
            Database.AddMovie(title, year, director, genre, length, price);
        }

        private static void RemoveMovie()
        {
            foreach (KeyValuePair<int, Movie> movie in Database.SortedDatabase) Console.WriteLine($"{movie.Key}: {movie.Value.Title}");
            Console.Write("Type ID of movie you want to remove: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Database.RemoveMovie(id);
        }

        private static void EditMovie()
        {
            foreach (KeyValuePair<int, Movie> movie in Database.SortedDatabase) Console.WriteLine($"{movie.Key}: {movie.Value.Title}");
            Console.Write("Type ID of movie you want to edit: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("\n1. Title\n" +
                              "2. Year of premiere\n" +
                              "3. Director\n" +
                              "4. Genre\n" +
                              "5. Length of movie\n" +
                              "6. Price\n");
            Console.Write("What do you want to change in this movie?: ");
            var editAction = (MovieActionType)int.Parse(Console.ReadLine() ?? string.Empty);
            switch (editAction)
            {
                case MovieActionType.Title:
                    Console.Write("Enter new title: ");
                    string title = Console.ReadLine() ?? string.Empty;
                    Database.EditMovieTitle(id, title);
                    break;
                case MovieActionType.Year:
                    Console.Write("Enter new year of premiere: ");
                    int year = int.Parse(Console.ReadLine() ?? string.Empty);
                    Database.EditMovieYear(id, year);
                    break;
                case MovieActionType.Director:
                    Console.Write("Enter new director: ");
                    string director = Console.ReadLine() ?? string.Empty;
                    Database.EditMovieDirector(id, director);
                    break;
                case MovieActionType.Genre:
                    Console.Write("Enter new genre: ");
                    string genre = Console.ReadLine() ?? string.Empty;
                    Database.EditMovieGenre(id, genre);
                    break;
                case MovieActionType.Length:
                    Console.Write("Enter new movie length (in minutes): ");
                    int length = int.Parse(Console.ReadLine() ?? string.Empty);
                    Database.EditMovieLength(id, length);
                    break;
                case MovieActionType.Price:
                    Console.Write("Enter new price: ");
                    float price = float.Parse(Console.ReadLine() ?? string.Empty);
                    Database.EditMoviePrice(id, price);
                    break;
            }
        }

        private static void EditStatus()
        {
            foreach (KeyValuePair<int, PurchasedMovie> movie in Database.SortedLibrary) Console.WriteLine($"{movie.Key}: {movie.Value.Title}");
            Console.Write("Type ID of movie which status you want to edit: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("\n1. Not watched\n" +
                              "2. In progress\n" +
                              "3. Watched\n");
            Console.Write("Choose new status: ");
            var statusAction = (PurchasedMovie.StatusType)int.Parse(Console.ReadLine() ?? string.Empty);
            Database.EditMovieStatus(id, statusAction);
        }

        private static void PurchaseMovie()
        {
            foreach (KeyValuePair<int, Movie> movie in Database.SortedDatabase) Console.WriteLine($"{movie.Key}: {movie.Value.Title}, {movie.Value.Price}");
            Console.Write("Type ID of movie you want to buy: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Database.PurchaseMovie(id);
        }

        private static void DisplayFilterMenu(bool database)
        {
            Console.WriteLine("1. Title\n" +
                              "2. Year of premiere\n" +
                              "3. Director\n" +
                              "4. Genre\n" +
                              "5. Length of movie\n" +
                              "6. Price\n" +
                              "7. Status\n");
            Console.Write("How do you want to filter movies?: ");
            var movieAction = (MovieActionType)int.Parse(Console.ReadLine() ?? string.Empty);
            switch (movieAction)
            {
                case MovieActionType.Title:
                    FilterByTitle(database);
                    break;
                case MovieActionType.Year:
                    FilterByYear(database);
                    break;
                case MovieActionType.Director:
                    FilterByDirector(database);
                    break;
                case MovieActionType.Genre:
                    FilterByGenre(database);
                    break;
                case MovieActionType.Length:
                    FilterByLength(database);
                    break;
                case MovieActionType.Price:
                    FilterByPrice(database);
                    break;
                case MovieActionType.Status:
                    if (database) break;
                    FilterByStatus();
                    break;
            }
        }

        private static void FilterByTitle(bool database)
        {
            Console.Write("Type title: ");
            string title = Console.ReadLine() ?? string.Empty;
            Console.Clear();
            if (database)
            {
                foreach (KeyValuePair<int, Movie> movie in Database.FilterDatabaseByTitle(title))
                {
                    Console.WriteLine(MovieFromDatabaseToString(movie.Key, movie.Value));
                }
            }
            else
            {
                foreach (KeyValuePair<int, PurchasedMovie> movie in Database.FilterLibraryByTitle(title))
                {
                    Console.WriteLine(MovieFromLibraryToString(movie.Key, movie.Value));
                }
            }
        }

        private static void FilterByYear(bool database)
        {
            Console.Write("Type year's lower bound: ");
            int yearMin = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write("Type year's upper bound: ");
            int yearMax = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Clear();
            if (database)
            {
                foreach (KeyValuePair<int, Movie> movie in Database.FilterDatabaseByYear(yearMin, yearMax))
                {
                    Console.WriteLine(MovieFromDatabaseToString(movie.Key, movie.Value));
                }
            }
            else
            {
                foreach (KeyValuePair<int, PurchasedMovie> movie in Database.FilterLibraryByYear(yearMin, yearMax))
                {
                    Console.WriteLine(MovieFromLibraryToString(movie.Key, movie.Value));
                }
            }
        }

        private static void FilterByDirector(bool database)
        {
            Console.Write("Type director: ");
            string director = Console.ReadLine() ?? string.Empty;
            Console.Clear();
            if (database)
            {
                foreach (KeyValuePair<int, Movie> movie in Database.FilterDatabaseByDirector(director))
                {
                    Console.WriteLine(MovieFromDatabaseToString(movie.Key, movie.Value));
                }
            }
            else
            {
                foreach (KeyValuePair<int, PurchasedMovie> movie in Database.FilterLibraryByDirector(director))
                {
                    Console.WriteLine(MovieFromLibraryToString(movie.Key, movie.Value));
                }
            }
        }

        private static void FilterByGenre(bool database)
        {
            Console.Write("Type genre: ");
            string genre = Console.ReadLine() ?? string.Empty;
            Console.Clear();
            if (database)
            {
                foreach (KeyValuePair<int, Movie> movie in Database.FilterDatabaseByGenre(genre))
                {
                    Console.WriteLine(MovieFromDatabaseToString(movie.Key, movie.Value));
                }
            }
            else
            {
                foreach (KeyValuePair<int, PurchasedMovie> movie in Database.FilterLibraryByGenre(genre))
                {
                    Console.WriteLine(MovieFromLibraryToString(movie.Key, movie.Value));
                }
            }
        }

        private static void FilterByLength(bool database)
        {
            Console.Write("Type length's lower bound: ");
            int lengthMin = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write("Type length's upper bound: ");
            int lengthMax = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Clear();
            if (database)
            {
                foreach (KeyValuePair<int, Movie> movie in Database.FilterDatabaseByLength(lengthMin, lengthMax))
                {
                    Console.WriteLine(MovieFromDatabaseToString(movie.Key, movie.Value));
                }
            }
            else
            {
                foreach (KeyValuePair<int, PurchasedMovie> movie in Database.FilterLibraryByLength(lengthMin, lengthMax))
                {
                    Console.WriteLine(MovieFromLibraryToString(movie.Key, movie.Value));
                }
            }
        }

        private static void FilterByPrice(bool database)
        {
            Console.Write("Type price's lower bound: ");
            float priceMin = float.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write("Type price's upper bound: ");
            float priceMax = float.Parse(Console.ReadLine() ?? string.Empty);
            Console.Clear();
            if (database)
            {
                foreach (KeyValuePair<int, Movie> movie in Database.FilterDatabaseByPrice(priceMin, priceMax))
                {
                    Console.WriteLine(MovieFromDatabaseToString(movie.Key, movie.Value));
                }
            }
            else
            {
                foreach (KeyValuePair<int, PurchasedMovie> movie in Database.FilterLibraryByPrice(priceMin, priceMax))
                {
                    Console.WriteLine(MovieFromLibraryToString(movie.Key, movie.Value));
                }
            }
        }

        private static void FilterByStatus()
        {
            Console.WriteLine("\n1. Not watched\n" +
                              "2. In progress\n" +
                              "3. Watched\n");
            Console.Write("Choose status: ");
            var statusAction = (PurchasedMovie.StatusType)int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Clear();
            foreach (KeyValuePair<int, PurchasedMovie> movie in Database.FilterLibraryByStatus(statusAction))
            {
                Console.WriteLine(MovieFromLibraryToString(movie.Key, movie.Value));
            }
        }

        private static void DisplayDatabase()
        {
            foreach (KeyValuePair<int,Movie> movie in Database.SortedDatabase)
            {
                Console.WriteLine(MovieFromDatabaseToString(movie.Key, movie.Value));
            }
        }

        private static void DisplayLibrary()
        {
            foreach (KeyValuePair<int,PurchasedMovie> movie in Database.SortedLibrary)
            {
                Console.WriteLine(MovieFromLibraryToString(movie.Key, movie.Value));
            }
        }

        private static string MovieFromDatabaseToString(int id, Movie movie)
        {
            return $"ID: {id}\n" +
                   $"Title: {movie.Title}\n" +
                   $"Year of premiere: {movie.Year}\n" +
                   $"Director: {movie.Director}\n" +
                   $"Genre: {movie.Genre}\n" +
                   $"Length of movie (in minutes): {movie.Length}\n" +
                   $"Price: {movie.Price}\n";
        }

        private static string MovieFromLibraryToString(int id, PurchasedMovie movie)
        {
            return $"Purchase ID: {id}\n" +
                   $"Title: {movie.Title}\n" +
                   $"Year of premiere: {movie.Year}\n" +
                   $"Director: {movie.Director}\n" +
                   $"Genre: {movie.Genre}\n" +
                   $"Length of movie (in minutes): {movie.Length}\n" +
                   $"Price: {movie.Price}\n" +
                   $"Status: {movie.Status}\n";
        }

        private static void DisplayStats()
        {
            Console.WriteLine($"Total library value: {Database.TotalLibraryValue()}");
            Console.WriteLine($"Total time of watched movies: {Database.TotalWatchedMoviesInHours()} hrs");
            Console.WriteLine($"Average movie watch time: {Database.AverageMovieWatchTime()} mins");
            Console.WriteLine($"Percentage of watched movies from library: {Database.PercentageOfWatchedMovies()}%");
            Console.WriteLine($"Most watched genre: {Database.MostWatchedGenre()}");
        }
    }
}