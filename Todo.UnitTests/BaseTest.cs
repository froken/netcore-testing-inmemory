﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net.Http;
using Todo.Database.Contexts;


namespace Todo.UnitTests
{
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
}
