namespace CodeShellCore.Data
{
    public interface IEditable
    {        
        string State { get; set; }
    }

    public interface IEditable<TPrime> : IEditable
    {
        TPrime Id { get; set; }
    }
}
