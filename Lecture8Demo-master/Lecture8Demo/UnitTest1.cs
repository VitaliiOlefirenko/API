using System.Net;
using FluentAssertions;
using Flurl;
using Flurl.Http;
using NUnit.Framework;

namespace Lecture8Demo
{
    public class Tests
    {
        private static readonly IFlurlClient flurlClient = new FlurlClient("https://jsonplaceholder.typicode.com");

        [Test]
        [Description("Get post by id 3")]
        public void GetTest()
        {
            PostModel expectedPost = new PostModel()
            {
                userId = 1,
                id = 3,
                title = "ea molestias quasi exercitationem repellat qui ipsa sit aut",
                body = "et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut"
            };

            var response = flurlClient
                .BaseUrl
                .AppendPathSegment("posts")
                .AppendPathSegments(expectedPost.id)
                .WithClient(flurlClient)
                .GetAsync()
                .Result;

            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var responseBody = response.ResponseMessage.Content.ReadAsStringAsync().Result;
            PostModel actualPost = DeserializationHelper.Deserialize(responseBody);

            actualPost.Should().BeEquivalentTo(expectedPost);
        }

        [Test]
        [Description("Create new post. POST is always for creating a resources")]
        public void PostTest()
        {
            PostModel expectedPost = new PostModel()
            {
                userId = 11,
                id = 101,
                title = "test title",
                body = "test body"
            };

            var response = flurlClient
                .BaseUrl
                .AppendPathSegment("posts")
                .WithClient(flurlClient)
                .PostJsonAsync(expectedPost)
                .Result;

            response.StatusCode.Should().Be((int)HttpStatusCode.Created);

            var responseBody = response.ResponseMessage.Content.ReadAsStringAsync().Result;
            PostModel actualPost = DeserializationHelper.Deserialize(responseBody);

            actualPost.Should().BeEquivalentTo(expectedPost);
        }

        [Test]
        [Description("Update post with id = 3. Put updates resource if exists otherwise creates new.")]
        public void PutTest()
        {
            PostModel expectedPost = new PostModel()
            {
                userId = 1,
                id = 3,
                title = "test title",
                body = "test body"
            };

            var response = flurlClient
                .BaseUrl
                .AppendPathSegment("posts")
                .AppendPathSegments(expectedPost.id)
                .WithClient(flurlClient)
                .PutJsonAsync(expectedPost)
                .Result;

            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var responseBody = response.ResponseMessage.Content.ReadAsStringAsync().Result;
            PostModel actualPost = DeserializationHelper.Deserialize(responseBody);

            actualPost.Should().BeEquivalentTo(expectedPost);
        }

        [Test]
        [Description("Update resource with id = 3. PATCH is always for update a resource, will fail if no object found")]
        public void PatchTest()
        {
            PostModel expectedPost = new PostModel()
            {
                userId = 1,
                id = 3,
                title = "test title",
                body = "test body"
            };

            var response = flurlClient
                .BaseUrl
                .AppendPathSegment("posts")
                .AppendPathSegments(expectedPost.id)
                .WithClient(flurlClient)
                .PutJsonAsync(expectedPost)
                .Result;

            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var responseBody = response.ResponseMessage.Content.ReadAsStringAsync().Result;
            PostModel actualPost = DeserializationHelper.Deserialize(responseBody);

            actualPost.Should().BeEquivalentTo(expectedPost);
        }

        [Test]
        [Description("Delete resource")]
        public void DeleteTest()
        {
            PostModel expectedPost = new PostModel()
            {
                userId = 1,
                id = 3,
                title = "test title",
                body = "test body"
            };

            var response = flurlClient
                .BaseUrl
                .AppendPathSegment("posts")
                .AppendPathSegments(expectedPost.id)
                .WithClient(flurlClient)
                .DeleteAsync()
                .Result;

            response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}