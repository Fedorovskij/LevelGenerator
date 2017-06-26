using UnityEngine;

public interface ITriggerable
{
    void Trigger(bool setButtonPressed);

    bool IsPressed{ get; }
}


