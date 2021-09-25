using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Tug : Spells, IEffect
{
    private GameObject target;
    private RaycastHit hit;
    public float reach;
    public Transform relocateTarget;
    public float speed = .01f;
    private Vector3 zero, start, end;
    private bool pull;
    private LineRenderer line;
    private float time;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        time = Hertz;
    }
    public void Apply(Animator animatorId = null)
    {
        line.enabled = true;
        line.SetPosition(0, zero);
        line.SetPosition(1, start);
        line.SetPosition(2, end);
    }
    public override void Aim()
    {
        var origin = relocateTarget.position;
        var dir = GetComponentInParent<PlayerController>().cam.transform.forward;
        int mask = LayerMask.GetMask("Team1", "Team2"); 
        int weaponIgnore = LayerMask.GetMask("weaponIgnore");

        Debug.DrawRay(origin, dir * reach, Color.white);
        if (Physics.Raycast(origin, dir, out hit, reach, ~weaponIgnore))
        {
            Debug.DrawLine(origin, hit.point, Color.red);
            var player = hit.collider.GetComponentInParent<PlayerController>();
            if (player)
            {
                if (((1 << player.gameObject.layer) & mask) == (1 << player.gameObject.layer))
                    target = hit.collider.gameObject;
            }

        }
        else
        {
            target = null;
        }
    }
    [PunRPC]
    public override void Use()
    {
        Aim();
        if (time <= 0)
        {
            time = Hertz;

            foreach (var statu in status)
            {
                statu.Apply();
            }

            if (target)
            {
                zero = transform.parent.transform.position + Vector3.down / 2;
                start = relocateTarget.position;
                end = target.transform.position;

                Apply();

                pull = true;
            }
            else
            {
                zero = transform.parent.transform.position + Vector3.down / 2;
                start = relocateTarget.position;
                end = transform.position + GetComponentInParent<PlayerController>().cam.transform.forward * reach;

                pull = false;

                Apply();

                Invoke("Finhish", .5f);
            }
        }
    }
    private void Finhish()
    {
        line.enabled = false;
    }
    private void translateTo()
    {
        end = Vector3.Lerp(end, start, speed);

        line.SetPosition(2, end);
        target.transform.position = end;

        if (Vector3.Distance(start, end) < 0.1f)
        {
            pull = false;
            target = null;
            line.enabled = false;
        }
    }
    private void Update()
    {
        if (pull)
        {
            translateTo();
        }
        time -= Time.deltaTime;
    }
}
