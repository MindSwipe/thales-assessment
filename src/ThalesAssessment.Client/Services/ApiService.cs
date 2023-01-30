using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using ThalesAssessment.ApiModels.RequestModels;
using ThalesAssessment.Entities;
using ThalesAssessment.Entities.Settings;

namespace ThalesAssessment.Client.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(AppSettings appSettings)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(appSettings.ApiUrl);
    }

    public async Task<List<Person>?> GetAllPersons()
    {
        return await _httpClient.GetFromJsonAsync<List<Person>>("/person/getAll");
    }

    public async Task<List<Role>?> GetAllRoles()
    {
        return await _httpClient.GetFromJsonAsync<List<Role>>("/role/getAll");
    }

    public async Task AssignRoleToPerson(Person person, Role role)
    {
        await _httpClient.PostAsJsonAsync("/person/assignRoleToPerson", new AssignRoleToPersonModel
        {
            PersonId = person.Id,
            RoleId = role.Id,
        });
    }

    public async Task CreateNewRole(string name)
    {
        await _httpClient.PostAsJsonAsync("/role/create", new Role
        {
            Name = name,
        });
    }
}
