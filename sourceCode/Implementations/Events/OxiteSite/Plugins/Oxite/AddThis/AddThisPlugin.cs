using Oxite.Modules.Plugins.Models;

public class AddThisPlugin
{
    public object Definition
    {
        get
        {
            return new
            {
                DisplayName = "AddThis",
                Description = "This plugin can show an AddThis button on blog posts for easy bookmarking and sharing.",
                Authors = "Nathan Heskew, Erik Porter",
                AuthorUrls = "http://nathan.heskew.com, http://erikporter.com",
                IconLarge = "addThis_32x32.png",
                IconSmall = "addThis_16x16.png",
                Category = "Social",
                Tags = "AddThis, Sharing",
                HomePage = "http://addthis.com",
                Version = "1.0"
            };
        }
    }

    public string Username { get; set; }
    public object UsernameDefinition
    {
        get
        {
            return new
            {
                LabelText = "Username (required to track analytics data - http://www.addthis.com/help/analytics-setup)",
                Group = new
                {
                    Name = "Analytics",
                    Order = 1
                },
                Appearance = new
                {
                    Width = "50%"
                }
            };
        }
    }

    public string ButtonText { get; set; }
    public object ButtonTextDefinition
    {
        get
        {
            return new
            {
                LabelText = "Text to show next to the AddThis button",
                Group = new
                {
                    Name = "Button Text",
                    Order = 2
                },
                Appearance = new
                {
                    Width = "35%"
                },
                DefaultValue = "Share"
            };
        }
    }

    public void RegisterTemplates(TemplateList templates)
    {
        templates.Add("AddThisButton", ".post .content", SelectorType.AppendTo, "Item");
    }
}
