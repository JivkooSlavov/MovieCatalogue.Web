
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Review
{
    public class ReviewIndexViewModel
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public string MovieTitle { get; set; } = null!;
        public ICollection<ReviewViewModel> Reviews { get; set; } = new HashSet<ReviewViewModel>();
    }
}
