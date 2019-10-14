using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonagramBlade : Projectile
{
    [HideInInspector]
    public Vector2[] points;
    private int index = 0;
    public float segmentTime;
    private float timer = 0;
    public Transform parent;

    protected override void Update()
    {
        timer += Time.deltaTime;
        int target = index + 1;

        if(target > points.Length)
        {
            target -= points.Length;
        }

        Vector2 direction = points[target] - points[index];
        transform.position = (Vector2)parent.position + points[index] + direction * (timer / segmentTime);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if(timer >= segmentTime)
        {
            timer = 0;
        }
    }
}
