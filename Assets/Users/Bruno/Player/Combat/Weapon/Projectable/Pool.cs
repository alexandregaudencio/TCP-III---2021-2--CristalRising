using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Pool : MonoBehaviourPun
{
    private List<GameObject> activeGroup;
    private List<GameObject> inactiveGroup;
    public GameObject projectable;
    public int count;
    private GameObject factory;
    private void Awake()
    {
        if (photonView.IsMine)
        {
            factory = PhotonNetwork.Instantiate(Path.Combine("Projectable", projectable.name), Vector3.zero, Quaternion.identity, 0);
        }
    }
    void Start()
    {
        activeGroup = new List<GameObject>();
        inactiveGroup = new List<GameObject>();

        if (photonView.IsMine)
        {
            photonView.RPC("AddMoreElement", RpcTarget.All);
        }
    }
    [PunRPC]
    private void AddMoreElement()
    {
        for (int i = 0; i < count; i++)
        {
            inactiveGroup.Add(factory.GetComponent<ProjectableFactory>().BulletFactory(this));
        }
    }
    private bool HalfEmpty()
    {
        if (inactiveGroup.Count > 1)
        {
            return true;
        }
        return false;
    }

    internal void SetEllement(GameObject go)
    {
        inactiveGroup.Add(go);
        activeGroup.Remove(go);
    }

    public GameObject GetEllement()
    {
        if (!photonView.IsMine)
            return null;
        GameObject go;
        if (!HalfEmpty())
        {
            photonView.RPC("AddMoreElement", RpcTarget.All);
        }
        go = inactiveGroup[0];
        inactiveGroup.Remove(go);
        activeGroup.Add(go);
        return go;
    }
}
