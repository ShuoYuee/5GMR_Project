using ccU3DEngine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ccU3DEngine
{
    /// <summary>
    /// 语言管理器
    /// </summary>
    public class LanguageManager
    {
        Dictionary<UpdateText, UpdateText> _dirUpdateText = new Dictionary<UpdateText, UpdateText>();
        Dictionary<string, string> _dirData = new Dictionary<string, string>();
        List<string> _aSCList = new List<string>();

        private Locale _Locale = Locale.None;
        private static LanguageManager _Instance = null;
        public static LanguageManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new LanguageManager();
            }
            return _Instance;
        }

        #region 内部方法

        void SetLanguage(Locale tLocale)
        {
            if (_Locale == tLocale)
            {
                return;
            }
            _Locale = tLocale;
            _dirData.Clear();
            LoadSC();
        }

        private void LoadSC()
        {
            string strFileName = _Locale.ToString();
            strFileName = strFileName.ToLower();
            if (_aSCList.Count == 0)
            {
                MessageBox.ASSERT("注册语言文字脚本为0，语言脚本错误");
            }
            for (int i = 0; i < _aSCList.Count; i++)
            {
                AssetHelper.LoadAsset<TextAsset>(strFileName, _aSCList[i], (string uiName, TextAsset tTextAsset, object callBackData) =>
                {
                    string ppSQL = tTextAsset.text;
                    DispSC(ppSQL);
                    UpdateAllText();
                });
            }
        }

        private void DispSC(string strContent)
        {
            string[] aData = ccMath.f_String2ArrayString(strContent, "\r\n");
            for (int i = 0; i < aData.Length; i++)
            {
                string[] strArrays = aData[i].Split(new char[] { '\t' });
                if (strArrays.Length >= 2)
                {
                    if (_dirData.ContainsKey(strArrays[0]))
                    {
                        MessageBox.ASSERT("重复的语言关健值。" + strArrays[0]);
                        continue;
                    }
                    _dirData.Add(strArrays[0], strArrays[1]);
                }
            }
        }

        private string GetLocaleText(string strLanuageKey)
        {
            string ppSQL = "";
            if (_dirData.TryGetValue(strLanuageKey, out ppSQL))
            {
                return ppSQL;
            }
            MessageBox.ASSERT("未支持的文本。" + strLanuageKey);
            return "";
        }


        internal void f_SetText(UpdateText tUpdateText, Text text, string strLanuageKey)
        {
            string localeText = GetLocaleText(strLanuageKey);
            text.text = localeText;
            RegUpdateText(tUpdateText);
        }

        internal void f_SetText(UpdateText tUpdateText, Text text)
        {
            f_SetText(tUpdateText, text, text.text);
        }

        internal void f_SetText(UpdateText tUpdateText, Text text, params object[] values)
        {
            f_SetText(tUpdateText, text, text, values);
        }

        internal void f_SetText(UpdateText tUpdateText, Text text, string strLanuageKey, params object[] values)
        {
            string localeText = GetLocaleText(strLanuageKey);
            localeText = string.Format(localeText, values);
            text.text = localeText;
            RegUpdateText(tUpdateText);
        }

        private void RegUpdateText(UpdateText tUpdateText)
        {
            if (_dirUpdateText.ContainsKey(tUpdateText))
            {
                return;
            }
            _dirUpdateText.Add(tUpdateText, tUpdateText);
        }

        internal void f_UnRegUpdateText(UpdateText tUpdateText)
        {
            if (!_dirUpdateText.ContainsKey(tUpdateText))
            {
                return;
            }
            _dirUpdateText.Remove(tUpdateText);
        }

        private void UpdateAllText()
        {
            UpdateText tCurItem = null;
            try
            {
                foreach (KeyValuePair<UpdateText, UpdateText> tItem in _dirUpdateText)
                {
                    tCurItem = tItem.Value;
                    if (tItem.Value != null)
                    {
                        tItem.Value.f_Update();
                    }
                }
            }
            catch (Exception e)
            {
                f_UnRegUpdateText(tCurItem);
            }
        }

        #endregion


        #region 外部接口

        /// <summary>
        /// 切换语言，切换语言时会同时刷新相应的显示UI的文字信息
        /// </summary>
        /// <param name="tLocale"></param>
        public void f_ChangeLanguage(Locale tLocale)
        {
            if (tLocale == Locale.None)
            {
                MessageBox.ASSERT("未设置合法的语言类型. " + _Locale.ToString());
            }
            SetLanguage(tLocale);
        }

        /// <summary>
        /// 注册语言文字脚本
        /// </summary>
        /// <param name="strLanguageSC"></param>
        public void f_RegSC(string strLanguageSC)
        {
            if (_aSCList.Contains(strLanguageSC))
            {
                return;
            }
            _aSCList.Add(strLanguageSC);
        }

        /// <summary>
        /// 获取语言KEY对应的文字信息
        /// </summary>
        /// <param name="strLanuageKey">语言KEY</param>
        /// <param name="values">参数列表</param>
        /// <returns></returns>
        public string f_GetText(string strLanuageKey, params object[] values)
        {
            string localeText = GetLocaleText(strLanuageKey);
            localeText = string.Format(localeText, values);
            return localeText;
        }

        /// <summary>
        /// 获取语言KEY对应的文字信息
        /// </summary>
        /// <param name="strLanuageKey">语言KEY</param>
        /// <returns></returns>
        public string f_GetText(string strLanuageKey)
        {
            string localeText = GetLocaleText(strLanuageKey);
            return localeText;
        }

        #endregion

    }
}