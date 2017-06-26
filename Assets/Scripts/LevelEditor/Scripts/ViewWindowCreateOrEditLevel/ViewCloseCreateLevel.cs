using UnityEngine;
using UnityEngine.UI;

public class ViewCloseCreateLevel : MonoBehaviour {

	[SerializeField]
	private Button _buttonCloseCreateNewLevel;
	[SerializeField]
	private GameObject _windowCreateNewLevel;

	void Start () 
	{
		_buttonCloseCreateNewLevel.onClick.AddListener(CloseWindowCreate);
	}
	

	void CloseWindowCreate () 
	{
		_windowCreateNewLevel.SetActive(false);
	}
}
