using System;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Net.Http.Headers; // Added for AuthenticationHeaderValue
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// using dnSpy.Contracts.App; // Example: For IAppStatusbar (optional for now)
// using Newtonsoft.Json; // Example: If you were to use Newtonsoft.Json

namespace dnSpy.AI {
    [Export(typeof(IAiCodeExplainer))]
    public class AiCodeExplainerService : IAiCodeExplainer {
        private const string ApiKeyEnvVar = "DNSPY_AI_API_KEY";
        // TODO: Replace with the actual API endpoint for the AI service you intend to use.
        private const string AiApiEndpoint = "https_YOUR_AI_API_ENDPOINT_HERE"; // Ensure this is a valid string
        private readonly string? _apiKey;
        private static readonly HttpClient httpClient = new HttpClient();

        public AiCodeExplainerService() {
            _apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvVar);
            // httpClient.Timeout = TimeSpan.FromSeconds(30); // Optional timeout
        }

        public async Task<string?> ExplainCodeAsync(string code, CancellationToken cancellationToken) {
            if (string.IsNullOrWhiteSpace(_apiKey)) {
                Console.Error.WriteLine("AI Explainer: API key is not set. Please set the DNSPY_AI_API_KEY environment variable.");
                return "Error: AI API key is not configured. Please set the DNSPY_AI_API_KEY environment variable.";
            }

            if (string.IsNullOrWhiteSpace(code)) {
                Console.Error.WriteLine("AI Explainer: No code provided to explain.");
                return "Error: No code provided to explain.";
            }
            
            try {
                // Correctly escaped JSON string
                string jsonRequestBody = "{\"prompt\": \"Explain this code snippet.\", \"model\": \"text-davinci-003\", \"max_tokens\": 200}";

                using (var request = new HttpRequestMessage(HttpMethod.Post, AiApiEndpoint)) {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                    request.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                    // Actual API call is commented out as endpoint is a placeholder
                    // HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken);
                    //
                    // if (response.IsSuccessStatusCode) {
                    //     string responseBody = await response.Content.ReadAsStringAsync();
                    //     // TODO: Parse the responseBody to extract the explanation
                    //     // Example (highly dependent on actual API):
                    //     // var parsedResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    //     // return parsedResponse?.choices[0]?.text?.ToString()?.Trim();
                    //     return $"AI Explanation (length: {code.Length}): {responseBody}"; // Placeholder
                    // }
                    // else {
                    //     string errorContent = await response.Content.ReadAsStringAsync();
                    //     Console.Error.WriteLine($"AI Explainer: API call failed with status {response.StatusCode}. Details: {errorContent}");
                    //     return $"Error: AI service returned status {response.StatusCode}. Details: {errorContent}";
                    // }

                    // Using a placeholder since the API endpoint is not real yet
                    await Task.Delay(500, cancellationToken); // Simulate network delay
                    return $"[Placeholder] AI explanation for the code: '{code.Substring(0, Math.Min(code.Length, 50))}...'";
                }
            }
            catch (HttpRequestException ex) {
                Console.Error.WriteLine($"AI Explainer: HTTP request failed. {ex.Message}");
                return $"Error: Could not connect to the AI service. {ex.Message}";
            }
            catch (TaskCanceledException) {
                // This catch block must come before the generic Exception catch block
                Console.Error.WriteLine("AI Explainer: Code explanation request was canceled.");
                return "Error: Code explanation request was canceled.";
            }
            catch (Exception ex) {
                Console.Error.WriteLine($"AI Explainer: An unexpected error occurred. {ex.ToString()}");
                return "Error: An unexpected error occurred while trying to get the explanation.";
            }
        }
    }
}
