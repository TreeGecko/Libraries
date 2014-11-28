using System;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using TreeGecko.Library.Common.Helpers;

namespace TreeGecko.Library.AWS.Helpers
{
    public static class S3Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AmazonS3Client GetS3()
        {
            string user = Config.GetSettingValue("AWSAccessKey", null);
            string password = Config.GetSettingValue("AWSSecretKey", null);

            return GetS3(user, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_accessKey"></param>
        /// <param name="_secretKey"></param>
        /// <returns></returns>
        public static AmazonS3Client GetS3(string _accessKey, string _secretKey)
        {
            AmazonS3Client s3;

            if (_accessKey == null
                && _secretKey == null)
            {
                s3 = new AmazonS3Client();
            }
            else
            {
                s3 = new AmazonS3Client(_accessKey, _secretKey); 
            }

            return s3;
        }

        

        public static string GetFolder(Guid _fileGuid)
        {
            string file = _fileGuid.ToString().ToLower();
            string first3 = file.Substring(0, 3);

            return first3;
        }

        public static byte[] GetFile(string _bucketName, string _filename)
        {
            AmazonS3Client client = GetS3();

            return GetFile(client, _bucketName, _filename);
        }

        public static byte[] GetFile(AmazonS3Client _client, string _bucketName,
            string _filename)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = _filename
            };

            using (GetObjectResponse response = _client.GetObject(request))
            {
                if (response != null
                    && response.ResponseStream != null)
                {
                    using (BinaryReader reader = new BinaryReader(response.ResponseStream))
                    {
                        byte[] buffer = reader.ReadBytes((Int32) response.ContentLength);

                        return buffer;
                    }
                }
            }

            return null;
        }

        public static void PersistFile(string _bucketName, string _filename,
            string _mimeType, byte[] _data)
        {
            AmazonS3Client client = GetS3();

            PersistFile(client, _bucketName, _filename, _mimeType, _data);
        }

        public static void PersistFile(AmazonS3Client _client, string _bucketName, string _filename,
            string _mimeType, byte[] _data)
        {

            using (MemoryStream ms = new MemoryStream(_data))
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = _filename,
                    InputStream = ms,
                    ContentType = _mimeType
                };

                PutObjectResponse response = _client.PutObject(request);
                if (response != null)
                {

                }
            }

        }
    }
}
