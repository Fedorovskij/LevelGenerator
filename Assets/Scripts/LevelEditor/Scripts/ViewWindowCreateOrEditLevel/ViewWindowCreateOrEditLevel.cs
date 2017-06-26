using UnityEngine;
using UnityEngine.UI;

public class ViewWindowCreateOrEditLevel : WindowViewBase
{
    [SerializeField]	
    private Button _btnEditLevel;

    [SerializeField]
    private Button _btnCreateLevel;

    [SerializeField]
    ViewWindowCreateLevel viewWindowCreateLevel;

    [SerializeField]
    ShowFieldInEditor _showFieldInEditor;
	 
    private LevelProfile _currentLevel;

    public override void OnEnable()
    {
        OnShowWindowEvent();
    }

    public  override void Start()
    {
        LevelSelectInEditor = -1;

        _btnEditLevel.onClick.AddListener(EditLevel);
             
        _btnCreateLevel.onClick.AddListener(OnButtonCreateWindow);
    }

    public override void OnPush()
    {
        OnDeactivateWindowEvent();
    }

    private void OnButtonCreateWindow()
    {
        viewWindowCreateLevel.gameObject.SetActive(true);
    }

    public void SetCurrentLevel(ViewCellInLevelList cell)
    {         
        Debug.Log("SetCurrentLevel cell.Name " + cell.Name);

        _currentLevel = XmlSerializator.OpenLevel(cell.Name);

        cell.ProfileContainedInCell = _currentLevel;

        _showFieldInEditor.ShowField(_currentLevel);

        _btnEditLevel.interactable = true;

        LevelSelectInEditor = int.Parse(_currentLevel.nameOfLevel);    
    }

    public override void Close()
    {
        Debug.Log("Close  ");
        UnityPoolManager.Instance.Push(LoadWindowManager.eWindowNameInLocationLevelEditor.LevelEditorWindow.ToString(), this);
    }

    public void EditLevel()
    {
        Debug.Log("_currentLevel " + _currentLevel.nameOfLevel);

        if (_currentLevel != null)
        {
            Level _newLevel = new Level();

            _newLevel.profile = _currentLevel;

            FieldController fc = UnityPoolManager.Instance.GetUnityPoolObject(LoadWindowManager.eWindowNameInLocationLevelEditor.LevelEditorWindow) as FieldController;

            fc.InstantiateField(_newLevel);           

            UnityPoolManager.Instance.Pop(LoadWindowManager.eWindowNameInLocationLevelEditor.LevelEditorWindow);

        }
        else
        {   
            Debug.LogError("newLevelProfile = nuul");
        }
    }

    public static int LevelSelectInEditor = -1;


}
