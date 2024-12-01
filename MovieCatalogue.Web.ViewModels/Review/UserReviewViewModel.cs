using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Review
{
    public class UserReviewViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string MovieTitle { get; set; } = null!;
        public Guid MovieId { get; set; }
    }
}
