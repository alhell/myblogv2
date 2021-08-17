using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class BlogPost
    {
        public string PostRoute { get; set; }
        public DateTime Date { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string MainImageUrl { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
