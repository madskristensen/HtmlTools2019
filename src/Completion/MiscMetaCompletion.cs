using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System;
using System.Collections.Generic;

namespace HtmlTools
{
    [HtmlCompletionProvider(CompletionTypes.Values, "meta", "content")]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    public class MiscMetaCompletion : StaticListCompletion
    {
        protected override string KeyProperty => "name";
        public MiscMetaCompletion()
            : base(new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "generator",  Values("Visual Studio") },
                { "robots",     Values("index", "noindex", "follow", "nofollow", "noindex, nofollow", "noindex, follow", "index, nofollow") }
          })
        { }
    }
}
