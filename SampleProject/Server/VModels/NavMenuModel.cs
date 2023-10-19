using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SampleProject.Server.VModels;
public class NavMenuModel : BaseModel
{
    public NavMenuModel()
    {
        Name = string.Empty;
        Url = string.Empty;
        Title = true;
        IconComponent = new();
        Badge = new();
        LinkProps = string.Empty;
        var test = new List<Children>();
        test.Add(new VModels.Children { Name = "Ehsan", Url = "#" });
        Children = test;
    }

    public string Name { get; set; }
    public string Url { get; set; }
    public bool Title { get; set; }
    public IconComponent IconComponent { get; set; }
    public Badge Badge { get; set; }
    public string LinkProps { get; set; }
    public IList<Children> Children { get; set; }
    public bool Divider { get; set; }
}

public class Children
{
    public string Name { get; set; }
    public string Url { get; set; }
}

public class IconComponent : BaseEntity
{
    public string Name { get; set; } = "cil-drop";
}

public class Badge : BaseEntity
{
    public string Color { get; set; } = "success";
    public string text { get; set; } = "FREE";
}