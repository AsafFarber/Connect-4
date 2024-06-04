using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ExportPackage
{
    // This tool is used To export package with physics layers.
    [MenuItem("Export Package/Export with layers")]
    static void Export()
    {
        AssetDatabase.ExportPackage(AssetDatabase.GetAllAssetPaths(), PlayerSettings.productName + ".unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies | ExportPackageOptions.IncludeLibraryAssets);
    }
}