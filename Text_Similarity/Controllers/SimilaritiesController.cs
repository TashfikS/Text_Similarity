using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Text_Similarity.Models;

namespace Text_Similarity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimilaritiesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] SimilarityRequest request)
        {
            // Validate input data
            if (string.IsNullOrWhiteSpace(request.Text1) || string.IsNullOrWhiteSpace(request.Text2))
            {
                return BadRequest("Both text1 and text2 are required.");
            }

            // Split the texts into words and convert to lowercase
            string[] text1Words = SplitAndNormalize(request.Text1);
            string[] text2Words = SplitAndNormalize(request.Text2);

            // Calculate the percentage of unique words in text1 that also appear in text2
            double similarity1 = CalculateSimilarity(text1Words, text2Words);
         
            // Calculate the percentage of unique words in text2 that also appear in text1
            double similarity2 = CalculateSimilarity(text2Words, text1Words);
        
            // Calculate the average similarity
            double averageSimilarity = (similarity1 + similarity2) / 2;

            return Ok(new SimilarityResponse { Similarity = averageSimilarity });
        }

        private string[] SplitAndNormalize(string text)
        {
            return text.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.ToLowerInvariant())
                .ToArray();
        }

        private double CalculateSimilarity(string[] words1, string[] words2)
        {
            int matchingWordCount = words1.Count(word => words2.Contains(word, StringComparer.OrdinalIgnoreCase));
            Debug.WriteLine(words1.Length);

            double similarity = Math.Ceiling(((double)matchingWordCount / words1.Length * 100) * 100) / 100;



            return similarity;
        }
    }

}
