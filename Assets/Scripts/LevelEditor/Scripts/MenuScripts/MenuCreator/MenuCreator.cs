using UnityEngine;

public class MenuCreator  : MonoBehaviour
{
    [SerializeField]
    private ViewEditWidth _editWidthScript;

    [SerializeField]
    private ViewEditHeight _editHeightScript;

    [SerializeField]
    private ViewEditCountColor _editCountColorScript;

    [SerializeField]
    private ViewEditScoreStars _scoreStarsPanel;

    [SerializeField]
    private ViewEditMovesOrTime _editMovesOrTimeScript;

    [SerializeField]
    private ViewEditMoveCount _editMovesCount;

    [SerializeField]
    private ViewEditTimeCount _editTimeCount;

    [SerializeField]
    private ViewEditTarget _editTarget;

    [SerializeField]
    private ViewSugarDrop _sugarDropPanel;

    [SerializeField]
    private ViewEditPortion _portionCount;

    [SerializeField]
    private Level _level;

    int countOfDifferentColors;

    public void SetInfo(Level level)
    {
        _level = level;       

        Debug.Log(" _level " + _level.profile.target);

        _editWidthScript.Construct(_level.profile.width);

        _editHeightScript.Construct(_level.profile.height);

        _editCountColorScript.Construct(_level.profile.chipCount);

        _scoreStarsPanel.Construct(_level.profile.firstStarScore, _level.profile.secondStarScore, _level.profile.thirdStarScore);

        _portionCount.Construct(_level.profile.buttonPortion);
		
        _editMovesOrTimeScript.Construct(_level.profile.limitation);
		
        _editMovesCount.Construct(_level.profile.moveCount);
		
        _editTimeCount.Construct(_level.profile.duration);
		
        _editTarget.Construct(_level.profile.target);		

        _editTarget.GetTargetColorPanel.Construct(_level.profile.countOfEachTargetCount);

        _sugarDropPanel.Construct(_level.profile.targetSugarDropsCount);
    }
}
