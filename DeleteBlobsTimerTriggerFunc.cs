using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzFunations
{
    public static class DeleteBlobsTimerTriggerFunc
    {

        [FunctionName("DeleteBlobTimerTriggerFunc")]
        public static void Run([TimerTrigger("%TimerSchedule%")] TimerInfo myTimer,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            BlobContainerClient blobContainerClient =
                new BlobContainerClient("Connectionstring", "blobContainerName");

            var blobs = blobContainerClient.GetBlobs();
            foreach (BlobItem blobItem in blobs)
            {
                blobContainerClient.DeleteBlobIfExistsAsync(blobItem.Name);
                log.LogInformation($"Blob Name {blobItem.Name} is deleted successfully.");
            }
        }
    }
}
