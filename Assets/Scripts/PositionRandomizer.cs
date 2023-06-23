using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    private float period = 0f;
    private Vector3[] positions = new Vector3[8];
    // Start is called before the first frame update
    void Start()
    {
        positions[0] = new Vector3(12.5f, 4.5f);
        positions[1] = new Vector3(12.5f, -6.5f);
        positions[2] = new Vector3(-12.5f, 4.5f);
        positions[3] = new Vector3(-12.5f, -6.5f);
        positions[4] = new Vector3(-7.5f, -0.5f);
        positions[5] = new Vector3(-6.5f, -2.5f);
        positions[6] = new Vector3(7.5f, -0.5f);
        positions[7] = new Vector3(6.5f, -2.5f);

        transform.position = positions[Mathf.RoundToInt(Random.Range(0, 7) + 1)];
    }

    // Update is called once per frame
    void Update()
    {
        if (period > 0.5f) {//If 1 second passed, find a new path
            transform.position = positions[Mathf.RoundToInt(Random.Range(0, 7) + 1)];
            period = 0f;//Reset period
        }
        period += Time.deltaTime;
    }
}
