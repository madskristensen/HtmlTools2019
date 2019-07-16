using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Completion;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;

namespace HtmlTools
{
    [Export(typeof(IHtmlCompletionListFilter))]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    public class MSApplicationCompletionFilter : IHtmlCompletionListFilter
    {
        private static readonly BitmapSource _icon = ImageHelper.GetImage(KnownMonikers.Windows);
        public void FilterCompletionList(IList<HtmlCompletion> completions, HtmlCompletionContext context)
        {
            foreach (HtmlCompletion completion in completions)
            {
                if (completion.DisplayText.StartsWith("msapplication-", StringComparison.OrdinalIgnoreCase) ||
                    completion.DisplayText.StartsWith("x-ms-", StringComparison.OrdinalIgnoreCase))
                {
                    completion.IconSource = _icon;
                }
            }
        }
    }
}
