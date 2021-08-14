using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.IO;

public class ProjectableFactory : MonoBehaviourPunCallbacks
{
    public static ProjectableFactory instance;
    public GameObject bulletPrefab;
    public GameObject vfxPrefab;
    public GameObject speel;
    public String animationName;

    private void Awake()
    {
        instance = this;
    }
    public int BulletFactory()
    {
        GameObject b = PhotonNetwork.Instantiate(Path.Combine("Projectable", bulletPrefab.name), Vector3.zero, Quaternion.identity);
        return b.GetComponent<PhotonView>().ViewID;
    }
    [PunRPC]
    public void BulletSetUp(int bulletDd, int poolId)
    {
        var bullet = PhotonView.Find(bulletDd).gameObject;
        var pool = PhotonView.Find(poolId).gameObject;

        bullet.GetComponent<Bullet>().effect = speel.GetComponent<IEffect>();
        bullet.GetComponent<Bullet>().animationName = animationName;

        bullet.GetComponent<Bullet>().pool = pool.GetComponent<Pool>();
    }
    //esse efeito tem que está junto da magia
    public int BulletEffect()
    {
        var vfx = PhotonNetwork.Instantiate(Path.Combine("Projectable/Explosion/vfx", vfxPrefab.name), Vector3.zero, Quaternion.identity);
        return vfx.GetComponent<PhotonView>().ViewID;
    }
    [PunRPC]
    public void PhotonSetParent(int bulletId, int vfxID)
    {
        GameObject b = PhotonView.Find(bulletId).gameObject;
        GameObject v = PhotonView.Find(vfxID).gameObject;

        v.transform.SetParent(b.transform);
    }
}
