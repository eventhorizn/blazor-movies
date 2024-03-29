﻿using AutoMapper;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IFileStorageService fileStorageService;
    private readonly IMapper mapper;

    public PeopleController(ApplicationDbContext context,
        IFileStorageService fileStorageService,
        IMapper mapper)
    {
        this.context = context;
        this.fileStorageService = fileStorageService;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Person>>> Get([FromQuery] PaginationDTO pagintationDTO)
    {
        var queryable = context.People.AsQueryable();
        await HttpContext.InsertPaginationParametersInResponse(queryable, pagintationDTO.RecordsPerPage);
        return await queryable.Paginate(pagintationDTO).ToListAsync();
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Person>> Get(int id)
    {
        var person = await context.People
            .Include(x => x.MovieActors).ThenInclude(x => x.Movie)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (person == null) { return NotFound(); }
        return person;
    }

    [HttpGet("search/{searchText}")]
    public async Task<ActionResult<List<Person>>> FilterByName(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText)) { return new List<Person>(); }
        return await context.People.Where(x => x.Name.Contains(searchText))
            .Take(5)
            .ToListAsync();
    }


    [HttpPost]
    public async Task<ActionResult<int>> Post(Person person)
    {
        if (!string.IsNullOrWhiteSpace(person.Picture))
        {
            var personPicture = Convert.FromBase64String(person.Picture);
            person.Picture = await fileStorageService.SaveFile(personPicture, "jpg", "people");
        }

        context.Add(person);
        await context.SaveChangesAsync();
        return person.Id;
    }

    [HttpPut]
    public async Task<ActionResult> Put(Person person)
    {
        var personDB = await context.People.FirstOrDefaultAsync(x => x.Id == person.Id);

        if (personDB == null) { return NotFound(); }

        personDB = mapper.Map(person, personDB);

        if (!string.IsNullOrEmpty(person.Picture))
        {
            var personPicture = Convert.FromBase64String(person.Picture);
            personDB.Picture = await fileStorageService.EditFile(personPicture, "jpg", "people", personDB.Picture);
        }

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
        if (person == null)
        {
            return NotFound();
        }

        context.Remove(person);
        await context.SaveChangesAsync();
        return NoContent();
    }
}