using Azure.Storage.Blobs;
using Medical.DAL.BlobStorage.Interfaces;

namespace Medical.DAL.BlobStorage
{
    public class BlobStorage : IBlobStorage
    {
        private readonly BlobServiceClient _client;

        private const string BlobContainerName = "medical";

        public BlobStorage(BlobConfiguration blobConfiguration)
        {
            _client = new BlobServiceClient(blobConfiguration.ConnectionString);
        }

        /// <summary>
        /// Check if a blob file exists in the Azure Blob Storage container by its filename.
        /// </summary>
        /// <param name="filename">The name of the blob file to check.</param>
        /// <returns>True if the file exists, false otherwise.</returns>
        public async Task<bool> ContainsFileByNameAsync(string filename) =>
            await _client.GetBlobContainerClient(BlobContainerName).GetBlobClient(filename).ExistsAsync();

        /// <summary>
        /// Upload a context to the Azure Blob Storage container with the given filename.
        /// </summary>
        /// <param name="filename">The name of the blob file to upload.</param>
        public async Task PutContextAsync(string filename) =>
            await _client.GetBlobContainerClient(BlobContainerName).GetBlobClient(filename).UploadAsync(new MemoryStream());

        /// <summary>
        /// Find a list of doctor IDs associated with a specific hospital in the Azure Blob Storage container.
        /// </summary>
        /// <param name="hospitalId">The unique identifier of the hospital.</param>
        /// <returns>A list of doctor IDs associated with the hospital.</returns>
        public List<int> FindDoctorByHospitalId(Guid hospitalId)
        {
            var doctors = _client.GetBlobContainerClient(BlobContainerName)
                                 .GetBlobs(prefix: hospitalId.ToString("N"))
                                 .AsPages(default, 1000)
                                 .SelectMany(dt => dt.Values).Select(bi => int.Parse(bi.Name.Split('_').Last()))
                                 .ToList();

            return doctors;
        }
    }
}
