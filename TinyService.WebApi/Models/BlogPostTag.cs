using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyService.WebApi.Models
{
    public partial class BlogPostTag
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tagged product count
        /// </summary>
        public int BlogPostCount { get; set; }
    }
}