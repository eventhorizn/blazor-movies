﻿using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.Entities;

namespace BlazorMovies.Client.Repository;

public interface IPersonRepository
{
    Task CreatePerson(Person person);
    Task DeletePerson(int id);
    Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO paginationDTO);
    Task<List<Person>> GetPeopleByName(string name);
    Task<Person> GetPersonById(int id);
    Task UpdatePerson(Person person);
}