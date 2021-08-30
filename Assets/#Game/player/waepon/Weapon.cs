using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ManagerBullet))]
[RequireComponent(typeof(Pool))]
public class Weapon : CombatControl
{
    private ManagerBullet mangerBullet;
    private Pool bulletPool;
    private RaycastHit hit;
    public float maxBulletDistance;
    private float timeCount;
    public Image cross;
    public Camera cam;

    private Vector3 mark;
    private PhotonView pv;

    private void Awake()
    {
        mangerBullet = GetComponent<ManagerBullet>();
        this.bulletPool = GetComponent<Pool>();
        pv = GetComponentInParent<PhotonView>();
    }
    private void Start()
    {
        if (!pv.IsMine)
        {
            cam.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (!pv.IsMine)
        {
            return;
        }

        int mask = LayerMask.GetMask(LayerMask.LayerToName(transform.parent.gameObject.layer), "CircleArea");

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition).origin, cam.ScreenPointToRay(Input.mousePosition).direction, out hit, maxBulletDistance, ~mask))
        {
            Debug.Log("cagou para a porra da mascara " + LayerMask.LayerToName(transform.parent.gameObject.layer));
            Debug.DrawLine(cam.transform.position, hit.point, Color.red);
            mark = hit.point;
        }
        else
        {
            var origin = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)).origin;
            mark = (cam.transform.forward * maxBulletDistance) + origin;
            Debug.DrawLine(mangerBullet.transform.position, mark, Color.yellow);
        }
        #region test
        if (Physics.Raycast(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.forward, out hit, maxBulletDistance, ~mask))
        {
            Debug.Log("enfiou a porra da mascara no cu " + LayerMask.LayerToName(transform.parent.gameObject.layer));

            Debug.DrawLine(mangerBullet.bulletTransform.position, hit.point, Color.green);
        }
        #endregion
        this.timeCount -= Time.deltaTime;
        Aim();
    }
    [PunRPC]
    public override void Use()
    {
        if (!photonView.IsMine)
            return;
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


        var bullet = PhotonView.Find(GetComponent<Pool>().ActiveInstance()).gameObject.GetComponent<Bullet>();

        float distance = Vector3.Distance(mangerBullet.bulletTransform.position, hit.point);

        Vector3 pos = mangerBullet.bulletTransform.position;
        Vector3 rot = mangerBullet.bulletTransform.rotation.eulerAngles;


        //Player[] playersTeamBlue;
        //Player[] playersTeamRed;

        //PhotonTeamsManager.Instance.TryGetTeamMembers("Blue", out playersTeamBlue);
        //PhotonTeamsManager.Instance.TryGetTeamMembers("Red", out playersTeamRed);

        //Color color = Color.white;
        //foreach (Player p in playersTeamBlue)
        //    if (GetComponent<PhotonView>().Controller.Equals(p))
        //        color = Color.blue;
        //foreach (Player p in playersTeamRed)
        //    if (GetComponent<PhotonView>().Controller.Equals(p))
        //        color = Color.red;


        if (!hit.collider)
            bullet.photonView.RPC("Inicialize", RpcTarget.All, mark, distance, pos, rot, bullet.photonView.ViewID/*, new Vector3(color.r,color.g,color.b)*/);
        else
        {
            PhotonView targetId = hit.collider.gameObject.GetComponent<PhotonView>();
            if (!targetId)
            {
                bullet.photonView.RPC("Inicialize", RpcTarget.All, mark, distance, pos, rot, bullet.photonView.ViewID/*, new Vector3(color.r,color.g,color.b)*/);
            }
            else
                bullet.photonView.RPC("Inicialize", RpcTarget.All, mark, distance, pos, rot, targetId.ViewID/*, new Vector3(color.r,color.g,color.b)*/);
        }
    }
    public override void Reload()
    {
        count = 0;
    }

    public override void Aim()
    {
        transform.LookAt(mark);
        //cross.rectTransform.position = Input.mousePosition;
    }
}
