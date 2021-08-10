using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.IO;

public class ProjectableFactory : MonoBehaviourPun
{
    public GameObject bullet;
    public GameObject vfxPrefab;
    public GameObject speel;
    public String animationName;
    private List<GameObject> obj;

    public GameObject BulletFactory(Pool pool)
    {
        GameObject b = PhotonNetwork.Instantiate(Path.Combine("Projectable", bullet.name), Vector3.zero, Quaternion.identity);

        b.GetComponent<Bullet>().effect = speel.GetComponent<IEffect>();
        b.GetComponent<Bullet>().animationName = animationName;

        b.GetComponent<Bullet>().pool = pool;

        BulletEffect(b.transform);

        return b;
    }

    //esse efeito tem que está junto da magia
    public void BulletEffect(Transform parent)
    {
        var vfx = PhotonNetwork.Instantiate(Path.Combine("Projectable/Explosion/vfx", vfxPrefab.name), Vector3.zero, Quaternion.identity);
        vfx.transform.parent = parent;
    }
}
