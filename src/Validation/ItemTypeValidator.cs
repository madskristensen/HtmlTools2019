using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Validation.Def;
using Microsoft.WebTools.Languages.Html.Editor.Validation.Errors;
using Microsoft.WebTools.Languages.Html.Editor.Validation.Validators;
using Microsoft.WebTools.Languages.Html.Tree.Nodes;
using Microsoft.WebTools.Languages.Shared.ContentTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;

namespace HtmlTools
{
    [Export(typeof(IHtmlElementValidatorProvider))]
    [ContentType(HtmlContentTypeDefinition.HtmlContentType)]
    public class ItemTypeValidatorProvider : BaseHtmlElementValidatorProvider<ItemTypeValidator>
    { }

    public class ItemTypeValidator : BaseValidator
    {
        public override IList<IHtmlValidationError> ValidateElement(ElementNode element)
        {
            var results = new ValidationErrorCollection();

            if (element.IsDocType())
            {
                return results;
            }

            for (int i = 0; i < element.Attributes.Count; i++)
            {
                AttributeNode attr = element.Attributes[i];

                if (attr.Name == "itemtype" && attr.HasValue() && !attr.Value.Contains("@"))
                {
                    if (!Uri.IsWellFormedUriString(attr.Value, UriKind.Absolute))
                    {
                        results.AddAttributeError(element, string.Format(CultureInfo.CurrentCulture, "The value of {0} must be an absolute URI", attr.Name), HtmlValidationErrorLocation.AttributeValue, i);
                        break;
                    }
                }
            }

            return results;
        }
    }
}
