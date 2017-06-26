using UnityEngine;
using UnityEngine.UI;

public class ViewEditHeight : MonoBehaviour
{
    [SerializeField]
    private InputField _inputHeight;

    [SerializeField]
    private FieldController _fc;

    public void Construct(int height)
    {
        _inputHeight.text = height.ToString();
    }

    private void Start()
    {
        _inputHeight.onEndEdit.AddListener(EditTextHeight);
    }

    private void EditTextHeight(string newHeight)
    {
        int intNewHeight;

        if (int.TryParse(newHeight, out intNewHeight))
        {
            if (intNewHeight > Constants.MaxHeightOfField)
            {
                _inputHeight.text = Constants.MaxHeightOfField.ToString();

                _fc.EditHeight(Constants.MaxHeightOfField);

                return;
            }
            if (intNewHeight < Constants.MinHeightOfField)
            {
                _inputHeight.text = Constants.MinHeightOfField.ToString();

                _fc.EditHeight(Constants.MinHeightOfField);

                return;
            }

            _fc.EditHeight(intNewHeight);

            return;
        }

    }

}
