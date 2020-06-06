using Education.Core;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Layout.Element;

namespace Education.IText.Cases {
    public class BasicFunctionalityCase : BaseCase, ICase {
        protected override void InvokeLogic() {
            AddParagraph("Some text for test");
        }

        private void AddParagraph(string textToPdf) {
            InitPdfToWriteData("paragraph");
            document.Add(new Paragraph(textToPdf));
            document.Close();
        }

        private void AddTextAnnotation() {
            InitPdfToWriteData("annotations");
            PdfLinkAnnotation annotation = ((PdfLinkAnnotation)new PdfLinkAnnotation(new Rectangle(0, 0))
                .SetAction(PdfAction.CreateURI("https://itextpdf.com/")));
            var link = new Link("here", annotation);

            document.Add(new Paragraph("some annotatio. Click ").Add(link).Add(" to learn more..."));
            document.Close();
        }
    }
}
