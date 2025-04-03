using Microsoft.EntityFrameworkCore;
using PokemoniArena.Controllers;
using System.Text;

namespace PokemoniArena.Models
{
    public class Souboj
    {
        /// <summary>
        /// ID souboje
        /// </summary>
        public int Id {  get; set; }
        /// <summary>
        /// Pokemon vybraný hráčem
        /// </summary>
        public string HracJmeno { get; set; }
        /// <summary>
        /// Počet životů Pokemona vybraného hráčem
        /// </summary>
        public int HracZivoty { get; set; }
        /// <summary>
        /// Jméno Pokemona ovládaného počítačem
        /// </summary>
        public string ProtivnikJmeno { get; set; }
        /// <summary>
        /// Počet životů Pokemona ovládaného počítačem
        /// </summary>
        public int ProtivnikZivoty { get; set; }
        /// <summary>
        /// Určuje zda byl souboj ukončen
        /// </summary>
        public bool KonecSouboje { get; set; }          
    }
}
