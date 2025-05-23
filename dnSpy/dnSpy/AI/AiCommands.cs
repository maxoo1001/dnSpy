using System.Windows.Input;

namespace dnSpy.AI {
    public static class AiCommands {
        public static readonly RoutedUICommand ExplainCodeWithAi = new RoutedUICommand(
            "Explain Code with AI", 
            "ExplainCodeWithAi", 
            typeof(AiCommands)
            // Optional: Add InputGestures here, e.g., new InputGestureCollection() { new KeyGesture(Key.E, ModifierKeys.Control | ModifierKeys.Alt) }
        );
    }
}
