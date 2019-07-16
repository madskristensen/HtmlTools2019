using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.WebTools.Languages.Css.Editor.Parser;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.TreeItems;
using Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives;
using Microsoft.WebTools.Languages.Css.TreeItems.Selectors;
using Microsoft.WebTools.Languages.Shared.Editor.EditorHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace HtmlTools
{
    internal class IdResultSource : IPeekResultSource
    {
        private readonly IdDefinitionPeekItem peekableItem;

        public IdResultSource(IdDefinitionPeekItem peekableItem)
        {
            this.peekableItem = peekableItem;
        }

        public void FindResults(string relationshipName, IPeekResultCollection resultCollection, CancellationToken cancellationToken, IFindPeekResultsCallback callback)
        {
            if (relationshipName != PredefinedPeekRelationships.Definitions.Name)
            {
                return;
            }

            string file = FindRuleSetInFile(new[] { ".less", ".scss", ".css" }, peekableItem._id, out RuleSet rule);

            if (rule == null)
            {
                callback.ReportProgress(1);
                return;
            }

            using (var displayInfo = new PeekResultDisplayInfo(label: peekableItem._id, labelTooltip: file, title: Path.GetFileName(file), titleTooltip: file))
            {
                IDocumentPeekResult result = peekableItem._peekResultFactory.Create
                (
                    displayInfo,
                    file,
                    new Span(rule.Start, rule.Length),
                    rule.Start,
                    false
                );

                resultCollection.Add(result);
                callback.ReportProgress(1);
            }
        }

        private string FindRuleSetInFile(IEnumerable<string> extensions, string id, out RuleSet rule)
        {
            string root = ProjectHelpers.GetProjectFolder(peekableItem._textbuffer.GetFileName());
            string result = null;
            bool isLow = false, isMedium = false;
            rule = null;

            foreach (string ext in extensions)
            {
                ICssParser parser = CssParserLocator.FindComponent(ProjectHelpers.GetContentType(ext.Trim('.'))).CreateParser();

                foreach (string file in Directory.EnumerateFiles(root, "*" + ext, SearchOption.AllDirectories))
                {
                    if (file.EndsWith(".min" + ext, StringComparison.OrdinalIgnoreCase) ||
                        file.Contains("node_modules") ||
                        file.Contains("bower_components"))
                    {
                        continue;
                    }

                    string text = File.ReadAllText(file);
                    int index = text.IndexOf("#" + id, StringComparison.Ordinal);

                    if (index == -1)
                    {
                        continue;
                    }

                    StyleSheet css = parser.Parse(text, true);
                    var visitor = new CssItemCollector<IdSelector>(false);
                    css.Accept(visitor);

                    IEnumerable<IdSelector> selectors = visitor.Items.Where(c => c.HashName.Text == "#" + id);
                    IdSelector high = selectors.FirstOrDefault(c => c.FindType<AtDirective>() == null && (c.Parent.NextSibling == null || c.Parent.NextSibling.Text == ","));

                    if (high != null)
                    {
                        rule = high.FindType<RuleSet>();
                        return file;
                    }

                    IdSelector medium = selectors.FirstOrDefault(c => c.Parent.NextSibling == null || c.Parent.NextSibling.Text == ",");

                    if (medium != null && !isMedium)
                    {
                        rule = medium.FindType<RuleSet>();
                        result = file;
                        isMedium = true;
                        continue;
                    }

                    IdSelector low = selectors.FirstOrDefault();

                    if (low != null && !isMedium && !isLow)
                    {
                        rule = low.FindType<RuleSet>();
                        result = file;
                        isLow = true;
                        continue;
                    }
                }
            }

            return result;
        }
    }
}
