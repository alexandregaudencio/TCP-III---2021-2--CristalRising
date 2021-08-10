using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PhotonVFX : MonoBehaviourPun
{
    //[HideInInspector]
    //public Transform parent;
    private void Start()
    {
        //photonView.RPC("ToRealte", RpcTarget.All);
        GetComponentInParent<Bullet>().ActiveAll(false);
    }

    [PunRPC]
    private void ToRealte(Transform parent) {
        transform.parent = parent;
    }
}
