using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUIProps : MonoBehaviour
{
    [SerializeField] private Image[] orderedHabilIcon;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;


    public void SetUIProps(int characterIndex)
    {
        HabillityButton[] h = FindObjectsOfType<HabillityButton>();
        foreach(HabillityButton habillity in h)
        {
            habillity.characterIndex = characterIndex;
            
        }

        SetUICharProps(characterIndex);

    }


    private void SetUICharProps(int characterIndex)
    {
        Character character = RoomConfigs.instance.charactersOrdered[characterIndex];
        
        characterName.text = character.name;
        characterDescription.text = character.Descricao;

        for(int i= 0; i < orderedHabilIcon.Length; i++)
        {
            orderedHabilIcon[i].sprite = character.ordenedHabillityIcon[i];
        }

    }


}
