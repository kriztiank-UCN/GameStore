// Purpose: DTO for Game entity.
// A DTO is a contract with the client, it defines how the data will be sent to the client and must be kept at all times.
namespace GameStore.Api.Dtos;

// a record is imutable
public record class GameDetailsDto
(
  int Id,
  string Name,
  int GenreId,
  decimal Price,
  DateOnly ReleaseDate
);