using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using UniversityApi;
using UniversityApi.Data;
using UniversityApi.Models;
using UniversityApi.Repository;

namespace UniversityApiTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetStudents()
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/university/students");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }

        [Test]
        public async Task GetStudentResponse()
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
            var client = factory.CreateClient();

            var expectedPayload = new List<Student>()
            {
                new Student { Id = 1, Name = "John Doe" },
                new Student { Id = 2, Name = "Jane Doe" },
                new Student { Id = 3, Name = "John Smith" },
                new Student { Id = 4, Name = "Jane Smith" }
            };

            // Act
            var response = await client.GetAsync("/university/students");

            var responsePayload = response.Content.ReadFromJsonAsync<IEnumerable<Student>>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            Assert.That(responsePayload.Result.Count(), Is.EqualTo(4));
            Assert.That(responsePayload.Result.ElementAtOrDefault(0).Id, Is.EqualTo(expectedPayload[0].Id));
            Assert.That(responsePayload.Result.ElementAtOrDefault(0).Name, Is.EqualTo(expectedPayload[0].Name));

        }
    }
}