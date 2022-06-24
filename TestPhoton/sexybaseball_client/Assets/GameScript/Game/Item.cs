// Do not change number if it's in production.
public enum EM_Item
{
    None = 0,
    RPS_LuckyCharm = 1,
    SlotMach_NoDoublePlay = 2,
    SlotMach_HomerunCharm = 3,
    SlotMach_SluggingCharm = 4,
}

public static class ItemExtend
{
    public static string GetResPath(this EM_Item item)
    {
        switch (item)
        {
            case EM_Item.RPS_LuckyCharm:
            case EM_Item.SlotMach_NoDoublePlay:
            case EM_Item.SlotMach_HomerunCharm:
            case EM_Item.SlotMach_SluggingCharm:
                return StrItemIcon.IconRoot + GetPrefabName(item);
        }
        return "";
    }

    public static string GetPrefabName(this EM_Item item)
    {
        switch (item)
        {
            case EM_Item.RPS_LuckyCharm:
                return StrItemIcon.RPS_LuckyCharm;
            case EM_Item.SlotMach_NoDoublePlay:
                return StrItemIcon.SlotMach_NoDoublePlay;
            case EM_Item.SlotMach_HomerunCharm:
                return StrItemIcon.SlotMach_HomerunCharm;
            case EM_Item.SlotMach_SluggingCharm:
                return StrItemIcon.SlotMach_SluggingCharm;
        }
        return "";
    }
}