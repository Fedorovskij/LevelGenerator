using UnityEngine;
using System;

public static class LoadWindowManager
{

    public static void LoadAllWindows(Transform parent)
    {  

        foreach (var t in Enum.GetValues(typeof(eWindowNameInLocationLevelEditor)))
        {
            UnityPoolManager.Instance.PushStartScene(t.ToString(), LoadObj("Windows/" +
                    t.ToString(), parent).GetComponent<UnityPoolObject>());
        }

    }

    private static GameObject LoadObj(string path, Transform parent)
    {
        GameObject temp = Resources.Load(path) as GameObject;
 
        if (temp == null)
            Debug.LogError("load obj " + path);

        temp.SetActive(false);

        temp = GameObject.Instantiate(temp) as GameObject;

        temp.transform.SetParent(parent, false);

        temp.transform.SetSiblingIndex(0);
 
        return temp;
    }

    public enum eWindowNameInLocationLevelEditor
    {
        LevelEditorWindow,
        LevelSelectWindow
    }
}
