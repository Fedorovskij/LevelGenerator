using UnityEngine;
using UnityEngine.UI;

public class ViewEditScoreStars : MonoBehaviour
{
    [SerializeField]
    private InputField _inputScoreFirst;

    [SerializeField]
    private InputField _inputScoreSecond;

    [SerializeField]
    private InputField _inputScoreThird;

    [SerializeField]
    private FieldController _fc;

    public void Construct(int firstScore, int secondScore, int thirdScore)
    {
        _inputScoreFirst.text = firstScore.ToString();

        _inputScoreSecond.text = secondScore.ToString();

        _inputScoreThird.text = thirdScore.ToString();
    }

    void Start()
    {
        _inputScoreFirst.onEndEdit.AddListener(EditTextScoreFirst);

        _inputScoreSecond.onEndEdit.AddListener(EditTextScoreSecond);

        _inputScoreThird.onEndEdit.AddListener(EditTextScoreThird);
    }

    void EditTextScoreFirst(string newScoreFirst)
    {
        int intNewScoreFirst;

        if (int.TryParse(newScoreFirst, out intNewScoreFirst))
        {
            if (intNewScoreFirst < Constants.MinFirstScore)
            {
                _inputScoreFirst.text = Constants.MinFirstScore.ToString();

                _fc.EditFirstScore(Constants.MinFirstScore);

                return;
            }

            _fc.EditFirstScore(intNewScoreFirst);

            return;
        }
    }

    void EditTextScoreSecond(string newScoreSecond)
    {
        int intNewScoreSecond;

        if (int.TryParse(newScoreSecond, out intNewScoreSecond))
        {
            if (intNewScoreSecond < Constants.MinSecondScore)
            {
                _inputScoreSecond.text = Constants.MinSecondScore.ToString();

                _fc.EditSecondScore(Constants.MinSecondScore);

                return;
            }

            _fc.EditSecondScore(intNewScoreSecond);

            return;
        }   
    }

    void EditTextScoreThird(string newScoreThird)
    {
        int intNewScoreThrid;

        if (int.TryParse(newScoreThird, out intNewScoreThrid))
        {
            if (intNewScoreThrid < Constants.MinThirdScore)
            {
                _inputScoreThird.text = Constants.MinThirdScore.ToString();

                _fc.EditThirdScore(Constants.MinThirdScore);

                return;
            }
            _fc.EditThirdScore(intNewScoreThrid);

            return;
        }
    }
	

}
