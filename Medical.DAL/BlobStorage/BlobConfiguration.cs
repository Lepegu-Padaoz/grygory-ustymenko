namespace Medical.DAL.BlobStorage
{
    public class BlobConfiguration
    {
        public string ConnectionString { get; set; }

        public BlobConfiguration(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
