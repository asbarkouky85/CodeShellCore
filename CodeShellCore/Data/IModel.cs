namespace CodeShellCore.Data
{
    public interface IModel { }
    public interface IModel<TPrimary> : IModel
    {
        TPrimary Id { get; set; }
    }
}
