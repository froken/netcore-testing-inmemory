using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Todo.Server.Models;

namespace Todo.UnitTests
{
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

        [TestMethod]
        public async Task AddAndGetTask_TaskIsValid_ReturnTask()
        {
            // Arrange
            var task = new TodoTaskModel
            {
                Text = "Test add and get"
            };

            var taskContent = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

            // Act
            var postResponse = await Client.PostAsync("/api/tasks", taskContent);
            var postResult = await postResponse.Content.ReadAsAsync<TodoTaskModel>();
            var getResponse = await Client.GetAsync(string.Format("/api/tasks/{0}", postResult.Id));
            var getResult = await getResponse.Content.ReadAsAsync<TodoTaskModel>();

            // Assert
            Assert.AreEqual(task.Text, getResult.Text);
        }
    }
}
