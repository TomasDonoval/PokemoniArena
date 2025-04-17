namespace PokemoniArena.Models
{
    public class Kostka
    {
        /// <summary>
        /// Počet stěn kostky
        /// </summary>
        public int PocetSten { get; set; }

        /// <summary>
        /// Generátor náhodných čísel
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        /// Inicializuje novou instanci kostky s daným počtem stěn
        /// </summary>
        /// <param name="pocetSten">Počet stěn kostky</param>
        public Kostka (int pocetSten)
        {
            PocetSten = pocetSten;
        }

        /// <summary>
        /// Provede hod kostkou
        /// </summary>
        /// <returns>Hodnota v rozmezí 1 až počet stěn kostky</returns>
        public int Hod()
        {
            int nahodneCislo = random.Next(1,PocetSten + 1);
            return nahodneCislo;
        }
    }
}
