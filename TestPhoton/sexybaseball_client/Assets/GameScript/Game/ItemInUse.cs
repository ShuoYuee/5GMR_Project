using System;

public class ItemInUse//TODO�G���D�㪺�ϥ�(�O�_���b�ϥΡA�O�_�i�H���ƨϥ�)
{
    public EM_Item m_emItem;
    public bool m_bIsOneTimeUse;
    public DateTime m_dtTimeUpDate;

    public DateTime TimeUpDate => m_dtTimeUpDate;
}