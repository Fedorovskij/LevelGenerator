using UnityEngine;
using UnityEngine.UI;

public class ViewEditPortion : MonoBehaviour
{
    [SerializeField]
    private InputField _inputPortion;

    [SerializeField]
    private FieldController _fc;

    public void Construct(float portion)
    {
        _inputPortion.text = portion.ToString();
    }

    void Start()
    {
        _inputPortion.onEndEdit.AddListener(EditTextPortion);
    }


    void EditTextPortion(string newPortion)
    {
        float intNewPortion;

        if (float.TryParse(newPortion, out intNewPortion))
        {
            if (intNewPortion > Constants.MaxCountPortion)
            {
                _inputPortion.text = Constants.MaxCountPortion.ToString();

                _fc.EditCountPortion(Constants.MaxCountPortion);
				
                return;
            }

            if (intNewPortion < Constants.MinCountPortion)
            {
                _inputPortion.text = Constants.MinCountPortion.ToString();

                _fc.EditCountPortion(Constants.MinCountPortion);
				
                return;
            }

            _inputPortion.text = intNewPortion.ToString();

            _fc.EditCountPortion(intNewPortion);
        }

    }
}
