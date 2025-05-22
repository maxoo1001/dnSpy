using System.Threading;
using System.Threading.Tasks;

namespace dnSpy.dnSpy.AI {
    /// <summary>
    /// Service for interacting with an AI model to explain code.
    /// </summary>
    public interface IAiCodeExplainer {
        /// <summary>
        /// Gets an explanation for the given code from an AI model.
        /// </summary>
        /// <param name="code">The code to explain.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A string containing the explanation, or null if an error occurred.</returns>
        Task<string?> ExplainCodeAsync(string code, CancellationToken cancellationToken);
    }
}
