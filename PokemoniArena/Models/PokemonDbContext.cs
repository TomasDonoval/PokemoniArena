using Microsoft.EntityFrameworkCore;
namespace PokemoniArena.Models
{
    public class PokemonDbContext : DbContext
    {
        /// <summary>
        /// Databázová sada obsahující souboje mezi Pokemony
        /// </summary>
        public DbSet<Souboj> Souboje { get; set; }
        /// <summary>
        /// Inicializuje nová kontext databáze
        /// </summary>
        /// <param name="options">Možnosti konfigurace dazabázového kontextu</param>
        public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) { }
        /// <summary>
        /// Konfigurace databázového modelu
        /// Nastavuje primární klíč entity Souboj
        /// </summary>
        /// <param name="modelBuilder">Instance ModelBuilder pro konfiguraci entit</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Souboj>().HasKey(s => s.Id);
        }
    }
}
