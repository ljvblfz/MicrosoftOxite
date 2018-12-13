using Oxite.Modules.Plugins.Models;

public class XboxLiveGamercardPlugin
{
    public object Definition
    {
        get
        {
            return new
            {
                DisplayName = "Xbox Live Gamercard",
                Description = "This plugin can show a Silverlight Xbox Gamercard in the sidebar of your blog homepage to show what you've been up to on Xbox Live.",
                Authors = new [] {"Erik Porter", "Nathan Heskew", "Adam Kinney" },
                AuthorUrls = new [] { "http://erikporter.com", "http://nathan.heskew.com", "http://adamkinney.com" },
                IconLarge = "xfw_32x32.png",
                //IconSmall = "addThis_16x16.png",
                Category = "Social",
                Tags = "Xbox",
                HomePage = "http://www.xbox.com/live",
                Version = new System.Version(1, 0)
            };
        }
    }

    public string Gamertag { get; set; }
    public object GamertagDefinition
    {
        get
        {
            return new
            {
                LabelText = "Xbox Live Gamertag",
                Required = true,
                StringValidation = new
                {
                    MaxLength = 15/*,
                    RegularExpressionMatcher = { limit to only letters, numbers and spaces }*/
                }
            };
        }
    }

    public void RegisterTemplates(TemplateList templates)
    {
        templates.Add("Gamercard", "div.secondary div.search", SelectorType.InsertAfter);
    }
}
