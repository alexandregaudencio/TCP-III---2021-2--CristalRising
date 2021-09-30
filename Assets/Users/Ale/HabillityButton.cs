using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HabillityButton : MonoBehaviour
{
    public int habillityIndex;
    public int characterIndex;

    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text characterDescriptText;
    [SerializeField] private GameObject habillityObj;




    public void UpdateUIProps()
    {
        Character character = RoomConfigs.instance.charactersOrdered[characterIndex];

        //icon
        //GetComponentInChildren<Image>().sprite = character.ordenedHabillityIcon[habillityIndex];
        //Name
        characterNameText.text = character.name;
        characterDescriptText.text = character.Descricao;

    }

    public void ShowHabillityDetails()
    {
        Character character = RoomConfigs.instance.charactersOrdered[characterIndex];
        TMP_Text[] texts = habillityObj.GetComponentsInChildren<TMP_Text>();
        texts[0].text = character.ordenedHabillityName[habillityIndex];
        texts[1].text = character.ordenedHabillityDescription[habillityIndex];

    }


}
