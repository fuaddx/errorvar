using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.Entities
{
    public class Blog:BaseEntity
    {
        [MaxLength(1024)]
        public string Content { get; set; }

        public string Author { get; set; }
        [NotMapped]
        public DateTime UpdatedTime { get; set; }

        [NotMapped]
        public int UpdatedCount { get; set; }

        public string Title { get; set; }

        public ICollection<Files> Files { get; set; }
        public IEnumerable<BlogTopic> BlogTopics { get; set; }
    }
}
