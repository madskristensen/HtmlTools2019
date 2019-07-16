using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System.ComponentModel.Composition;

namespace HtmlTools
{
    [Export(typeof(IPeekableItemSourceProvider))]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    [Name("HTML ID Peekable Item Provider")]
    [SupportsStandaloneFiles(false)]
    internal class IdPeekItemProvider : IPeekableItemSourceProvider
    {
#pragma warning disable 649 // "field never assigned to" -- field is set by MEF.
        [Import]
        private readonly IPeekResultFactory _peekResultFactory;
#pragma warning restore 649

        public IPeekableItemSource TryCreatePeekableItemSource(ITextBuffer textBuffer)
        {
            return textBuffer.Properties.GetOrCreateSingletonProperty(() => new IdPeekItemSource(textBuffer, _peekResultFactory));
        }
    }
}