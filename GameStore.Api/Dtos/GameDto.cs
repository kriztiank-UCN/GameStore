namespace GameStore.Api.Dtos;

// a record is imutable
public record class GameDto
(
  int Id,
  string Name,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate
);