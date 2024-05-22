using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PREFINAL_ASSIGNMENT_TWO_POKEMON_REYES_MARTIN_BSIT_32E1.Models;


namespace PREFINAL_ASSIGNMENT_TWO_POKEMON_REYES_MARTIN_BSIT_32E1.Controllers
{
    public class PokemonController : Controller
    {
        private readonly HttpClient _httpClient;

        public PokemonController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class PokeApiResponse
        {
            public int Count { get; set; }
            public string Next { get; set; }
            public string Previous { get; set; }
            public List<PokemonDetail> Results { get; set; }
        }

        public class PokemonDetail
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<PokeApiResponse>(content);
                var pokemonList = apiResponse.Results.Select(p => new Pokemon { Name = p.Name }).ToList();
                return View(pokemonList);
            }
            return View("Error");
        }

        public async Task<IActionResult> Details(string name)
        {
            // Fetch Pokemon details by name
            HttpResponseMessage response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var pokemon = JsonConvert.DeserializeObject<Pokemon>(content);
                return View(pokemon);
            }
            return View("Error");
        }
    }
}
