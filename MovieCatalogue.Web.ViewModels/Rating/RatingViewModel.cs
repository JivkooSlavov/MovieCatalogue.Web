using MovieCatalogue.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MovieCatalogue.Common.EntityValidationConstants.RatingConstants;
using static MovieCatalogue.Common.Messages;

namespace MovieCatalogue.Web.ViewModels.Rating
{
    public class RatingViewModel
    {
        [Required]
        [Range(RatingValueMin, RatingValueMax, ErrorMessage = StringLengthErrorMessage)]
        public int Value { get; set; }
        public Guid MovieId { get; set; }
    }
}
