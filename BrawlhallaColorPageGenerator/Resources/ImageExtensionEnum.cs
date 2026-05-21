namespace BrawlhallaColorPageGenerator;

public enum ImageExtensionEnum
{
    Png,
    Gif,
    Webp,
}

public static class ImageExtensionEnumExtensions
{
    public static string GetName(this ImageExtensionEnum imageExtension) => imageExtension switch
    {
        ImageExtensionEnum.Png => "png",
        ImageExtensionEnum.Gif => "gif",
        ImageExtensionEnum.Webp => "webp",
        _ => "ERROR",
    };
}