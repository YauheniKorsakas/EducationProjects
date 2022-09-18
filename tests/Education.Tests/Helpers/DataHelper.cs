namespace Education.Tests.Helpers
{
    public class DataHelper
    {
        public string GetName() => "zheka";

        public string GetStatusMessage(bool status) {
            if (status) {
                return "Its ok";
            } else {
                return "Not ok";
            }
        }
    }
}
