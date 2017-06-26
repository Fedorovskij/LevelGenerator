using UnityEngine;
using UnityEngine.UI;

public class ViewEditMovesOrTime : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggleTime;

    [SerializeField]
    private Toggle _toggleMoves;

    [SerializeField]
    private GameObject _timePanel;

    [SerializeField]
    private GameObject _movesPanel;

    [SerializeField]
    private FieldController _fc;

    public void Construct(Limitation lim)
    {
        if (lim == Limitation.Moves)
        {
            _toggleMoves.isOn = true;
			
            _toggleTime.isOn = false;
			
            ActiveTimePanel(false);
        }
        else
        {
            _toggleMoves.isOn = false;
			
            _toggleTime.isOn = true;
			
            ActiveTimePanel(true);
        }
    }

    void Start()
    {
        _toggleTime.onValueChanged.AddListener(ActiveTimePanel);

        _toggleMoves.onValueChanged.AddListener(_fc.EditToggleMoves);

        _toggleTime.onValueChanged.AddListener(_fc.EditToggleTime);
    }


    public void ActiveTimePanel(bool value)
    {
        if (value)
        {
            _timePanel.SetActive(true);
			
            _movesPanel.SetActive(false);
        }
        else
        {
            _timePanel.SetActive(false);
			
            _movesPanel.SetActive(true);
        }
    }

}
