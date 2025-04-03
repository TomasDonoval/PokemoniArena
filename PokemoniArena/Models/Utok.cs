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
        public int Poskozeni { get; set; }
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
        public Utok(string jmeno, int poskozeni, string typ, Kostka kostka)
        {
            Jmeno = jmeno;
            Poskozeni = poskozeni;
            Typ = typ;
            Kostka = kostka;
        }
    }
}
