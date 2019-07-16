using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System;
using System.Collections.Generic;

namespace HtmlTools
{
    [HtmlCompletionProvider(CompletionTypes.Values, "meta", "content")]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    public class TwitterCardCompletion : StaticListCompletion
    {
        protected override string KeyProperty => "name";
        public TwitterCardCompletion()
            : base(new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "twitter:card",  Values("app", "gallery", "photo", "player", "product", "summary", "summary_large_image") }
            })
        { }
    }
}
