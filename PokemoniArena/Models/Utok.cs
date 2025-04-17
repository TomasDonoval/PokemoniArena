namespace PokemoniArena.Models
{
    public class Utok
    {
        /// <summary>
        /// Název útoku
        /// </summary>
        public string Jmeno { get; set; }

        /// <summary>
        /// Základni hodnota poškození útoku
        /// </summary>
        public int ZakladniPoskozeni { get; set; }

        /// <summary>
        /// Textový popis rozsahu celkového poškození (základní poškození + hod kostkou)
        /// Např. 10 - 16 znamená, že útok způsobí mezi 10 a 16 body poškození
        /// </summary>
        public string RozsahPoskozeni { get; set; }

        /// <summary>
        /// Typ útoku (např. ohnivý, vodní, elektrický)
        /// </summary>
        public string Typ {  get; set; }

        /// <summary>
        /// Kostka, která ovlivní poškození útoku
        /// </summary>
        public Kostka Kostka { get; set; }

        /// <summary>
        /// Vytvooří novou instanci útoku
        /// </summary>
        /// <param name="jmeno">Název útoku</param>
        /// <param name="poskozeni">Základní poškození útoku</param>
        /// <param name="typ">Typ útoku</param>
        /// <param name="kostka">Kostka pro dodatečné poškození</param>
        public Utok(string jmeno, int zakladniPoskozeni, string typ, Kostka kostka)
        {
            Jmeno = jmeno;
            ZakladniPoskozeni = zakladniPoskozeni;
            Typ = typ;
            Kostka = kostka;
            RozsahPoskozeni = $"{zakladniPoskozeni + 1} - {kostka.PocetSten + zakladniPoskozeni}";
        }
    }
}
