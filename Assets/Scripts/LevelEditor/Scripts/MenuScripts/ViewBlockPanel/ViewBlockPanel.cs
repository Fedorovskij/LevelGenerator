using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewBlockPanel : MonoBehaviour,IPanel
{

    private List<ITriggerable> _listOfButtons = new List<ITriggerable>();

    public void AddToList(ITriggerable buttonScript)
    {
        _listOfButtons.Add(buttonScript);
    }

    public void UnpressedAllButtonsUnlessOne(ITriggerable buttonToPress, Action actionOnButton, List<ITriggerable> list)
    {
        foreach (ITriggerable it in _listOfButtons)
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
        return _listOfButtons;
    }
}
