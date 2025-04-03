using System.Runtime.InteropServices;

namespace PokemoniArena.Models
{
    public class Pokemon
    {
        /// <summary>
        /// Název Pokemona
        /// </summary>
        public string Jmeno { get; set; }
        /// <summary>
        /// Typ Pokemona (např. ohivý, travní, elektrický)
        /// </summary>
        public string Typ { get; set; }
        /// <summary>
        /// Maximální počet životů Pokemona
        /// </summary>
        public int MaxZivoty { get; set; }
        /// <summary>
        /// Aktuální počet životů Pokemona
        /// </summary>
        public int Zivoty { get; set; }
        /// <summary>
        /// Primární útok Pokemona
        /// </summary>
        public Utok UtokJedna { get; set; }
        /// <summary>
        /// Sekundární útok Pokemona
        /// </summary>
        public Utok UtokDva { get; set; }

        /// <summary>
        /// Vytvoří novou instanci Pokemona
        /// </summary>
        /// <param name="jmeno">Název Pokemona</param>
        /// <param name="typ">Typ Pokemona</param>
        /// <param name="maxZivoty">Maximální počet životů</param>
        /// <param name="utokJedna">Primární útok</param>
        /// <param name="utokDva">Sekundární útok</param>
        public Pokemon(string jmeno, string typ, int maxZivoty, Utok utokJedna, Utok utokDva)
        {
            Jmeno = jmeno;
            Typ = typ;
            MaxZivoty = maxZivoty;
            Zivoty = maxZivoty;
            UtokJedna = utokJedna;
            UtokDva = utokDva;
        }

        /// <summary>
        /// Provede útok na soupeře a vypočítá poškození
        /// </summary>
        /// <param name="souper">Pokemon na kterého se útočí</param>
        /// <param name="utok"> Použitý útok</param>
        public void Utoc (Pokemon souper, Utok utok)
        {
            int poskozeni = utok.Poskozeni + (utok.Kostka.Hod());
            souper.BranSe(poskozeni, utok);
        }

        /// <summary>
        /// Ověření jestli je Pokemon stále živý
        /// </summary>
        /// <returns>Vrací true pokud má Pokemon více než 1 život</returns>
        public bool NaZivu() => Zivoty > 0;

        /// <summary>
        /// Pokemon se brání proti útoku a příjmá poškození
        /// </summary>
        /// <param name="poskozeni">Hodnota poškození, které má Pokemon obdržet</param>
        /// <param name="utok">Typ útoku, který ho zasáhl</param>
        public void BranSe(int poskozeni, Utok utok)
        {
            int zraneni = (int)(poskozeni * ZiskejCinitel(utok.Typ, this.Typ));
            Zivoty -= zraneni;
        }

        /// <summary>
        /// Určuje efektivitu útoku podle typu útoku a obránce
        /// </summary>
        /// <param name="utokTyp">Typ útoku</param>
        /// <param name="obranaTyp">Typ obránce</param>
        /// <returns>Činitel ovlivňující poškození</returns>
        public double ZiskejCinitel(string utokTyp, string obranaTyp)
        {
            return (utokTyp, obranaTyp) switch
            {
                ("Travní", "Ohnivý") => 0.95,
                ("Ohnivý", "Travní") => 1.05,
                _ => 1.0
            };
        }

        /// <summary>
        /// Vytvoří kopii Pokemona se stejnými vlastnostmi
        /// </summary>
        /// <returns>Nová instatnce Pokemona</returns>
        public Pokemon Clone()
        {
            return new Pokemon(Jmeno, Typ, MaxZivoty, UtokJedna, UtokDva);
        }
    }
}
