using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSwitchPanel : MonoBehaviour, IPanel
{
    private List<ITriggerable> _listOfButtonsSwitchPanel = new List<ITriggerable>();

    private List<GameObject> _listOfObjectsSwitchPanel = new List<GameObject>();

    [SerializeField]
    private ViewSlotPanel _slotPanelScript;

    [SerializeField]
    private FieldController _fc;

    public void AddToList(ITriggerable buttonScript)
    {
        _listOfButtonsSwitchPanel.Add(buttonScript);

    }

    public void AddToListObjectSwithPanel(GameObject gameObjectInPanel)
    {
        _listOfObjectsSwitchPanel.Add(gameObjectInPanel);
    }

    public List<GameObject> GetListObjectSwithPanel()
    {
        return _listOfObjectsSwitchPanel;
    }

    public void UnpressedAllButtonsUnlessOne(ITriggerable buttonToPress, Action actionOnButton, List<ITriggerable> list)
    {
        _fc.RemoveAllHandlers();
		
        foreach (ITriggerable it in list)
        {
            if (it == buttonToPress)
            {
                it.Trigger(true);
            }
            else
            {
                it.Trigger(false);
            }
        }
        if (actionOnButton != null)
        {
            actionOnButton();
        }
    }

    public List<ITriggerable> GetListInPanel()
    {
        return _listOfButtonsSwitchPanel;
    }

    public void DeactivateAllObjectUnlessOne(GameObject GameObjectToActive)
    {
        foreach (GameObject go in _listOfObjectsSwitchPanel)
        {
            if (go == GameObjectToActive)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
