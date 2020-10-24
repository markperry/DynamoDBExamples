using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDBExamples
{
    public class ObjectPersistenceModelInterface
    {
        private readonly DynamoDBContext _context;

        public ObjectPersistenceModelInterface(IAmazonDynamoDB client, string tableName)
        {
            _context = new DynamoDBContext(client);
        }

        public async Task PutItem(Person person)
        {
            await _context.SaveAsync(person);
            Console.WriteLine($"Write suceeded");
        }
        
        public async Task GetItem(Guid id)
        {
            var item = await _context.LoadAsync<Person>(id);
            Console.WriteLine($"Retrieved person {id}, name is {item.Name}, age is {item.Age}");
        }
    }
}