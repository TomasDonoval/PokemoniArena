using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemoniArena.Migrations;
using PokemoniArena.Models;

namespace PokemoniArena.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokemonDbContext _dbContext;
        private readonly PrubehSouboje _prubehSouboje;

        /// <summary>
        /// Inicializuje kontroler s databázý a službou pro správu soubojù
        /// </summary>
        /// <param name="dbContext">Databáze pro práci s Pokemony v souboji</param>
        public HomeController(PokemonDbContext dbContext)
        {
            _dbContext = dbContext;
            _prubehSouboje = new PrubehSouboje(dbContext);
        }

        /// <summary>
        /// Zobrazí úvodní stránku s vytvoøenými Pokemony
        /// </summary>
        /// <returns>Vrací pohled s pøehledem Pokemonù</returns>
        public IActionResult Index()
        {
            return View(_prubehSouboje.GetPokemons());
        }

        /// <summary>
        /// Zobrazí prùbìh souboje
        /// </summary>
        /// <param name="soubojId">ID souboje</param>
        /// <returns>Vrací pohled sestavem souboje</returns>
        public IActionResult Souboj(int soubojId)
        {
            var souboj = _dbContext.Souboje.FirstOrDefault(s => s.Id == soubojId);
            if (souboj == null) return NotFound();

            var model = _prubehSouboje.GetSoubojViewModel(soubojId);
            return View(model);
        }

        /// <summary>
        /// Spustí nový souboj se zvolenými Pokemony
        /// </summary>
        /// <param name="vybranyPokemon">Jméno vybraného Pokemona</param>
        /// <returns>Pøesmìruje na stránku souboje</returns>
        [HttpPost]
        public IActionResult SoubojStart(string vybranyPokemon)
        {
            if (string.IsNullOrEmpty(vybranyPokemon))
            {
                return RedirectToAction("Index");
            }

            int soubojId = _prubehSouboje.ZacatekSouboje(vybranyPokemon);
            return RedirectToAction("Souboj", new { soubojId });
        }

        /// <summary>
        /// Zpracuje jedno kolo souboje
        /// </summary>
        /// <param name="soubojId">ID souboje</param>
        /// <param name="utokJmeno">Jméno útoku</param>
        /// <returns>Vrací aktualizovaný pohled</returns>
        [HttpPost]
        public IActionResult SoubojKolo(int soubojId, string utokJmeno)
        {
            if (string.IsNullOrEmpty(utokJmeno))
            {
                return RedirectToAction("Souboj", new { soubojId });
            }
            
            var model = _prubehSouboje.SoubojKolo(soubojId, utokJmeno);
            return View("Souboj", model);
        }

        /// <summary>
        /// Zobrazí stránku s chybovou zprávou
        /// </summary>
        /// <returns>Vrací pohled Error s deataily chyb</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}