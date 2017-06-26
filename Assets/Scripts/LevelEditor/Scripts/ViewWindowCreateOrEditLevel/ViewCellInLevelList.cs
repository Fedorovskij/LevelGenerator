using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewCellInLevelList : MonoBehaviour
{
    [SerializeField]
    private ViewListOfLevelsController _viewLevelListScript;

    [SerializeField]
    private Text _textNameofLevel;
	
    public bool activateButton;

    private LevelProfile _profileContainedInCell = null;

    [SerializeField]
    public Button btnSelectLevel;

    private Color _btnColor;

    private int _numberLevel;

    public int NumberLevel
    {
        get { return _numberLevel; }
    }

    public LevelProfile ProfileContainedInCell
    {
        set
        {
            _profileContainedInCell = value;
        }
        get
        {
            return _profileContainedInCell;
        }
    }

    public string Name
    {
        set
        {
            name = value;
        }
        get
        {
            return name;
        }
    }

    void Start()
    {
        btnSelectLevel.onClick.AddListener(OnSelectLevel);

        _btnColor = gameObject.GetComponent<Image>().color;
    }

    public void OnSelectLevel()
    {
        if (!activateButton)
        {
            activateButton = !activateButton;           

            var _view = _viewLevelListScript.GetListLevel.Find(x => 

                x.activateButton == true && x != this
                        );

            if (_view != null)
            {
                _view.GetComponent<Image>().color = _btnColor;  

                _view.activateButton = false; 
            }

            _viewLevelListScript.SetActiveLevel(this);

            gameObject.GetComponent<Image>().color = Color.green;
        }
        
    }

    public void SetName(string newName)
    {
        Name = newName;
        _textNameofLevel.text = Name;
        gameObject.name = newName;

        _numberLevel = Int32.Parse(newName);
    }
}
