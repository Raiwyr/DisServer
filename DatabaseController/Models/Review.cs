using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseController.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int Assessment { get; set; }

        public string Message { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime DateReview { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
