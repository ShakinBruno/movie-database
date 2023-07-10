using MovieDatabase.IO;
using MovieDatabase.Movies;

namespace MovieDatabase.Databases;

public static class Database
{
    private static readonly Dictionary<int, Movie> database = JsonParser.LoadMovieDatabase();
    private static readonly Dictionary<int, PurchasedMovie> library = JsonParser.LoadLibraryDatabase();
    private static int nextAvailableDatabaseId => Enumerable.Range(0, database.Count + 1).Except(database.Keys).First();
    private static int nextAvailableLibraryId => Enumerable.Range(0, library.Count + 1).Except(library.Keys).First();

    public static List<KeyValuePair<int, Movie>> SortedDatabase => database.OrderBy(key => key.Key).ToList();
    public static List<KeyValuePair<int, PurchasedMovie>> SortedLibrary => library.OrderBy(key => key.Key).ToList();

    public static void AddMovie(string title, int year, string director, string genre, int length, float price)
    {
        var movie = new Movie(title, year, director, genre, length, price);
        database.Add(nextAvailableDatabaseId, movie);
        JsonParser.SaveMovieDatabase(database);
    }

    public static void PurchaseMovie(int id)
    {
        if (!database.ContainsKey(id)) return;
        var purchasedMovie = new PurchasedMovie(database[id]);
        library.Add(nextAvailableLibraryId, purchasedMovie);
        JsonParser.SaveLibraryDatabase(library);
    }

    public static void RemoveMovie(int id)
    {
        if (!database.ContainsKey(id)) return;
        database.Remove(id);
        JsonParser.SaveMovieDatabase(database);
        
        if (!library.ContainsKey(id)) return;
        library.Remove(id);
        JsonParser.SaveLibraryDatabase(library);
    }

    public static void EditMovieTitle(int id, string title)
    {
        if (!database.ContainsKey(id)) return;
        database[id].Title = title;
        JsonParser.SaveMovieDatabase(database);
        
        if (!library.ContainsKey(id)) return;
        library[id].Title = title;
        JsonParser.SaveLibraryDatabase(library);
    }
    
    public static void EditMovieYear(int id, int year)
    {
        if (!database.ContainsKey(id)) return;
        database[id].Year = year;
        JsonParser.SaveMovieDatabase(database);
        
        if (!library.ContainsKey(id)) return;
        library[id].Year = year;
        JsonParser.SaveLibraryDatabase(library);
    }
    
    public static void EditMovieDirector(int id, string director)
    {
        if (!database.ContainsKey(id)) return;
        database[id].Director = director;
        JsonParser.SaveMovieDatabase(database);
        
        if (!library.ContainsKey(id)) return;
        library[id].Director = director;
        JsonParser.SaveLibraryDatabase(library);
    }
    
    public static void EditMovieGenre(int id, string genre)
    {
        if (!database.ContainsKey(id)) return;
        database[id].Genre = genre;
        JsonParser.SaveMovieDatabase(database);
        
        if (!library.ContainsKey(id)) return;
        library[id].Genre = genre;
        JsonParser.SaveLibraryDatabase(library);
    }
    
    public static void EditMovieLength(int id, int length)
    {
        if (!database.ContainsKey(id)) return;
        database[id].Length = length;
        JsonParser.SaveMovieDatabase(database);
        
        if (!library.ContainsKey(id)) return;
        library[id].Length = length;
        JsonParser.SaveLibraryDatabase(library);
    }
    
    public static void EditMoviePrice(int id, float price)
    {
        if (!database.ContainsKey(id)) return;
        database[id].Price = price;
        JsonParser.SaveMovieDatabase(database);
        
        if (!library.ContainsKey(id)) return;
        library[id].Price = price;
        JsonParser.SaveLibraryDatabase(library);
    }

    public static void EditMovieStatus(int id, PurchasedMovie.StatusType status)
    {
        if (!library.ContainsKey(id)) return;
        library[id].Status = status == PurchasedMovie.StatusType.Watched ? DateTime.Now.ToString("g") : status.ToString();
        JsonParser.SaveLibraryDatabase(library);
    }

    public static List<KeyValuePair<int, Movie>> FilterDatabaseByTitle(string title)
    {
        return database.Where(movie => movie.Value.Title.Contains(title)).ToList();
    }
    
    public static List<KeyValuePair<int, PurchasedMovie>> FilterLibraryByTitle(string title)
    {
        return library.Where(movie => movie.Value.Title.Contains(title)).ToList();
    }

    public static List<KeyValuePair<int, Movie>> FilterDatabaseByYear(int min, int max)
    {
        return database.Where(movie => movie.Value.Year >= min && movie.Value.Year <= max).ToList();
    }
    
    public static List<KeyValuePair<int, PurchasedMovie>> FilterLibraryByYear(int min, int max)
    {
        return library.Where(movie => movie.Value.Year >= min && movie.Value.Year <= max).ToList();
    }
    
    public static List<KeyValuePair<int, Movie>> FilterDatabaseByDirector(string director)
    {
        return database.Where(movie => movie.Value.Director.Contains(director)).ToList();
    }
    
    public static List<KeyValuePair<int, PurchasedMovie>> FilterLibraryByDirector(string director)
    {
        return library.Where(movie => movie.Value.Director.Contains(director)).ToList();
    }
    
    public static List<KeyValuePair<int, Movie>> FilterDatabaseByGenre(string genre)
    {
        return database.Where(movie => movie.Value.Genre.Contains(genre)).ToList();
    }
    
    public static List<KeyValuePair<int, PurchasedMovie>> FilterLibraryByGenre(string genre)
    {
        return library.Where(movie => movie.Value.Genre.Contains(genre)).ToList();
    }
    
    public static List<KeyValuePair<int, Movie>> FilterDatabaseByLength(int min, int max)
    {
        return database.Where(movie => movie.Value.Length >= min && movie.Value.Length <= max).ToList();
    }
    
    public static List<KeyValuePair<int, PurchasedMovie>> FilterLibraryByLength(int min, int max)
    {
        return library.Where(movie => movie.Value.Length >= min && movie.Value.Length <= max).ToList();
    }
    
    public static List<KeyValuePair<int, Movie>> FilterDatabaseByPrice(float min, float max)
    {
        return database.Where(movie => movie.Value.Length >= min && movie.Value.Length <= max).ToList();
    }
    
    public static List<KeyValuePair<int, PurchasedMovie>> FilterLibraryByPrice(float min, float max)
    {
        return library.Where(movie => movie.Value.Price >= min && movie.Value.Price <= max).ToList();
    }
    
    public static List<KeyValuePair<int, PurchasedMovie>> FilterLibraryByStatus(PurchasedMovie.StatusType status)
    {
        return status == PurchasedMovie.StatusType.Watched ? 
            library.Where(movie => DateTime.TryParse(movie.Value.Status, out DateTime _)).ToList() : 
            library.Where(movie => movie.Value.Status == status.ToString()).ToList();
    }

    public static float TotalLibraryValue()
    {
        return library.Values.Sum(movie => movie.Price);
    }

    public static float TotalWatchedMoviesInHours()
    {
        int totalMinutes = library
            .Where(movie => DateTime.TryParse(movie.Value.Status, out DateTime _))
            .Sum(key => key.Value.Length);
        return MathF.Round(totalMinutes / 60f, 2);
    }

    public static float AverageMovieWatchTime()
    {
        double averageWatchTime = library
            .Where(movie => DateTime.TryParse(movie.Value.Status, out DateTime _))
            .Average(key => key.Value.Length);
        return MathF.Round((float)averageWatchTime, 2);
    } 

    public static float PercentageOfWatchedMovies()
    {
        int totalWatchedMovies = library.Count(movie => DateTime.TryParse(movie.Value.Status, out DateTime _));
        return MathF.Round(totalWatchedMovies * 100f / library.Count, 2);
    }

    public static string MostWatchedGenre()
    {
        return library
            .Where(movie => DateTime.TryParse(movie.Value.Status, out DateTime _))
            .GroupBy(key => key.Value.Genre)
            .OrderByDescending(key => key.Count())
            .Select(key => key.Key)
            .FirstOrDefault() ?? string.Empty;
    }
}