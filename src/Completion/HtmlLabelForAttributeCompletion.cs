using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Completion;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using Microsoft.WebTools.Languages.Html.Tree.Nodes;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System.Collections.Generic;
using System.Linq;

namespace HtmlTools
{
    [HtmlCompletionProvider(CompletionTypes.Values, "label", "for")]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    public class HtmlLabelForAttributeCompletion : IHtmlCompletionListProvider, IHtmlTreeVisitor
    {
        private static readonly List<string> _inputTypes = new List<string>() { "input", "textarea", "select" };
        public string CompletionType => CompletionTypes.Values;

        public IList<HtmlCompletion> GetEntries(HtmlCompletionContext context)
        {
            var list = new HashSet<string>();

            context.Document.HtmlEditorTree.RootNode.Accept(this, list);

            return new List<HtmlCompletion>(list.Select(s => new SimpleHtmlCompletion(s, context.Session)));
        }

        public bool Visit(ElementNode element, object parameter)
        {
            if (_inputTypes.Contains(element.Name.ToLowerInvariant()))
            {
                var list = (HashSet<string>)parameter;
                AttributeNode id = element.GetAttribute("id");

                if (id != null)
                {
                    list.Add(id.Value);
                }
            }

            return true;
        }
    }
}
