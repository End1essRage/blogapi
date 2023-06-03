using BlogApi.Api.Controllers;
using BlogApi.Data.Models;
using BlogApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BlogApi.Test.Controllers
{
    public class ArticlesControllerTest
    {
        private Mock<IArticlesRepository> articlesRepoMock;
        private ArticlesController controller;

        public ArticlesControllerTest()
        {
            articlesRepoMock = new Mock<IArticlesRepository>(); 
            controller = new ArticlesController(articlesRepoMock.Object);
        }

        [Fact]
        public async Task GetAllArticlesTest_ReturnsArticlesList()
        {
            // Arrange
            var mockArticlesList = new List<Article>
            {
                new Article { Title = "mock article 1" },
                new Article { Title = "mock article 2" }
            };
            articlesRepoMock.Setup(repo => repo.GetAll()).Returns(Task.FromResult(mockArticlesList));

            // Act
            var result = await controller.GetAllArticles();

            // Assert
            Assert.Equal(mockArticlesList, result);
        }

        [Fact]
        public async Task GetArticleByIdTest_ReturnsNotFound_WhenArticleDoesNotExists()
        {
            // Arrange
            var mockId = 42;
            articlesRepoMock.Setup(repo => repo.GetOne(mockId)).Returns(Task.FromResult<Article>(null));

            // Act
            var result = await controller.GetArticleById(mockId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetArticleByIdTest_ReturnsArticle_WhenArticleExists()
        {
            // Arrange
            var mockId = 42;
            var mockArticle = new Article { Title = "mock article" };
            articlesRepoMock.Setup(repo => repo.GetOne(mockId)).Returns(Task.FromResult(mockArticle));

            // Act
            var result = await controller.GetArticleById(mockId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(mockArticle, actionResult.Value);
        }

        [Fact]
        public async Task AddArticleTest_ReturnsArticleSuccessfullyAdded()
        {
            // Arrange
            var mockArticle = new Article { Title = "mock article" };
            articlesRepoMock.Setup(repo => repo.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await controller.AddArticle(mockArticle);

            // Assert
            articlesRepoMock.Verify(repo => repo.Add(mockArticle));
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(mockArticle, actionResult.Value);
        }

        [Fact]
        public async Task DeleteArticleTest_ReturnsNotFound_WhenArticleDoesNorExists()
        {
            // Arrange
            var mockId = 42;
            articlesRepoMock.Setup(repo => repo.GetOne(mockId)).Returns(Task.FromResult<Article>(null));

            // Act
            var result = await controller.DeleteArticle(mockId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteArticleTest_ReturnsSuccessCode_AfterRemovingArticleFromRepository()
        {
            // Arrange
            var mockId = 42;
            var mockArticle = new Article { Title = "mock article" };
            articlesRepoMock.Setup(repo => repo.GetOne(mockId)).Returns(Task.FromResult(mockArticle));
            articlesRepoMock.Setup(repo => repo.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteArticle(mockId);

            // Assert
            articlesRepoMock.Verify(repo => repo.Remove(mockArticle));
            Assert.IsType<NoContentResult>(result);
        }
    }
}
