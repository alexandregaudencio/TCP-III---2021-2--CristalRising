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
    public Transform pivot;

    private Vector3 mark;

    private void Awake()
    {
        mangerBullet = GetComponent<ManagerBullet>();
        this.bulletPool = GetComponent<Pool>();
    }
    private void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) {
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
    public override void Use()
    {
        if (!GetComponent<PhotonView>().IsMine)
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
        GameObject go;
        go = bulletPool.GetEllement();
        go.GetComponent<Bullet>().Inicialize();

        float distance = Vector3.Distance(mangerBullet.bulletTransform.position, hit.point);
        go.GetComponent<Bullet>().TimeOfArrival(distance);

        go.GetComponent<Bullet>().hit = hit;
        //seta a posição da bala
        go.transform.SetPositionAndRotation(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.rotation);

    }
    public override void Reload()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            return;
        }
        count = 0;
    }

    public override void Aim()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            return;
        }
        transform.LookAt(mark);
        //cross.rectTransform.position = Input.mousePosition;
    }
}
