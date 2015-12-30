using HtmlAgilityPack;
using Hydra.Utils;
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

namespace Hydra.Converters {
    /// <summary>
    /// This class converts a (partial) HTML string to a XAML-layout (using RichTextBlock).
    /// 
    /// You'd think something as a basic HTML parser/displayer was already available in a decent framework.
    /// Well, think again.
    /// 
    /// Code is a modified version of https://code.msdn.microsoft.com/windowsapps/Social-Media-Dashboard-135436da
    /// </summary>
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
        /// This is called when the HTML has changed so that we can generate the XAML.
        /// </summary> 
        /// <param name="d"></param> 
        /// <param name="e"></param> 
        private static void HtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            RichTextBlock richText = d as RichTextBlock;
            if (richText == null) return;

            // Generate blocks 
            string xhtml = e.NewValue as string;
            HtmlToXamlTranslator translator = new HtmlToXamlTranslator(xhtml);
            List<Block> blocks = translator.ParseHtml();

            // Add the blocks to the RichTextBlock 
            richText.Blocks.Clear();
            foreach (Block b in blocks) {
                richText.Blocks.Add(b);
            }
        }
    }
}
