namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionRepository
    {
        string CollectionId { get; set; }
        string EntityName { get; }
    }
}
