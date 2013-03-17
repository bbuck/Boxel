using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mode1Controller : BasePlayerController 
{
    public float fireCooldown = 1f;

    private float nextFire = 0f;
    private Transform fireUp, fireDown, fireLeft, fireRight;

    protected override void OnStart()
    {
        base.OnStart();
        fireUp = transform.Find("fire_up");
        fireDown = transform.Find("fire_down");
        fireLeft = transform.Find("fire_left");
        fireRight = transform.Find("fire_right");
    }

    protected override void CheckControls()
    {
        Motor.targetVelocity.x = Input.GetAxis("Horizontal") * speed;
        Motor.targetVelocity.z = Input.GetAxis("Vertical") * speed;

        CheckBounds();
    }

    void CheckBounds()
    {
        Vector3 topRight = Camera.main.WorldToViewportPoint(transform.position + collider.bounds.extents);
        Vector3 bottomLeft = Camera.main.WorldToViewportPoint(transform.position - collider.bounds.extents);

        if ((topRight.x > 1 && Motor.targetVelocity.x > 0) 
          || (bottomLeft.x < 0 && Motor.targetVelocity.x < 0))
            Motor.targetVelocity.x = 0;
        if ((topRight.y > 1 && Motor.targetVelocity.z > 0)
          || (bottomLeft.y < 0 && Motor.targetVelocity.z < 0))
            Motor.targetVelocity.z = 0;

        if (Time.time > nextFire)
        {
            bool fired = false;
            Quaternion rotation = Quaternion.identity;
            Vector3 position = Vector3.zero;
            Vector3 fireDirection = Vector3.zero;
            float fireHoriz = Input.GetAxis("FireHoriz");
            float fireVert = Input.GetAxis("FireVert");
            if (fireVert > 0)
            {
                fired = true;
                rotation = Quaternion.Euler(90, 0, 0);
                position = fireUp.position;
                fireDirection = Vector3.forward;
            }
            if (fireHoriz < 0)
            {
                fired = true;
                rotation = Quaternion.Euler(90, -90, 0);
                position = fireLeft.position;
                fireDirection = Vector3.left;
            }
            if (fireVert < 0)
            {
                fired = true;
                rotation = Quaternion.Euler(-90, 0, 0);
                position = fireDown.position;
                fireDirection = Vector3.back;
            }
            if (fireHoriz > 0)
            {
                fired = true;
                rotation = Quaternion.Euler(90, 90, 0);
                position = fireRight.position;
                fireDirection = Vector3.right;
            }

            if (fired)
            {
                nextFire = Time.time + fireCooldown;
                GameObject bulletObject = PrefabManager.Instance.CreateInstance("Bullet", position, rotation);
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                bullet.direction = fireDirection;
                bullet.Fire();
            }
        }
    }
}