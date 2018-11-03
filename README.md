# .Net Core Web Api Server + InMemory Integration Tests
Web Server contains a basic todo controller.
Test project runs the server with its database inmemory using Microsoft.AspNetCore.TestHost and Microsoft.EntityFrameworkCore.Sqlite.
Test classes inherit from the base test class:

```c#
[TestClass]
public class BaseTest
{
    public static SqliteConnection Connection { get; set; }
    public static TestServer Server { get; set; }
    public static HttpClient Client { get; set; }

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        StartDatabase();
        StartServer();
        UpdateDatabase();
    }

    private static void StartDatabase()
    {
        Connection = new SqliteConnection("DataSource=:memory:");
        Connection.Open();
        TestStartup.Connection = Connection;
    }

    private static void StartServer()
    {
        var webHostBuilder = WebHost.CreateDefaultBuilder(new string[] { })
            .UseStartup<TestStartup>()
            .UseContentRoot(Path.GetFullPath("../../../../Todo.Server"));

        Server = new TestServer(webHostBuilder);
        Client = Server.CreateClient();
    }

    private static void UpdateDatabase()
    {
        using (var context = Server.Host.Services.GetService<TodoContext>())
        {
            context.Database.EnsureCreated();
        }
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        Server.Dispose();
        Client.Dispose();
        Connection.Close();
    }
}
```

Test classes are using a prepared HttpClient and look quite simple:
```c#
[TestClass]
public class TodoTaskTests : BaseTest
{
    [TestMethod]
    public async Task AddTask_TaskIsValid_ReturnCreated()
    {
        // Arrange
        var task = new TodoTaskModel
        {
            Text = "Test solution"
        };
        var taskContent = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/api/tasks", taskContent);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
}
