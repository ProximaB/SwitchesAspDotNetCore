using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class ReviewRequest
    {
        public int MovieId { get; set; }

        public string Comment { get; set; }

        public short Rate { get; set; }
    }
}
