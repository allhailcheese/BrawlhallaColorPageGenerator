namespace BrawlhallaColorPageGenerator;

public enum DescriptionTypeEnum
{
    Desc,
    Cost,
}

public static class DescriptionTypeEnumExtensions
{
    public static string GetName(this DescriptionTypeEnum descriptionType) => descriptionType switch
    {
        DescriptionTypeEnum.Desc => "desc",
        DescriptionTypeEnum.Cost => "cost",
        _ => "ERROR",
    };
}