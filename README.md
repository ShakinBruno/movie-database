# Object Oriented Programming Class - Project: Movie Database

### Author: Dominik Mikołajczyk

## Project Overview

The Movie Database project is a console application designed to manage a collection of movies using an object-oriented programming approach. The application allows users to perform various operations on a movie database, including adding, removing, editing, and purchasing movies. The database is persisted in a JSON format, ensuring data is saved between sessions.

## Features

- **Add Movie to Database**: Users can input movie details to add a new movie to the database.
- **Delete Movie from Database**: Users can remove a movie by selecting its ID. If the movie is in the user's library, it will also be removed from there.
- **Edit Movie Information**: Users can modify details of an existing movie by selecting its ID and specifying the information to update.
- **Edit Movie Watch Status**: Users can update the watch status of movies in their library, with options for different statuses. When marked as watched, the current time is recorded.
- **Purchase a Movie**: Users can purchase a movie, adding it to their personal library.
- **Display Movie Database**: Users can view all movies in the database, with options to filter by various attributes.
- **Display Your Library**: Users can view all movies in their library, with options to filter by attributes and watch status.
- **Display Movie Statistics**: Provides various statistics about the user's library.
- **Exit the Program**: Closes the application.

## Data Persistence

The application saves data in JSON format under the "Data" folder. This allows users to manually edit the database using a text editor if needed. The `JsonParser` class handles loading and saving of both the movie database and the user's library.

## Requirements

- .NET 7.0 is required to run this application in an IDE. Ensure you have the correct version installed to avoid potential errors.

## Usage

Upon launching the application, the main menu is displayed. Users can select actions by typing the corresponding number. Invalid inputs result in cancellation of the action and a return to the main menu.

## Code Structure

- **Movie.cs**: Defines the `Movie` and `PurchasedMovie` classes, including constructors and status management.
- **JsonParser.cs**: Contains methods for loading and saving the movie database and user library to JSON files.
- **Database.cs**: Manages the in-memory representation of the movie database and user library. It provides methods for adding, removing, editing, and filtering movies, as well as calculating statistics.
- **App.cs**: Contains the main application logic, including the user interface and handling of user inputs. It orchestrates the interaction between the user and the database.
