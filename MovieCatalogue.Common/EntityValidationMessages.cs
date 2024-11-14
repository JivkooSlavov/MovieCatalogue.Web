using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Common
{
    public static class Messages
    {
        public const string RequireErrorMessage = "The field {0} is required";
        public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1} characters long";

        public const string ReleaseDateRequiredMessage = "Release date is required in format dd-MM-yyyy";

    }
}
