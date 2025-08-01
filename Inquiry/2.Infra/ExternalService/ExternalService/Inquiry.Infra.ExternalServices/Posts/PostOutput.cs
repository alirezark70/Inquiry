using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.ExternalServices.Posts
{
    public class PostOutput
    {
        public int Id { get; set; }

        public int userId { get; set; }

        public string title { get; set; }

        public string body { get; set; }
    }
}
