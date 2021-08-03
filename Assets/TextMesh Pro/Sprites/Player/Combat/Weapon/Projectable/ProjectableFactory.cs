using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectableFactory : MonoBehaviour
{
    public Mesh bullet;
    public Material material;
    public Vector3 size;
    public float speed;
    public new string name;
    public float bulletTimeout;
    public GameObject vfxPrefab;
    public GameObject speel;
    public string animationName;

    public GameObject BulletFactory(Pool pool)
    {
        GameObject go = new GameObject();

        go.name = name;

        go.AddComponent<Bullet>();
        go.GetComponent<Bullet>().speed = speed;
        go.GetComponent<Bullet>().effect = speel.GetComponent<IEffect>();
        go.GetComponent<Bullet>().existenceTomeout = bulletTimeout;
        go.GetComponent<Bullet>().pool = pool;
        go.GetComponent<Bullet>().animationName = animationName;

        BulletEffect(go.transform);
        BulletBody(go.transform);

        for (int i = 0; i < go.transform.childCount; i++)
        {
            go.transform.GetChild(i).gameObject.SetActive(false);
        }
        go.SetActive(false);
        return go;
    }
    private GameObject BulletBody(Transform parent)
    {
        GameObject mesh = new GameObject();

        mesh.name = "mesh_" + name;

        mesh.AddComponent<MeshFilter>();
        mesh.AddComponent<MeshRenderer>();
        mesh.GetComponent<MeshRenderer>().material = material;
        mesh.GetComponent<MeshFilter>().mesh = this.bullet;
        mesh.GetComponent<Transform>().localScale = this.size;
        mesh.GetComponent<Transform>().parent = parent;
        return mesh;
    }

    //esse efeito tem que está junto da magia
    private GameObject BulletEffect(Transform parent)
    {
        GameObject vfx = Instantiate(vfxPrefab);
        vfx.transform.parent = parent;
        vfx.transform.position = Vector3.zero;
        return vfx;
    }
}
