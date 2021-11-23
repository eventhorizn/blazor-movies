using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Shared.DTO;

public class IndexPageDTO
{
    public List<Movie>? InTheaters { get; set; }
    public List<Movie>? UpcomingReleases { get; set; }
}