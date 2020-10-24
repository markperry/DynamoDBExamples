using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDBExamples
{
    public class LowLevelInterface
    {
        private readonly IAmazonDynamoDB _client;
        private readonly string _tableName;

        public LowLevelInterface(IAmazonDynamoDB client, string tableName)
        {
            _client = client;
            _tableName = tableName;
        }
        public async Task PutItem(Person person)
        {
            var putResponse = await _client.PutItemAsync(new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {
                        "id", new AttributeValue
                        {
                            S = person.Id.ToString()
                        }
                    },
                    {
                        "name", new AttributeValue
                        {
                            S = person.Name
                        }
                    },
                    {
                        "age", new AttributeValue
                        {
                            N = person.Age.ToString()
                        }
                    }
                }
            });
            
            Console.WriteLine($"Put status code: {putResponse.HttpStatusCode}");
        }

        public async Task GetItem(Guid id)
        {
            var item = await _client.GetItemAsync(new GetItemRequest(_tableName, new Dictionary<string, AttributeValue>
            {
                {"id", new AttributeValue(id.ToString())}
            }));

            var personsName = item.Item["name"].S;
            var personsAge = item.Item["age"].N;
            Console.WriteLine($"Retrieved person {id}, name is {personsName}, age is {personsAge}");
        }
    }
}