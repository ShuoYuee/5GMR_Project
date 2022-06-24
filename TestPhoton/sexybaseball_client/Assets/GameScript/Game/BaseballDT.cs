[System.Flags]
public enum Flag_Bases : short
{
    None = 0x00,
    OnBase1 = 0x01,
    OnBase2 = 0x02,
    OnBase3 = 0x04,
}

public class BaseballState
{
    public bool m_bOnBase1 = false;
    public bool m_bOnBase2 = false;
    public bool m_bOnBase3 = false;
}


public static class EM_BaseballResultExtend
{
    public static string GetLangKey(this EM_BaseballResult item)
    {
        switch (item)
        {
            case EM_BaseballResult.Single: return "StrEM_SlotMachine_Single";
            case EM_BaseballResult.Double: return "StrEM_SlotMachine_Double";
            case EM_BaseballResult.Triple: return "StrEM_SlotMachine_Triple";
            case EM_BaseballResult.HomeRun: return "StrEM_SlotMachine_HomeRun";
            case EM_BaseballResult.Strikeout: return "StrEM_SlotMachine_StrikeOut";
            case EM_BaseballResult.Groundout: return "StrEM_SlotMachine_GroundOut";
            case EM_BaseballResult.Putout: return "StrEM_SlotMachine_PutOut";
            case EM_BaseballResult.DoublePlay: return "StrEM_SlotMachine_DoublePlay";
        }
        return $"!!{item}!!";
    }
}

public enum EM_BaseballResult//棒球結果與拉霸機結果一起
{
    /// <summary>
    /// 無
    /// </summary>
    None = 0,

    /// <summary>
    /// 安打，前進一個壘包
    /// </summary>
    Single,

    /// <summary>
    /// 安打，前進兩個壘包
    /// </summary>
    Double,

    /// <summary>
    /// 安打，前進三個壘包
    /// </summary>
    Triple,

    /// <summary>
    /// 全壘打
    /// </summary>
    HomeRun,

    /// <summary>
    /// 三振
    /// </summary>
    Strikeout,

    /// <summary>
    /// 刺殺
    /// </summary>
    Groundout,

    /// <summary>
    /// 接殺
    /// </summary>
    Putout,

    /// <summary>
    /// 雙殺
    /// </summary>
    DoublePlay
}