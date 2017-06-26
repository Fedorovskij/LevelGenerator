using UnityEngine;
using UnityEngine.UI;

public class ViewSugarDrop  : MonoBehaviour
{
    [SerializeField]
    private InputField _inputSugarDropCount;

    [SerializeField]
    private FieldController _fc;  

    private int _countOfSugarDrop;

    public void Construct(int sugarDropCount)
    {
        _inputSugarDropCount.text = sugarDropCount.ToString();
    }

    void Start()
    {
        _inputSugarDropCount.onValueChanged.AddListener(EditCountOfSugarDrop);
    }

    void EditCountOfSugarDrop(string newWidth)
    {
        int intNewWidth;

        if (int.TryParse(newWidth, out intNewWidth))
        {
            if (intNewWidth > Constants.MaxCountOfSugarDrop)
            {
                _inputSugarDropCount.text = Constants.MaxCountOfSugarDrop.ToString();

                _fc.EditSugarDropCount(Constants.MaxCountOfSugarDrop);

                _countOfSugarDrop = Constants.MaxCountOfSugarDrop;

                return;
            }
            if (intNewWidth < Constants.MinCountOfSugarDrop)
            {
                _inputSugarDropCount.text = Constants.MinCountOfSugarDrop.ToString();

                _fc.EditSugarDropCount(Constants.MinCountOfSugarDrop);

                _countOfSugarDrop = Constants.MinCountOfSugarDrop;

                return;
            }

            _inputSugarDropCount.text = intNewWidth.ToString();

            _fc.EditSugarDropCount(intNewWidth);

            _countOfSugarDrop = intNewWidth;

            return;
        }

    }

    void OnEnable()
    {
        if (_fc != null)
        {
            EditCountOfSugarDrop(_countOfSugarDrop.ToString());

            _fc.EditSugarDropCount(_countOfSugarDrop);
        }
    }

    void OnDisable()
    {
        _fc.EditSugarDropCount(0);
    }

}
