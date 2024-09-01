// Purpose: Contains the GameStoreContext class which is used to interact with the database.
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options)
         : DbContext(options)
    {
        // Create the Games table
        public DbSet<Game> Games => Set<Game>();
        // Create the Genres table
        public DbSet<Genre> Genres => Set<Genre>();


    }
}