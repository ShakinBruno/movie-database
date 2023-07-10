using System.Text.Json.Serialization;

namespace MovieDatabase.Movies;

public class Movie
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string Director { get; set; }
    public string Genre { get; set; }
    public int Length { get; set; }
    public float Price { get; set; }

    [JsonConstructor]
    public Movie()
    {
        Title = string.Empty;
        Director = string.Empty;
        Genre = string.Empty;
    }
    
    public Movie(string title, int year, string director, string genre, int length, float price)
    {
        Title = title;
        Year = year;
        Director = director;
        Genre = genre;
        Length = length;
        Price = price;
    }
}

public class PurchasedMovie : Movie
{
    public string Status { get; set; }

    public enum StatusType
    {
        None,
        NotWatched,
        InProgress,
        Watched
    }

    [JsonConstructor]
    public PurchasedMovie()
    {
        Status = StatusType.None.ToString();
    }
    
    public PurchasedMovie(Movie movie) : base(movie.Title, movie.Year, movie.Director, movie.Genre, movie.Length, movie.Price)
    {
        Status = StatusType.NotWatched.ToString();
    }
}