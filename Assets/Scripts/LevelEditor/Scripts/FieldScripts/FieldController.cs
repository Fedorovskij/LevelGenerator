using UnityEngine;
using UnityEngine.UI;

delegate void OnEditFieldDelegate(int x,int y);

public class FieldController : WindowViewBase
{
    private event OnEditFieldDelegate OnEditField;

    [SerializeField]
    private ViewFieldCell _cellBase;

    [SerializeField]
    private ViewWallBase _wallH;

    [SerializeField]
    private ViewWallBase _wallV;

    [SerializeField]
    private MenuCreator _menuCreator;

    [SerializeField]
    private Button _buttonSave;

    [SerializeField]
    Button _buttonExit;

    private  float _widthOfCell;
 
    private int _widthOfField;

    private int _heightOfField;

    private ViewFieldCell[,] _field;

    private ViewFieldCell _objectForTeleport;

    private eColors colorToCell;

    private Level _level;

    private ViewWallBase[,] _wallVertical;

    private ViewWallBase[,] _wallHorizontal;

    public const float paddingBetweenCells = 4;

    public override void OnEnable()
    {
        OnShowWindowEvent();

        gameObject.transform.SetSiblingIndex(gameObject.transform.parent.transform.childCount);
    }

    private void OnDisable()
    {
        ExitLevel();
    }

    public override void OnPush()
    {
        gameObject.SetActive(false);

        OnDeactivateWindowEvent();
    }

    public override void Start()
    {
        base.Start();

        _cellBase.gameObject.SetActive(false);

        if (_buttonExit != null)
        {
            _buttonExit.onClick.AddListener(Close);
        }
        
        if (_buttonSave != null)
        {
            _buttonSave.onClick.AddListener(() =>
                {
                    XmlSerializator.SaveLevel(_level.profile);
                });
        }
    }

    public  override void Close()
    {
        UnityPoolManager.Instance.Push(LoadWindowManager.eWindowNameInLocationLevelEditor.LevelEditorWindow.ToString(), this);
    }

    private void Construct()
    {
        _widthOfField = _level.profile.width;

        _heightOfField = _level.profile.height;

        for (int i = 0; i < Constants.MaxWidthOfField; i++)
        {
            for (int k = 0; k < Constants.MaxHeightOfField; k++)
            {	
                _field[i, k].Construct(i, k, Constants.GetEnumById(_level.profile.GetChip(i, k)),
                    
                    _level.profile.GetGravity(i, k), _level.profile.GetGenerator(i, k), _level.profile.GetBlock(i, k),
                  
                    _level.profile.GetJelly(i, k), _level.profile.GetTeleport(i, k),

                    _level.profile.GetSugarDrop(i, k), _level.profile.GetSlot(i, k));

                if (i > _level.profile.width - 1 || k > _level.profile.height - 1)
                {
                    _field[i, k].IsActive = false;
                }

            }
        }

        for (int i = 0; i < Constants.MaxWidthOfField - 1; i++)
        {
            for (int k = 0; k < Constants.MaxHeightOfField; k++)
            {	
                _wallVertical[i, k].Construct(i, k, _level.profile.GetWallV(i, k), true);
            }
        }

        for (int i = 0; i < Constants.MaxWidthOfField; i++)
        {
            for (int k = 0; k < Constants.MaxHeightOfField - 1; k++)
            {	
                _wallHorizontal[i, k].Construct(i, k, _level.profile.GetWallH(i, k), false);
            }
        }

        ShowWalls(false);
    }

    public void ExitLevel()
    {
        _level = null;

        if (_field == null)
            return;
        
        for (int i = 0; i < Constants.MaxWidthOfField; i++)
        {
            for (int k = 0; k < Constants.MaxHeightOfField; k++)
            {	
                if (_field[i, k] != null)
                    Destroy(_field[i, k].gameObject);
            }
        }

        for (int i = 0; i < Constants.MaxWidthOfField - 1; i++)
        {
            for (int k = 0; k < Constants.MaxHeightOfField; k++)
            {	
                if (_wallVertical[i, k] != null)
                    Destroy(_wallVertical[i, k].gameObject);
            }
        }

        for (int i = 0; i < Constants.MaxWidthOfField; i++)
        {
            for (int k = 0; k < Constants.MaxHeightOfField - 1; k++)
            {	
                if (_wallHorizontal[i, k] != null)
                    Destroy(_wallHorizontal[i, k].gameObject);
            }
        }
    }

    public void ActivateCellForEdit(int posCell_x, int posCell_y)
    {
        if (OnEditField != null)
            OnEditField(posCell_x, posCell_y);
    }

  
    public void InstantiateField(Level level)
    {   
        _level = level;    
        
        _widthOfCell = _cellBase.gameObject.GetComponent<BoxCollider2D>().size.x;

        _field = new ViewFieldCell[Constants.MaxWidthOfField, Constants.MaxHeightOfField];

        _wallVertical = new ViewWallBase[Constants.MaxWidthOfField - 1, Constants.MaxHeightOfField];

        _wallHorizontal = new ViewWallBase[Constants.MaxWidthOfField, Constants.MaxHeightOfField - 1];
 
        int countCell = 1;

        for (int i = 0; i < Constants.MaxWidthOfField; i++)
        {
            countCell = i + 1;

            for (int k = 0; k < Constants.MaxHeightOfField; k++)
            {			
                GameObject cell = Instantiate(_cellBase.gameObject, _cellBase.gameObject.transform.parent, false) as GameObject;        

                cell.transform.localPosition = _cellBase.transform.localPosition;

                cell.SetActive(true);

                cell.GetComponent<RectTransform>().anchoredPosition = 

                    (new Vector2(i * (_widthOfCell + paddingBetweenCells) + _cellBase.GetComponent<RectTransform>().anchoredPosition.x, 
                    
                    -k * (_widthOfCell + paddingBetweenCells) + _cellBase.GetComponent<RectTransform>().anchoredPosition.y));
               

                ViewFieldCell viewFieldCell = cell.GetComponent<ViewFieldCell>();

                viewFieldCell.PositionX = k;

                viewFieldCell.PositionY = i;

                viewFieldCell.FieldController = this;

                viewFieldCell._textNumberCell.text = (countCell).ToString();

                countCell += 12;

                _field[i, k] = viewFieldCell;

            }
        }

        InstantiateWalls();

        if (_menuCreator != null)
            _menuCreator.SetInfo(_level);

        Construct();
    }


    public void InstantiateWalls()
    {
        for (int i = 0; i < Constants.MaxWidthOfField - 1; i++)
        {
            for (int k = 0; k < Constants.MaxHeightOfField; k++)
            {	
                GameObject wallV = Instantiate(_wallV.gameObject, _wallV.gameObject.transform.parent, false) as GameObject; 

                wallV.SetActive(true);

                wallV.GetComponent<RectTransform>().anchoredPosition = 

                    (new Vector2(k * (_widthOfCell + paddingBetweenCells) + _cellBase.GetComponent<RectTransform>().anchoredPosition.x
                    + _widthOfCell / 2f + paddingBetweenCells / 2f, 

                    -i * (_widthOfCell + paddingBetweenCells) + _cellBase.GetComponent<RectTransform>().anchoredPosition.y + _widthOfCell / 2f));
                
                ViewWallBase wallScript = wallV.GetComponent<ViewWallBase>();

                _wallVertical[i, k] = wallScript;
            }
        }

        for (int i = 0; i < Constants.MaxWidthOfField; i++)
        {
            for (int k = 0; k < Constants.MaxHeightOfField - 1; k++)
            {	
                GameObject wallH = Instantiate(_wallH.gameObject, _wallH.gameObject.transform.parent, false) as GameObject;
     
                wallH.SetActive(true);

                wallH.GetComponent<RectTransform>().anchoredPosition = 

                    (new Vector2(i * (_widthOfCell + paddingBetweenCells) + _cellBase.GetComponent<RectTransform>().anchoredPosition.x
                    + _widthOfCell / 2f - paddingBetweenCells / 4f, 

                    -k * (_widthOfCell + paddingBetweenCells) + _cellBase.GetComponent<RectTransform>().anchoredPosition.y - _widthOfCell / 2f -
                    paddingBetweenCells / 2f));
                
                ViewWallBase wallScript = wallH.GetComponent<ViewWallBase>();

                _wallHorizontal[i, k] = wallScript;
            }
        }

    }

    public Level GetLevelScript
    {
        get
        { 
            return _level;
        }

    }

    public ViewFieldCell GetCellByPosition(int x, int y)
    {
        return _field[x, y];
    }

    public void RemoveAllHandlers()
    {
        if (_objectForTeleport != null)
        {
            _objectForTeleport.SetTeleportColor(false);

            _objectForTeleport = null;
        }

        OnEditField = null;

        ShowWalls(false);
    }

    public  void EditWidth(int newWidth)
    {
        if (newWidth > Constants.MaxWidthOfField)
        {
            EditWidth(Constants.MaxWidthOfField);

            return;
        }
        if (newWidth < Constants.MinWidthOfField)
        {
            EditWidth(Constants.MinWidthOfField);

            return;
        }

        int differenceWidth = Mathf.Abs(newWidth - _widthOfField);

        if (newWidth > _widthOfField)
        {
            for (int i = 0; i < differenceWidth; i++)
            {
                for (int k = 0; k < _heightOfField; k++)
                {
                    _level.profile.SetSlot(_widthOfField + i, k, true);

                    if (_field[_widthOfField + i, k].IsActive == false)
                    {
                        _field[_widthOfField + i, k].IsActive = true;
                    }
                }
            }
        }

        if (newWidth < _widthOfField)
        {
            for (int i = _widthOfField - 1; i > _widthOfField - differenceWidth - 1; i--)
            {
                for (int k = 0; k < _heightOfField; k++)
                {
                    _field[i, k].IsActive = false;
                }
            }
        }

        _widthOfField = newWidth;

        _level.profile.width = _widthOfField;
    }

    public void EditHeight(int newHeight)
    {
		
        if (newHeight > Constants.MaxHeightOfField)
        {
            EditHeight(Constants.MaxHeightOfField);

            return;
        }

        if (newHeight < Constants.MinHeightOfField)
        {
            EditHeight(Constants.MinHeightOfField);

            return;
        }

        int differenceHeight = Mathf.Abs(newHeight - _heightOfField);


        if (newHeight > _heightOfField)
        {
            Debug.Log("newHeight > _heightOfField");

            for (int i = 0; i < _widthOfField; i++)
            {
                for (int k = 0; k < differenceHeight; k++)
                {
                    if (_field[i, _heightOfField + k].IsActive == false)
                    {
                        _field[i, _heightOfField + k].IsActive = true;

                        Debug.Log("set active True");
                    }
                }
            }
        }

        if (newHeight < _heightOfField)
        {
            for (int i = 0; i < _widthOfField; i++)
            {
                for (int k = _heightOfField - 1; k > _heightOfField - differenceHeight - 1; k--)
                {
                    _field[i, k].IsActive = false;
                }
            }

        }

        _heightOfField = newHeight;

        _level.profile.height = _heightOfField;
    }

    public void EditCountOfColors(int newCountColors)
    {
        if (newCountColors > Constants.MaxCountOfColors)
        {
            EditCountOfColors(Constants.MaxCountOfColors);

            return;
        }

        if (newCountColors < Constants.MinCountOfColors)
        {
            EditCountOfColors(Constants.MinCountOfColors);

            return;
        }

        _level.profile.chipCount = newCountColors;
    }

    public void EditCountPortion(float newPotrion)
    {
        if (newPotrion > Constants.MaxCountPortion)
        {
            EditCountPortion(Constants.MaxCountPortion);

            return;
        }
        if (newPotrion < Constants.MinCountPortion)
        {
            EditCountPortion(Constants.MinCountPortion);

            return;
        }
        _level.profile.buttonPortion = newPotrion;
    }


    public void EditFirstScore(int newFirstScore)
    {
		
        _level.profile.firstStarScore = newFirstScore;

    }


    public void EditSecondScore(int newSecondScore)
    {
		
        _level.profile.secondStarScore = newSecondScore;

    }

    public void EditThirdScore(int newThirdScore)
    {
		
        _level.profile.thirdStarScore = newThirdScore;

    }

    public void EditToggleTime(bool isOnTime)
    {
        if (isOnTime)
        {
            _level.profile.limitation = Limitation.Time;
        }
    }

    public void EditToggleMoves(bool isOnMoves)
    {
        if (isOnMoves)
        {
            _level.profile.limitation = Limitation.Moves;
        }
    }

    public void EditMoveCount(int newMoveCount)
    {
        _level.profile.moveCount = newMoveCount;
    }

    public void EditTimeCount(int newTimeCount)
    {
        _level.profile.duration = newTimeCount;
    }

    public void EditTarget(int numberOfTarget)
    {
        if (numberOfTarget == 0)
        {
            _level.profile.target = FieldTarget.None;
        }
        if (numberOfTarget == 1)
        {
            _level.profile.target = FieldTarget.Jelly;
        }
        if (numberOfTarget == 2)
        {
            _level.profile.target = FieldTarget.Block;
        }
        if (numberOfTarget == 3)
        {
            _level.profile.target = FieldTarget.Color;
        }
        if (numberOfTarget == 4)
        {
            _level.profile.target = FieldTarget.SugarDrop;
        }
    }

    public void ActivateSlotButton()
    {
        RemoveAllHandlers();

        OnEditField += ButtonSlotFunction;
    }

    public void ActivateGeneratorButton()
    {
        RemoveAllHandlers();

        OnEditField += ButtonGeneratorFunction;
    }

    public void ActivateTeleportsButton()
    {
        RemoveAllHandlers();

        OnEditField += ButtonTeleportFunction;
    }

    public void ActivateGravityButton()
    {
        RemoveAllHandlers();

        OnEditField += ButtonGravityFunction;
    }

    public void ActivateColor(eColors color)
    {        
        RemoveAllHandlers();

        colorToCell = color;

        OnEditField += ButtonColorFunction;
    }

    public void ActivateButtonSimpleBlock()
    {
        RemoveAllHandlers();

        OnEditField += ActivateButtonSimpleBlockFunction;
    }

    public void ActivateButtonWeed()
    {
        RemoveAllHandlers();

        OnEditField += ActivateButtonWeedFunction;
    }

    public void ActivateButtonBranch()
    {
        RemoveAllHandlers();

        OnEditField += ActivateButtonBranchFunction;
    }

    public void ActivateButtonJelly()
    {
        RemoveAllHandlers();

        OnEditField += ActivateButtonJellyFunction;
    }

    public void ActivateSugarDropButton()
    {
        RemoveAllHandlers();

        OnEditField += ButtonSugarDropFunction;
    }

    public void ButtonSlotFunction(int x, int y)
    {
        GameObject fg = _field[x, y].gameObject;

        if (_field[x, y].IsActive)
        {
            _field[x, y].IsActive = false;

            _level.profile.SetSlot(x, y, false);

            ShowWalls(false);
        }
        else
        {
            _field[x, y].IsActive = true;

            _level.profile.SetSlot(x, y, true);

            ShowWalls(false);
        }
    }

    public void ButtonGeneratorFunction(int x, int y)
    {		
        ViewFieldCell cellScript = _field[x, y];

        if (cellScript.gameObject.activeSelf)
        {
            _level.profile.SetGenerator(x, y, cellScript.SetGeneretorInCell());
        }
    }

    public void ButtonTeleportFunction(int x, int y)
    {
        ViewFieldCell cellScript = _field[x, y];

        if (_objectForTeleport == null)
        {
            _objectForTeleport = cellScript;

            cellScript.SetTeleportColor(true);

            return;
        }
        else
        {
            _objectForTeleport.SetTeleportColor(false);

            if (_objectForTeleport != cellScript)
            {
                _objectForTeleport.SetTeleportInCell(cellScript.PositionY * Constants.MaxWidthOfField + cellScript.PositionX + 1);

                _level.profile.SetTeleport(_objectForTeleport.PositionX, _objectForTeleport.PositionY, cellScript.PositionY * Constants.MaxWidthOfField + cellScript.PositionX + 1);

                _objectForTeleport = null;
            }
            else
            {
                _objectForTeleport = null;
            }

        }
    }

    public void ButtonGravityFunction(int x, int y)
    {
        ViewFieldCell cellScript = _field[x, y];

        if (cellScript.gameObject.activeSelf)
        {
            _level.profile.SetGravity(x, y, cellScript.SetGravityInCell());
        }

    }

    public void ButtonColorFunction(int x, int y)
    {
        ViewFieldCell cellScript = _field[x, y];

        if (cellScript.gameObject.activeSelf)
        {
            if (cellScript.ColorOfCell == colorToCell)
            {
                _level.profile.SetChip(x, y, 0);

                cellScript.SetColorInCell(eColors.ColorRandom);
            }
            else
            {
                _level.profile.SetChip(x, y, Constants.GetIdByEnum(colorToCell));

                cellScript.SetColorInCell(colorToCell);
            }
        }
    }

    public void ActivateButtonSimpleBlockFunction(int x, int y)
    {
        ViewFieldCell cellScript = _field[x, y];

        if (cellScript.gameObject.activeSelf)
        {
            _level.profile.SetBlock(x, y, cellScript.SetBlock(1));
        }
    }


    public void ActivateButtonWeedFunction(int x, int y)
    {
        ViewFieldCell cellScript = _field[x, y];

        if (cellScript.gameObject.activeSelf)
        {
            _level.profile.SetBlock(x, y, cellScript.SetBlock(4));
        }
    }

    public void ActivateButtonBranchFunction(int x, int y)
    {
        ViewFieldCell cellScript = _field[x, y];

        if (cellScript.gameObject.activeSelf)
        {
            _level.profile.SetBlock(x, y, cellScript.SetBlock(5));
        }
    }

    public void ActivateButtonJellyFunction(int x, int y)
    {
        ViewFieldCell cellScript = _field[x, y];

        if (cellScript.gameObject.activeSelf)
        {
            _level.profile.SetJelly(x, y, cellScript.SetJelly(1));
        }
    }

    public void EditSugarDropCount(int newCountOfSugarDrop)
    {
        if (newCountOfSugarDrop > Constants.MaxCountOfSugarDrop)
        {
            EditSugarDropCount(Constants.MaxCountOfSugarDrop);

            return;
        }
        if (newCountOfSugarDrop < Constants.MinCountOfSugarDrop)
        {
            EditSugarDropCount(Constants.MinCountOfSugarDrop);

            return;
        }
        _level.profile.targetSugarDropsCount = newCountOfSugarDrop;
    }

    public void EditTargetColor(int newCountOfTargetColor)
    {
        if (newCountOfTargetColor > Constants.MaxCountTargetColor)
        {
            EditTargetColor(Constants.MaxCountTargetColor);

            return;
        }
        if (newCountOfTargetColor < Constants.MinCountTargetColor)
        {
            EditTargetColor(Constants.MinCountTargetColor);

            return;
        }

    }

    public void EditCountOfOneTargetColor(eColors color, int newCountOfTargetColor)
    {
        if (newCountOfTargetColor > Constants.MaxCountOfOneTargetColor)
        {
            EditCountOfOneTargetColor(color, Constants.MaxCountOfOneTargetColor);

            return;
        }

        _level.profile.SetTargetCount(Constants.GetIdByEnum(color), newCountOfTargetColor);

        return;
    }

    public void ShowJellyOptions(bool show)
    {
        foreach (ViewFieldCell fc in _field)
        {
            if (show)
            {
                fc.ShowJellyOptions(true);
            }
            else
            {
                fc.ShowJellyOptions(false);
            }
        }
    }

    public void ButtonSugarDropFunction(int x, int y)
    {
        ViewFieldCell cellScript = _field[x, y];

        if (cellScript.gameObject.activeSelf)
        {
            _level.profile.SetSugarDrop(x, y, cellScript.SetSugarDropInCell());
        }
    }

    public void ShowSugarDropOptions(bool show)
    {
        foreach (ViewFieldCell fc in _field)
        {
            if (show)
            {
                fc.ShowSugarDropOptions(true);
            }
            else
            {
                fc.ShowSugarDropOptions(false);
            }
        }
    }

    public void ShowWalls(bool show)
    {
        if (show)
        {
            for (int i = 0; i < Constants.MaxWidthOfField; i++)
            {
                for (int k = 0; k < Constants.MaxHeightOfField - 1; k++)
                {
                    _wallHorizontal[i, k].SetWallFunction(true);

                    if (i > _widthOfField - 1 || k > _heightOfField - 2)
                    {
                        _wallHorizontal[i, k].gameObject.SetActive(false);
                    }
                    else
                    {
                        if (!_field[i, k].IsActive || !_field[i, k + 1].IsActive)
                        {
                            _wallHorizontal[i, k].gameObject.SetActive(false);
                        }
                        else
                        {
                            _wallHorizontal[i, k].gameObject.SetActive(true);

                        }
                    }
                }
            }
            for (int i = 0; i < Constants.MaxWidthOfField - 1; i++)
            {
                for (int k = 0; k < Constants.MaxHeightOfField; k++)
                {	
                    _wallVertical[i, k].SetWallFunction(true);

                    if (i > _widthOfField - 2 || k > _heightOfField - 1)
                    {
                        _wallVertical[i, k].gameObject.SetActive(false);
                    }
                    else
                    {
                        if (!_field[i, k].IsActive || !_field[i + 1, k].IsActive)
                        {
                            _wallVertical[i, k].gameObject.SetActive(false);
                        }
                        else
                        {
                            _wallVertical[i, k].gameObject.SetActive(true);
                        }
                    }

                }
            }

        }
        else
        {
            for (int i = 0; i < Constants.MaxWidthOfField; i++)
            {
                for (int k = 0; k < Constants.MaxHeightOfField - 1; k++)
                {
                    _wallHorizontal[i, k].SetWallFunction(false);

                    if (i > _widthOfField - 1 || k > _heightOfField - 2)
                    {
                        _wallHorizontal[i, k].gameObject.SetActive(false);
                    }
                    else
                    {
                        if (!_field[i, k].IsActive || !_field[i, k + 1].IsActive)
                        {
                            _wallHorizontal[i, k].gameObject.SetActive(false);
                        }
                        else
                        {
                            if (_wallHorizontal[i, k].IsOn)
                            {
                                _wallHorizontal[i, k].gameObject.SetActive(true);
                            }
                            else
                            {
                                _wallHorizontal[i, k].gameObject.SetActive(false);
                            }

                        }
                    }
                }
            }
            for (int i = 0; i < Constants.MaxWidthOfField - 1; i++)
            {
                for (int k = 0; k < Constants.MaxHeightOfField; k++)
                {	
                    _wallVertical[i, k].SetWallFunction(false);

                    if (i > _widthOfField - 2 || k > _heightOfField - 1)
                    {
                        _wallVertical[i, k].gameObject.SetActive(false);
                    }
                    else
                    {
                        if (!_field[i, k].IsActive || !_field[i + 1, k].IsActive)
                        {
                            _wallVertical[i, k].gameObject.SetActive(false);
                        }
                        else
                        {
                            if (_wallVertical[i, k].IsOn)
                            {
                                _wallVertical[i, k].gameObject.SetActive(true);
                            }
                            else
                            {
                                _wallVertical[i, k].gameObject.SetActive(false);
                            }

                        }
                    }
                }
            }
        }
    }

    public void ActivateButtonWall()
    {
        ShowWalls(true);
    }

    public void WallClick(int x, int y, bool setOn, bool isVertical)
    {
        if (isVertical)
        {
            _level.profile.SetWallV(x, y, setOn);
        }
        else
        {
            _level.profile.SetWallH(x, y, setOn);
        }
    }
}
