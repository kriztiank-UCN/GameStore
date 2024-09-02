// Purpose: Contains endpoints for the Games controller.
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;
// static class with extension methods, with one call we can map all the endpoints
public static class GamesEndpoints
{
  const string GetGameEndpointName = "GetGame";
  
  // declare extension method to extend the functionality of WebApplication
  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {
    // Using route groups to group related endpoints
    var group = app.MapGroup("games").WithParameterValidation();

    // GET /games
    group.MapGet("/", (GameStoreContext DbContext) =>
        DbContext.Games
            .Include(game => game.Genre)
            .Select(game => game.ToGameSummaryDto())
            .AsNoTracking());

    // GET /games/1
    group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
    {
      Game? game = dbContext.Games.Find(id);
      // 404 Not Found
      return game is null
        ? Results.NotFound()
        : Results.Ok(game.ToGameDetailsDto());
    })
    .WithName(GetGameEndpointName);

    // POST /games
    // CreateGameDto is a record class that contains the properties of a game
    group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
    {
      // Create entity from DTO (ToEntity is passed in from GameMapping.cs)
      Game game = newGame.ToEntity();

      // Add entity to the database
      dbContext.Add(game);
      dbContext.SaveChanges();

      // Return back to the client (ToDto is passed in from GameMapping.cs)
      return Results.CreatedAtRoute(
        GetGameEndpointName,
        new { id = game.Id },
        game.ToGameDetailsDto());
    });

    // PUT /games/1
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
    {
      var existingGame = dbContext.Games.Find(id);

      // 404 Not Found
      if (existingGame is null)
      {
        return Results.NotFound();
      }
      // Perform Update, replace existing entity with a new entity from the dto
      dbContext.Entry(existingGame)
          .CurrentValues
          .SetValues(updatedGame.ToEntity(id));

      dbContext.SaveChanges();

      return Results.NoContent();
    });

    // DELETE /games/1
    group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
    {
      dbContext.Games
          .Where(game => game.Id == id)
          .ExecuteDelete();

      return Results.NoContent();
    });
    return group;
  }
}
