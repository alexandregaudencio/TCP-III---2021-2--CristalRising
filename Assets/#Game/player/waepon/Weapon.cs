using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ManagerBullet))]
[RequireComponent(typeof(Pool))]
public class Weapon : CombatControl
{
    [SerializeField] GameObject setUpGameplay;
    private ManagerBullet mangerBullet;
    private Pool bulletPool;
    private RaycastHit hit;
    public float maxBulletDistance;
    private float timeCount;
    public Image cross;
    public GameObject cam;

    private Vector3 mark;
    private PhotonView pv;

    private int maxAmmo;
    private int ammo;

    public float temporizadorRecarga;
    public bool recarregando;

    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public int Ammo { get => ammo; set => ammo = value; }

    private void Awake()
    {
        mangerBullet = GetComponent<ManagerBullet>();
        this.bulletPool = GetComponent<Pool>();
        pv = GetComponentInParent<PhotonView>();
        setAmmo();
    }
    private void Start()
    {
        if (pv.IsMine)
        {
            Camera.main.transform.parent = cam.transform;
            Camera.main.transform.SetPositionAndRotation(cam.transform.position, cam.transform.rotation);
        }
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.R) && !recarregando) recarregando = true;


        if (ammo <= 0 && Input.GetMouseButtonDown(0) && !recarregando)
        {
            recarregando = true;
        }

        if (recarregando == true)
        {
            temporizadorRecarga += Time.deltaTime;

            if (temporizadorRecarga >= 2.5f)
            {
                Reload();
            }
        }

        if (!pv.IsMine)
        {
            return;
        }

        int mask = LayerMask.GetMask(LayerMask.LayerToName(transform.parent.gameObject.layer), "weaponIgnore");

        if (Physics.Raycast(cam.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition).origin, cam.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition).direction, out hit, maxBulletDistance, ~mask))
        {
            Debug.DrawLine(cam.transform.position, hit.point, Color.red);
            mark = hit.point;
        }
        else
        {
            var origin = cam.GetComponentInChildren<Camera>().ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)).origin;
            mark = (cam.transform.forward * maxBulletDistance) + origin;
            Debug.DrawLine(mangerBullet.transform.position, mark, Color.yellow);
        }
        #region test
        if (Physics.Raycast(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.forward, out hit, maxBulletDistance, ~mask))
        {
            Debug.DrawLine(mangerBullet.bulletTransform.position, hit.point, Color.green);
        }
        #endregion
        this.timeCount -= Time.deltaTime;
        Aim();
    }

    [PunRPC]
    public override void Use()
    {
        if (!pv.IsMine)
        {
            return;
        }
        if (this.count >= this.Limit)
        {
            return;
        }
        if (this.timeCount <= 0.001f)
        {
            this.timeCount = this.Hertz;
        }
        else
        {
            return;
        }
        this.count++;

        ammo--;
        //audio tiro
        //int characterIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["characterIndex"];
        
        //audioGameplayController.instance.audioPlayerFire("fire", characterIndex);

        var bullet = PhotonView.Find(bulletPool.ActiveInstance()).gameObject.GetComponent<Bullet>();

        float distance = Vector3.Distance(mangerBullet.bulletTransform.position, hit.point);

        Vector3 pos = mangerBullet.bulletTransform.position;
        Vector3 rot = mangerBullet.bulletTransform.rotation.eulerAngles;

        //int indexPlayer = (int)PhotonNetwork.LocalPlayer.CustomProperties["indexPlayer"];
        //string name = PhotonNetwork.LocalPlayer.NickName;
        if (!hit.collider)
            bullet.photonView.RPC("Inicialize", RpcTarget.All, mark, distance, pos, rot, bullet.photonView.ViewID /*name*//*, new Vector3(color.r,color.g,color.b)*/);
        else
        {
            PhotonView targetId = hit.collider.gameObject.GetComponent<PhotonView>();
            if (!targetId)
            {
                bullet.photonView.RPC("Inicialize", RpcTarget.All, mark, distance, pos, rot, bullet.photonView.ViewID /*name*//*, new Vector3(color.r,color.g,color.b)*/);
            }
            else
                bullet.photonView.RPC("Inicialize", RpcTarget.All, mark, distance, pos, rot, targetId.ViewID /*name*//*, new Vector3(color.r,color.g,color.b)*/);

        }
    }
    public override void Reload()
    {
        count = 0;
        ammo = MaxAmmo;
        recarregando = false;
        temporizadorRecarga = 0.000f;

    }

    public override void Aim()
    {
        transform.LookAt(mark);
        //cross.rectTransform.position = Input.mousePosition;
    }

    public void setAmmo()
    {
        int characterIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["characterIndex"];
        ammo = RoomConfigs.instance.charactersOrdered[characterIndex].ammo;
        MaxAmmo = RoomConfigs.instance.charactersOrdered[characterIndex].ammo;
    }

}
