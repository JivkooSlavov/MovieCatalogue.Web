using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MovieCatalogue.Common.EntityValidationConstants.ReviewConstants;
using static MovieCatalogue.Common.Messages;

namespace MovieCatalogue.Web.ViewModels.Review
{
    public class ReviewCreateViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        public string? MovieName { get; set; }

        [Required]
        [StringLength(ReviewContentMax, MinimumLength = ReviewContentMin, ErrorMessage = StringLengthErrorMessage)]
        public string Content { get; set; } = null!;
        public string? DatePosted { get; set; } = null!;
        public DateTime UpdatePosted { get; set; }

        public Guid CreatedByUserId { get; set; }
    }
}
