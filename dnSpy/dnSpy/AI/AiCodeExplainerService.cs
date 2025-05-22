using System;
using System.ComponentModel.Composition; // Added for MEF Export
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using dnSpy.Contracts.App; 

namespace dnSpy.dnSpy.AI {
    /// <summary>
    /// Implements IAiCodeExplainer using a hypothetical AI API.
    /// </summary>
    [Export(typeof(IAiCodeExplainer))] // Added MEF Export attribute
    public class AiCodeExplainerService : IAiCodeExplainer {
        // TODO: Potentially inject IAppStatusbar or a logger service to report errors/status.
        // For now, errors will be indicated by a null return or exceptions.

        private const string ApiKeyEnvVar = "DNSPY_AI_API_KEY";
        // TODO: Replace with the actual API endpoint for the AI service you intend to use.
        private const string AiApiEndpoint = "https_YOUR_AI_API_ENDPOINT_HERE"; 
        private readonly string? _apiKey;
        private static readonly HttpClient httpClient = new HttpClient();

        public AiCodeExplainerService(/* Potentially inject other services here */) {
            _apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvVar);
            // Optional: Add a timeout to the HttpClient
            // httpClient.Timeout = TimeSpan.FromSeconds(30); 
        }

        /// <inheritdoc/>
        public async Task<string?> ExplainCodeAsync(string code, CancellationToken cancellationToken) {
            if (string.IsNullOrWhiteSpace(_apiKey)) {
                // Consider logging this error or showing a message to the user via IAppStatusbar
                Console.Error.WriteLine("AI Explainer: API key is not set. Please set the DNSPY_AI_API_KEY environment variable.");
                return "Error: AI API key is not configured. Please set the DNSPY_AI_API_KEY environment variable.";
            }

            if (string.IsNullOrWhiteSpace(code)) {
                return "Error: No code provided to explain.";
            }
            
            try {
                var requestBody = new {
                    model = "text-davinci-003", 
                    prompt = $"Explain the following C# code:

{code}

Explanation:",
                    max_tokens = 200 
                };
                string jsonRequestBody = $"{{"prompt": "Explain this code: {code}"}}";

                using (var request = new HttpRequestMessage(HttpMethod.Post, AiApiEndpoint)) {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
                    request.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                    // Using a placeholder since the API endpoint is not real yet
                    await Task.Delay(500, cancellationToken); 
                    return $"[Placeholder] AI explanation for the code: '{code.Substring(0, Math.Min(code.Length, 50))}...'";
                }
            }
            catch (HttpRequestException ex) {
                Console.Error.WriteLine($"AI Explainer: HTTP request failed. {ex.Message}");
                return $"Error: Could not connect to the AI service. {ex.Message}";
            }
            catch (TaskCanceledException) {
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
