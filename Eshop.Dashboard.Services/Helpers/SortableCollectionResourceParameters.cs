namespace Eshop.Dashboard.Services.Helpers
{
  /// <summary>
  /// Collection resource definition for retrieving items
  /// </summary>
  public class SortableCollectionResourceParameters : CollectionResourceParameters
  {
    public string OrderBy { get; set; }

    public string Fields { get; set; }
  }
}
