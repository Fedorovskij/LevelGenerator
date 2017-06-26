using UnityEngine;
using UnityEngine.UI;

public class ViewButtonSlot : ControlButtons
{
    void Start()
    {
        actionOnButton = _fc.ActivateSlotButton;
    }
}
