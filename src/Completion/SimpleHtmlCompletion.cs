using Microsoft.WebTools.Languages.Html.Editor.Completion;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Html;
using Microsoft.WebTools.Languages.Shared.Editor.Imaging;
using System.Windows.Media;
using vs = Microsoft.VisualStudio.Language.Intellisense;

namespace HtmlTools
{
    public class SimpleHtmlCompletion : HtmlCompletion
    {
        private static readonly ImageSource _glyph = GlyphService.GetGlyph(vs.StandardGlyphGroup.GlyphGroupVariable, vs.StandardGlyphItem.GlyphItemPublic);

        public SimpleHtmlCompletion(string displayText, vs.ICompletionSession session)
            : base(displayText, displayText, string.Empty, _glyph, HtmlIconAutomationText.AttributeIconText, session)
        { }

        public SimpleHtmlCompletion(string displayText, string description, vs.ICompletionSession session)
            : base(displayText, displayText, description, _glyph, HtmlIconAutomationText.AttributeIconText, session)
        { }

        public SimpleHtmlCompletion(string displayText, string description, string insertion, ImageSource glyph, vs.ICompletionSession session)
            : base(displayText, insertion, description, glyph, HtmlIconAutomationText.AttributeIconText, session)
        { }
    }
}
