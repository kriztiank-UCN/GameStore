// Purpose: Contains endpoints for the Games controller.
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Endpoints;
// static class with extension methods, with one call we can map all the endpoints
public static class GamesEndpoints
{
  const string GetGameEndpointName = "GetGame";
  private static readonly List<GameDto> games = [
      new(
        1,
        "Street Fighting II",
        "Fighting",
        19.99m,
        new DateOnly(1992, 7, 15)
    ),
    new(
        2,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99m,
        new DateOnly(2010, 9, 30)
    ),
    new(
        3,
        "FIFA 23",
        "Sports",
        69.99m,
        new DateOnly(2022, 9, 27)
    ),
];
  // declare extension method to extend the functionality of WebApplication
  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {
    // Using route groups to group related endpoints
    var group = app.MapGroup("games").WithParameterValidation();

    // GET /games
    group.MapGet("/", () => games);

    // GET /games/1
    group.MapGet("/{id}", (int id) =>
    {
      GameDto? game = games.Find(game => game.Id == id);
      // 404 Not Found
      return game is null ? Results.NotFound() : Results.Ok(game);
    })
    .WithName(GetGameEndpointName);

    // POST /games
    // CreateGameDto is a record class that contains the properties of a game
    group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
    {

      Game game = new()
      {
        Name = newGame.Name,
        Genre = dbContext.Genres.Find(newGame.GenreId),
        GenreId = newGame.GenreId,
        Price = newGame.Price,
        ReleaseDate = newGame.ReleaseDate
      };

      dbContext.Add(game);
      dbContext.SaveChanges();

      GameDto gameDto = new(
        game.Id,
        game.Name,
        // ! null forgiving operator, is used to tell the compiler that Genre is not null
        game.Genre!.Name,
        game.Price,
        game.ReleaseDate
        );

      return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
    });

    // PUT /games/1
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
    {
      var index = games.FindIndex(game => game.Id == id);
      // 404 Not Found
      if (index == -1)
      {
        return Results.NotFound();
      }

      games[index] = new GameDto(
      id,
      updatedGame.Name,
      updatedGame.Genre,
      updatedGame.Price,
      updatedGame.ReleaseDate
  );

      return Results.NoContent();
    });

    // DELETE /games/1
    group.MapDelete("/{id}", (int id) =>
    {
      games.RemoveAll(game => game.Id == id);

      return Results.NoContent();
    });
    return group;
  }
}
