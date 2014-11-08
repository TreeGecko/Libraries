using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.AWS.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class DynamoDBHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tableName"></param>
        /// <param name="_tgs"></param>
        /// <returns></returns>
        public static PutItemRequest GetPutItemRequest(string _tableName, TGSerializedObject _tgs)
        {
            PutItemRequest pir = new PutItemRequest {TableName = _tableName, Item = GetDictionary(_tgs)};

            return pir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tableName"></param>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static PutItemRequest GetPutItemRequest(string _tableName, ITGSerializable _input)
        {
            TGSerializedObject bcn = _input.GetTGSerializedObject();

            return GetPutItemRequest(_tableName, bcn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_client"></param>
        /// <param name="_tableName"></param>
        /// <param name="_bcn"></param>
        public static void PutItem(AmazonDynamoDBClient _client, string _tableName, ITGSerializable _bcn)
        {
            PutItemRequest pir = GetPutItemRequest(_tableName, _bcn);

            _client.PutItem(pir);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_client"></param>
        /// <param name="_tableName"></param>
        /// <param name="_tgs"></param>
        public static void PutItem(AmazonDynamoDBClient _client, string _tableName, TGSerializedObject _tgs)
        {
            PutItemRequest pir = GetPutItemRequest(_tableName, _tgs);

            _client.PutItem(pir);
        }


        public static GetItemRequest GetGetItemRequest(string _tableName, string _column, string _keyValue)
        {
            GetItemRequest gir = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>() {{_column, new AttributeValue {S = _keyValue}}}
            };

            return gir;
        }

        public static GetItemRequest GetGetItemsRequest(string _tableName, string _column, string _keyValue)
        {
            GetItemRequest gir = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>() {{_column, new AttributeValue {S = _keyValue}}}
            };

            return gir;
        }


        public static Dictionary<string, AttributeValue> GetDictionary(TGSerializedObject _tgs)
        {
            Dictionary<string, AttributeValue> dictionary = new Dictionary<string, AttributeValue>();

            foreach (string key in _tgs.Properties.Keys)
            {
                TGSerializedProperty property = _tgs.Properties[key];

                if (!string.IsNullOrEmpty(property.SerializedValue))
                {
                    AttributeValue av = new AttributeValue() { S = property.SerializedValue };

                    dictionary.Add(property.PropertyName, av);
                }
            }

            dictionary.Add("TGObjectType", new AttributeValue { S = _tgs.TGObjectType });

            return dictionary;
        }


        public static void PopulateTGSerializedObject(TGSerializedObject _tgs,
                                                       Dictionary<string, AttributeValue> _values)
        {
            foreach (string key in _values.Keys)
            {
                AttributeValue av = _values[key];

                if (key == "TGObjectType")
                {
                    _tgs.TGObjectType = av.S;
                }
                else
                {
                    TGSerializedProperty tgp = new TGSerializedProperty(av.S);
                    _tgs.Properties.Add(key, tgp);
                }
            }
        }

  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AmazonDynamoDBClient GetDB()
        {
            string user = Config.GetSettingValue("AWSAccessKey");
            string password = Config.GetSettingValue("AWSSecretKey");

            return GetDB(user, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_accessKey"></param>
        /// <param name="_secretKey"></param>
        /// <returns></returns>
        public static AmazonDynamoDBClient GetDB(string _accessKey, string _secretKey)
        {
            AmazonDynamoDBClient db = new AmazonDynamoDBClient(_accessKey, _secretKey);

            return db;
        }
    }
}
