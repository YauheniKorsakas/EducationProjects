using iText.Kernel.Pdf;
using iText.Layout;
using System.IO;

namespace Education.IText.Cases {
    public abstract class BaseCase {
        protected readonly string pdfPlace = "D:/Repositories/Education/Education/Education.IText/Pdfs";
        
        protected (PdfWriter writer, PdfDocument pdf, Document document) GeneratePdfData(string docName) {
            var dest = $"{pdfPlace}/{docName}.pdf";
            RemoveIfExists(dest);

            var writer = new PdfWriter(dest);
            var pdf = new PdfDocument(writer);

            return (writer, pdf, new Document(pdf));
        }

        private void RemoveIfExists(string filePath) {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }
}
