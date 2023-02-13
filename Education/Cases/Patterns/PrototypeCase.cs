using Education.Core;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Prototype
{
    public class PrototypeCase : ICase {
        public async Task RunAsync() {
            IDocumentPrototype yearReport = new YearReportDocument();
            IDocumentPrototype monthReport = new MonthlyReportDocument();
            yearReport.Id = 1;
            monthReport.Id = 2;
            var yearReportCopy = yearReport.GetClone();
            var monthReportCopy = monthReport.GetClone();
            // 1, 2
        }

        public interface IDocumentPrototype {
            int Id { get; set; }
            IDocumentPrototype GetClone();
        }

        public class YearReportDocument : IDocumentPrototype
        {
            public int Id { get; set; }

            public IDocumentPrototype GetClone() {
                return (IDocumentPrototype)MemberwiseClone();
            }
        }

        public class MonthlyReportDocument : IDocumentPrototype
        {
            public int Id { get; set; }

            public IDocumentPrototype GetClone() {
                return (IDocumentPrototype)MemberwiseClone();
            }
        }
    }
}
