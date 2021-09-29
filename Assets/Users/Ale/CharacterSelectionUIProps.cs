using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionUIProps : MonoBehaviour
{

    public void SetUIProps(int characterIndex)
    {
        HabillityButton[] h = FindObjectsOfType<HabillityButton>();
        foreach(HabillityButton habillity in h)
        {
            habillity.characterIndex = characterIndex;
            habillity.UpdateUIProps();
        }

    }



    private void ActiveCharacterButton()
    {

    }


}
