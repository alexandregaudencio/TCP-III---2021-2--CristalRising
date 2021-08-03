using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Status : ScriptableObject
{
    public Image icon;
    public float duration;
    public virtual void Apply() { }
}