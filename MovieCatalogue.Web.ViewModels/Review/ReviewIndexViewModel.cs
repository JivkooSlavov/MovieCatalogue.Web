
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Review
{
    public class ReviewIndexViewModel
    {
        public Guid MovieId { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string MovieTitle { get; set; } = null!;
        public List<ReviewViewModel> Reviews { get; set; } = new();
    }
}
