namespace Eshop.Dashboard.Services.Helpers
{
  /// <summary>
  /// Product resource definition for retrieving products
  /// </summary>
  public class ProductResourceParameters
  {
    const int MaxPageSize = 100;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
      get { return _pageSize; }
      set {  _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
    }

    public string SearchQuery { get; set; }

    public string OrderBy { get; set; } = "Name";

    public string Fields { get; set; }
  }
}
