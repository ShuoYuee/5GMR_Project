using System;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class Interactable_GameCtrl : MonoBehaviour
{
    private AudioSource _AudioSource;

    protected List<Interactable_Component> _Components = new List<Interactable_Component>();
    protected bool _bInteract = true;

    private void Awake()
    {
        _AudioSource = gameObject.GetComponent<AudioSource>();
        if (_AudioSource == null)
        {
            _AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void f_AddComponent(Interactable_Component _Component)
    {
        _Components.Add(_Component);
    }

    public virtual void f_Interactable()
    {
        if (!_bInteract) { return; }
        for(int i = 0; i < _Components.Count; i++)
        {
            _Components[i].f_Interactable();
            _AudioSource.clip = glo_Main.GetInstance().m_ResourceManager.f_CreateAudio(_Components[i].f_GetAudio());
        }
    }

    public virtual void f_Interactable(int iIndex)
    {
        if (!_bInteract) { return; }
        _Components[iIndex].f_Interactable();
        _AudioSource.clip = glo_Main.GetInstance().m_ResourceManager.f_CreateAudio(_Components[iIndex].f_GetAudio());
    }

    public virtual void f_InteractableV1(int iSet)
    {
        if (!_bInteract) { return; }
        for (int i = 0; i < _Components.Count; i++)
        {
            _Components[i].f_Interactable(iSet);
            _AudioSource.clip = glo_Main.GetInstance().m_ResourceManager.f_CreateAudio(_Components[i].f_GetAudio());
        }
    }

    public virtual void f_InteractableV1(int iIndex, int iSet)
    {
        if (!_bInteract) { return; }
        _Components[iSet].f_Interactable(iSet);
        _AudioSource.clip = glo_Main.GetInstance().m_ResourceManager.f_CreateAudio(_Components[iIndex].f_GetAudio());
    }

    public virtual void f_InteractableEM(int iEM)
    {
        for (int i = 0; i < _Components.Count; i++)
        {
            if (_Components[i].f_CheckID(iEM))
            {
                _Components[i].f_Interactable();
                _AudioSource.clip = glo_Main.GetInstance().m_ResourceManager.f_CreateAudio(_Components[i].f_GetAudio());
            }
        }
    }

    public virtual void f_InteractableEM(int iEM, int iSet)
    {
        for (int i = 0; i < _Components.Count; i++)
        {
            if (_Components[i].f_CheckID(iEM))
            {
                _Components[i].f_Interactable(iSet);
                _AudioSource.clip = glo_Main.GetInstance().m_ResourceManager.f_CreateAudio(_Components[i].f_GetAudio());
            }
        }
    }
}

public class Interactable_Component
{
    private int _iId;
    private string[] _strAudioGroup;

    public Interactable_Component(int iId)
    {
        _iId = iId;
    }

    public bool f_CheckID(int iId)
    {
        return _iId == iId ? true : false;
    }

    public virtual void f_Init(CharacterDT tCharacterDT)
    {
        _strAudioGroup = ccMath.f_String2ArrayString(tCharacterDT.szAudioSource, ";");
    }

    public virtual void f_Interactable()
    {

    }

    public virtual void f_Interactable(int iSet)
    {

    }

    public string f_GetAudio(int iId = 0)
    {// Audio/ + strFile
        try
        {
            return _strAudioGroup[iId];
        }
        catch
        {
            return "";
        }
    }
}
