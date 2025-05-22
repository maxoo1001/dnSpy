# AI Code Explanation Feature

This directory contains components related to the AI-powered code explanation feature in dnSpy.

## Configuration

To use the AI code explanation feature, you need to provide an API key for the AI service. This is done by setting an environment variable:

1.  **Obtain an API Key:**
    *   You will need to get an API key from an AI provider that offers code explanation capabilities (e.g., OpenAI, or another similar service).
    *   The service endpoint is currently a placeholder (`https_YOUR_AI_API_ENDPOINT_HERE`) in `AiCodeExplainerService.cs` and will need to be updated to point to your chosen provider's API.

2.  **Set the Environment Variable:**
    *   Name: `DNSPY_AI_API_KEY`
    *   Value: Your actual API key from the AI provider.

    **How to set an environment variable:**
    *   **Windows:**
        *   You can set it temporarily in a Command Prompt for the current session:
          ```cmd
          set DNSPY_AI_API_KEY=your_api_key_here
          ```
        *   Or set it permanently through System Properties:
            1.  Search for "environment variables" in the Start Menu.
            2.  Click "Edit the system environment variables."
            3.  Click the "Environment Variables..." button.
            4.  In the "User variables" or "System variables" section, click "New..."
            5.  Variable name: `DNSPY_AI_API_KEY`
            6.  Variable value: `your_api_key_here`
            7.  Click OK on all dialogs. You might need to restart dnSpy or your computer for the changes to take full effect.
    *   **Linux/macOS:**
        *   You can set it temporarily in your terminal for the current session:
          ```bash
          export DNSPY_AI_API_KEY='your_api_key_here'
          ```
        *   To make it permanent, add the `export` line to your shell's configuration file (e.g., `~/.bashrc`, `~/.zshrc`, `~/.profile`) and then source the file or restart your terminal. For example:
          ```bash
          echo "export DNSPY_AI_API_KEY='your_api_key_here'" >> ~/.bashrc
          source ~/.bashrc
          ```

3.  **Restart dnSpy:**
    *   If dnSpy was running while you set the environment variable, you'll likely need to restart it to pick up the new configuration.

## Current Status

*   The AI service integration is currently using a **placeholder implementation**.
*   You will need to modify `AiCodeExplainerService.cs` to integrate with a real AI service API endpoint and handle its specific request/response format.
