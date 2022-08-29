
using ConsoleUI;
using System.Net.Http.Headers;
using System.Net.Http.Json;

HttpClient client = new();
client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

HttpResponseMessage response = await client.GetAsync("/todos");

if (response.IsSuccessStatusCode)
{
    var issues = await response.Content.ReadFromJsonAsync<IList<IssueDto>>();

	if (issues == null) throw new ArgumentNullException(nameof(issues));

	foreach (var issue in issues)
	{
		Console.WriteLine(issue.title);
	}

}
else
{
	Console.WriteLine("No Results!");
}