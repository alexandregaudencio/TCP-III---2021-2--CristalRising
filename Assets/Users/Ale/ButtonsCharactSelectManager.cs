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
    //Pra dar bom, os parâmetros abaixo precisam estar na mesma ordem.
    
    [SerializeField] private Button[] characterSelectButtons;

    [SerializeField] private GameObject CharacterDefault;   

    [SerializeField] private GameObject cancelButton;

    private ExitGames.Client.Photon.Hashtable characterIcon = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        PhotonNetwork.LocalPlayer.TagObject = CharacterDefault.name;

        //for (int i = 0; i < charactersIcons.Length; i++)
        //{
        //    PrefabIconName.Add(, charactersIcons[i].name);
        //}
    }

    private void Update()
    {
        
    }

    //private void OnHeighLightButtonActive()
    //{
    //    foreach(Button buttons in characterSelectButtons)
    //    {
    //        //buttons.OnPointerEnter;
            
    //    }
    //}

    public void ChooseCharacter(GameObject characterPrefab)
    {
        PhotonNetwork.LocalPlayer.TagObject = characterPrefab.name;
        SwitchButtonsInteractable(false);

    }

    public void SwitchingIcon(int indexImgIcon)
    {
        characterIcon["characterIcon"] = indexImgIcon;
        PhotonNetwork.LocalPlayer.SetCustomProperties(characterIcon) ;
        //foreach (Button button in characterSelectButtons)
        //{
        //    //if (button.OnPointerEnter();
        //}
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

    //public Image chos(Image imageIcon)
    //{
    //    return (imageIcon);
        
    //}




}

