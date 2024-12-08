using MovieCatalogue.Web.ViewModels.Review;

public class UserReviewsListViewModel
{
    public IEnumerable<UserReviewViewModel> Reviews { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
