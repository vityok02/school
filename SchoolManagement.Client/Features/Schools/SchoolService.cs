using SchoolManagement.Client.Constants;
using SchoolManagement.Client.Features.Schools.Dtos;

namespace SchoolManagement.Client.Features.Schools;

public class SchoolService : ISchoolService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;

    public SchoolService(IHttpClientFactory _clientFactory)
    {
        _httpClient = _clientFactory.CreateClient(ApiName.Value);
        _baseAddress = ($"{_httpClient.BaseAddress!.OriginalString}/schools");
    }

    public async Task<IEnumerable<SchoolItem>?> GetSchools()
    {
        return await _httpClient
            .GetFromJsonAsync<IEnumerable<SchoolItem>>(_baseAddress);
    }

    public async Task<School> GetSchool(int schoolId)
    {
        var result = await _httpClient
            .GetFromJsonAsync<School>($"{_baseAddress}/{schoolId}");

        return result!;
    }

    public async Task<int> CreateSchool(School school)
    {
        var response = await _httpClient
            .PostAsJsonAsync(_baseAddress, school);

        if (response.IsSuccessStatusCode)
        {
            var schoolFromResponse = await response.Content
                .ReadFromJsonAsync<School>();

            return schoolFromResponse!.Id;
        }

        return 0;
    }

    public async Task UpdateSchool(School school)
    {
        await _httpClient
            .PutAsJsonAsync($"{_baseAddress}/{school.Id}", school);
    }

    public async Task DeleteSchool(int schoolId)
    {
        await _httpClient
            .DeleteAsync($"{_baseAddress}/{schoolId}");
    }
}
