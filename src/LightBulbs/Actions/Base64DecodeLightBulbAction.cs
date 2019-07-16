using MadsKristensen.EditorExtensions.Html;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.WebTools.Languages.Html.Tree.Nodes;
using Microsoft.WebTools.Languages.Shared.Editor.EditorHelpers;
using Microsoft.WebTools.Languages.Shared.Editor.Text;
using System.Threading;

namespace HtmlTools
{
    internal class HtmlBase64DecodeLightBulbAction : HtmlSuggestedActionBase
    {
        public HtmlBase64DecodeLightBulbAction(ITextView textView, ITextBuffer textBuffer, ElementNode element, AttributeNode attribute)
            : base(textView, textBuffer, element, attribute, "Save as File...")
        { }

        public override void Invoke(CancellationToken cancellationToken)
        {
            string mimeType = FileHelpers.GetMimeTypeFromBase64(Attribute.Value);
            string extension = FileHelpers.GetExtension(mimeType) ?? "png";

            string fileName = FileHelpers.ShowDialog(extension);

            if (!string.IsNullOrEmpty(fileName) && FileHelpers.SaveDataUriToFile(Attribute.Value, fileName))
            {
                string relative = FileHelpers.RelativePath(TextBuffer.GetFileName(), fileName);

                try
                {
                    ProjectHelpers.DTE.UndoContext.Open(DisplayText);
                    using (ITextEdit edit = TextBuffer.CreateEdit())
                    {
                        edit.Replace(Attribute.ValueRangeUnquoted.ToSpan(), relative.ToLowerInvariant());
                        edit.Apply();
                    }
                }
                finally
                {
                    ProjectHelpers.DTE.UndoContext.Close();
                }
            }
        }
    }
}
