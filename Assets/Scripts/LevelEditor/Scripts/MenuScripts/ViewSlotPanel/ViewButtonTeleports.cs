using UnityEngine;
using UnityEngine.UI;

public class ViewButtonTeleports : ControlButtons
{
    void Start()
    {
        actionOnButton = _fc.ActivateTeleportsButton;
    }
}
