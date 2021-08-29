using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class ButtonsCharactSelectManager : MonoBehaviour
{
    //Pra dar bom os ícones os dois abaixo precisam estar na mesma ordem.
    private GameObject characters;
    [SerializeField] private Button[] characterSelectButtons;
    [SerializeField] private GameObject cancelButton;
    

    public void ChooseCharacter(GameObject characterPrefab)
    {
        characters = characterPrefab;
        PhotonNetwork.LocalPlayer.TagObject = characterPrefab.name;
        SwitchButtonsInteractable(false);

    }


    public void ResetChange()
    {
        SwitchButtonsInteractable(true);
    }



    private void SwitchButtonsInteractable(bool boolean)
    {
        foreach (Button buttons in characterSelectButtons)
        {
            buttons.interactable = boolean;
        }

        cancelButton.SetActive(!boolean);
    }


}
