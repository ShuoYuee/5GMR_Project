using ccU3DEngineEditor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class QuickBuildTools
{
    public const string BuildPath = "Builds/";

    public static string GameName
    {
        get
        {
            return Application.productName;
        }
    }

    public static string[] GetScenes()
    {
        string[] scenes = new string[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenes[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }
        return scenes;
    }

    public static BuildOptions CommonBuildOptions()
    {
        if (Application.isBatchMode)
        {
            return BuildOptions.None;
        }
        return BuildOptions.ShowBuiltPlayer;
    }

    [MenuItem("Builds/Windows (x64)")]
    public static void BuildWindowsX64()
    {
        string path = BuildPath + "x64";
        BuildTarget target = BuildTarget.StandaloneWindows64;
        BuildOptions options = CommonBuildOptions();

        AssetbundlesMenuItems.BuildAssetBundlesLZ4();
        AssetbundlesMenuItems.CopyAssetBundles();
        BuildPipeline.BuildPlayer(GetScenes(), path + "/" + GameName + ".exe", target, options);
    }

    [MenuItem("Builds/Android")]
    public static void BuildAndroid()
    {
        string path = BuildPath + "Android";
        BuildTarget target = BuildTarget.Android;
        BuildOptions options = CommonBuildOptions();

        AssetbundlesMenuItems.BuildAssetBundlesLZ4();
        AssetbundlesMenuItems.CopyAssetBundles();
        BuildPipeline.BuildPlayer(GetScenes(), path + "/" + GameName + ".apk", target, options);
    }
}

public class GameBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {

    }
}