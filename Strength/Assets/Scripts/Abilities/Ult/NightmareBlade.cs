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

    private List<GameObject> swords;
    private List<Vector2> nonagramPositions;
    private float timer = 0;
    private bool ulting = false;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        float angle = (Random.value * Mathf.PI * 2) / 9;

        for (int i = 0; i < 9; i++)
        {
            float currentAngle = angle * i;
            nonagramPositions.Add(new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)) * ultRadius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ulting)
        {
            timer += Time.deltaTime;
            index = Mathf.FloorToInt(ultLength / timer);

            float sectionPercent = (timer - (index * (ultLength / 3))) / (timer - ((index + 1) * (ultLength / 3)));

            for (int i = 0; i < 9; i++)
            {
                Vector2 nextPosition = Vector2.zero;

                if (i + 3 > 9)
                {
                    nextPosition = nonagramPositions[i - 6];
                }
                else
                {
                    nextPosition = nonagramPositions[i + 3];
                }

                Vector2 targetPosition = nonagramPositions[i] + (nonagramPositions[i + 3] - nonagramPositions[i]);
                swords[i].transform.position = targetPosition;
            }

            if (timer >= ultLength)
            {
                ulting = false;

                for (int i = swords.Count - 1; i >= 0; i++)
                {
                    Destroy(swords[i]);
                    swords.Remove(swords[i]);
                }
            }
        }
    }

    public override void Activate(Entity entity)
    {
        base.Activate(entity);

        foreach (Vector2 pos in nonagramPositions)
        {
            swords.Add(Instantiate(swordPrefab, pos, Quaternion.identity, entity.transform));
        }
    }
}
