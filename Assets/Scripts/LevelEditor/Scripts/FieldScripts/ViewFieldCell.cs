using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewFieldCell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject _generatorImage;

    [SerializeField]
    private GameObject _sugarDropImage;

    [SerializeField]
    private GameObject _gravityImage;

    [SerializeField]
    private Text _teleportText;

    [SerializeField]
    private Text _textOfBlock;

    [SerializeField]
    public Text _textNumberCell;

    [SerializeField]
    private Text _textOfJelly;

    [SerializeField]
    private CanvasGroup cg;

    private int x;

    private int y;

    private eColors _color;

    private int _gravity;

    private bool _isGenerator;

    private int _typeOfBlock;

    private int _typeOfJelly;

    private int _teleport;

    private bool _isSugarDrop;

    private bool _isActive;

    private float _cell_alpha = 0.1f;

    public FieldController FieldController { get; set; }

    public void SetSetAlpha(float cell_alpha)
    {
        Debug.LogError(cell_alpha);

        _cell_alpha = cell_alpha;
    }

    public void OnPointerDown(PointerEventData eventData)
    {       
        FieldController.ActivateCellForEdit(x, y);
    }

    public bool IsActive
    {
        get
        { 
            return _isActive;
        }

        set
        {	 
            switch (value)
            {                
                case true:
                    
                    cg.alpha = 1;

                    break;

                case false:

                    cg.alpha = _cell_alpha;

                    break;
            }

            _isActive = value;
        }
    }

    public int PositionX
    {
        get
        { 
            return x;
        }
        set
        {
            x = value;
        }
    }

    public int PositionY
    {
        get
        { 
            return y;
        }
        set
        {
            y = value;
        }
    }

    public eColors ColorOfCell
    {
        get
        { 
            return _color;
        }
    }

    public void Construct(int x, int y, eColors color, int gravity, bool isGenerator, int typeOfBlock, int typeOfJelly, int teleport, bool isSugarDrop, bool isActive)
    {
        this.x = x;

        this.y = y;

        _color = color;

        SetColorInCell(_color);

        _gravity = gravity;
		
        _gravityImage.transform.Rotate(new Vector3(0, 0, -90) * _gravity);

        _isGenerator = isGenerator;

        if (isGenerator)
        {
            _generatorImage.SetActive(true);
        }

        _textOfBlock.text = typeOfBlock.ToString();
		
        SetBlock(typeOfBlock);

        _typeOfJelly = typeOfJelly;

        _textOfJelly.text = TypeOfJellyInString(_typeOfJelly);

        _teleport = teleport;

        if (_teleport == 0)
        {
            _teleportText.text = "";
        }
        else
        {
            _teleportText.text = "T - " + teleport.ToString();
        }

        _isSugarDrop = isSugarDrop;
		
        if (_isSugarDrop)
        {
            _sugarDropImage.SetActive(true);
        }
       
        IsActive = isActive;
    }


    public bool SetGeneretorInCell()
    {
        if (_generatorImage.activeSelf)
        {
            _generatorImage.SetActive(false);

            return false;
        }
        else
        {
            _generatorImage.SetActive(true);

            return true;
        }
    }

    public int SetGravityInCell()
    {
        _gravity++;

        if (_gravity > 3)
        {
            _gravity = 0;
        }
        _gravityImage.transform.Rotate(new Vector3(0, 0, -90));

        return _gravity;
    }

    public void SetTeleportInCell(int teleportToCell)
    {
        _teleportText.text = "T - " + teleportToCell.ToString();
    }

    public void SetColorInCell(eColors color)
    {
        GetComponent<Image>().color = Constants.GetColorByEnum(color);

        _color = color;
    }


    public int SetBlock(int blockCell)
    {
        if (blockCell == 0)
        {
            _typeOfBlock = 0;

            _textOfBlock.text = "";

            return _typeOfBlock;
        }

        if (blockCell == 1)
        {
            _typeOfBlock++;

            _textOfBlock.text = "B:" + _typeOfBlock.ToString();

            if (_typeOfBlock > 3)
            {
                _typeOfBlock = 0;

                _textOfBlock.text = "";
            }

            return _typeOfBlock;
        }
        else
        {
            if (blockCell == 4)
            {
                if (_typeOfBlock == 4)
                {
                    _typeOfBlock = 0;

                    _textOfBlock.text = "";

                    return _typeOfBlock;
                }
                else
                {
                    _typeOfBlock = 4;

                    _textOfBlock.text = "Weed";

                    return _typeOfBlock;
                }
            }

            if (blockCell == 5)
            {
                if (_typeOfBlock == 5)
                {
                    _typeOfBlock = 0;

                    _textOfBlock.text = "";

                    return _typeOfBlock;
                }
                else
                {
                    _typeOfBlock = 5;

                    _textOfBlock.text = "Branch";

                    return _typeOfBlock;
                }
            }
        }

        return -1;
    }

    public int SetJelly(int jellyCell)
    {
        if (jellyCell == 1)
        {
            _typeOfJelly++;

            _textOfJelly.text = TypeOfJellyInString(_typeOfJelly);

            return _typeOfJelly;
        }

        return -1;
    }

    public void ShowJellyOptions(bool show)
    {
        if (show)
        {
            _textOfJelly.text = TypeOfJellyInString(_typeOfJelly);
        }
        else
        {
            _textOfJelly.text = "";
        }

    }

    private string TypeOfJellyInString(int type)
    {
        switch (_typeOfJelly)
        {
            case 0:
                
                return "";

                break;

            case 1:
                
                return "JS";

                break;

            case 2:
                
                return "JT";

                break;

            default:
                
                _typeOfJelly = 0;

                return "";

                break;                
        }

    }

    public bool SetSugarDropInCell()
    {
        if (_sugarDropImage.activeSelf)
        {
            _sugarDropImage.SetActive(false);

            _isSugarDrop = false;

            return false;
        }
        else
        {
            Debug.Log("true");

            _sugarDropImage.SetActive(true);

            _isSugarDrop = true;

            return true;
        }

    }

    public void ShowSugarDropOptions(bool show)
    {
        if (show)
        {
            if (_isSugarDrop)
            {
                Debug.Log("true");

                _sugarDropImage.SetActive(true);
            }
        }
        else
        {
            _sugarDropImage.SetActive(false);
        }

    }

    public void SetTeleportColor(bool set)
    {
        if (set)
        {
            GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            GetComponent<Image>().color = Constants.GetColorByEnum(_color);
        }
    }
}
