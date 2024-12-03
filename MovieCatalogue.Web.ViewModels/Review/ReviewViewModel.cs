using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Review
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }

        public Guid MovieId { get; set; }
        public string Content { get; set; } = null!;
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? MovieTitle { get; set; }

        public bool IsDeleted { get; set; }
    }
}
