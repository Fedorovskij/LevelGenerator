using UnityEngine;
using UnityEngine.UI;

public class ViewColorInput : MonoBehaviour 
{

	[SerializeField]
	private InputField _inputColorA;

	public eColors ColorOfTarget = eColors.ColorRandom;

	private int _countOfTarget;	

    [SerializeField]
    private FieldController _fc;

    void Start () 
    {
		_inputColorA.onEndEdit.AddListener(EditCountTargetColor);
	}

    public void Construct(int countOfEachTargetCount )
    {
        _inputColorA.text = countOfEachTargetCount.ToString();

        _countOfTarget = countOfEachTargetCount;

        _fc.EditCountOfOneTargetColor(ColorOfTarget,_countOfTarget); 
    }

	void EditCountTargetColor(string newTargetColor)
	{
		int intNewTargetColor;

		if (int.TryParse(newTargetColor, out intNewTargetColor))
		{
			if (intNewTargetColor > Constants.MaxCountOfOneTargetColor)
			{
				_inputColorA.text = Constants.MaxCountOfOneTargetColor.ToString();

				_fc.EditCountOfOneTargetColor(ColorOfTarget,Constants.MaxCountOfOneTargetColor);

				_countOfTarget = Constants.MaxCountOfOneTargetColor;

				return;
			}
			if (intNewTargetColor < Constants.MinCountOfOneTargetColor)
			{
				_inputColorA.text = Constants.MinCountOfOneTargetColor.ToString();
				
                _fc.EditCountOfOneTargetColor(ColorOfTarget,Constants.MinCountOfOneTargetColor);
				
                _countOfTarget = Constants.MinCountOfOneTargetColor;
				
                return;
			}

			_inputColorA.text = intNewTargetColor.ToString();

			_fc.EditCountOfOneTargetColor(ColorOfTarget,intNewTargetColor);
			
             _countOfTarget = intNewTargetColor;
			
            return;
		}
	}

}
