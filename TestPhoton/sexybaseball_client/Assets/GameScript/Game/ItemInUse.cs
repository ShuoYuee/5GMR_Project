using System;

public class ItemInUse//TODO：此道具的使用(是否正在使用，是否可以重複使用)
{
    public EM_Item m_emItem;
    public bool m_bIsOneTimeUse;
    public DateTime m_dtTimeUpDate;

    public DateTime TimeUpDate => m_dtTimeUpDate;
}