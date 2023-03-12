namespace NLayer.Core.Models
{
    public class DatabaseOptions
    {
        public const string ConnectionStringName = "NlayerDb";
        public const string DatabaseSection = "Database";

        public string ConnectionString { get; set; }
        public int RetryCount { get; set; }
        public int OperationTimeout { get; set; }
    }
}
