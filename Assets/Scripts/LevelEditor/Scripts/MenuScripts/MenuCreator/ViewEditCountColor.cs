using UnityEngine;
using UnityEngine.UI;

public class ViewEditCountColor : MonoBehaviour
{
    [SerializeField]
    private InputField _inputCountColors;

    [SerializeField]
    private ViewTargetColorInFieldInGame _tagetColorPanelScript;

    [SerializeField]
    private FieldController _fc;

    public void Construct(int countColors)
    {
        _inputCountColors.text = countColors.ToString();
    }

    void Start()
    {              
        _inputCountColors.onEndEdit.AddListener(EditTextCountColor);
    }

    void EditTextCountColor(string newCountColor)
    {
        int intNewCountColor;

        if (int.TryParse(newCountColor, out intNewCountColor))
        {
            if (intNewCountColor > Constants.MaxCountOfColors)
            {
                _inputCountColors.text = Constants.MaxCountOfColors.ToString();

                EditTextCountColor(Constants.MaxCountOfColors.ToString());

                return;
            }
            if (intNewCountColor < Constants.MinCountOfColors)
            {
                _inputCountColors.text = Constants.MinCountOfColors.ToString();

                EditTextCountColor(Constants.MinCountOfColors.ToString());

                return;
            }

            _inputCountColors.text = intNewCountColor.ToString();

            _fc.EditCountOfColors(intNewCountColor);

            _tagetColorPanelScript.EditMaxCountOfPosibleColors(intNewCountColor);

        }

    }


}
