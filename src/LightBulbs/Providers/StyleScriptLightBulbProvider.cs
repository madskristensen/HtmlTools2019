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
    [Name("HtmlStyleScriptLightBulbProvider")]
    internal class HtmlStyleScriptLightBulbProvider : IHtmlSuggestedActionProvider
    {
        public IEnumerable<ISuggestedAction> GetSuggestedActions(ITextView textView, ITextBuffer textBuffer, int caretPosition, ElementNode element, AttributeNode attribute, HtmlPositionType positionType)
        {
            return new ISuggestedAction[] {
                new HtmlExtractLightBulbAction(textView, textBuffer, element),
            };
        }

        public bool HasSuggestedActions(ITextView textView, ITextBuffer textBuffer, int caretPosition, ElementNode element, AttributeNode attribute, HtmlPositionType positionType)
        {
            if (!element.StartTag.Contains(caretPosition))
            {
                return false;
            }

            if (element.InnerRange.Length < 5)
            {
                return false;
            }

            return element.IsStyleBlock() || element.IsJavaScriptBlock();
        }
    }
}
