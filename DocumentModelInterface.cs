using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDBExamples
{
    public class DocumentModelInterface
    {
        private readonly Table _table;

        public DocumentModelInterface(IAmazonDynamoDB client, string tableName)
        {
            _table = Table.LoadTable(client, tableName);
        }

        public async Task PutItem(Person person)
        {
            var putResponse = await _table.PutItemAsync(new Document
            {
                {"id", new Primitive(person.Id.ToString())},
                {"name", new Primitive(person.Name)},
                {"age", new Primitive(person.Age.ToString())}
            });

            Console.WriteLine("Write succeeded");
        }

        public async Task GetItem(Guid id)
        {
            var item = await _table.GetItemAsync(new Primitive(id.ToString()));
            Console.WriteLine($"Retrieved person {id}, name is {item["name"]}, age is {item["age"]}");
        }
    }
}