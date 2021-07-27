#define test
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MangerBullet))]
[RequireComponent(typeof(Pool))]
public class Weapon : CombatControl
{
    private Transform direction;
    private Transform target;
    private MangerBullet mangerBullet;
    private Pool bulletPool;
    private GameObject temp;
    private RaycastHit hit;
    public float maxBulletDistance;
    public GameObject speels;

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
#if test

        if (Physics.Raycast(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.forward, out hit, maxBulletDistance))
        {
            Debug.DrawLine(mangerBullet.bulletTransform.position, hit.point, Color.red);
            mark = hit.point;

        }
        else
        {
            Debug.DrawLine(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.position + mangerBullet.bulletTransform.forward * maxBulletDistance);
            mark = mangerBullet.bulletTransform.forward * 1000;
        }
#endif
    }
    public override void Use()
    {
        GameObject go;
        go = bulletPool.GetEllement();
        go.GetComponent<Bullet>().Inicialize();

        float distance = Vector3.Distance(mangerBullet.bulletTransform.position, mark);
        go.GetComponent<Bullet>().TimeOfArrival(distance);

        //coloca magia na bala
        go.GetComponent<Bullet>().Inject(speels);
        go.GetComponent<Bullet>().hit = hit;

        //seta a posição da bala
        go.transform.SetPositionAndRotation(mangerBullet.bulletTransform.position, mangerBullet.bulletTransform.rotation);

    }
}
