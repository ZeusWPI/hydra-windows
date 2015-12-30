using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Hydra.Utils {
    /// <summary>
    /// Parses HTML to a structure of XAML-blocks.
    /// Remark: only uses HTML-tags for styling, ignores style sheets or styling attributes.
    /// </summary>
    public class HtmlToXamlTranslator {

        private HtmlDocument htmlDoc;

        // Keeps track of list numbering
        private Stack<int> listNumbers;

        #region StyleParameters

        #region Hyperlinks
        public bool HyperlinkShowTooltip { get; set; } = true;
        #endregion

        #region Lists
        public int ListIndentation { get; set; } = 5;
        public string UlBulletType { get; set; } = "•";
        #endregion

        #region Headers
        public int H1FontSize { get; set; } = 30;
        public int H2FontSize { get; set; } = 24;
        public int H3FontSize { get; set; } = 20;
        #endregion

        #endregion

        public HtmlToXamlTranslator(string html) {
            this.htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            listNumbers = new Stack<int>();
        }

        /// <summary>
        /// Parses the HTML string into a list of Blocks, which can be used inside a RichtTextBlock
        /// </summary>
        /// <param name="html">The HTML that will be parsed</param>
        /// <returns>List of Blocks</returns>
        public List<Block> ParseHtml() {
            List<Block> bc = new List<Block>();

            try {
                Block b = GenerateParagraph(this.htmlDoc.DocumentNode);
                bc.Add(b);
            } catch (Exception ex) {
                Debug.WriteLine("Something went wrong trying to generate the XAML for the HTML.");
                Debug.WriteLine(ex.Message);
            }

            return bc;
        }

        /// <summary> 
        /// Cleans HTML text for display in paragraphs 
        /// </summary> 
        /// <param name="input"></param> 
        /// <returns></returns> 
        private string CleanText(string input) {
            string clean = Windows.Data.Html.HtmlUtilities.ConvertToText(input);
            if (clean == "\0")
                clean = "\n";
            return clean;
        }

        private Block GenerateBlockForTopNode(HtmlNode node) {
            return GenerateParagraph(node);
        }

        /// <summary>
        /// Adds a node (and its chilren) as inlines to an element.
        /// </summary>
        /// <param name="inlines">The collection of inlines of the parent node</param>
        /// <param name="node">The node that will be added</param>
        private void AddChildren(InlineCollection inlines, HtmlNode node) {
            bool noChildren = true;

            foreach (HtmlNode child in node.ChildNodes) {
                Inline i = GenerateBlockForNode(child);
                if (i != null) {
                    inlines.Add(i);
                    noChildren = false;
                }
            }

            if (noChildren) {
                inlines.Add(new Run() { Text = CleanText(node.InnerText) });
            }
        }

        /// <summary>
        /// Creates specific kind of block, based on the type of html tag.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Inline GenerateBlockForNode(HtmlNode node) {
            switch (node.Name) {
                case "div":
                    return GenerateSpan(node);
                case "p":
                case "P":
                    return GenerateInnerParagraph(node);
                case "img":
                case "IMG":
                    return GenerateImage(node);
                case "a":
                case "A":
                    if (node.ChildNodes.Count >= 1 && (node.FirstChild.Name == "img" || node.FirstChild.Name == "IMG"))
                        // Ignore links in images
                        return GenerateImage(node.FirstChild);
                    else
                        return GenerateHyperLink(node);
                case "li":
                case "LI":
                    return GenerateLI(node);
                case "b":
                case "B":
                    return GenerateBold(node);
                case "i":
                case "I":
                    return GenerateItalic(node);
                case "u":
                case "U":
                    return GenerateUnderline(node);
                case "br":
                case "BR":
                    return new LineBreak();
                case "span":
                case "Span":
                    return GenerateSpan(node);
                case "iframe":
                case "Iframe":
                    return GenerateIFrame(node);
                case "#text":
                    if (!string.IsNullOrWhiteSpace(node.InnerText))
                        return new Run() { Text = CleanText(node.InnerText) };
                    break;
                case "h1":
                case "H1":
                    return GenerateH1(node);
                case "h2":
                case "H2":
                    return GenerateH2(node);
                case "h3":
                case "H3":
                    return GenerateH3(node);
                case "ul":
                case "UL":
                case "ol":
                case "OL":
                    // Handled the same way, numbering is done in GenerateLI
                    return GenerateUL(node);
                default:
                    return GenerateSpanWithNewLine(node);
            }
            return null;
        }

        private Inline GenerateLI(HtmlNode node) {
            Span s = new Span();
            InlineUIContainer iui = new InlineUIContainer();
            TextBlock itemBullet = new TextBlock() {
                Margin = new Thickness(listNumbers.Count * this.ListIndentation, 0, 5, 0)
            };

            int itemNumber = listNumbers.Pop();
            if (node.ParentNode.Name == "ul" || node.ParentNode.Name == "UL") {
                itemBullet.Text = UlBulletType;
            } else {
                itemBullet.Text = itemNumber.ToString() + ")";
            }
            listNumbers.Push(itemNumber + 1);

            iui.Child = itemBullet;
            s.Inlines.Add(iui);
            AddChildren(s.Inlines, node);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private Inline GenerateImage(HtmlNode node) {
            Span s = new Span();
            try {
                InlineUIContainer iui = new InlineUIContainer();
                var sourceUri = System.Net.WebUtility.HtmlDecode(node.Attributes["src"].Value);
                Image img = new Image() {
                    Source = new BitmapImage(new Uri(sourceUri, UriKind.Absolute)),
                    Stretch = Stretch.Uniform,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                img.ImageOpened += img_ImageOpened;
                img.ImageFailed += img_ImageFailed;
                iui.Child = img;
                s.Inlines.Add(iui);
                s.Inlines.Add(new LineBreak());
            } catch (Exception ex) {
                Debug.WriteLine("Something went wrong trying to generate the image with source <" + node.Attributes["src"] + ">.");
                Debug.WriteLine(ex.Message);
            }
            return s;
        }

        void img_ImageFailed(object sender, ExceptionRoutedEventArgs e) {
            Debug.WriteLine("Failed to load image.");
        }

        void img_ImageOpened(object sender, RoutedEventArgs e) {
            Image img = sender as Image;
            BitmapImage bimg = img.Source as BitmapImage;
            if (bimg.PixelWidth > 800 || bimg.PixelHeight > 600) {
                img.Width = 800; img.Height = 600;
                if (bimg.PixelWidth > 800) {
                    img.Width = 800;
                    img.Height = (800.0 / (double)bimg.PixelWidth) * bimg.PixelHeight;
                }
                if (img.Height > 600) {
                    img.Height = 600;
                    img.Width = (600.0 / (double)img.Height) * img.Width;
                }
            } else {
                img.Height = bimg.PixelHeight;
                img.Width = bimg.PixelWidth;
            }
        }

        private Inline GenerateHyperLink(HtmlNode node) {
            Span s = new Span();
            InlineUIContainer iui = new InlineUIContainer();

            string href = node.Attributes["href"].Value;
            HyperlinkButton hb = new HyperlinkButton() {
                NavigateUri = new Uri(href, UriKind.Absolute),
                Content = CleanText(node.InnerText),
                Margin = new Thickness(0, 0, 0, -10)
            };

            if(this.HyperlinkShowTooltip) {
                ToolTipService.SetToolTip(hb, href);
            }

            iui.Child = hb;
            s.Inlines.Add(iui);
            return s;
        }

        private Inline GenerateIFrame(HtmlNode node) {
            try {
                Span s = new Span();
                s.Inlines.Add(new LineBreak());
                InlineUIContainer iui = new InlineUIContainer();
                WebView ww = new WebView() {
                    Source = new Uri(node.Attributes["src"].Value, UriKind.Absolute),
                    Width = Int32.Parse(node.Attributes["width"].Value),
                    Height = Int32.Parse(node.Attributes["height"].Value)
                };
                iui.Child = ww;
                s.Inlines.Add(iui);
                s.Inlines.Add(new LineBreak());
                return s;
            } catch (Exception ex) {
                Debug.WriteLine("Something went wrong generating an iframe.");
                Debug.WriteLine(ex.Message);
                // Just omit the iframe from the result.
                return null;
            }
        }

        private Block GenerateTopIFrame(HtmlNode node) {
            try {
                Paragraph p = new Paragraph();
                InlineUIContainer iui = new InlineUIContainer();
                WebView ww = new WebView() {
                    Source = new Uri(node.Attributes["src"].Value, UriKind.Absolute),
                    Width = Int32.Parse(node.Attributes["width"].Value),
                    Height = Int32.Parse(node.Attributes["height"].Value)
                };
                iui.Child = ww;
                p.Inlines.Add(iui);
                return p;
            } catch (Exception ex) {
                Debug.WriteLine("Something went wrong generating an iframe.");
                Debug.WriteLine(ex.Message);
                // Just omit it from the whole
                return null;
            }
        }

        private Inline GenerateBold(HtmlNode node) {
            Bold b = new Bold();
            AddChildren(b.Inlines, node);
            return b;
        }

        private Inline GenerateUnderline(HtmlNode node) {
            Underline u = new Underline();
            AddChildren(u.Inlines, node);
            return u;
        }

        private Inline GenerateItalic(HtmlNode node) {
            Italic i = new Italic();
            AddChildren(i.Inlines, node);
            return i;
        }

        private Block GenerateParagraph(HtmlNode node) {
            Paragraph p = new Paragraph();
            AddChildren(p.Inlines, node);
            return p;
        }

        private Inline GenerateUL(HtmlNode node) {
            listNumbers.Push(1);
            Span s = new Span();
            s.Inlines.Add(new LineBreak());
            AddChildren(s.Inlines, node);
            listNumbers.Pop();
            return s;
        }

        private Inline GenerateInnerParagraph(HtmlNode node) {
            Span s = new Span();
            s.Inlines.Add(new LineBreak());
            AddChildren(s.Inlines, node);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private Inline GenerateSpan(HtmlNode node) {
            Span s = new Span();
            AddChildren(s.Inlines, node);
            return s;
        }

        private Inline GenerateSpanWithNewLine(HtmlNode node) {
            Span s = new Span();
            AddChildren(s.Inlines, node);
            if (s.Inlines.Count > 0)
                s.Inlines.Add(new LineBreak());
            return s;
        }

        private Span GenerateH3(HtmlNode node) {
            Span s = new Span() { FontSize = this.H3FontSize };
            s.Inlines.Add(new LineBreak());
            Bold bold = new Bold();
            Run r = new Run() { Text = CleanText(node.InnerText) };
            bold.Inlines.Add(r);
            s.Inlines.Add(bold);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private Inline GenerateH2(HtmlNode node) {
            Span s = new Span() { FontSize = this.H2FontSize };
            s.Inlines.Add(new LineBreak());
            Run r = new Run() { Text = CleanText(node.InnerText) };
            s.Inlines.Add(r);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private Inline GenerateH1(HtmlNode node) {
            Span s = new Span() { FontSize = this.H1FontSize };
            s.Inlines.Add(new LineBreak());
            Run r = new Run() { Text = CleanText(node.InnerText) };
            s.Inlines.Add(r);
            s.Inlines.Add(new LineBreak());
            return s;
        }
    }
}
