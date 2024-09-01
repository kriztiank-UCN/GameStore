// Purpose: DTO for creating a game.
// A DTO is a contract with the client, it defines how the data will be sent to the client and must be kept at all times.
using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto
(
  [Required][StringLength(50)] string Name,
  int GenreId,
  [Range(1, 100)] decimal Price,
  DateOnly ReleaseDate
);
