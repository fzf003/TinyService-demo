using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using TinyService.Domain.Entities;

namespace TinyService.WebApi.Models
{
    public partial class BlogPost : Entity<int>
    {
        private ICollection<BlogComment> _blogComments;
        static int i = 0;
        public BlogPost()
        {
            this.ID = Interlocked.Increment(ref i);
        }

        public string Title { get; set; }

      
        public string Body { get; set; }

 
        public bool AllowComments { get; set; }

    
        public int CommentCount { get; set; }

   
        public string Tags { get; set; }

        public string MetaKeywords { get; set; }

  
        public string MetaDescription { get; set; }


        public string MetaTitle { get; set; }

        public virtual ICollection<BlogComment> BlogComments
        {
            get { return _blogComments ?? (_blogComments = new List<BlogComment>()); }
            protected set { _blogComments = value; }
        }
    }
}