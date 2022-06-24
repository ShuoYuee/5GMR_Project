using System.Linq;
using UnityEditor;
using UnityEngine;

// 自動賦予AssetBundle名稱，懶得手動打XD
public class FixABName
{
    [MenuItem("AvgEditor/Extended/修正已選取的AB名")]
    private static void FixSelectedABName()
    {
        // Get selected assets that has paths.
        var assets = Selection.objects.Where(o => !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(o))).ToArray();

        foreach (var asset in assets)
        {
            string abName = "";
            if (asset.name.StartsWith("UIP_"))
            {
                abName = "a" + asset.name.ToLower() + ".bundle";
            }
            else
            {
                continue; // Skip this asset.
            }

            string assetPath = AssetDatabase.GetAssetPath(asset);
            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(abName, "");
            Debug.Log($"'{asset.name}' 的 AB 名已修改為 '{abName}'");
        }
    }
}