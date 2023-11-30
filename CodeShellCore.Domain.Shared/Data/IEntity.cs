namespace CodeShellCore.Data
{
    public interface IModel { }
    public interface IEntity<TPrimary> : IModel
    {
        TPrimary Id { get; set; }
    }
}
