using Education.Web.Services.Interfaces;

namespace Education.Web.Services {
    public class DataService : IDataService {
        public string GetData() => "Some data from service.";
    }
}
