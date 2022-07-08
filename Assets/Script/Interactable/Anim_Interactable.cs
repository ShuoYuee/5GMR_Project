using System;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class Anim_Interactable : Interactable_Component
{
    private Animator _Animator;
    private string _strInteract = "Interactable";
    private string[] _strAnim;

    public Anim_Interactable() : base((int)EM_InterState.Anim)
    {

    }

    public override void f_Init(CharacterDT tCharacterDT)
    {
        base.f_Init(tCharacterDT);
        _strAnim = ccMath.f_String2ArrayString(tCharacterDT.szAnimGroup, ";");
    }

    public void f_Init(Animator tAnimator)
    {
        _Animator = tAnimator;
    }

    public override void f_Interactable()
    {
        base.f_Interactable();
        try
        {
            GameTools.f_PlayAnimator(_Animator, _strAnim[0], 1f, true);
        }
        catch
        {
            GameTools.f_PlayAnimator(_Animator, _strInteract, 1f, true);
        }
    }

    public override void f_Interactable(int iSet)
    {
        base.f_Interactable(iSet);
        try
        {
            GameTools.f_PlayAnimator(_Animator, _strAnim[iSet], 1f, true);
        }
        catch
        {
            GameTools.f_PlayAnimator(_Animator, _strInteract, 1f, true);
        }
    }
}
