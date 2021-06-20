using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Education.IText.Cases
{
    public class Order {
        public string OrderDate { get; set; }
        public string Confirmation { get; set; }
        public string Acct { get; set; }
        public string EstShipDate { get; set; }
        public string OrderTotal { get; set; }
    }

    public class ParseCase : BaseCase {
        private string _docName;
        private readonly Dictionary<string, string> _orderedLeftDetailElementsBorders = new Dictionary<string, string> {
            { "Order Date:", "Confirmation #:" },
            { "Confirmation #", "Cust PO #:" },
            { "Acct #:", "Attn:" },
            { "Est Ship Date:", "Ship Via:" },
            { "Order Total:", "" }
        };

        private readonly int _leftSideWidth = 200;

        protected override void InvokeLogic() {
            _docName = "Confirmation 4357089-2";
            ParsePdfUsingLeftDetailsExtractionStrategy();
        }

        /// <summary>
        /// Parse and get text from the wholse first page.
        /// </summary>
        private void ParseDocument() {
            InitPdfToReadData(_docName);
            var listener = new FilteredEventListener();
            var extractionStategy = listener.AttachEventListener(new LocationTextExtractionStrategy());
            var pdfCanvasProcessor = new PdfCanvasProcessor(listener);

            pdfCanvasProcessor.ProcessPageContent(pdf.GetFirstPage());
            ShowData(extractionStategy.GetResultantText());
        }

        private void ParsePdfFromBytes() {
            docPath = $"{pdfPlace}/{_docName}.pdf";
            var pdfContent = File.ReadAllBytes(docPath);

            using (var pdfStream = new MemoryStream(pdfContent)) {
                reader = new PdfReader(pdfStream);
                pdf = new PdfDocument(reader);
                document = new Document(pdf);

                var docSize = pdf.GetFirstPage().GetPageSize();
                var leftDecriptionRect = new Rectangle(_leftSideWidth, docSize.GetHeight());
                var confirmationFilter = new TextRegionEventFilter(leftDecriptionRect);
                var _leftDetailItemRectangles = new Dictionary<string, Rectangle>();
                var listener = new FilteredEventListener();
                var locationTextExtractionStrategy = new LocationTextExtractionStrategy();
                // Find rectangles for needed keys in details.
                // var leftDetailsExtractionStrategy = new LeftDetailsExtractionStrategy(_orderedLeftDetailElementsBorders, _leftDetailItemRectangles);
                var extractionStrategy = listener.AttachEventListener(locationTextExtractionStrategy, confirmationFilter);

                var pdfCanvasProcessor = new PdfCanvasProcessor(listener);
                pdfCanvasProcessor.ProcessPageContent(pdf.GetFirstPage());
                ShowData(extractionStrategy.GetResultantText());
                //pdfCanvasProcessor.Reset();
                pdf.Close();
            }
        }

        private void ParsePdfUsingLeftDetailsExtractionStrategy() {
            InitPdfToReadData(_docName);
            var linesStartCoordinates = new List<(string line, Point startPoint)>();

            var docSize = pdf.GetFirstPage().GetPageSize();
            var detailsRectangle = new Rectangle(_leftSideWidth, docSize.GetHeight());
            var confirmationFilter = new TextRegionEventFilter(detailsRectangle);
            var listener = new FilteredEventListener();
            var customStrategy = new LeftDetailsExtractionStrategy(linesStartCoordinates);
            listener.AttachEventListener(customStrategy, confirmationFilter);

            var pdfCanvasProcessor = new PdfCanvasProcessor(listener);
            pdfCanvasProcessor.ProcessPageContent(pdf.GetFirstPage());

            GetDataFromParsedStrings(linesStartCoordinates);
            pdf.Close();
        }

        private void ShowData(string data) => Console.WriteLine($"Data:\n\n{data}");

        private void GetDataFromParsedStrings(List<(string line, Point startPoint)> linesStartCoordinates) {
            // Order by Y coord, group by y and concat results with same y to get single row if it was parsed as two or more.
            var grouped = linesStartCoordinates
                .OrderByDescending(s => s.startPoint.y)
                .GroupBy(s => s.startPoint.y)
                .ToList();

            // Sorted and splitted.
            var handledLines = linesStartCoordinates
                .OrderByDescending(s => s.startPoint.y)
                .GroupBy(s => s.startPoint.y)
                .Select(s =>
                    s.Count() == 1
                        ? s.First().line
                        : string.Join("", s.OrderBy(sbx => sbx.startPoint.x).Select(sbx => sbx.line))
                )
                .ToList();

            var separators = new char[] { ':', '$' };
            var keysAndResults = new Dictionary<string, string>() {
                { "Order Date:", "" },
                { "Confirmation #:", "" },
                { "Acct #:", "" },
                { "Est Ship Date:", "" },
                { "$", "" }
            };

            // Find key and add all strings after it until you meet some another key or string that contains separators.
            string currentKey = "";
            for (var i = 0; i < handledLines.Count; i++) {
                var nextKey = keysAndResults.Keys.FirstOrDefault(k => handledLines[i].StartsWith(k, StringComparison.OrdinalIgnoreCase));

                if (currentKey != nextKey && !string.IsNullOrEmpty(nextKey)) {
                    currentKey = nextKey;
                    var substring = handledLines[i].Substring(currentKey.Length, handledLines[i].Length - currentKey.Length);
                    keysAndResults[currentKey] += substring;
                    currentKey = null;
                }

                // this code if we need value from many lines
                 
                //if (!string.IsNullOrEmpty(currentKey) && separators.All(sep => !handledLines[i].Contains(sep))) {
                //    keysAndResults[currentKey] += handledLines[i];
                //} else if (separators.Any(sep => handledLines[i].Contains(sep))) {
                //    currentKey = null;
                //}
            }

            var result = GetOrder(keysAndResults);
        }

        private Order GetOrder(Dictionary<string, string> keysAndResults) =>
            new Order {
                OrderDate = keysAndResults["Order Date:"],
                Confirmation = keysAndResults["Confirmation #:"],
                Acct = keysAndResults["Acct #:"],
                EstShipDate = keysAndResults["Est Ship Date:"],
                OrderTotal = keysAndResults["$"]
            };
    }

    //public class LeftDetailsExtractionStrategy : LocationTextExtractionStrategy {
    //    // key and its right top point.
    //    private Point _lastStartBorderTopRightPoint;
    //    // rectangles for particular keys
    //    private readonly Dictionary<string, Rectangle> _leftDetailItemRectangles;
    //    private readonly Dictionary<string, string> _orderedLeftDetailElementsBorders;

    //    public LeftDetailsExtractionStrategy(
    //        Dictionary<string, string> orderedLeftDetailElementsBorders,
    //        Dictionary<string, Rectangle> leftDetailItemRectangles
    //    ) : base() {
    //        _orderedLeftDetailElementsBorders = orderedLeftDetailElementsBorders;
    //        _leftDetailItemRectangles = leftDetailItemRectangles;
    //    }

    //    public override void EventOccurred(IEventData data, EventType type) {
    //        if (type == EventType.RENDER_TEXT) {
    //            var renderInfo = (TextRenderInfo)data;
    //            var currentText = renderInfo.GetText();

    //            if (string.IsNullOrEmpty(currentText)) {
    //                return;
    //            }

    //            for (var i = 0; i < _orderedLeftDetailElementsBorders.Count; i++) {
    //                var currentBorderPair = _orderedLeftDetailElementsBorders.ElementAt(i);
    //                var (currentStartBorder, currentEndBorder) = (currentBorderPair.Key, currentBorderPair.Value);

    //                // find right top point of text
    //                // mb create Dictionary instead of single point
    //                if (currentText.StartsWith(currentStartBorder, StringComparison.InvariantCultureIgnoreCase)) {
    //                    var topRightVector = renderInfo.GetAscentLine().GetEndPoint();
    //                    _lastStartBorderTopRightPoint = new Point(topRightVector.Get(0), topRightVector.Get(1));
    //                }

    //                // check if its the end border of some point. then get rectangle from this point to prev.
    //                if (currentText.StartsWith(currentEndBorder, StringComparison.InvariantCultureIgnoreCase) && _lastStartBorderTopRightPoint != null) {
    //                    if (currentText.StartsWith("Total", StringComparison.InvariantCultureIgnoreCase)) {
    //                        var totalExists = true;
    //                    }
    //                    var currentEndBorderLeftX = renderInfo.GetDescentLine().GetStartPoint().Get(0);
    //                    var currentEndBorderTopY = renderInfo.GetAscentLine().GetEndPoint().Get(1);
    //                    var width = _lastStartBorderTopRightPoint.GetX() - currentEndBorderLeftX;
    //                    var height = _lastStartBorderTopRightPoint.GetY() - currentEndBorderTopY;

    //                    var rect = new Rectangle(currentEndBorderLeftX, currentEndBorderTopY, (float)width, (float)height);

    //                    _lastStartBorderTopRightPoint = null;
    //                    _leftDetailItemRectangles.Add(currentStartBorder, rect);
    //                }
    //            }
    //        }
    //    }
    //}

    public class LeftDetailsExtractionStrategy : LocationTextExtractionStrategy {
        /// <summary>
        /// Lines and their bottom left coordinates.
        /// </summary>
        private readonly List<(string line, Point startPoint)> _linesStartCoordinates;

        public LeftDetailsExtractionStrategy(List<(string line, Point startPoint)> stringStartCoordinates) : base() {
            _linesStartCoordinates = stringStartCoordinates;
        }

        public override void EventOccurred(IEventData data, EventType type) {
            if (type == EventType.RENDER_TEXT) {
                var renderInfo = (TextRenderInfo)data;
                var currentLine = renderInfo.GetText();

                if (!string.IsNullOrEmpty(currentLine)) {
                    var bottomLeftVector = renderInfo.GetDescentLine().GetStartPoint();
                    var bottomLeftPoint = new Point(bottomLeftVector.Get(0), bottomLeftVector.Get(1));

                    _linesStartCoordinates.Add((currentLine, bottomLeftPoint));
                }
            }
        }
    }





    //private readonly Dictionary<string, string> _orderedLeftDetailElementsBorders = new Dictionary<string, string> {
    //        { "Order Date:", "Confirmation #:" },
    //        { "Confirmation #", "Cust PO #:" },
    //        { "Acct #:", "Attn:" },
    //        { "Est Ship Date:", "Ship Via:" },
    //        { "Order Total:", "Total does not include shipping charges" }
    //    };

    public class CustomTextFilter : TextRegionEventFilter {
        public CustomTextFilter(Rectangle rect) : base(rect) { }

        public override bool Accept(IEventData data, EventType type) {
            if (type == EventType.RENDER_TEXT) {
                var renderInfo = data as TextRenderInfo;

                if (renderInfo != null) {
                    var actualText = renderInfo.GetText();

                    if (actualText.StartsWith("Acct #", StringComparison.InvariantCultureIgnoreCase)) {
                        // TODO: распарсить весь текст и найти координаты каждой строки, 
                        // Потом отсортировать координаты по Y и доставать из получившихся строк то что надо по тайтлам
                        var bottomLeft = renderInfo.GetDescentLine().GetStartPoint();
                        var topRight = renderInfo.GetAscentLine().GetEndPoint();

                        var p1 = new Point(bottomLeft.Get(0), bottomLeft.Get(1));
                        var p2 = new Point(topRight.Get(0), topRight.Get(1));

                        var rectangle = new Rectangle(20, 324, 83, 9);
                    }
                }

            }

            return false;
        }
    }
}
