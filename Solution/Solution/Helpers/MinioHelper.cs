using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Solution.Helpers
{
    public static class MinioHelper
    {
        private const string File = "appsettings.json";

        private static Task<IMinioClient> BuildMinioClient(IConfigurationRoot config)
        {
            string endpoint = config["Minio:EndPoint"] ?? string.Empty;
            var accessKey = config["Minio:AccessKey"] ?? string.Empty;
            var secretKey = config["Minio:SecretKey"] ?? string.Empty;

            IMinioClient minio = new MinioClient()
                                    .WithEndpoint(endpoint)
                                    .WithCredentials(accessKey, secretKey)
                                    .Build();

            return Task.FromResult(minio);
        }

        public static async Task<bool> SendAsync(string objectName, Stream streamObject, string contentType)
        {

            try
            {
                IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile(File).Build();
                var bucketName = config["Minio:BucketName"] ?? string.Empty;
                var minio = await BuildMinioClient(config);
                // Make a bucket on the server, if not already present.
                var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
                bool found = await minio.BucketExistsAsync(beArgs).ConfigureAwait(false);
                if (!found)
                {
                    var mbArgs = new MakeBucketArgs()
                        .WithBucket(bucketName);
                    await minio.MakeBucketAsync(mbArgs).ConfigureAwait(false);
                }
                // Upload a file to bucket.
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithStreamData(streamObject)
                    .WithObjectSize(streamObject.Length)
                    .WithContentType(contentType);
                await minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);

                return true;
            }
            catch (MinioException ex)
            {
                
                return false;
            }
        }

        public static async Task<bool> RemoveAsync(string objectName)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var bucketName = config["Minio:BucketName"] ?? string.Empty;//"test";
                var minio = await BuildMinioClient(config);
                // Make a bucket on the server, if not already present.
                var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
                bool found = await minio.BucketExistsAsync(beArgs).ConfigureAwait(false);
                if (!found)
                {
                    var mbArgs = new MakeBucketArgs()
                        .WithBucket(bucketName);
                    await minio.MakeBucketAsync(mbArgs).ConfigureAwait(false);
                }
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName);

                await minio.RemoveObjectAsync(removeObjectArgs);

                return true;
            }
            catch (MinioException ex)
            {
                return false;
            }
        }

        public static string GetStaticUrlAsync(string objectName)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var bucketName = config["Minio:BucketName"] ?? string.Empty;//"test";
                string endpoint = config["Minio:EndPoint"] ?? string.Empty;
                return $"http://{endpoint}/{bucketName}/{objectName}";
            }
            catch (MinioException ex)
            {
                return string.Empty;
            }
        }
    }
}
