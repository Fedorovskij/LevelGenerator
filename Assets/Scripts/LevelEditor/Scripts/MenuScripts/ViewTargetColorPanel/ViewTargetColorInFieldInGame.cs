using UnityEngine;
using UnityEngine.UI;

public class ViewTargetColorInFieldInGame : MonoBehaviour
{
    [SerializeField]
    private InputField _inputTargetColor;

    [SerializeField]
    private ViewColorInput _colorA;

    [SerializeField]
    private ViewColorInput _colorB;

    [SerializeField]
    private ViewColorInput _colorC;

    [SerializeField]
    private ViewColorInput _colorD;

    [SerializeField]
    private ViewColorInput _colorE;

    [SerializeField]
    private ViewColorInput _colorF;

    private int _currentCountOfPosibleColors;

    private int _maxCountOfPosibleColors = Constants.MaxCountTargetColor;

    private ViewColorInput[] _arrayOfColorInput = new ViewColorInput[Constants.MaxCountTargetColor];

    public void Construct(int[] countOfEachTargetCount)
    {
        int countOfDifferentColors = 0;

        for (int i = 0; i < countOfEachTargetCount.Length; i++)
        {
            if (countOfEachTargetCount[i] != 0)
            {
                countOfDifferentColors++;
            }
        }

        _arrayOfColorInput[0] = _colorA;

        _arrayOfColorInput[1] = _colorB;

        _arrayOfColorInput[2] = _colorC;

        _arrayOfColorInput[3] = _colorD;

        _arrayOfColorInput[4] = _colorE;

        _arrayOfColorInput[5] = _colorF;

        for (int i = 0; i < Constants.MaxCountTargetColor; i++)
        {
            _arrayOfColorInput[i].Construct(countOfEachTargetCount[i]);
        }

        Debug.Log("countOfDifferentColors " + countOfDifferentColors);

        _inputTargetColor.text = countOfDifferentColors.ToString();
		
        EditCountOfTargetColor(countOfDifferentColors.ToString());
    }


    void Start()
    {
        _inputTargetColor.onEndEdit.AddListener(EditCountOfTargetColor);
    }

    public void EditMaxCountOfPosibleColors(int newMax)
    {
        {
            if (gameObject.activeSelf == true)
            {
                EditCountOfTargetColor(newMax.ToString());
            }

            _maxCountOfPosibleColors = newMax;

            _currentCountOfPosibleColors = _maxCountOfPosibleColors;

            _inputTargetColor.text = newMax.ToString();
        }
    }

    private void EditCountOfTargetColor(string newTargetColor)
    {
        int intNewTargetColor;

        if (int.TryParse(newTargetColor, out intNewTargetColor))
        {
			
            if (intNewTargetColor > _maxCountOfPosibleColors)
            {
                _inputTargetColor.text = _maxCountOfPosibleColors.ToString();

                _currentCountOfPosibleColors = _maxCountOfPosibleColors;

                EditCountOfTargetColor(_maxCountOfPosibleColors.ToString());

                return;
            }

            if (intNewTargetColor < Constants.MinCountTargetColor)
            {
                _inputTargetColor.text = Constants.MinCountTargetColor.ToString();

                _currentCountOfPosibleColors = Constants.MinCountTargetColor;

                EditCountOfTargetColor(Constants.MinCountTargetColor.ToString());

                return;
            }

            for (int i = 0; i < Constants.MaxCountTargetColor; i++)
            {
                if (i < intNewTargetColor)
                {
                    _arrayOfColorInput[i].gameObject.SetActive(true);
                }
                else
                {
                    _arrayOfColorInput[i].gameObject.SetActive(false);

                }
            }

            _currentCountOfPosibleColors = intNewTargetColor;
        }
    }
}
