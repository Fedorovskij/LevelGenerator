using UnityEngine;
using UnityEngine.UI;

public class ViewEditMoveCount : MonoBehaviour
{
    [SerializeField]
    private InputField _inputMoveCount;

    [SerializeField]
    private FieldController _fc;

    public void Construct(int movesCount)
    {
        _inputMoveCount.text = movesCount.ToString();
    }

    void Start()
    {
        _inputMoveCount.onEndEdit.AddListener(EditTextCountMoves);
    }

    void EditTextCountMoves(string newMovesCount)
    {
        int intNewMovesCount;

        if (int.TryParse(newMovesCount, out intNewMovesCount))
        {
            if (intNewMovesCount > Constants.MaxCountOfMoves)
            {
                _inputMoveCount.text = Constants.MaxCountOfMoves.ToString();

                _fc.EditMoveCount(Constants.MaxCountOfMoves);

                return;

            }
            if (intNewMovesCount < Constants.MinCountOfMoves)
            {
                _inputMoveCount.text = Constants.MinCountOfMoves.ToString();

                _fc.EditMoveCount(Constants.MinCountOfMoves);

                return;
            }

            _fc.EditMoveCount(intNewMovesCount);

            return;
        }

    }
}
