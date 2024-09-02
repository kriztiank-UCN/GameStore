using GameStore.Api.Dtos;
using GameStore.Api.Entities;
namespace GameStore.Api.Mapping;

// use a static class for extension methods
public static class GameMapping
{
    // Map CreateGameDto to Game entity
    public static Game ToEntity(this CreateGameDto game)
    {
        // Create entity from DTO
        return new Game
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    // Map UpdateGameDto to Game entity
    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        // Create entity from DTO
        return new Game
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    // Map Game entity back into a GameDto 
    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        // Create DTO from entity
        return new GameSummaryDto(
            game.Id,
            game.Name,
            // ! null forgiving operator, is used to tell the compiler that Genre is not null
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        // Create DTO from entity
        return new GameDetailsDto(
            game.Id,
            game.Name,
            // ! null forgiving operator, is used to tell the compiler that Genre is not null
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }
}
