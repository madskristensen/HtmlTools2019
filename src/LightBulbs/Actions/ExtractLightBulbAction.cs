using MadsKristensen.EditorExtensions.Html;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.WebTools.Languages.Html.Tree.Nodes;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace HtmlTools
{
    internal class HtmlExtractLightBulbAction : HtmlSuggestedActionBase
    {
        public HtmlExtractLightBulbAction(ITextView textView, ITextBuffer textBuffer, ElementNode element)
            : base(textView, textBuffer, element, "Extract to File...")
        { }

        public override async void Invoke(CancellationToken cancellationToken)
        {
            string root = ProjectHelpers.GetProjectFolder(ProjectHelpers.DTE.ActiveDocument.FullName);

            if (CanSaveFile(root, out string file))
            {
                await MakeChanges(root, file);
            }
        }

        private bool CanSaveFile(string folder, out string fileName)
        {
            string ext = Element.IsStyleBlock() ? "css" : "js";

            fileName = null;

            using (var dialog = new SaveFileDialog())
            {
                dialog.FileName = "file." + ext;
                dialog.DefaultExt = "." + ext;
                dialog.Filter = ext.ToUpperInvariant() + " files | *." + ext;
                dialog.InitialDirectory = folder;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                fileName = dialog.FileName;
            }

            return true;
        }

        private async Task MakeChanges(string root, string fileName)
        {
            string text = Element.GetText(Element.InnerRange).Trim();
            string reference = GetReference(Element, fileName, root);

            try
            {
                ProjectHelpers.DTE.UndoContext.Open(DisplayText);
                using (ITextEdit edit = TextBuffer.CreateEdit())
                {
                    edit.Replace(new Span(Element.Start, Element.Length), reference);
                    edit.Apply();
                }

                File.WriteAllText(fileName, text);
                ProjectHelpers.AddFileToActiveProject(fileName);
                ProjectHelpers.DTE.ItemOperations.OpenFile(fileName);

                await Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                {
                    ProjectHelpers.DTE.ExecuteCommand("Edit.FormatDocument");

                }), DispatcherPriority.ApplicationIdle, null);
            }
            finally
            {
                ProjectHelpers.DTE.UndoContext.Close();
            }
        }

        private static string GetReference(ElementNode element, string fileName, string root)
        {
            string relative = FileHelpers.RelativePath(root, fileName);
            string reference = "<script src=\"/{0}\"></script>";

            if (element.IsStyleBlock())
            {
                reference = "<link rel=\"stylesheet\" href=\"/{0}\" />";
            }

            return string.Format(CultureInfo.CurrentCulture, reference, relative);
        }
    }
}
