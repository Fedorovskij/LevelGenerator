using UnityEngine;
using UnityEngine.UI;

public class ViewButtonGravity : ControlButtons
{
    void Start()
    {
        actionOnButton = _fc.ActivateGravityButton;
    }
}
