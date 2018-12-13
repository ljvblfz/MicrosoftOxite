using Oxite.Modules.Plugins.Models;
using Oxite.Plugins;
using Oxite.Plugins.Attributes;
using Oxite.Plugins.Models;

[DisplayName("Twitter")]
[Description("This plugin can send a tweet on your behalf announcing newly published posts to your blog. It can also let you select which posts to tweet (or not) and show your twitter feed on your blog homepage.")]
[Authors("Erik Porter", "Nathan Heskew")]
[AuthorUrls("http://erikporter.com", "http://nathan.heskew.com")]
[IconLarge("t_large.png")]
[Category("Social")]
[Tags("Twitter")]
[HomePage("http://twitter.com")]
[Version(1, 0)]
public class TwitterPlugin
{
    private const int tweetCharCount = 140;

    [LabelText("user name or email address")]
    [Group("sign In", 1)]
    [Order(1)]
    [Appearance(Width = "30%")]
    [Required]
    public string Username { get; set; }
    
    [LabelText("password")]
    [Group("sign In", 1)]
    [Order(2)]
    [Appearance(Width = "25%")]
    [Required]
    public string Password { get; set; }

    [LabelText("tweet all blog posts by default")]
    [Group("settings", 2)]
    [Order(1)]
    [Appearance(Width = "auto")]
    [DefaultValue(true)]
    public bool TweetByDefault { get; set; }

    [LabelText("tweet prefix")]
    [Group("settings", 2)]
    [Order(2)]
    [Appearance(Width = "20%")]
    [StringValidation(MaxLength = 10)]
    [DefaultValue("BLOG: ")]
    public string PublishedPrefix { get; set; }

    public void RegisterTemplates(TemplateList templates)
    {
        templates.Add("PostCheckBox", ".commenting", SelectorType.InsertAfter, "ItemAdd");
        templates.Add("PostCheckBox", ".commenting", SelectorType.InsertAfter, "ItemEdit");
    }

    public void PostPublished(ContextItem<PostReadOnly> context)
    {
        sendTweet(context.Item);
    }

    private void sendTweet(PostReadOnly post)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(tweetCharCount);

        sb.Append(PublishedPrefix);

        sb.Append(makeTinyUrl(post.Url));
        sb.Append(" ");

        int maxTitleLength = tweetCharCount - sb.Length;

        if (post.Title.Length > maxTitleLength)
        {
            sb.Append(post.Title.Substring(0, maxTitleLength));
            sb.Append("...");
        }
        else
            sb.Append(post.Title);

        //TODO: (erikpo) Using the StringBuilder above, call out to twitter service and tweet it
    }

    private string makeTinyUrl(string url)
    {
        //TODO: (erikpo) call out to tinyurl and get a shorter url

        return url;
    }
}
