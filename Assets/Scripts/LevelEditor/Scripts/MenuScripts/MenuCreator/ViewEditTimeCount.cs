using UnityEngine;
using UnityEngine.UI;

public class ViewEditTimeCount : MonoBehaviour
{
    [SerializeField]
    private InputField _inputTimeCount;

    [SerializeField]
    private FieldController _fc;

    public void Construct(int timeCount)
    {
        _inputTimeCount.text = timeCount.ToString();
    }

    void Start()
    {
        _inputTimeCount.onEndEdit.AddListener(EditTextCountTime);
    }

    void EditTextCountTime(string newTimeCount)
    {
        int intNewTimeCount;

        if (int.TryParse(newTimeCount, out intNewTimeCount))
        {
            if (intNewTimeCount > Constants.MaxCountOfTime)
            {
                _inputTimeCount.text = Constants.MaxCountOfTime.ToString();

                _fc.EditTimeCount(Constants.MaxCountOfTime);

                return;

            }
            if (intNewTimeCount < Constants.MinCountOfTime)
            {
                _inputTimeCount.text = Constants.MinCountOfTime.ToString();

                _fc.EditTimeCount(Constants.MinCountOfTime);

                return;

            }

            _fc.EditTimeCount(intNewTimeCount);

            return;
        }


    }
	


}
