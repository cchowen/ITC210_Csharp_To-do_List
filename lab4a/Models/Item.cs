using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4a.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserId")]
        public int UserId { get; set; }
        [BsonElement("Text")]
        public string Text { get; set; }
        [BsonElement("Done")]
        public bool Done { get; set; }
        [BsonElement("Date")]
        public DateTime Date { get; set; }
    }
}
