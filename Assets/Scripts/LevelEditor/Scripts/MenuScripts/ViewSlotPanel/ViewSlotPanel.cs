using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSlotPanel : MonoBehaviour,IPanel
{
    private List<ITriggerable> _listOfButtons = new List<ITriggerable>();

    [SerializeField]
    private FieldController _fc;

    public void AddToList(ITriggerable buttonScript)
    {
        _listOfButtons.Add(buttonScript);
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
        return _listOfButtons;
    }

}
