namespace SampleProject.Server.VModels;
public class NavMenuModel : BaseModel
{
    public NavMenuModel()
    {
        Name = string.Empty;
        Url = string.Empty;
        Title = true;
        IconComponent_Name = string.Empty;
        BadgeColor = string.Empty;
        BadgeText = string.Empty;
        LinkProps = string.Empty;
        ParentId = new int();
    }

    public string Name { get; set; }
    public string Url { get; set; }
    public bool Title { get; set; }
    public string IconComponent_Name { get; set; }
    public string BadgeColor { get; set; }
    public string BadgeText { get; set; }
    public string LinkProps { get; set; }
    public int ParentId { get; set; }
}