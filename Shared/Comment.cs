using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }

        public bool IsApproved { get; set; }
    }
}
