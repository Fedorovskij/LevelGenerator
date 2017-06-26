using UnityEngine;

public class GameManager : MonoBehaviour 
{
	void Start ()
	{
        LoadWindowManager.LoadAllWindows(transform);

        UnityPoolManager.Instance.Pop(LoadWindowManager.eWindowNameInLocationLevelEditor.LevelSelectWindow);
	}
}
