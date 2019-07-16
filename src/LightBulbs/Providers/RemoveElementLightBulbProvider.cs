using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.SuggestedActions;
using Microsoft.WebTools.Languages.Html.Tree.Nodes;
using Microsoft.WebTools.Languages.Html.Tree.Utility;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace HtmlTools
{
    [Export(typeof(IHtmlSuggestedActionProvider))]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    [Name("HtmlRemoveElementLightBulbProvider")]
    internal class HtmlRemoveElementLightBulbProvider : IHtmlSuggestedActionProvider
    {
        public IEnumerable<ISuggestedAction> GetSuggestedActions(ITextView textView, ITextBuffer textBuffer, int caretPosition, ElementNode element, AttributeNode attribute, HtmlPositionType positionType)
        {
            return new ISuggestedAction[] {
                new HtmlRemoveElementLightBulbAction(textView, textBuffer, element)
            };
        }

        public bool HasSuggestedActions(ITextView textView, ITextBuffer textBuffer, int caretPosition, ElementNode element, AttributeNode attribute, HtmlPositionType positionType)
        {
            if (element.IsRoot || element.EndTag == null || (!element.StartTag.Contains(caretPosition) && !element.EndTag.Contains(caretPosition)))
            {
                return false;
            }

            return element.InnerRange != null && element.GetText(element.InnerRange).Trim().Length > 0;
        }
    }
}
