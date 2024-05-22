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

        public async Task<IActionResult> Index()
        {
            // Fetch Pokemon data from PokéAPI
            HttpResponseMessage response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var pokemonList = JsonConvert.DeserializeObject<List<Pokemon>>(content);
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
