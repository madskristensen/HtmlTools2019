using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System;
using System.Collections.Generic;

namespace HtmlTools
{
    [HtmlCompletionProvider(CompletionTypes.Values, "meta", "content")]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    public class AppleMetaCompletion : StaticListCompletion
    {
        protected override string KeyProperty => "name";
        public AppleMetaCompletion()
            : base(new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "apple-mobile-web-app-capable",           Values("yes", "no") },
                { "format-detection",                       Values("telephone=yes", "telephone=no") },
                { "apple-mobile-web-app-status-bar-style",  Values("default", "black", "black-translucent") }
            })
        { }
    }
}
