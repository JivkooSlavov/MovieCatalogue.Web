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
    public class ReviewEditViewModel
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }

        [Required]
        public string MovieName { get; set; } = null!;

        [Required]
        [Range(ReviewContentMin, ReviewContentMax, ErrorMessage = StringLengthErrorMessage)]
        public string Content { get; set; } = null!;

    }
}
