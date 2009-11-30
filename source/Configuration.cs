public class Configuration
{
    public string FilesDirectory { get; private set; }
    public string IndexDirectory { get; protected set; }

    public Configuration()
	{
	    FilesDirectory = @"F:\ptwiki-latest-pages-articles.xml\files";
	    IndexDirectory = @"F:\ptwiki-latest-pages-articles.xml\index";

	}
}
