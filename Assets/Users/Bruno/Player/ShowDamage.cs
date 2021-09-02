using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDamage : MonoBehaviour
{
    public float time;
    public float speed;
    private string value;
    private List<GameObject> damages;
    public GameObject damagePrefab;
    public int amountDamage;
    public string Value
    {
        get
        {
            return value;
        }
        set
        {
            bool check = false;
            for (int i = 0; i < damages.Count; i++)
            {
                if (!damages[i].gameObject.activeInHierarchy)
                {
                    damages[i].SetActive(true);
                    damages[i].GetComponent<TextMeshPro>().text = "-"+ value.ToString();
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                var instance = Instantiate(damagePrefab);
                instance.GetComponent<TextMeshPro>().text = "-" + value.ToString();
                instance.transform.SetParent(transform);
                damages.Add(instance);
            }
        }
    }
    void Start()
    {
        damages = new List<GameObject>();
        while (damages.Count < amountDamage)
        {
            var instance = Instantiate(damagePrefab);
            instance.transform.SetParent(transform);
            instance.SetActive(false);
            damages.Add(instance);
        }
    }

    private void Update()
    {
        foreach (var e in damages)
            e.GetComponent<DamageText>().Finish();
    }
}
