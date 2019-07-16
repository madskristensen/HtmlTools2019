using MadsKristensen.EditorExtensions.Html;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.WebTools.Languages.Html.Tree.Nodes;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;

namespace HtmlTools
{
    internal class IntegrityLightBulbAction : HtmlSuggestedActionBase
    {
        public IntegrityLightBulbAction(ITextView textView, ITextBuffer textBuffer, ElementNode element)
            : base(textView, textBuffer, element, "Calculate integrity")
        { }

        public override void Invoke(CancellationToken cancellationToken)
        {
            AttributeNode src = Element.GetAttribute("src") ?? Element.GetAttribute("href");
            AttributeNode integrity = Element.GetAttribute("integrity");
            AttributeNode crossorigin = Element.GetAttribute("crossorigin");

            string url = src.Value;

            if (url.StartsWith("//"))
            {
                url = "http:" + url;
            }

            string hash = CalculateHash(url);

            if (string.IsNullOrEmpty(hash))
            {
                MessageBox.Show("Could not resolve the URL to generate the hash", "Web Essentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                ProjectHelpers.DTE.UndoContext.Open(DisplayText);
                using (ITextEdit edit = TextBuffer.CreateEdit())
                {
                    if (integrity != null)
                    {
                        var span = new Span(integrity.ValueRangeUnquoted.Start, integrity.ValueRangeUnquoted.Length);
                        edit.Replace(span, hash);
                    }
                    else
                    {
                        edit.Insert(src.ValueRange.End, " integrity=\"" + hash + "\"");
                    }

                    if (crossorigin == null)
                    {
                        edit.Insert(src.ValueRange.End, " crossorigin=\"anonymous\"");
                    }

                    edit.Apply();
                }
            }
            finally
            {
                ProjectHelpers.DTE.UndoContext.Close();
            }
        }

        private static string CalculateHash(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    byte[] bytes = client.DownloadData(new Uri(url));

                    HashAlgorithm sha = SHA384.Create();
                    string hash = Convert.ToBase64String(sha.ComputeHash(bytes));
                    return $"sha384-{hash}";
                }
            }
            catch
            {
                return null;
            }
        }
    }
}