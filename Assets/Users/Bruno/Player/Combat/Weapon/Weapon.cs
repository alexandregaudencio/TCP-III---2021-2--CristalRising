#define test
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MangerBullet))]
[RequireComponent(typeof(Pool))]
public class Weapon : CombatControl
{
    private MangerBullet mangerBullet;
    private Pool bulletPool;
    private RaycastHit hit;
    public float maxBulletDistance;
    public GameObject speels;
    private float timeCount;
    public Image cross;

    private Vector3 mark;

    private void Awake()
    {
        mangerBullet = GetComponent<MangerBullet>();
        this.bulletPool = GetComponent<Pool>();
    }
    private void Start()
    {

    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction, out hit, maxBulletDistance))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            mark = hit.point;
        }
        else
        {
            mark = Camera.main.ScreenPointToRay(Input.mousePosition).direction * maxBulletDistance;
            Debug.DrawLine(transform.position, mark);
        }
        if (Physics.Raycast(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.forward, out hit, maxBulletDistance))
        {
            Debug.DrawLine(mangerBullet.bulletTransform.position, hit.point, Color.green);
            //mark = hit.point;
        }
    }
    private void Update()
    {
        this.timeCount -= Time.deltaTime;
        Aim();
    }
    public override void Use()
    {
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

        //coloca magia na bala
        go.GetComponent<Bullet>().Inject(speels);
        go.GetComponent<Bullet>().hit = hit;

        //seta a posição da bala
        go.transform.SetPositionAndRotation(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.rotation);

    }
    public override void Reload()
    {
        count = 0;
    }

    public override void Aim()
    {
        transform.LookAt(mark);
        cross.rectTransform.position = Input.mousePosition;
    }
}
