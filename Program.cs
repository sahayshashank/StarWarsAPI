namespace WebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            StarWarsObject.CharactersMovies(@"{'character': 'Luke Skywalker'}");
            StarWarsObject.StarshipsInfo(@"{'starship': 'Millennium Falcon'}");
            var movies = StarWarsObject.GetMovies();
            foreach (KeyValuePair<string, string> kvp in movies.OrderBy(key => key.Value))
               Console.WriteLine(" {0}, Release Date : {1}", kvp.Key, kvp.Value);
        }
}
}