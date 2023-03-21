// See https://aka.ms/new-console-template for more information
using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
string movieFilePath = Directory.GetCurrentDirectory() + "//movies.scrubbed.csv";
logger.Info("Program started");

MovieFile movieFile = new MovieFile(movieFilePath);

string choice = "";

do
{
  // display choices to user
  Console.WriteLine("1) Add Movie");
  Console.WriteLine("2) Display All Movies");
  Console.WriteLine("Enter to quit");
  // input selection
  choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);

  if (choice == "1")
  {
    // Add movie
    Movie movie = new Movie();
    // ask user to input movie title
    Console.WriteLine("Enter movie title");
    // input title
    movie.title = Console.ReadLine();

    // verify title is unique
    if (movieFile.isUniqueTitle(movie.title)){
      // input genres
      string input;

      do
      {
        // ask user to enter genre
        Console.WriteLine("Enter genre (or done to quit)");
        // input genre
        input = Console.ReadLine();
        // if user enters "done"
        // or does not enter a genre do not add it to list
        if (input != "done" && input.Length > 0)
        {
          movie.genres.Add(input);
        }
      } while (input != "done");

      // specify if no genres are entered
      if (movie.genres.Count == 0)
      {
        movie.genres.Add("(no genres listed)");
      }

      // add director
      Console.WriteLine("Enter director name:");
      movie.director = Console.ReadLine();

      // add run time
      Console.WriteLine("Enter the length of the movie (hours): ");
      int hours = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine("Enter the length of the movie (minutes): ");
      int minutes = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine("Enter the length of the movie (seconds): ");
      int seconds = Convert.ToInt32(Console.ReadLine());
      movie.runningTime = new TimeSpan(hours, minutes, seconds);
      
      // add movie
      movieFile.AddMovie(movie);
    }
  } 
  
  else if (choice == "2")
  {
    // Display All Movies
    foreach(Movie m in movieFile.Movies)
    {
      Console.WriteLine(m.Display());
    }
  }
} while (choice == "1" || choice == "2");

logger.Info("Program ended");
