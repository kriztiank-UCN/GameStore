// Purpose: DTO for GameSummaryDto entity.
// A DTO is a contract with the client, it defines how the data will be sent to the client and must be kept at all times.
namespace GameStore.Api.Dtos;

// a record is imutable
public record class GameSummaryDto
(
  int Id,
  string Name,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate
);