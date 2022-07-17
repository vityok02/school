namespace SchoolNamespace;

[Flags]
public enum RoomType
{
    Regular = 1,
    Math = 2,
    Biology = 4,
    Literature = 8,
    Informatic = 16,
    Gym = 32,
    Physics = 64,
    Hall = 128,
    Workshop = 256,
}

public static class RoomTypeExt
{
    public static readonly IDictionary<int, string> RoomTypes = new Dictionary<int, string>()
    {
        { (int)RoomType.Regular, nameof(RoomType.Regular) },
        { (int)RoomType.Math, nameof(RoomType.Math) },
        { (int)RoomType.Biology, nameof(RoomType.Biology) },
        { (int)RoomType.Literature, nameof(RoomType.Literature) },
        { (int)RoomType.Informatic, nameof(RoomType.Informatic) },
        { (int)RoomType.Gym, nameof(RoomType.Gym) },
        { (int)RoomType.Physics, nameof(RoomType.Physics) },
        { (int)RoomType.Hall, nameof(RoomType.Hall) },
        { (int)RoomType.Workshop, nameof(RoomType.Workshop) }
    };
}
