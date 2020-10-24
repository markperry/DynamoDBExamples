using System;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDBExamples
{
    [DynamoDBTable("person")]
    public class Person
    {
        [DynamoDBHashKey("id")]
        public Guid Id { get; set; }
        
        [DynamoDBProperty("name")]
        public string Name { get; set; }
        
        [DynamoDBProperty("age")]
        public int Age { get; set; }
    }
}