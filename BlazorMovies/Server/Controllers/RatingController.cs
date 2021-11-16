using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public RatingController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Rate(MovieRating movieRating)
    {
        var currentRating = await context.MovieRatings
            .FirstOrDefaultAsync(x => x.MovieId == movieRating.MovieId);

        if (currentRating == null)
        {
            movieRating.RatingDate = DateTime.Today;
            context.Add(movieRating);
            await context.SaveChangesAsync();
        }
        else
        {
            currentRating.Rate = movieRating.Rate;
            await context.SaveChangesAsync();
        }

        return NoContent();
    }
}