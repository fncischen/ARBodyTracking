using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterEvent", menuName = "ScriptableObjects/CharacterEvent")]
public class CharacterEvent : ScriptableObject
{
    public virtual void TriggerCharacterEvent()
    {
        Debug.Log("This event is being triggered at " + this.name);
    }
}
