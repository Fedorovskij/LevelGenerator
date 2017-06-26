using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour,ITriggerable
{

    [SerializeField]
    protected FieldController _fc;

    [SerializeField]
    protected Button _buttonToClick;

    protected bool _buttonIsPressed;

    [SerializeField]
    protected GameObject _panelToActive;

    protected GameObject _panelContainsButtonToClick;

    protected IPanel _panelContainsButtonToClickScript;

    protected Color _pressedColor = Color.green;

    protected Color _unpressedColor = Color.white;

    protected Action actionOnButton;

    void Awake()
    {
        _panelContainsButtonToClick = _buttonToClick.gameObject.transform.parent.gameObject;

        _panelContainsButtonToClickScript = _panelContainsButtonToClick.GetComponent<IPanel>();

        _panelContainsButtonToClickScript.AddToList(_buttonToClick.GetComponent<ITriggerable>());

        _buttonToClick.onClick.AddListener(UnpressedAllButtonsUnlessOne);

        Debug.Log("_buttonToClick " + gameObject.name);

        if (_panelToActive != null)
        {
            _buttonToClick.onClick.AddListener(DeactivateAllObjectUnlessOne);

            _buttonToClick.GetComponentInParent<ViewSwitchPanel>().AddToListObjectSwithPanel(_panelToActive);
        }
    }

    public void UnpressedAllButtonsUnlessOne()
    {
        Debug.Log("UnpressedAllButtonsUnlessOne");

        _panelContainsButtonToClickScript.UnpressedAllButtonsUnlessOne(_buttonToClick.GetComponent<ITriggerable>(), actionOnButton, _panelContainsButtonToClickScript.GetListInPanel());
    }

    public void DeactivateAllObjectUnlessOne()
    {
        ViewSwitchPanel viewSwitchPanelScript = _buttonToClick.transform.parent.GetComponent<ViewSwitchPanel>();

        viewSwitchPanelScript.DeactivateAllObjectUnlessOne(_panelToActive);

        List<GameObject> listOfGO = viewSwitchPanelScript.GetListObjectSwithPanel();

        foreach (GameObject go in listOfGO)
        {
            go.GetComponent<IPanel>().UnpressedAllButtonsUnlessOne(null, null, go.GetComponent<IPanel>().GetListInPanel());
        }

    }

    public void Trigger(bool setButtonPressed)
    {
        if (setButtonPressed == false)
        {
            _buttonIsPressed = false;

            _buttonToClick.GetComponent<Image>().color = _unpressedColor;

            if (_panelToActive == null)
            {
                _fc.RemoveAllHandlers();
            }

        }
        else
        {
			
            if (_buttonIsPressed != true)
            {
                _buttonIsPressed = true;

                _buttonToClick.GetComponent<Image>().color = _pressedColor;

                if (_panelToActive != null)
                {
                    _panelToActive.SetActive(true);
                }
            }
            else
            {
                if (_panelToActive == null)
                {
                    _buttonIsPressed = false;

                    _buttonToClick.GetComponent<Image>().color = _unpressedColor;

                    _fc.RemoveAllHandlers();
                }
            }

        }
    }

    public bool IsPressed
    { 
        get
        { 
            return _buttonIsPressed;
        }
    }
	


}
