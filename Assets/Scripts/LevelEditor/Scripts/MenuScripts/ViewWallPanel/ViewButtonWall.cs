using UnityEngine;
using UnityEngine.UI;

public class ViewButtonWall : ControlButtons
{
    void Start()
    {   
        actionOnButton = _fc.ActivateButtonWall;
    }
}
