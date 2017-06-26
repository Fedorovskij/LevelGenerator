using UnityEngine;

public class ShowFieldInEditor : FieldController
{
    public void ShowField(LevelProfile level)
    {      
        Level _newLevel = new Level();

        _newLevel.profile = level;

        ExitLevel();

        InstantiateField(_newLevel);
    }

    public override void OnEnable()
    {
        OnShowWindowEvent();
    }
}
