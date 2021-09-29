using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public GameObject characterPrefab;
    public string characterName, frase, Descricao;
    public int characterIndex, damage, ammo;
    public int HP;
    public Sprite characterIcon;
    public Sprite[] ordenedHabillityIcon;
    public string[] ordenedHabillityName, ordenedHabillityDescription;
    public AudioClip selectCharacter;
    public AudioClip gameStarted;
    public AudioClip shoot;
    public AudioClip firstBlood;
    public AudioClip ultimate;

}
