namespace Crawler.BLL.Locator
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}
