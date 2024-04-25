namespace CodeShellCore.Files.Uploads
{
    public interface IBlobContainerFactory
    {
        IBlobContainer GetContainer(string v);
    }
}