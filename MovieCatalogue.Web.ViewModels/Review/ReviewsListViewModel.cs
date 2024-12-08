using MovieCatalogue.Web.ViewModels.Review;

public class ReviewsListViewModel
{
    public IEnumerable<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
