// Purpose: Contains endpoints for the Games controller.
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;
// static class with extension methods, with one call we can map all the endpoints
public static class GamesEndpoints
{
  const string GetGameEndpointName = "GetGame";
  // list of games
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
  public static WebApplication MapGamesEndpoints(this WebApplication app)
  {
    // GET /games
    app.MapGet("/games", () => games);

    // GET /games/1
    app.MapGet("/games/{id}", (int id) =>
    {
      GameDto? game = games.Find(game => game.Id == id);
      // 404 Not Found
      return game is null ? Results.NotFound() : Results.Ok(game);
    })
        .WithName(GetGameEndpointName);

    // POST /games
    app.MapPost("/games", (CreateGameDto newGame) =>
    {
      GameDto game = new(
      games.Count + 1,
      newGame.Name,
      newGame.Genre,
      newGame.Price,
      newGame.ReleaseDate
  );
      games.Add(game);
      return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
    });

    // PUT /games/1
    app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
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
    app.MapDelete("/games/{id}", (int id) =>
    {
      games.RemoveAll(game => game.Id == id);

      return Results.NoContent();
    });
    return app;
  }
}
