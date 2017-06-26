using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ViewListOfLevelsController : MonoBehaviour
{
    private List<ViewCellInLevelList> _listOfLevels = new List<ViewCellInLevelList>();

    private ViewCellInLevelList _activeCell;

    [SerializeField]
    private ViewCellInLevelList _cellBase;

    [SerializeField]
    private ViewWindowCreateOrEditLevel _windowCreateOrEditLevel;

    public ViewCellInLevelList ActiveCell
    {
        set
        {
            _activeCell = value;
        }
        get
        {
            return _activeCell;
        }
    }

    private void Awake()
    {
        _cellBase.gameObject.SetActive(false);
    }

    private void Start()
    {
        SetListOfLevels();
    }

    private void SetListOfLevels()
    {
        foreach (ViewCellInLevelList cell in _listOfLevels)
        {
            Destroy(cell.gameObject);
        }

        _listOfLevels.Clear();

        string _levelPath = Application.streamingAssetsPath + "/Levels/";
       
        #if UNITY_IOS && !UNITY_EDITOR
        _levelPath = Application.persistentDataPath + "/Levels/";
        #endif

        DirectoryInfo dir = new DirectoryInfo(_levelPath);
		
        FileInfo[] filesInfo = dir.GetFiles("*.xml");

        foreach (FileInfo fi in filesInfo)
        {
            if (fi.Name != ".xml")
            {
                GameObject temp;
                temp = Instantiate(_cellBase.gameObject) as GameObject;
                temp.gameObject.transform.SetParent(_cellBase.gameObject.transform.parent.transform, false);
                temp.GetComponent<ViewCellInLevelList>().SetName(fi.Name.Remove(fi.Name.Length - 4));
                temp.SetActive(true);

                _listOfLevels.Add(temp.GetComponent<ViewCellInLevelList>());

                SortLevel();
            }
        }
    }

    public void AddNewLevel(string nameLevel)
    {
        GameObject temp;
        temp = Instantiate(_cellBase.gameObject) as GameObject;
        temp.gameObject.transform.SetParent(_cellBase.gameObject.transform.parent.transform, false);
        temp.GetComponent<ViewCellInLevelList>().SetName(nameLevel);
        temp.SetActive(true);

        _listOfLevels.Add(temp.GetComponent<ViewCellInLevelList>());

        SortLevel(); 
    }

    private void SortLevel()
    {
        List<int> allNumbers = new List<int>();

        foreach (ViewCellInLevelList v in _listOfLevels)
        {
            if (v.NumberLevel != 0)
                allNumbers.Add(v.NumberLevel);
        }

        allNumbers.Sort();

        foreach (ViewCellInLevelList v in _listOfLevels)
        {
            int index = allNumbers.IndexOf(v.NumberLevel);

            v.transform.SetSiblingIndex(index);
        }
    }

    public void SetActiveLevel(ViewCellInLevelList newActiveCell)
    {
        _activeCell = newActiveCell;    	

        _windowCreateOrEditLevel.SetCurrentLevel(newActiveCell);
    }

    public  List<ViewCellInLevelList> GetListLevel
    {
        get { return _listOfLevels; }
    }
}
