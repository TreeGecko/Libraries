using System.Collections.Generic;
using Amazon.SQS;
using Amazon.SQS.Model;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.AWS.Helpers
{
    public static class SQSHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AmazonSQSClient GetSQS()
        {
            string user = Config.GetSettingValue("AWSAccessKey");
            string password = Config.GetSettingValue("AWSSecretKey");

            return GetSQS(user, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_accessKey"></param>
        /// <param name="_secretKey"></param>
        /// <returns></returns>
        public static AmazonSQSClient GetSQS(string _accessKey, string _secretKey)
        {
            AmazonSQSClient sqs = new AmazonSQSClient(_accessKey, _secretKey);

            return sqs;
        }


        public static void SendMessage(AmazonSQSClient _sqs,
                                       string _queueUrl,
                                       TGSerializedObject _tgs)
        {
            SendMessageRequest request = new SendMessageRequest {QueueUrl = _queueUrl, MessageBody = _tgs.ToString()};

            _sqs.SendMessage(request);
        }

        public static void DeleteMessage(AmazonSQSClient _sqs,
                                       string _queueUrl,
                                       string _receiptHandle)
        {
            DeleteMessageRequest request = new DeleteMessageRequest
            {
                QueueUrl = _queueUrl,
                ReceiptHandle = _receiptHandle
            };

            DeleteMessageResponse response = _sqs.DeleteMessage(request);

            if (response != null)
            {
                
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sqs"></param>
        /// <param name="_dlqUrl"></param>
        /// <param name="_queueUrl"></param>
        public static void ResetDLQ(AmazonSQSClient _sqs,
                                    string _dlqUrl,
                                    string _queueUrl)
        {
            if (_dlqUrl != null
                && _queueUrl != null)
            {
                Dictionary<string, TGSerializedObject> messages = ReceiveAllMessages(_sqs, _dlqUrl);

                foreach (string handle in messages.Keys)
                {
                    TGSerializedObject bcn = messages[handle];

                    SendMessage(_sqs, _queueUrl, bcn);
                    DeleteMessage(_sqs, _dlqUrl, handle);
                }
            }
        }

        public static Dictionary<string, TGSerializedObject> ReceiveAllMessages(AmazonSQSClient _client,
                                                                                 string _url)
        {
            Dictionary<string, TGSerializedObject> messages = new Dictionary<string, TGSerializedObject>();

            bool bolDone = false;

            do
            {
                ReceiveMessageRequest request = new ReceiveMessageRequest
                {
                    QueueUrl = _url, 
                    MaxNumberOfMessages = 10
                };

                ReceiveMessageResponse response = _client.ReceiveMessage(request);
                if (response != null)
                {
                    foreach (Message message in response.Messages)
                    {
                        if (message != null)
                        {
                            string receiptHandle = message.ReceiptHandle;
                            string body = message.Body;

                            if (body != null)
                            {
                                TGSerializedObject bcnSerialized = new TGSerializedObject(body);

                                messages.Add(receiptHandle, bcnSerialized);
                            }
                        }
                    }

                    if (response.Messages.Count == 0)
                    {
                        bolDone = true;
                    }
                }
            } while (!bolDone);


            return messages;
        }

        public static void SendMessage(AmazonSQSClient _sqs,
                                       string _queueUrl,
                                       ITGSerializable _tgSerializable)
        {
            TGSerializedObject bcn = _tgSerializable.GetTGSerializedObject();

            SendMessage(_sqs, _queueUrl, bcn);
        }

        public static TGSerializedObject ReceiveMessage(AmazonSQSClient _sqs,
                                                         string _queueUrl,
                                                         out string _receiptHandle)
        {
            ReceiveMessageRequest request = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 1,
                WaitTimeSeconds = 1
            };

            ReceiveMessageResponse response = _sqs.ReceiveMessage(request);
            if (response != null
                && response.Messages != null
                && response.Messages.Count > 0)
            {
                Message message = response.Messages[0];

                if (message != null)
                {
                    _receiptHandle = message.ReceiptHandle;
                    string body = message.Body;

                    if (body != null)
                    {
                        TGSerializedObject tgs = new TGSerializedObject(body);

                        return tgs;
                    }
                }

            }

            _receiptHandle = null;
            return null;
        }
    }
}
