using UnityEngine;
using UnityEngine.UI;

public class ViewWindowCreateLevel : MonoBehaviour
{
    [SerializeField]
    private Button _btnClose;

    [SerializeField]
    private Button _btnCreate;

    [SerializeField]
    private InputField _inputNameOfNewLevel;

    [SerializeField]
    private ViewListOfLevelsController _viewLevelListScript;

    [SerializeField]
    private GameObject _windowNoCreate;

    [SerializeField]
    private Button _btnCloseWindowNoCreate;

    void OnEnable()
    { 
        _inputNameOfNewLevel.text = "";
    }

    void Start()
    {
        _btnClose.onClick.AddListener(OnButtonCloseWindow);

        _btnCreate.onClick.AddListener(OnButtonCreateLevel);

        _inputNameOfNewLevel.onEndEdit.AddListener(CheckInputText);

        _btnCloseWindowNoCreate.onClick.AddListener(CloseWindowNoCreate);

    }

    private void OnButtonCreateLevel()
    {
        var level = _viewLevelListScript.GetListLevel.Find(x => x.name == _inputNameOfNewLevel.text);

        if (level == null)
        {
            gameObject.SetActive(false);
 
            FieldController fc = UnityPoolManager.Instance.GetUnityPoolObject(LoadWindowManager.eWindowNameInLocationLevelEditor.LevelEditorWindow) as FieldController;

            Level _newLevel = new Level();

            _newLevel.profile = new LevelProfile();

            _newLevel.profile.nameOfLevel = _inputNameOfNewLevel.text;

            fc.InstantiateField(_newLevel);

            XmlSerializator.SaveLevel(_newLevel.profile);

            UnityPoolManager.Instance.Pop(LoadWindowManager.eWindowNameInLocationLevelEditor.LevelEditorWindow);
 
            gameObject.SetActive(false);

            _viewLevelListScript.AddNewLevel(_inputNameOfNewLevel.text);
        }
        else
        {
            _windowNoCreate.SetActive(true);

            _windowNoCreate.transform.Find("BGContent/LabelInfoNoCreate").GetComponent<Text>().text =
                "this level exist !!!";
        }
    }

    public void CheckInputText(string newText)
    {
        if (newText != "")
        {
            _btnCreate.interactable = true;
        }
        else
        {
            _btnCreate.interactable = false;
        }
    }

    private void OnButtonCloseWindow()
    {
        gameObject.SetActive(false);
    }

    private void CloseWindowNoCreate()
    {
        _windowNoCreate.SetActive(false);
    }
}
