using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nightmare Blade", menuName = "Abilities/Ult/NightmareBlade")]
public class NightmareBlade : Ult
{
    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private float ultLength;
    [SerializeField]
    private float ultRadius;

    private List<Vector2> nonagramPositions;
    private float timer = 0;
    private bool ulting = false;
    private int index = 0;

    public override void Activate(Entity entity)
    {
        base.Activate(entity);

        float angle = (Mathf.PI * 2) / 9;

        for (int i = 0; i < 9; i++)
        {
            float currentAngle = angle * i;
            nonagramPositions.Add(new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)) * ultRadius);
        }

        for (int i = 0; i < nonagramPositions.Count; i++)
        {
            NonagramBlade projectile = Instantiate(swordPrefab, entity.transform.position + (Vector3)nonagramPositions[i], Quaternion.identity, entity.transform).GetComponent<NonagramBlade>();
            int p1 = i;
            int p2 = i + 3;
            int p3 = i + 6;

            if(p2 >= 9)
            {
                p2 -= 9;
            }

            if (p3 >= 9)
            {
                p3 -= 9;
            }

            projectile.points = new Vector2[3] { nonagramPositions[p1], nonagramPositions[p2], nonagramPositions[p3] };
            projectile.segmentTime = ultLength / 3;
            projectile.parent = entity.transform;
            Destroy(projectile.gameObject, ultLength);
        }
    }
}
