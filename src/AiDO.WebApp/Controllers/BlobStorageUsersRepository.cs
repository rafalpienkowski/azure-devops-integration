using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq;
using System.Threading.Tasks;

namespace AiDO.WebApp.Controllers
{
    public class BlobStorageUsersRepository : IUsersRepository
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _blobClient;
        private readonly string _containerName = "users";

        public BlobStorageUsersRepository(string storageAccountConnectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(storageAccountConnectionString);
            _blobClient = _storageAccount.CreateCloudBlobClient();
        }

        public async Task<IEnumerable<string>> GetUserNames()
        {
            var containtersResultSegment = await _blobClient.ListContainersSegmentedAsync(null);

            var container = _blobClient.GetContainerReference(_containerName);
            var blobResultSegment = await container.ListBlobsSegmentedAsync(null);
            var names = blobResultSegment.Results.Select(i => i.Uri.Segments.Last()).ToList();;

            if (names.Any())
            {
                return names;
            }

            return new [] { "No names to display" };
        }
    }
}
