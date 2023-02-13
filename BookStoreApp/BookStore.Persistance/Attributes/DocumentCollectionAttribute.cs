using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Mongo.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DocumentCollectionAttribute : Attribute
    {
        public string CollectionName { get; }
        public DocumentCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
