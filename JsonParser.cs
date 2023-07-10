using System.Text.Json;
using MovieDatabase.Movies;

namespace MovieDatabase.IO;

public static class JsonParser
{
    private const string dataDirectory = "Data";
    private const string movieDatabasePath = "movies.json";
    private const string libraryDatabasePath = "library.json";
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        WriteIndented = true
    };

    public static Dictionary<int, Movie> LoadMovieDatabase()
    {
        string movieDirectoryPath = Path.Combine(dataDirectory, movieDatabasePath);
        if (!Directory.Exists(dataDirectory)) Directory.CreateDirectory(dataDirectory);
        if (File.Exists(movieDirectoryPath))
        {
            using var stream = new StreamReader(movieDirectoryPath);
            var database = JsonSerializer.Deserialize<Dictionary<int, Movie>>(stream.BaseStream, jsonOptions);
            return database ?? new Dictionary<int, Movie>();
        }
        else
        {
            var emptyDictionary = new Dictionary<int, Movie>();
            using var stream = new StreamWriter(movieDirectoryPath);
            string jsonDatabase = JsonSerializer.Serialize(emptyDictionary, jsonOptions);
            stream.WriteLine(jsonDatabase);
            return emptyDictionary;
        }
    }

    public static void SaveMovieDatabase(Dictionary<int, Movie> movies)
    {
        string movieDirectoryPath = Path.Combine(dataDirectory, movieDatabasePath);
        using var stream = new StreamWriter(movieDirectoryPath);
        string jsonDatabase = JsonSerializer.Serialize(movies, jsonOptions);
        stream.WriteLine(jsonDatabase);
    }

    public static Dictionary<int, PurchasedMovie> LoadLibraryDatabase()
    {
        string libraryDirectoryPath = Path.Combine(dataDirectory, libraryDatabasePath);
        if (!Directory.Exists(dataDirectory)) Directory.CreateDirectory(dataDirectory);
        if (File.Exists(libraryDirectoryPath))
        {
            using var stream = new StreamReader(libraryDirectoryPath);
            var database = JsonSerializer.Deserialize<Dictionary<int, PurchasedMovie>>(stream.BaseStream, jsonOptions);
            return database ?? new Dictionary<int, PurchasedMovie>();
        }
        else
        {
            var emptyDictionary = new Dictionary<int, PurchasedMovie>();
            using var stream = new StreamWriter(libraryDirectoryPath);
            string jsonDatabase = JsonSerializer.Serialize(emptyDictionary, jsonOptions);
            stream.WriteLine(jsonDatabase);
            return emptyDictionary;
        }
    }
    
    public static void SaveLibraryDatabase(Dictionary<int, PurchasedMovie> movies)
    {
        string libraryDirectoryPath = Path.Combine(dataDirectory, libraryDatabasePath);
        using var stream = new StreamWriter(libraryDirectoryPath);
        string jsonDatabase = JsonSerializer.Serialize(movies, jsonOptions);
        stream.WriteLine(jsonDatabase);
    }
}