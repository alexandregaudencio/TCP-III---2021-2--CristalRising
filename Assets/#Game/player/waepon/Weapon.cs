#define test
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (!pv.IsMine || !cam)
        {
            return;
        }
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition).origin, cam.ScreenPointToRay(Input.mousePosition).direction, out hit, maxBulletDistance))
        {
            Debug.DrawLine(cam.transform.position, hit.point, Color.red);
            mark = hit.point;
        }
        else
        {
            mark = cam.ScreenPointToRay(Input.mousePosition).direction * maxBulletDistance;
            Debug.DrawLine(mangerBullet.transform.position, mark);
        }
        if (Physics.Raycast(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.forward, out hit, maxBulletDistance))
        {
            Debug.DrawLine(mangerBullet.bulletTransform.position, hit.point, Color.green);
            //mark = hit.point;
        }
        this.timeCount -= Time.deltaTime;
        Aim();
    }
    [PunRPC]
    public override void Use()
    {
        if (!photonView.IsMine || !cam)
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

        bulletPool.photonView.RPC("GetEllement", RpcTarget.All);

        var bullet = PhotonView.Find(GetComponent<Pool>().selected).gameObject.GetComponent<Bullet>();
        bullet.photonView.RPC("Inicialize", RpcTarget.All);

        float distance = Vector3.Distance(mangerBullet.bulletTransform.position, hit.point);
        bullet.photonView.RPC("TimeOfArrival",RpcTarget.All,distance);
        photonView.RPC("prepareBullet", RpcTarget.All, GetComponent<Pool>().selected);
        Debug.Log("valor no tiro" + hit.point.ToString());
       
    }
    [PunRPC]
    private void prepareBullet(int id) {
        var bullet = PhotonView.Find(id).gameObject.GetComponent<Bullet>();
        bullet.hit = hit;
        Debug.Log(bullet.photonView.ViewID + bullet.hit.point.ToString() + "");
        //seta a posição da bala
        bullet.transform.SetPositionAndRotation(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.rotation);

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
