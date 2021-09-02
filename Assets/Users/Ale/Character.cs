using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    [SerializeField]
    GameObject characterPrefab;

    [SerializeField]
    string characterName, frase, Descricao;
    [SerializeField]
    int HP, damage, Ammo;
    [SerializeField]
    Sprite characterIcon;

    [SerializeField]
    Sprite[] ordenedHabillityIcon;
    [SerializeField]
    string[] ordenedHabillityName, ordenedHabillityDescription;



    

 

}
