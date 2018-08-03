using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.DealTracking.Lib.Services
{
    public class AzureStorageService
    {
        private IServiceProvider ServiceProvider { get; set; }
        private CloudStorageAccount StorageAccount { get; set; }
        private CloudBlobContainer StorageContainer { get; set; }
        private const string TIMESTAMP_FORMAT = "yyyyMMddHHmmssffff";

        public AzureStorageService(IServiceProvider serviceProvider)
        {
            string storageAccountName = Environment.GetEnvironmentVariable("StorageAccountName");
            string storageAccountKey = Environment.GetEnvironmentVariable("StorageAccountKey");
            string storageContainer = "deal-tracking";

            this.ServiceProvider = serviceProvider;
            this.StorageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), useHttps: true);
            this.StorageContainer = this.Configure(storageContainer).GetAwaiter().GetResult();
        }

        private async Task<CloudBlobContainer> Configure(string storageContainer)
        {
            CloudBlobClient cloudBlobClient = this.StorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(storageContainer);
            await cloudBlobContainer.CreateIfNotExistsAsync();

            BlobContainerPermissions permissions = SetContainerPermission(false);
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            return cloudBlobContainer;
        }

        private BlobContainerPermissions SetContainerPermission(Boolean isPublic)
        {
            BlobContainerPermissions permissions = new BlobContainerPermissions();
            if (isPublic)
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            else
                permissions.PublicAccess = BlobContainerPublicAccessType.Off;
            return permissions;
        }

        public string GenerateFileName(string fileName)
        {
            return String.Format("{0}_{1}", DateTime.Now.ToString(TIMESTAMP_FORMAT), fileName);
        }

        public async Task UploadFile(MemoryStream stream, string Module, string fileName)
        {
            stream.Position = 0;
            CloudBlobContainer container = this.StorageContainer;
            CloudBlobDirectory dir = container.GetDirectoryReference(Module);
            CloudBlockBlob blob = dir.GetBlockBlobReference(fileName);
            await blob.UploadFromStreamAsync(stream);
        }

        public async Task<MemoryStream> DownloadFile(string Module, string fileName)
        {
            CloudBlobContainer container = this.StorageContainer;
            CloudBlobDirectory dir = container.GetDirectoryReference(Module);
            CloudBlockBlob blob = dir.GetBlockBlobReference(fileName);
            await blob.FetchAttributesAsync();

            MemoryStream stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);

            return stream;
        }

        public async Task DeleteImage(string Module, string fileName)
        {
            CloudBlobContainer container = this.StorageContainer;
            CloudBlobDirectory dir = container.GetDirectoryReference(Module);

            CloudBlockBlob blob = dir.GetBlockBlobReference(fileName);
            await blob.DeleteIfExistsAsync();
        }
    }
}
