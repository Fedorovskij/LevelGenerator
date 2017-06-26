using UnityEngine;
using UnityEngine.UI;

public class ViewEditWidth : MonoBehaviour
{
    [SerializeField]
    private InputField _inputWidth;

    [SerializeField]
    private FieldController _fc;

    public void Construct(int width)
    {
        _inputWidth.text = width.ToString();
    }

    void Start()
    {
        _inputWidth.onEndEdit.AddListener(EditTextWidth);
    }

    private void EditTextWidth(string newWidth)
    {
        int intNewWidth;

        if (int.TryParse(newWidth, out intNewWidth))
        {
            if (intNewWidth > Constants.MaxWidthOfField)
            {
                _inputWidth.text = Constants.MaxWidthOfField.ToString();

                _fc.EditWidth(Constants.MaxWidthOfField);

                return;
            }

            if (intNewWidth < Constants.MinWidthOfField)
            {
                _inputWidth.text = Constants.MinWidthOfField.ToString();

                _fc.EditWidth(Constants.MinWidthOfField);

                return;
            }

            _fc.EditWidth(intNewWidth);

            return;
        }

    }

}
