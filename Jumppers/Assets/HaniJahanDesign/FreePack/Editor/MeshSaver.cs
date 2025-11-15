using UnityEngine;
using UnityEditor;

public class MeshSaver
{
    [MenuItem("Assets/Save Mesh From Selected")]
    public static void SaveMesh()
    {
        GameObject obj = Selection.activeObject as GameObject;

        if (obj == null)
        {
            Debug.LogError("Select a GameObject.");
            return;
        }

        MeshFilter mf = obj.GetComponent<MeshFilter>();
        if (mf == null || mf.sharedMesh == null)
        {
            Debug.LogError("No mesh found.");
            return;
        }

        string path = EditorUtility.SaveFilePanelInProject(
            "Save Mesh",
            obj.name + "_mesh",
            "asset",
            "Save mesh");

        if (path.Length > 0)
        {
            Mesh meshCopy = Object.Instantiate(mf.sharedMesh);
            AssetDatabase.CreateAsset(meshCopy, path);
            AssetDatabase.SaveAssets();
            Debug.Log("Mesh saved to: " + path);
        }
    }
}
