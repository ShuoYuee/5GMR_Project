using UnityEngine;
using System.Collections;
using ccU3DEngine;
/// <summary>
/// 聲音管理器
/// </summary>
public class AudioManager : MonoBehaviour
{
    public AudioSource _EffectAudio;
    public AudioSource _Bgm;
    public AudioSource _SoundAudio;
    /// <summary>
    /// 初始化
    /// </summary>
    void Awake()
    {
        //float musicVolumn = LocalDataManager.f_GetLocalDataIfNotExitSetData<float>(LocalDataType.Float_MusicVolumn, 1);
        //float effectVolumn = LocalDataManager.f_GetLocalDataIfNotExitSetData<float>(LocalDataType.Float_EffectVolumn, 1);
        //StaticValue.m_isPlayMusic = musicVolumn == 1 ? true : false;
        //StaticValue.m_isPlaySound = effectVolumn == 1 ? true : false;
        //if (!StaticValue.m_isPlayMusic)
        //{
        //    f_StopAudioMusic();
        //}
        //if (!StaticValue.m_isPlaySound)
        //{
        //    f_StopAudioButtle();
        //}
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 播放按鈕音效
    /// </summary>
    /// <param name="audioEffectType">音效類型</param>
    /// <param name="PlayTime">播放比例</param>
    public void f_PlayAudioEffect()
    {
        //AudioClip audioEffectClip = glo_Main.GetInstance().m_ResourceManager.f_GetAudioClip(0, (int)audioEffectType);
        //if (audioEffectClip == null)
        //{
        //    MessageBox.ASSERT("未能載入此音效！");
        //}
        //_ButtleAudio.clip = audioEffectClip;
        _EffectAudio.loop = false;
        _EffectAudio.volume = StaticValue.m_fEffectVolume;
        _EffectAudio.Play();
    }

    /// <summary>
    /// 播放遊戲背景音樂
    /// </summary>
    /// <param name="audioMusicType">音樂類型</param>
    /// <param name="isLoop">是否迴圈播放</param>
    public void f_PlayAudioMusic(bool isLoop = true)
    {
        //AudioClip audioMusicClip = glo_Main.GetInstance().m_ResourceManager.f_GetAudioClip(2, (int)audioMusicType);
        //if (audioMusicClip == null)
        //{
        //    MessageBox.ASSERT("未能載入此音效！");
        //}
        //_Bgm.clip = audioMusicClip;
        _Bgm.loop = isLoop;
        _Bgm.volume = StaticValue.m_fBgmVolume;
        _Bgm.Play();
    }

    /// <summary>
    /// 播放特效音效
    /// </summary>
    /// <param name="audioEffectType">音效類型</param>
    /// <param name="PlayTime">播放比例</param>
    public void f_PlayAudioSound()
    {
        //AudioClip audioEffectClip = glo_Main.GetInstance().m_ResourceManager.f_GetAudioClip(1, (int)audioEffectType);
        //if (audioEffectClip == null)
        //{
        //    MessageBox.ASSERT("未能載入此音效！");
        //}
        //_SoundAudio.clip = audioEffectClip;
        //_SoundAudio.loop = false;
        //_SoundAudio.SetScheduledEndTime((double)(PlayTime * audioEffectClip.length));
        _SoundAudio.volume = StaticValue.m_fSoundVolume;
        _SoundAudio.Play();
    }

    ///// <summary>
    ///// 播放技能音效
    ///// </summary>
    ///// <param name="audioEffectType">音效類型</param>
    ///// <param name="PlayTime">播放比例</param>
    //public void f_PlayAudioMagic(string strName, float pitch = 1f)
    //{
    //    AudioClip audioEffectClip = glo_Main.GetInstance().m_ResourceManager.f_GetAudioMagic(strName);
    //    if (audioEffectClip == null)
    //    {
    //        MessageBox.ASSERT("未能載入此音效！" + strName);
    //        return;
    //    }
    //    else
    //    {
    //        _EffectAudio.clip = audioEffectClip;
    //        _EffectAudio.loop = false;
    //        _EffectAudio.SetScheduledEndTime((double)(1 * audioEffectClip.length));
    //        _EffectAudio.volume = LocalDataManager.f_GetLocalData<float>(LocalDataType.Float_EffectVolumn);
    //        _EffectAudio.pitch = pitch;
    //        _EffectAudio.Play();
    //    }
    //}

    /// <summary>
    /// 停止背景音樂
    /// </summary>
    public void f_StopAudioMusic()
    {
        _Bgm.Stop();
    }
    public void f_StopAudioButtle()
    {
        _EffectAudio.Stop();
        _SoundAudio.Stop();
    }

    ///// <summary>
    ///// 播放背景音樂
    ///// </summary>
    //public void f_PlayAudioMusic()
    //{
    //    _Bgm.volume = LocalDataManager.f_GetLocalData<float>(LocalDataType.Float_MusicVolumn);
    //    _Bgm.Play();
    //}

    public void f_PlayAudioButtle()
    {
        _EffectAudio.Play();
        _SoundAudio.Play();
    }
    /// <summary>
    /// 暫停背景音樂
    /// </summary>
    public void f_PauseAudioMusic()
    {
        _Bgm.Pause();
        _Bgm.enabled = false;
    }
    public void f_PauseAudioButtle()
    {
        _EffectAudio.enabled = false;
        _SoundAudio.enabled = false;
    }

    ///// <summary>
    ///// 恢復暫停背景音樂
    ///// </summary>
    //public void f_UnPauseAudioMusic()
    //{
    //    _Bgm.volume = LocalDataManager.f_GetLocalData<float>(LocalDataType.Float_MusicVolumn);
    //    _Bgm.UnPause();
    //    _Bgm.enabled = true;
    //}

    public void f_UnPauseAudioButtle()
    {
        _EffectAudio.clip = null;
        _SoundAudio.clip = null;
        _EffectAudio.enabled = true;
        _SoundAudio.enabled = true;
    }
}
