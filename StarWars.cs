using Newtonsoft.Json;

namespace WebAPI
{
    class StarWarsObject {
        //Base API Url
        private const string baseURL = "https://swapi.dev/api/"; 

        /// <summary>
        /// Creates and returns an HttpClient that accepts JSON response.
        /// </summary>
        /// <returns>An HttpClientith ready to call APIs. </returns>
        public static HttpClient setUpClient() {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        /// <summary>
        /// Finds character on the star wars API and details about the movies they have appeared in. 
        /// </summary>
        /// /// <param name="chararcterName">The JSON payload with the name of the character to find. </param>
        /// <returns> JSON file with details about movies the character appeared in. </returns>
        public static void CharactersMovies(string chararcterName)
        {
            dynamic input = JsonConvert.DeserializeObject(chararcterName); //convert JSON string to JSON object
            string URL = baseURL + "people/?search=" + input.character; //use the JSON object to get the character name
            string filePath = @"..\..\..\CharactersMovies.json"; 
            File.AppendAllText(filePath, "["); //create JSON file
            HttpClient client = setUpClient();
            try
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(client.GetAsync(URL).Result.Content.ReadAsStringAsync().Result); //GET request 
                if (jsonObj.results.Count > 0)
                {
                    // loop over the list of movies for the character
                    foreach (string film in jsonObj.results[0].films)
                    {
                        dynamic callJsonObj = JsonConvert.DeserializeObject(client.GetAsync(film)       //GET request on movie APIs to get title
                            .Result.Content.ReadAsStringAsync().Result); 
                        string jsonString = JsonConvert.SerializeObject(new MovieInfo((string)callJsonObj.title,
                            (int)callJsonObj.episode_id, (string)callJsonObj.release_date)); //deserialize JSON output to string
                        File.AppendAllText(filePath, jsonString + ","); //append string to file
                    }
                    File.AppendAllText(filePath, "]");
                }
                else
                {
                    Console.WriteLine("Error 404 : Requested Resource Not found");
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            }
        }

        /// <summary>
        /// Finds starship on the star wars API and details about the starship. 
        /// </summary>
        /// /// <param name="starshipName">The JSON payload with the name of the starship to find. </param>
        /// <returns> JSON file with details about starship. </returns>
        public static void StarshipsInfo(string starshipName)
        {
            dynamic input = JsonConvert.DeserializeObject(starshipName); //convert JSON string to JSON object
            string URL = baseURL + "starships/?search=" + input.starShip; //use the JSON object to get the starship name
            string filePath = @"..\..\..\StarshipsInfo.json";
            File.AppendAllText(filePath, "["); //create JSON file
            HttpClient client = setUpClient();
            try
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(client.GetAsync(URL).Result.Content.ReadAsStringAsync().Result); //GET request 
                List<string> pilots = new List<string>();
                if (jsonObj.results.Count > 0)
                {
                    // loop over the list of pilots for the starship
                    foreach (string pilot in jsonObj.results[0].pilots)
                    {
                        dynamic callJsonObj = JsonConvert.DeserializeObject(client.GetAsync(pilot).Result.Content.ReadAsStringAsync().Result);
                        pilots.Add((string)callJsonObj.name);
                    }
                    string jsonString = JsonConvert.SerializeObject(new StarShipInfo(pilots, (string)jsonObj.results[0].passengers)); //deserialize JSON output to string
                    File.AppendAllText(filePath, jsonString + "]"); //append string to file
                }
                else
                {
                    Console.WriteLine("Error 404 : Requested Resource Not found");
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            }
        }

        /// <summary>
        /// Find all films in the API with their release dates. 
        /// </summary>
        /// <returns> Dictionary<string, string> containing movie title and their release dates as keys and values respectively. </returns>
        public static Dictionary<string, string> GetMovies()
        {
            string URL = baseURL + "films/";
            Dictionary<string, string> movies = new Dictionary<string, string>();
            HttpClient client = setUpClient();
            try
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(client.GetAsync(URL).Result.Content.ReadAsStringAsync().Result);//GET request
                if (jsonObj.results.Count > 0)
                {
                    foreach (dynamic film in jsonObj.results)
                        movies.Add((string)film.title, (string)film.release_date);
                }
                else
                {
                    Console.WriteLine("Error 404 : Requested Resource Not found");
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            }
            return movies;
        }
    }
}