using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class ViewWallBase : MonoBehaviour,IPointerDownHandler
{
    private int x;

    private int y;

    private bool _isVertical;

    private bool _isOn;

    private bool _wallFunctionIsOn;

    [SerializeField]
    private FieldController _fc;

    public bool IsOn
    {
        get
        {
            return _isOn;
        }
    }

    public void Construct(int x, int y, bool isOn, bool IsVertical)
    {
        this.x = x;

        this.y = y;

        _isVertical = IsVertical;

        _isOn = isOn;

        if (_isOn)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Constants.ColorWalls;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (_wallFunctionIsOn)
        {
			
            if (_isOn)
            {
                _isOn = false;

                gameObject.GetComponent<Image>().color = Constants.ColorWalls;
            }
            else
            {
                _isOn = true;

                gameObject.GetComponent<Image>().color = Color.red;
            }
            _fc.WallClick(x, y, _isOn, _isVertical);
        }
    }

    public void SetWallFunction(bool set)
    {
        _wallFunctionIsOn = set;
    }
}
