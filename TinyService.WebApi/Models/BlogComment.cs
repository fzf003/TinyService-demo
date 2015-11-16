using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using TinyService.Domain.Entities;

namespace TinyService.WebApi.Models
{
    public partial class BlogComment : Entity<int>
    {
          static int i = 0;
        public BlogComment()
        {
            this.ID = Interlocked.Increment(ref i);
        }
        public int CustomerId { get; set; }

        public string CommentText { get; set; }

        public int BlogPostId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

      
        public virtual BlogPost BlogPost { get; set; }

        public void SetBlogPost(BlogPost post)
        {
            this.BlogPost = post;
        }
    }
}