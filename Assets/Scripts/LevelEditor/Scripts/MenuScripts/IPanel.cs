using System.Collections.Generic;
using UnityEngine;

public interface IPanel
{
    void UnpressedAllButtonsUnlessOne(ITriggerable buttonToPress, Action actionOnButton, List<ITriggerable> list);

    List<ITriggerable> GetListInPanel();

    void AddToList(ITriggerable buttonScript);
}
