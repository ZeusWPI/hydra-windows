using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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

namespace Hydra.Converters {
    public class HtmlToRichTextConverter : DependencyObject {
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html", typeof(string), typeof(HtmlToRichTextConverter), new PropertyMetadata(null, HtmlChanged));

        /// <summary> 
        /// sets the HTML property 
        /// </summary> 
        /// <param name="obj"></param> 
        /// <param name="value"></param> 
        public static void SetHtml(DependencyObject obj, string value) {
            obj.SetValue(HtmlProperty, value);
        }

        /// <summary> 
        /// Gets the HTML property 
        /// </summary> 
        /// <param name="obj"></param> 
        /// <returns></returns> 
        public static string GetHtml(DependencyObject obj) {
            return (string)obj.GetValue(HtmlProperty);
        }


        /// <summary> 
        /// This is called when the HTML has changed so that we can generate RT content 
        /// </summary> 
        /// <param name="d"></param> 
        /// <param name="e"></param> 
        private static void HtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            RichTextBlock richText = d as RichTextBlock;
            if (richText == null) return;

            //Generate blocks 
            string xhtml = e.NewValue as string;
            List<Block> blocks = GenerateBlocksForHtml(xhtml);

            //Add the blocks to the RichTextBlock 
            richText.Blocks.Clear();
            foreach (Block b in blocks) {
                richText.Blocks.Add(b);
            }
        }

        private static List<Block> GenerateBlocksForHtml(string xhtml) {
            List<Block> bc = new List<Block>();

            try {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(xhtml);
                Block b = GenerateParagraph(doc.DocumentNode);
                bc.Add(b);
            } catch (Exception ex) {

            }

            return bc;
        }


        /// <summary> 
        /// Cleans HTML text for display in paragraphs 
        /// </summary> 
        /// <param name="input"></param> 
        /// <returns></returns> 
        private static string CleanText(string input) {
            string clean = Windows.Data.Html.HtmlUtilities.ConvertToText(input);
            //clean = System.Net.WebUtility.HtmlEncode(clean); 
            if (clean == "\0")
                clean = "\n";
            return clean;
        }

        private static Block GenerateBlockForTopNode(HtmlNode node) {
            return GenerateParagraph(node);
        }

        private static void AddChildren(Paragraph p, HtmlNode node) {
            bool added = false;
            foreach (HtmlNode child in node.ChildNodes) {
                Inline i = GenerateBlockForNode(child);
                if (i != null) {
                    p.Inlines.Add(i);
                    added = true;
                }
            }
            if (!added) {
                p.Inlines.Add(new Run() { Text = CleanText(node.InnerText) });
            }
        }

        private static void AddChildren(Span s, HtmlNode node) {
            bool added = false;

            foreach (HtmlNode child in node.ChildNodes) {
                Inline i = GenerateBlockForNode(child);
                if (i != null) {
                    s.Inlines.Add(i);
                    added = true;
                }
            }
            if (!added) {
                s.Inlines.Add(new Run() { Text = CleanText(node.InnerText) });
            }
        }


        private static Inline GenerateBlockForNode(HtmlNode node) {
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
                    return GenerateUL(node);
                default:
                    return GenerateSpanWNewLine(node);
                    //if (!string.IsNullOrWhiteSpace(node.InnerText)) 
                    //    return new Run() { Text = CleanText(node.InnerText) }; 
                    //break; 
            }
            return null;
        }

        private static Inline GenerateLI(HtmlNode node) {
            Span s = new Span();
            InlineUIContainer iui = new InlineUIContainer();
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Black);
            ellipse.Width = 6;
            ellipse.Height = 6;
            ellipse.Margin = new Thickness(-30, 0, 0, 1);
            iui.Child = ellipse;
            s.Inlines.Add(iui);
            AddChildren(s, node);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateImage(HtmlNode node) {
            Span s = new Span();
            try {
                InlineUIContainer iui = new InlineUIContainer();
                var sourceUri = System.Net.WebUtility.HtmlDecode(node.Attributes["src"].Value);
                Image img = new Image() { Source = new BitmapImage(new Uri(sourceUri, UriKind.Absolute)) };
                img.Stretch = Stretch.Uniform;
                img.VerticalAlignment = VerticalAlignment.Top;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.ImageOpened += img_ImageOpened;
                img.ImageFailed += img_ImageFailed;
                iui.Child = img;
                s.Inlines.Add(iui);
                s.Inlines.Add(new LineBreak());
            } catch (Exception ex) {
            }
            return s;
        }

        static void img_ImageFailed(object sender, ExceptionRoutedEventArgs e) {
            var i = 5;
        }

        static void img_ImageOpened(object sender, RoutedEventArgs e) {
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

        private static Inline GenerateHyperLink(HtmlNode node) {
            Span s = new Span();
            InlineUIContainer iui = new InlineUIContainer();
            HyperlinkButton hb = new HyperlinkButton() { NavigateUri = new Uri(node.Attributes["href"].Value, UriKind.Absolute), Content = CleanText(node.InnerText) };
            
            // Proper alignment
            hb.Margin = new Thickness(0,0,0,-10);

            iui.Child = hb;
            s.Inlines.Add(iui);
            return s;
        }

        private static Inline GenerateIFrame(HtmlNode node) {
            try {
                Span s = new Span();
                s.Inlines.Add(new LineBreak());
                InlineUIContainer iui = new InlineUIContainer();
                WebView ww = new WebView() { Source = new Uri(node.Attributes["src"].Value, UriKind.Absolute), Width = Int32.Parse(node.Attributes["width"].Value), Height = Int32.Parse(node.Attributes["height"].Value) };
                iui.Child = ww;
                s.Inlines.Add(iui);
                s.Inlines.Add(new LineBreak());
                return s;
            } catch (Exception ex) {
                return null;
            }
        }

        private static Block GenerateTopIFrame(HtmlNode node) {
            try {
                Paragraph p = new Paragraph();
                InlineUIContainer iui = new InlineUIContainer();
                WebView ww = new WebView() { Source = new Uri(node.Attributes["src"].Value, UriKind.Absolute), Width = Int32.Parse(node.Attributes["width"].Value), Height = Int32.Parse(node.Attributes["height"].Value) };
                iui.Child = ww;
                p.Inlines.Add(iui);
                return p;
            } catch (Exception ex) {
                return null;
            }
        }

        private static Inline GenerateBold(HtmlNode node) {
            Bold b = new Bold();
            AddChildren(b, node);
            return b;
        }

        private static Inline GenerateUnderline(HtmlNode node) {
            Underline u = new Underline();
            AddChildren(u, node);
            return u;
        }

        private static Inline GenerateItalic(HtmlNode node) {
            Italic i = new Italic();
            AddChildren(i, node);
            return i;
        }

        private static Block GenerateParagraph(HtmlNode node) {
            Paragraph p = new Paragraph();
            AddChildren(p, node);
            return p;
        }

        private static Inline GenerateUL(HtmlNode node) {
            Span s = new Span();
            s.Inlines.Add(new LineBreak());
            AddChildren(s, node);
            return s;
        }

        private static Inline GenerateInnerParagraph(HtmlNode node) {
            Span s = new Span();
            s.Inlines.Add(new LineBreak());
            AddChildren(s, node);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateSpan(HtmlNode node) {
            Span s = new Span();
            AddChildren(s, node);
            return s;
        }

        private static Inline GenerateSpanWNewLine(HtmlNode node) {
            Span s = new Span();
            AddChildren(s, node);
            if (s.Inlines.Count > 0)
                s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Span GenerateH3(HtmlNode node) {
            Span s = new Span();
            s.Inlines.Add(new LineBreak());
            Bold bold = new Bold();
            Run r = new Run() { Text = CleanText(node.InnerText) };
            bold.Inlines.Add(r);
            s.Inlines.Add(bold);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateH2(HtmlNode node) {
            Span s = new Span() { FontSize = 24 };
            s.Inlines.Add(new LineBreak());
            Run r = new Run() { Text = CleanText(node.InnerText) };
            s.Inlines.Add(r);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateH1(HtmlNode node) {
            Span s = new Span() { FontSize = 30 };
            s.Inlines.Add(new LineBreak());
            Run r = new Run() { Text = CleanText(node.InnerText) };
            s.Inlines.Add(r);
            s.Inlines.Add(new LineBreak());
            return s;
        }
    }
}
