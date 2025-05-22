using Microsoft.VisualStudio.TestTools.UnitTesting;
using dnSpy.dnSpy.AI; // Make sure this using directive can resolve
using System.Threading;
using System.Threading.Tasks;
using System;

namespace dnSpy.dnSpy.Tests.AI { // Adjust namespace to match test project structure
    [TestClass]
    public class AiCodeExplainerServiceTests {
        private const string TestApiKeyEnvVar = "DNSPY_AI_API_KEY";
        private string? _originalApiKey;

        [TestInitialize]
        public void TestInitialize() {
            _originalApiKey = Environment.GetEnvironmentVariable(TestApiKeyEnvVar);
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, null); // Clear for each test
        }

        [TestCleanup]
        public void TestCleanup() {
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, _originalApiKey);
        }

        [TestMethod]
        public async Task ExplainCodeAsync_NullApiKey_ReturnsError() {
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, null);
            var service = new AiCodeExplainerService();
            var result = await service.ExplainCodeAsync("code", CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("API key is not configured"));
        }

        [TestMethod]
        public async Task ExplainCodeAsync_EmptyApiKey_ReturnsError() {
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, string.Empty);
            var service = new AiCodeExplainerService();
            var result = await service.ExplainCodeAsync("code", CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("API key is not configured"));
        }

        [TestMethod]
        public async Task ExplainCodeAsync_NullCode_ReturnsError() {
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, "fake-api-key");
            var service = new AiCodeExplainerService();
            var result = await service.ExplainCodeAsync(null!, CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("No code provided to explain"));
        }

        [TestMethod]
        public async Task ExplainCodeAsync_EmptyCode_ReturnsError() {
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, "fake-api-key");
            var service = new AiCodeExplainerService();
            var result = await service.ExplainCodeAsync(string.Empty, CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("No code provided to explain"));
        }

        [TestMethod]
        public async Task ExplainCodeAsync_WhitespaceCode_ReturnsError() {
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, "fake-api-key");
            var service = new AiCodeExplainerService();
            var result = await service.ExplainCodeAsync("   ", CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("No code provided to explain"));
        }

        [TestMethod]
        public async Task ExplainCodeAsync_ValidKeyAndCode_ReturnsPlaceholder() {
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, "fake-api-key");
            var service = new AiCodeExplainerService();
            string code = "public void HelloWorld() {}";
            var result = await service.ExplainCodeAsync(code, CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StartsWith("[Placeholder] AI explanation"));
        }

        [TestMethod]
        public async Task ExplainCodeAsync_CancellationRequested_ReturnsCancellationError() {
            Environment.SetEnvironmentVariable(TestApiKeyEnvVar, "fake-api-key");
            var service = new AiCodeExplainerService();
            var cts = new CancellationTokenSource();
            cts.Cancel();
            var result = await service.ExplainCodeAsync("code", cts.Token);
            Assert.IsNotNull(result);
            Assert.AreEqual("Error: Code explanation request was canceled.", result);
        }
    }
}
