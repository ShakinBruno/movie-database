# Object Oriented Programming Class - Project: Movie Database
### author: Dominik Mikołajczyk

## The task the of project
The aim of the project was to write a console application that responds to commands, uses object-oriented programming model and also meets additional criteria:
- ability to add or remove a movie from the movie database
- editing information about particular movie
- purchasing the movie
- displaying whole database
- the entire database should be saved in a text file and read from it on the next startup

## Requirements:        
.NET 7.0 is required to run this application in IDE, otherwise you have to fix potential errors.
        
## Functionalities
When the main menu is displayed, user has to type the number of desirable action. Invalid input results in cancellation of the action and return to the main menu. Data is saved in JSON format under "Data" folder so it is possible to add, delete or modify database using only text editor. All functionalitites of application are as follows:
- ADD MOVIE TO DATABASE - user enters the information of movie one by one and after correct input the movie is added to the database.
- DELETE MOVIE FROM DATABASE - All ID's with movie titles are displayed, the user enters the correct ID of the movie he wants to delete and it is removed from the database. If the movie was in the user's library it is also deleted.
- EDIT MOVIE INFO - All ID's with movie titles are displayed, the user enters the correct ID of the movie he wants to modify then specifies which information about the movie he would like to modify and provides a new value.
- EDIT MOVIE WATCH STATUS - All ID's with movie titles from user's library are displayed, the user enters the correct ID of the movie the status he wants to modify then chooses one of three statuses. When movie is marked as watched, current time will be saved in database as status.
- PURCHASE A MOVIE - All ID's with movie titles are displayed, the user enters the correct ID of the movie he wants to buy. The movie will be added to user's library.
- DISPLAY MOVIE DATABASE - All movies with their all information will be displayed. The user has an option to filter movies by any of its information category.
- DISPLAY YOUR LIBRARY - All movies from library with their all information will be displayed. The user has an option to filter movies by any of its information category. Additionaly user can filter the library by movie watch status.
- DISPLAY MOVIE STATISTICS - displays various statistics about user's library.
- EXIT THE PROGRAM - exits the program.
