using Xunit;
using Text_Similarity.Controllers;
using Text_Similarity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Text_Similarity.Tests
{
    public class SimilaritiesControllerTests
    {
        private SimilaritiesController _controller;

        public SimilaritiesControllerTests()
        {
            _controller = new SimilaritiesController();
        }

        [Fact]
        public void Post_WithValidInput_ReturnsCorrectSimilarity()
        {
            // Arrange
            var request = new SimilarityRequest
            {
                Text1 = "The quick brown fox jumps over the lazy dog",
                Text2 = "The dog was not amused"
            };

            // Act
            IActionResult actionResult = _controller.Post(request);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            SimilarityResponse response = Assert.IsType<SimilarityResponse>(okResult.Value);

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(response);
            Assert.Equal(36.67, response.Similarity, 2); // Assert similarity with a precision of 2 decimal places
        }

        [Fact]
        public void Post_WithMissingInput_ReturnsBadRequest()
        {
            // Arrange
            var request = new SimilarityRequest
            {
                Text1 = "", // Missing input
                Text2 = "The dog was not amused"
            };

            // Act
            IActionResult actionResult = _controller.Post(request);
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            string errorMessage = Assert.IsType<string>(badRequestResult.Value);

            // Assert
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Both text1 and text2 are required.", errorMessage);
        }

        [Fact]
        public void Post_WithIdenticalTexts_ReturnsHundredPercentSimilarity()
        {
            // Arrange
            var request = new SimilarityRequest
            {
                Text1 = "The quick brown fox jumps over the lazy dog",
                Text2 = "The quick brown fox jumps over the lazy dog"
            };

            // Act
            IActionResult actionResult = _controller.Post(request);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            SimilarityResponse response = Assert.IsType<SimilarityResponse>(okResult.Value);

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(response);
            Assert.Equal(100.00, response.Similarity, 2); // Assert similarity with a precision of 2 decimal places
        }

        [Fact]
        public void Post_WithNoCommonWords_ReturnsZeroSimilarity()
        {
            // Arrange
            var request = new SimilarityRequest
            {
                Text1 = "The quick brown fox",
                Text2 = "Jump over lazy dog"
            };

            // Act
            IActionResult actionResult = _controller.Post(request);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            SimilarityResponse response = Assert.IsType<SimilarityResponse>(okResult.Value);

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(response);
            Assert.Equal(0.00, response.Similarity, 2); // Assert similarity with a precision of 2 decimal places
        }

        [Fact]
        public void Post_WithMixedCaseTexts_ReturnsCorrectSimilarity()
        {
            // Arrange
            var request = new SimilarityRequest
            {
                Text1 = "The Quick Brown Fox",
                Text2 = "the quick brown fox"
            };

            // Act
            IActionResult actionResult = _controller.Post(request);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            SimilarityResponse response = Assert.IsType<SimilarityResponse>(okResult.Value);

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(response);
            Assert.Equal(100.00, response.Similarity, 2); // Assert similarity with a precision of 2 decimal places
        }
    }
}
