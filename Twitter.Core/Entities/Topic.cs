using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entities;


namespace Twitter.Core.Entity
{
    public class Topic:BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<BlogTopic>BlogTopics { get; set; }
    }
}
