using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Completion;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using Microsoft.WebTools.Languages.Html.Tree.Nodes;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlTools
{
    [HtmlCompletionProvider(CompletionTypes.Values, "input", "list")]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    public class HtmlDataListCompletion : IHtmlCompletionListProvider, IHtmlTreeVisitor
    {
        public string CompletionType => CompletionTypes.Values;

        public IList<HtmlCompletion> GetEntries(HtmlCompletionContext context)
        {
            var list = new HashSet<string>();

            context.Document.HtmlEditorTree.RootNode.Accept(this, list);

            return list.Select(s => new SimpleHtmlCompletion(s, context.Session)).ToList<HtmlCompletion>();
        }

        public bool Visit(ElementNode element, object parameter)
        {
            if (element.Name.Equals("datalist", StringComparison.OrdinalIgnoreCase) && element.HasAttribute("id"))
            {
                var list = (HashSet<string>)parameter;
                list.Add(element.GetAttribute("id").Value);
            }

            return true;
        }
    }
}
