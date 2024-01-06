using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Dtos.BlogDtos
{
    public class BlogListDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedCount { get; set; }
        public string Title { get; set; }

    }
}
