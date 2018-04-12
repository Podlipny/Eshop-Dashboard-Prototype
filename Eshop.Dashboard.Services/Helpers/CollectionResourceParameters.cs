namespace Eshop.Dashboard.Services.Helpers
{
  /// <summary>
  /// Collection resource definition for retrieving items
  /// </summary>
  public class CollectionResourceParameters
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

    public string OrderBy { get; set; }

    public string Fields { get; set; }
  }
}
