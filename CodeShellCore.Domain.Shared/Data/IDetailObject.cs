namespace CodeShellCore.Data
{
    public interface IEditable
    {
        string State { get; set; }
    }

    public interface IDetailObject<TPrime> : IEditable
    {
        TPrime Id { get; set; }
        bool Selected { get; set; }
    }
}
