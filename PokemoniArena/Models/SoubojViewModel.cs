namespace PokemoniArena.Models
{
    public class SoubojViewModel
    {
        /// <summary>
        /// ID souboje
        /// </summary>
        public int SoubojId { get; set; }
        /// <summary>
        /// Pokemon vybraný hráčem
        /// </summary>
        public Pokemon HracPokemon { get; set; }
        /// <summary>
        /// Pokemon ovladáný počítačem
        /// </summary>
        public Pokemon Protivnik { get; set; }
        /// <summary>
        /// Úvodní zpráva obsahující základní informace o souboji
        /// </summary>
        public string UvodniZprava { get; set; }
        /// <summary>
        /// Zpráva obashující informace o aktuálním dění v souboji
        /// </summary>
        public string ZpravaPrubehuBoje { get; set; }
        /// <summary>
        /// Určuje zda byl souboj ukončen
        /// </summary>
        public bool KonecSouboje { get; set; }
    }
}
