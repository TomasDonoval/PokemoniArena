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
        /// Inicializuje kontroler s datab�z� a slu�bou pro spr�vu souboj�
        /// </summary>
        /// <param name="dbContext">Datab�ze pro pr�ci s Pokemony v souboji</param>
        public HomeController(PokemonDbContext dbContext)
        {
            _dbContext = dbContext;
            _prubehSouboje = new PrubehSouboje(dbContext);
        }
        /// <summary>
        /// Zobraz� �vodn� str�nku s vytvo�en�mi Pokemony
        /// </summary>
        /// <returns>Vrac� pohled s p�ehledem Pokemon�</returns>
        public IActionResult Index()
        {
            return View(_prubehSouboje.GetPokemons());
        }
        /// <summary>
        /// Zobraz� pr�b�h souboje
        /// </summary>
        /// <param name="soubojId">ID souboje</param>
        /// <returns>Vrac� pohled sestavem souboje</returns>
        public IActionResult Souboj(int soubojId)
        {
            var souboj = _dbContext.Souboje.FirstOrDefault(s => s.Id == soubojId);
            if (souboj == null) return NotFound();

            var model = _prubehSouboje.GetSoubojViewModel(soubojId);
            return View(model);
        }

        /// <summary>
        /// Spust� nov� souboj se zvolen�mi Pokemony
        /// </summary>
        /// <param name="vybranyPokemon">Jm�no vybran�ho Pokemona</param>
        /// <returns>P�esm�ruje na str�nku souboje</returns>
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
        /// <param name="utokJmeno">Jm�no �toku</param>
        /// <returns>Vrac� aktualizovan� pohled</returns>
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
        /// Zobraz� str�nku s chybovou zpr�vou
        /// </summary>
        /// <returns>Vrac� pohled Error s deataily chyb</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}