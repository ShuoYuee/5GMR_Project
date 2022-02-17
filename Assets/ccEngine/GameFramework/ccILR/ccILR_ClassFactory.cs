using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using System.IO;
using ILRuntime.CLR.TypeSystem;
using ccU3DEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Reflection;

namespace ccILR
{

    public class ccILR_ClassFactory : ccFactoryBaseClass
    {
        public static System.Action<string> TestActionDelegate;

        Dictionary<string, Type> _dirASClass = new Dictionary<string, Type>();
        ILRuntime.Runtime.Enviorment.AppDomain IlAppDomain = null;

        //private ccILR_CreateClassTool _ccILR_CreateClassTool;

        //class stRegClassDT
        //{
        //    public string m_strClassName = "";
        //    public ccILR_BaseClass m_ccILR_BaseClassModel = null;
        //}

        //private Dictionary<string, stRegClassDT> _aClass = new Dictionary<string, stRegClassDT>();

        public static ccILR_ClassFactory _Instance = null;
        public static ccILR_ClassFactory GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new ccILR_ClassFactory();
                _Instance.InitClass();
            }
            return _Instance;
        }

        public ILRuntime.Runtime.Enviorment.AppDomain f_GetAppDomain()
        {
            return IlAppDomain;
        }

        public byte[] f_LoadCatchFile(string strCatchFile)
        {
            if (!string.IsNullOrEmpty(strCatchFile))
            {
                string strCatthFilePath = string.Format("{0}/{1}/{2}", UpdateManager.Get().f_GetABPath(), ccU3DEngineParam.m_CurBuildTarget, strCatchFile);
                if (System.IO.File.Exists(strCatthFilePath))
                {
                    System.IO.FileStream fileStream = System.IO.File.OpenRead(strCatthFilePath);
                    byte[] aBuf = new byte[fileStream.Length];
                    fileStream.Read(aBuf, 0, (int)fileStream.Length);
                    return aBuf;
                }
            }
            return null;
        }


        public void f_LoadHotFixDLL(bool bOpenDebug = false)
        {
            IlAppDomain = new ILRuntime.Runtime.Enviorment.AppDomain();

            //glo_Main.GetInstance().f_StartCoroutine(LoadHotFixAssembly());

            byte[] aDll = null;
            byte[] aPdb = null;
            try
            {
                aDll = f_LoadCatchFile("UpdateInforIndd.txtt");
                aPdb = f_LoadCatchFile("UpdateInforInddp.txtt");

                fs = new MemoryStream(aDll);
                if (aPdb != null)
                {
                    p = new MemoryStream(aPdb);
                }
                else
                {
                    p = null;
                }

                //IlAppDomain.LoadAssembly(fs, p, new Mono.Cecil.Pdb.PdbReaderProvider());
                IlAppDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            }
            catch (Exception e)
            {
                Debug.LogError("載入熱更DLL失敗，" + e.ToString());
            }

            //using (System.IO.MemoryStream fs = new MemoryStream(aDll))
            //{
            //    using (System.IO.MemoryStream p = new MemoryStream(aPdb))
            //    {
            //        //IlAppDomain.LoadAssembly(fs, p, new Mono.Cecil.Pdb.PdbReaderProvider());
            //        IlAppDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            //    }
            //}
            if (bOpenDebug)
            {
                IlAppDomain.DebugService.StartDebugService(56000);
            }
            InitializeILRuntime();
            InitAdapter();
            //OnHotFixLoaded();
        }

        System.IO.MemoryStream fs;
        System.IO.MemoryStream p;
        IEnumerator LoadHotFixAssembly()
        {
            //首先產生實體ILRuntime的AppDomain，AppDomain是一個應用程式定義域，每個AppDomain都是一個獨立的沙箱
            //appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
            //正常項目中應該是自行從其他地方下載dll，或者打包在AssetBundle中讀取，平時開發以及為了演示方便直接從StreammingAssets中讀取，
            //正式發佈的時候需要大家自行從其他地方讀取dll

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //這個DLL檔是直接編譯HotFix_Project.sln生成的，已經在專案中設置好輸出目錄為StreamingAssets，在VS裡直接編譯即可生成到對應目錄，無需手動拷貝
#if UNITY_ANDROID
        WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");
#else
            WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFixDll.dll");
#endif
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                UnityEngine.Debug.LogError(www.error);
            byte[] dll = www.bytes;
            www.Dispose();

            //PDB檔是調試資料庫，如需要在日誌中顯示報錯的行號，則必須提供PDB檔，不過由於會額外耗用記憶體，正式發佈時請將PDB去掉，下面LoadAssembly的時候pdb傳null即可
#if UNITY_ANDROID
        www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
#else
            www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFixDll.pdb");
#endif
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                UnityEngine.Debug.LogError(www.error);
            byte[] pdb = www.bytes;
            fs = new MemoryStream(dll);
            p = new MemoryStream(pdb);
            try
            {
                IlAppDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
                //IlAppDomain.LoadAssembly(fs, p, new Mono.Cecil.Pdb.PdbReaderProvider());
            }
            catch
            {
                Debug.LogError("載入熱更DLL失敗，請確保已經通過VS打開Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln編譯過熱更DLL");
            }

            InitializeILRuntime();
            InitAdapter();
        }


        void InitializeILRuntime()
        {
            //這裡做一些ILRuntime的註冊
            IlAppDomain.DelegateManager.RegisterMethodDelegate<GameObject>();
            //IlAppDomain.DelegateManager.RegisterDelegateConvertor<UIEventListener.VoidDelegate>((action) =>
            //{
            //    return new UIEventListener.VoidDelegate((a) =>
            //    {
            //        ((System.Action<GameObject>)action)(a);
            //    });
            //});
            IlAppDomain.DelegateManager.RegisterMethodDelegate<string, string, LogType>();
            IlAppDomain.DelegateManager.RegisterDelegateConvertor<Application.LogCallback>((action) =>
            {
                return new Application.LogCallback((arg1, arg2, arg3) =>
                {
                    ((System.Action<string, string, LogType>)action)(arg1, arg2, arg3);
                });
            });
            IlAppDomain.DelegateManager.RegisterMethodDelegate<GameObject, object, object>();
            IlAppDomain.DelegateManager.RegisterDelegateConvertor<ccUIEventListener.VoidDelegateV2>((action) =>
            {
                return new ccUIEventListener.VoidDelegateV2((arg1, arg2, arg3) =>
                {
                    ((System.Action<GameObject, object, object>)action)(arg1, arg2, arg3);
                });
            });
            IlAppDomain.DelegateManager.RegisterMethodDelegate<object>();
            IlAppDomain.DelegateManager.RegisterDelegateConvertor<ccCallback>((action) =>
            {
                return new ccCallback((arg1) =>
                {
                    ((System.Action<object>)action)(arg1);
                });
            });

            IlAppDomain.DelegateManager.RegisterMethodDelegate<string>();
            IlAppDomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<string>>((action) =>
            {
                return new UnityEngine.Events.UnityAction<string>((a) =>
                {
                    ((Action<string>)action)(a);
                });
            });

            IlAppDomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
            {
                return new UnityEngine.Events.UnityAction(() =>
                {
                    ((Action)act)();
                });
            });

            IlAppDomain.DelegateManager.RegisterMethodDelegate<string>();
            IlAppDomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<BaseEventData>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<BaseEventData>((a) =>
                {
                    ((System.Action<BaseEventData>)act)(a);
                });
            });
        }

        void InitAdapter()
        {
            //IlAppDomain.RegisterCrossBindingAdaptor(new InheritanceAdapter());
            IlAppDomain.RegisterCrossBindingAdaptor(new ccILR_BaseClass_Adapter());
            IlAppDomain.RegisterCrossBindingAdaptor(new ccUILogicBase_Adapter());
            //IlAppDomain.RegisterCrossBindingAdaptor(new ccHotfixMono_Adapter());
        }

        void InitClass()
        {
            _dirASClass.Clear();
            System.Reflection.Assembly[] AS = System.AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < AS.Length; i++)
            {
                Type[] types = AS[i].GetTypes();
                if (AS[i].FullName.IndexOf("Assembly-CSharp,") != -1)
                {
                    for (int j = 0; j < types.Length; j++)
                    {
                        string className = types[j].FullName;
                        if (!_dirASClass.ContainsKey(className))
                        {
                            _dirASClass.Add(className, types[j]);
                        }
                        else
                        {
                            MessageBox.ASSERT("已存在同名的類" + className);
                        }
                    }
                }
            }
        }

        public override T f_CreateClass<T>(string strFullClassName, object[] args = null)
        {
            if (IlAppDomain == null)
            {
                Debug.LogError("AppDomain未初始化，f_CreateClass失敗." + strFullClassName);
            }
            ILTypeInstance tILTypeInstance = IlAppDomain.Instantiate(strFullClassName);
            //T tCreateClass = IlAppDomain.Instantiate<T>(strFullClassName);
            if (tILTypeInstance != null)
            {
                T tCreateClass = (T)tILTypeInstance.CLRInstance;
                //tCreateClass.f_Save(tILTypeInstance);
                return tCreateClass;
                ////預先獲得IMethod，可以減低每次調用查找方法耗用的時間
                //IType type = IlAppDomain.LoadedTypes[strFullClassName];

                //Debug.Log("產生實體熱更裡的類");
                //object obj = IlAppDomain.Instantiate(strFullClassName, null);
                ////第二種方式
                //object obj2 = ((ILType)type).Instantiate();

                //return (T) obj;

                //IlAppDomain.Invoke("HotFixDll.HelloWorld", "StaticPrint", null, null);
                //ILTypeInstance tCreateClass = IlAppDomain.Instantiate<ILTypeInstance>(strFullClassName);
                //T TTT = tCreateClass;
                //IMethod helloWorldPrint = helloWorld.Type.GetMethod("Print");
                //IlAppDomain.Invoke(helloWorldPrint, helloWorld);
            }
            else
            {
                Type tClassType = null;
                if (_dirASClass.TryGetValue(strFullClassName, out tClassType))
                {
                    return (T)Activator.CreateInstance(tClassType);
                }
            }
            return default(T);
        }


        public void f_Destory()
        {
            IlAppDomain.DebugService.StopDebugService();
        }

        //public void f_RegCreateClassTool(ccILR_CreateClassTool tccILR_CreateClassTool)
        //{
        //    _ccILR_CreateClassTool = tccILR_CreateClassTool;
        //}

        //private stRegClassDT GetClassDT(string strRegName)
        //{
        //    stRegClassDT tstRegClassDT = null;
        //    _aClass.TryGetValue(strRegName, out tstRegClassDT);
        //    return tstRegClassDT;
        //}

        //public void f_RegClass(string strClassName)
        //{
        //    stRegClassDT tstRegClassDT = GetClassDT(strClassName);
        //    if (tstRegClassDT == null)
        //    {
        //        tstRegClassDT = new stRegClassDT();
        //        tstRegClassDT.m_strClassName = strClassName;
        //        _aClass.Add(tstRegClassDT.m_strClassName, tstRegClassDT);
        //    }
        //}

        //public ccILR_BaseClass f_CreateClass(string strClassName)
        //{
        //    ccILR_BaseClass tNewClass = null;
        //    stRegClassDT tstRegClassDT = GetClassDT(strClassName);
        //    if (tstRegClassDT != null)
        //    {//外部DLL註冊
        //        tNewClass = CreateClasss(tstRegClassDT);
        //    }
        //    else
        //    {//直接走內部創建介面
        //        if (_ccILR_CreateClassTool == null)
        //        {
        //            Debug.LogError("ccILR_CreateClassTool 內部創建介面未初始，" + strClassName);
        //        }
        //        else
        //        {
        //            tNewClass = _ccILR_CreateClassTool.f_CreateClass(strClassName);
        //        }
        //    }
        //    return tNewClass;
        //}

        //private ccILR_BaseClass CreateClasss(stRegClassDT tstRegClassDT)
        //{
        //    ccILR_BaseClass tNewClass = null;
        //    if (tstRegClassDT.m_ccILR_BaseClassModel == null)
        //    {
        //        tstRegClassDT.m_ccILR_BaseClassModel = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(tstRegClassDT.m_strClassName, false) as ccILR_BaseClass;
        //    }
        //    if (tstRegClassDT.m_ccILR_BaseClassModel != null)
        //    {
        //        tNewClass = tstRegClassDT.m_ccILR_BaseClassModel.f_Clone();
        //    }
        //    return tNewClass;
        //}



    }

}
