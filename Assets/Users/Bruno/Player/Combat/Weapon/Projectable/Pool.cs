using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Pool : MonoBehaviourPun
{
    public List<GameObject> activeGroup;
    public List<GameObject> inactiveGroup;

    public GameObject projectable;
    public int count;
    private GameObject factory = null;
    public GameObject selected;
    [PunRPC]
    private void OpenFactory(int id)
    {
        factory = PhotonView.Find(id).gameObject;
    }
    void Start()
    {
        activeGroup = new List<GameObject>();
        inactiveGroup = new List<GameObject>();

        if (photonView.IsMine)
        {
            var obj = PhotonNetwork.Instantiate(Path.Combine("Projectable", projectable.name), Vector3.zero, Quaternion.identity, 0);
            photonView.RPC("OpenFactory", RpcTarget.All, obj.gameObject.GetComponent<PhotonView>().ViewID);

            photonView.RPC("AddMoreElement", RpcTarget.All);

        }

        //AddMoreElement();
    }

    [PunRPC]
    private void AddMoreElement()
    {
        var f = factory.GetComponent<ProjectableFactory>();
        for (int i = 0; i < count; i++)
        {
            int bulletId;
            int vfxId;

            if (photonView.IsMine)
            {
                bulletId = f.BulletFactory();
                vfxId = f.BulletEffect();

                GameObject bullet = PhotonView.Find(bulletId).gameObject; ;

                f.photonView.RPC("PhotonSetParent", RpcTarget.All, bulletId, vfxId);

                f.photonView.RPC("BulletSetUp", RpcTarget.All, bulletId, photonView.ViewID);

                bullet.GetComponent<Bullet>().photonView.RPC("ActiveAll", RpcTarget.All, false);

                photonView.RPC("add", RpcTarget.AllBufferedViaServer, bulletId);
            }
        }
    }
    [PunRPC]
    private void add(int id)
    {
        inactiveGroup.Add(PhotonView.Find(id).gameObject);
    }
    private bool HalfEmpty()
    {
        if (inactiveGroup.Count > 1)
        {
            return true;
        }
        return false;
    }

    [PunRPC]
    internal void SetEllement(int id)
    {
        inactiveGroup.Add(PhotonView.Find(id).gameObject);
        activeGroup.Remove(PhotonView.Find(id).gameObject);
    }
    [PunRPC]
    public void GetEllement()
    {
        if (!photonView.IsMine && photonView.IsMine)
            return;
        if (!HalfEmpty())
        {
            AddMoreElement();
        }
        selected = inactiveGroup[0];
        PhotonView.Find(photonView.ViewID).gameObject.GetComponent<Pool>().inactiveGroup.Remove(selected);
        PhotonView.Find(photonView.ViewID).gameObject.GetComponent<Pool>().activeGroup.Add(selected);
    }
}
