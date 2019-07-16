using MadsKristensen.EditorExtensions.Html;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.WebTools.Languages.Html.Tree.Nodes;
using Microsoft.WebTools.Languages.Shared.Editor.Text;
using System.Threading;

namespace HtmlTools
{
    internal class HtmlRemoveElementLightBulbAction : HtmlSuggestedActionBase
    {
        private readonly AttributeNode _src;

        public HtmlRemoveElementLightBulbAction(ITextView textView, ITextBuffer textBuffer, ElementNode element)
            : base(textView, textBuffer, element, element.Children.Count == 0 ? "Remove <" + element.StartTag.Name + "> tag" : "Remove <" + element.StartTag.Name + "> and Keep Children")
        {
            _src = element.GetAttribute("src", true);
        }

        public override void Invoke(CancellationToken cancellationToken)
        {
            string content = Element.GetText(Element.InnerRange).Trim();
            int start = Element.Start;
            int length = content.Length;

            try
            {
                ProjectHelpers.DTE.UndoContext.Open(DisplayText);
                using (ITextEdit edit = TextBuffer.CreateEdit())
                {
                    edit.Replace(Element.OuterRange.ToSpan(), content);
                    edit.Apply();
                }

                var span = new SnapshotSpan(TextView.TextBuffer.CurrentSnapshot, start, length);

                TextView.Selection.Select(span, false);
                ProjectHelpers.DTE.ExecuteCommand("Edit.FormatSelection");
                TextView.Caret.MoveTo(new SnapshotPoint(TextView.TextBuffer.CurrentSnapshot, start));
                TextView.Selection.Clear();
            }
            finally
            {
                ProjectHelpers.DTE.UndoContext.Close();
            }
        }
    }
}
