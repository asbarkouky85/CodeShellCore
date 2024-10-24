namespace CodeShellCore.Data
{
    public interface IEntityDto<TPrime>
    {
        TPrime Id { get; set; }
        bool Selected { get; set; }
    }
}
