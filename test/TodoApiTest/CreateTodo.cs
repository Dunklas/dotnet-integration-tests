using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TodoApiTest
{
    public class CreateTodo 
        : IClassFixture<InMemoryWebApplicationFactory<TodoApi.Startup>>
    {
        private readonly HttpClient client;
        private readonly ITestOutputHelper output;

        public CreateTodo(InMemoryWebApplicationFactory<TodoApi.Startup> factory, ITestOutputHelper outputHelper)
        {
            client = factory.CreateClient();
            output = outputHelper;
        }

        [Fact]
        public async void ShouldCreateTodo()
        {
            var response = await PostTodo("Ta helg", false);
            response.EnsureSuccessStatusCode();
        }

        private async Task<HttpResponseMessage> PostTodo(string name, bool completed)
        {
            var isCompleted = completed.ToString().ToLower();
            return await client.PostAsync("https://localhost:5001/api/TodoItems",
                new StringContent(
                    $"{{\"Name\": \"{name}\", \"IsComplete\": {isCompleted} }}",
                    Encoding.UTF8,
                    "application/json"
                ));
        }
    }
}
