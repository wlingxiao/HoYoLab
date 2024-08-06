namespace HoYoLab.Types;

public enum Region : byte
{
    America,
    Europe,
    Asia,
    TwHkMo
}

public static class RegionExtensions
{
    public static string ToGenshinRegion(this Region region) => region switch
    {
        Region.America => "os_usa",
        Region.Europe => "os_euro",
        Region.Asia => "os_asia",
        Region.TwHkMo => "os_cht",
        _ => throw new IndexOutOfRangeException(nameof(region))
    };

    public static string ToHsrRegion(this Region region) => region switch
    {
        Region.America => "prod_official_usa",
        Region.Europe => "prod_official_eur",
        Region.Asia => "prod_official_asia",
        Region.TwHkMo => "prod_official_cht",
        _ => throw new IndexOutOfRangeException(nameof(region))
    };

    public static string ToZzzRegion(this Region region) => region switch
    {
        Region.America => "prod_gf_us",
        Region.Europe => "prod_gf_eu",
        Region.Asia => "prod_gf_jp",
        Region.TwHkMo => "prod_gf_sg",
        _ => throw new IndexOutOfRangeException(nameof(region))
    };

    public static Region ToRegion(this string region)
    {
        foreach (var r in Enum.GetValues<Region>())
        {
            if (region == r.ToGenshinRegion() || region == r.ToHsrRegion() || region == r.ToZzzRegion())
            {
                return r;
            }
        }

        throw new ArgumentException($"Unknown region: {region}", nameof(region));
    }
}
