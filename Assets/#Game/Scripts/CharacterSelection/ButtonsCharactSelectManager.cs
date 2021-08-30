using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ButtonsCharactSelectManager : MonoBehaviour
{
    //Pra dar bom, a ordem de associação dos objetos via unity precisam estar na mesma ordem.
    [SerializeField] private Button[] characterSelectButtons;
    [SerializeField] private GameObject CharacterDefault;   
    [SerializeField] private GameObject cancelButton;

    private ExitGames.Client.Photon.Hashtable characterIcon = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        PhotonNetwork.LocalPlayer.TagObject = CharacterDefault.name;
    }


    public void ChooseCharacter(GameObject characterPrefab)
    {
        PhotonNetwork.LocalPlayer.TagObject = characterPrefab.name;
        SwitchButtonsInteractable(false);
    }

    //AÇÃO DOS BOTÕES DE SEÇÃO DE PERSONAGENS
    public void SwitchingIcon(int indexImgIcon)
    {
        characterIcon["characterIcon"] = indexImgIcon;
        PhotonNetwork.LocalPlayer.SetCustomProperties(characterIcon) ;
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

