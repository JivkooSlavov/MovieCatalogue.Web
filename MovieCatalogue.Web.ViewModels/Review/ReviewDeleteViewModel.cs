using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Review
{
    public class ReviewDeleteViewModel
    {
        public Guid Id { get; set; } 
        public Guid MovieId { get; set; } 
        public string MovieTitle { get; set; } = null!; 
        public string Content { get; set; } = null!; 
        public DateTime CreatedAt { get; set; } 

        public Guid CreatedByUserId { get; set; }
    }

}
