using Education.Core;
using iText.Kernel.Pdf;
using iText.Layout;
using System.IO;
using System.Threading.Tasks;

namespace Education.IText.Cases {
    public abstract class BaseCase : ICase {
        //protected readonly string pdfPlace = "D:/Repositories/Education/Education/Education.IText/Pdfs";
        protected readonly string pdfPlace = "D:/Confirmations/";
        protected string docPath;
        protected PdfWriter writer;
        protected PdfReader reader;
        protected PdfDocument pdf;
        protected Document document;

        public async Task RunAsync() {
            InvokeLogic();
        }

        protected abstract void InvokeLogic();
        
        protected void InitPdfToWriteData(string docName) {
            docPath = $"{pdfPlace}/{docName}.pdf";
            writer = new PdfWriter(docPath);
            reader = null;
            pdf = new PdfDocument(writer);
            document = new Document(pdf);
        }

        protected void InitPdfToReadData(string docName) {
            docPath = $"{pdfPlace}/{docName}.pdf";
            writer = null;
            reader = new PdfReader(docPath);
            pdf = new PdfDocument(reader);
            document = new Document(pdf);
        }

        private void RemoveIfExists(string filePath) {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }
}
