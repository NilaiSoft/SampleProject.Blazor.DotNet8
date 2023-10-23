namespace SampleProject.Server.Factory
{
    public interface INavMenuModelFactory
    {
        Task<IList<NavMenuModel>> PrepareNavMenuModelAsync(IList<NavMenu> product);
    }
}
