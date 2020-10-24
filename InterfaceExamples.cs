using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDBExamples
{
    public static class InterfaceExamples
    {
        public static async Task Main()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = "Bob",
                Age = 30
            };
            var client = new AmazonDynamoDBClient(RegionEndpoint.EUWest1);
            var tableName = "person";
            
            var lowLevel = new LowLevelInterface(client, tableName);
            await lowLevel.PutItem(person);
            await lowLevel.GetItem(person.Id);

            person.Id = Guid.NewGuid(); // Ensure we write a different record
            var documentModelInterface = new DocumentModelInterface(client, tableName);
            await documentModelInterface.PutItem(person);
            await documentModelInterface.GetItem(person.Id);
            
            person.Id = Guid.NewGuid(); // Ensure we write a different record
            var objectPersistenceModelInterface = new ObjectPersistenceModelInterface(client, tableName);
            await objectPersistenceModelInterface.PutItem(person);
            await objectPersistenceModelInterface.GetItem(person.Id);

        }
    }
}