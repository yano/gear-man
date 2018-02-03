using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFlagSpawner : MonoBehaviour
{
    public GameObject clearFlagPrefab;

    void Start()
    {
        int random_num = Random.Range(0, 100);

        int flag_num = 1;

        if (random_num < 5)
        {
            flag_num = 3;
        }
        else if (random_num < 35)
        {
            flag_num = 2;
        }

        for (int i = 0; i < flag_num; i++)
        {
            bool ok = false;

            Vector3 pos = Vector3.zero;

            while (!ok)
            {
                pos.x = Random.Range(-25.0f, 25.0f);
                pos.z = Random.Range(-18.0f, 18.0f);

                if (pos.x > -10.0f && pos.x < 10.0f && pos.z > -10.0f && pos.z < 10.0f)
                    ok = false;
                else
                    ok = true;
            }

            pos.y = -1.0f;
            //worldPos = new Vector3(worldPos.x, 1.0f, worldPos.z);

            Instantiate(clearFlagPrefab, pos, Quaternion.identity);
        }

    }
}

