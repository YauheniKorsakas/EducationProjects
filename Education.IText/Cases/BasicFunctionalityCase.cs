using Education.Core;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Layout.Element;
using System.Threading.Tasks;

namespace Education.IText.Cases {
    public class BasicFunctionalityCase : BaseCase, ICase {
        public virtual async Task RunAsync() {
            AddParagraph("Some text for test");
        }
        
        private void AddParagraph(string textToPdf) {
            var pdfData = GeneratePdfData("paragraph");
            pdfData.document.Add(new Paragraph(textToPdf));
            pdfData.document.Close();
        }

        private void AddTextAnnotation() {
            var pdfData = GeneratePdfData("annotations");
            PdfLinkAnnotation annotation = ((PdfLinkAnnotation)new PdfLinkAnnotation(new Rectangle(0, 0))
                .SetAction(PdfAction.CreateURI("https://itextpdf.com/")));
            var link = new Link("here", annotation);

            pdfData.document.Add(new Paragraph("some annotatio. Click ").Add(link).Add(" to learn more..."));
            pdfData.document.Close();
        }
    }
}
