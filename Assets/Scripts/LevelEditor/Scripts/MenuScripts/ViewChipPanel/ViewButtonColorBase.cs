using UnityEngine;
using UnityEngine.UI;

public class ViewButtonColorBase : ControlButtons
{
    public eColors ColorOfChip = eColors.ColorRandom;

    void Start()
    {
        _pressedColor = Color.cyan;

        _unpressedColor = Constants.GetColorByEnum(ColorOfChip);

        _buttonToClick.GetComponent<Image>().color = _unpressedColor;

        actionOnButton = ActivateButton;    
    }

    public void  ActivateButton()
    {
        _fc.ActivateColor(ColorOfChip);
    }
}
