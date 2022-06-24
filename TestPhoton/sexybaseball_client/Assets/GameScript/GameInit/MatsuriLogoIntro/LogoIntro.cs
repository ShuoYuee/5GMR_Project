using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LogoIntro : MonoBehaviour
{
    [Tooltip("在編輯器中可選擇跳過，編譯後的版本不會跳過。改完記得Build AB。")]
    public bool m_bSkipIntro = false;
    public UnityEvent m_onLogoCompleted;

    // Called from the animation in its animator.
    private void CompleteIntro()
    {
        m_onLogoCompleted.Invoke();
    }
}
