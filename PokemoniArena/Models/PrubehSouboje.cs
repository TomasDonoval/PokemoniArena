using Microsoft.EntityFrameworkCore;
using PokemoniArena.Controllers;
using System.Text;

namespace PokemoniArena.Models
{
    public class PrubehSouboje
    {
        private readonly PokemonDbContext _dbContext; // Odkaz na datavázový kontext
        public List<Pokemon> pokemons; // Seznam dostupných Pokemonů
        private List<Utok> seznamUtoku; // Seznam možných útoků
        private Kostka sestiStenka = new Kostka(6); // Šestistěnná kostka pro generování náhodných čísel
        private Kostka desetiStenka = new Kostka(10); // Desetistěnná kostka pro generování náhodných čísel
        private Kostka dvacetiStenka = new Kostka(20); // Dvacetistěnná kostka pro generování náhodných čísel

        /// <summary>
        /// Inicializuje novou instanci třídy PrubehSouboje a nastaví seznam dostupných pokemonů a útoků
        /// </summary>
        /// <param name="dbContext">Databázový kontext pro souboje</param>
        public PrubehSouboje(PokemonDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            seznamUtoku = new List<Utok>
        {
            new Utok("Wild Charge", 14, "Elektrický", sestiStenka),
            new Utok("Thunder Bolt", 12, "Elektrický", desetiStenka),
            new Utok("Seed Bomb", 13, "Travní", sestiStenka),
            new Utok("Tackle", 11, "Normální", desetiStenka),
            new Utok("Flamethrower", 16, "Ohnivý", sestiStenka),
            new Utok("Scratch", 14, "Normální", desetiStenka),
            new Utok("Hydro Pump", 9, "Vodní", dvacetiStenka)
        };
            pokemons = new List<Pokemon>
        {
            new Pokemon("Pikachu", "Elektrický", 220, seznamUtoku[0], seznamUtoku[1]),
            new Pokemon("Bulbasaur", "Travní", 240, seznamUtoku[2], seznamUtoku[3]),
            new Pokemon("Charmander", "Ohnivý", 200, seznamUtoku[4], seznamUtoku[5]),
            new Pokemon("Squirtle", "Vodní", 200, seznamUtoku[5], seznamUtoku[6])
        };
        }

        /// <summary>
        /// Vrátí seznam dostupnách Pokemonů
        /// </summary>
        public List<Pokemon> GetPokemons() => pokemons;

        /// <summary>
        /// Záhájí souboj mezi hráčovým Pokemonem a náhodně vybraným Pokemonem
        /// </summary>
        /// <param name="vybranyPokemon">Jméno hráčem vybraného Pokemona</param>
        /// <returns>ID vytvořeného souboje</returns>  
        public int ZacatekSouboje(string vybranyPokemon)
        {
            // Najdeme hráčova Pokémona
            Pokemon hrac = pokemons.FirstOrDefault(p => p.Jmeno == vybranyPokemon);

            // Náhodně vybereme soupeře (někoho jiného než hráčova Pokémona)
            Pokemon protivnik;
            do
            {
                protivnik = pokemons[new Random().Next(pokemons.Count)];
            } while (protivnik.Jmeno == hrac.Jmeno);

            // Vytvoření souboje a uložení do databáze
            var souboj = new Souboj
            {
                HracJmeno = hrac.Jmeno,
                HracZivoty = hrac.Zivoty,
                ProtivnikJmeno = protivnik.Jmeno,
                ProtivnikZivoty = protivnik.Zivoty,
                KonecSouboje = false
            };
            _dbContext.Souboje.Add(souboj);
            _dbContext.SaveChanges();

            return souboj.Id;
        }

        /// <summary>
        /// Zpracuje jedno kolo souboje
        /// </summary>
        /// <param name="soubojId">ID souboje, který zpracovává</param>
        /// <param name="utokJmeno">Jméno útoku, který hráč zvolil</param>
        /// <returns>Aktualizovaný model souboje pro zobrazení</returns>
        public SoubojViewModel SoubojKolo(int soubojId, string utokJmeno)
        {
            var souboj = _dbContext.Souboje.FirstOrDefault(s => s.Id == soubojId);

            // Najdu pokemony
            Pokemon hrac = pokemons.FirstOrDefault(p => p.Jmeno == souboj.HracJmeno)?.Clone();
            Pokemon protivnik = pokemons.FirstOrDefault(p => p.Jmeno == souboj.ProtivnikJmeno)?.Clone(); ;

            // Nastavím aktuální stav životů
            hrac.Zivoty = souboj.HracZivoty;
            protivnik.Zivoty = souboj.ProtivnikZivoty;

            // Útok hráče
            StringBuilder prubeh = new StringBuilder();
            Utok vybranyUtok = seznamUtoku.FirstOrDefault(p => p.Jmeno == utokJmeno);
            int poskozeniHrac = hrac.Utoc(protivnik, vybranyUtok);
            prubeh.AppendLine($"{hrac.Jmeno} použil {vybranyUtok.Jmeno} a způsobil {poskozeniHrac} poškození.");

            // Zkontroluji zda protivník přežil, pokud ano následuje jeho útok
            if (protivnik.NaZivu())
            {
                Utok protivnikUtok = new Random().Next(2) == 0 ? protivnik.UtokJedna : protivnik.UtokDva;
                int poskozeniProtivnik = protivnik.Utoc(hrac, protivnikUtok);
                prubeh.AppendLine($"{protivnik.Jmeno} použil {protivnikUtok.Jmeno} a způsobil {poskozeniProtivnik} poškození.");
            }

            // Určení zda souboj skončil
            bool konec = !hrac.NaZivu() || !protivnik.NaZivu();
            if (konec)
            {
                prubeh.AppendLine(hrac.NaZivu() ? $"{hrac.Jmeno} vyhrál souboj!" : $"{protivnik.Jmeno} vyhrál souboj!");
            }
            else
            {
                prubeh.AppendLine("Vyber další útok:");
            }

            //Aktualizace stavu souboje v databázi
            souboj.HracZivoty = hrac.Zivoty;
            souboj.ProtivnikZivoty = protivnik.Zivoty;
            souboj.KonecSouboje = konec;
            _dbContext.SaveChanges();

            return new SoubojViewModel
            {
                HracPokemon = hrac,
                Protivnik = protivnik,
                ZpravaPrubehuBoje = prubeh.ToString(),
                UvodniZprava = $"{hrac.Jmeno} vs {protivnik.Jmeno}",
                KonecSouboje = konec,
                SoubojId = soubojId
            };
        }

        /// <summary>
        /// Vrátí model souboje na základě jeho ID
        /// </summary>
        /// <param name="soubojId">ID souboje</param>
        /// <returns>ViewModel obsahující stav souboje</returns>
        public SoubojViewModel GetSoubojViewModel(int soubojId)
        {
            {
                var souboj = _dbContext.Souboje.FirstOrDefault(s => s.Id == soubojId);
                if (souboj == null) return null;

                Pokemon hrac = pokemons.FirstOrDefault(p => p.Jmeno == souboj.HracJmeno);
                Pokemon protivnik = pokemons.FirstOrDefault(p => p.Jmeno == souboj.ProtivnikJmeno);

                return new SoubojViewModel
                {
                    HracPokemon = hrac,
                    Protivnik = protivnik,
                    SoubojId = souboj.Id,
                    KonecSouboje = souboj.KonecSouboje,
                    UvodniZprava = $"{hrac.Jmeno} vs {protivnik.Jmeno}"
                };
            }

        }
    }
}
