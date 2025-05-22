using System;
using System.ComponentModel.Composition;
using System.Windows; // Required for UIElement
using System.Windows.Input; // Required for ICommandSource
using dnSpy.Contracts.Menus;
using dnSpy.Contracts.Documents.Tabs.DocViewer; // Required for IDocumentViewer
// using dnSpy.Properties; // For dnSpy_Resources if needed for header. Uncomment if dnSpy_Resources.ExplainCodeWithAiMenuHeader is defined.

namespace dnSpy.dnSpy.AI {
    [ExportMenuItem(Header = "Explain Code with AI", Group = MenuConstants.GROUP_CTX_DOCVIEWER_EDITOR, Order = 100)]
    sealed class ExplainCodeWithAiMenuItem : MenuItemBase {
        
        [ImportingConstructor]
        ExplainCodeWithAiMenuItem() {
            // Constructor can be empty or import services if needed directly by this menu item's logic
            // For now, we delegate to the command already bound in DocumentViewer.
        }

        public override void Execute(IMenuItemContext context) {
            // The command is AiCommands.ExplainCodeWithAi
            // The DocumentViewerControl (or its TextView) should have the CommandBinding.
            // We need to find an ICommandSource or UIElement in the context to execute it on.
            var documentViewer = context.Find<IDocumentViewer>();
            if (documentViewer?.UIObject is UIElement commandTarget) {
                 // It's good practice to check CanExecute before Execute, though the menu item's IsEnabled should cover this.
                 if (AiCommands.ExplainCodeWithAi.CanExecute(null, commandTarget)) {
                    AiCommands.ExplainCodeWithAi.Execute(null, commandTarget);
                }
            }
            // Alternative if direct execution on context/creator object is possible (less likely for RoutedUICommand)
            // else if (context.CreatorObject is UIElement creatorCommandTarget && AiCommands.ExplainCodeWithAi.CanExecute(null, creatorCommandTarget)) {
            //    AiCommands.ExplainCodeWithAi.Execute(null, creatorCommandTarget);
            // }
        }

        public override bool IsEnabled(IMenuItemContext context) {
            var documentViewer = context.Find<IDocumentViewer>();
            if (documentViewer?.UIObject is UIElement commandTarget) {
                return AiCommands.ExplainCodeWithAi.CanExecute(null, commandTarget);
            }
            return false;
        }

        public override bool IsVisible(IMenuItemContext context) {
            // Only show this in the document viewer's text editor context
            // MenuConstants.GUIDOBJ_DOCUMENTVIEWERCONTROL_GUID is the context for the text view itself.
            return context.CreatorObject?.Guid == new Guid(MenuConstants.GUIDOBJ_DOCUMENTVIEWERCONTROL_GUID);
        }

        // Optionally, if you want the header to be dynamic or come from resources:
        // public override string GetHeader(IMenuItemContext context) => dnSpy_Resources.ExplainCodeWithAiMenuHeader; 
        // Ensure dnSpy_Resources.ExplainCodeWithAiMenuHeader is defined in your Resources.resx or similar.
    }
}
