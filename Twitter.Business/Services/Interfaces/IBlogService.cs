using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.BlogDtos;
using Twitter.Business.Dtos.TopicDtos;

namespace Twitter.Business.Services.Interfaces
{
    public interface IBlogService
    {
        public IEnumerable<BlogListDto> GetAll();
        public Task<BlogDetailDto> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateDto model);
        public Task RemoveAsync(int id);
    }
}
