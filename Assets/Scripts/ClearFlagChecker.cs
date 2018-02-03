using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearFlagChecker : MonoBehaviour
{
    public Renderer rend;

    bool onFlag = false;
    float checkTimeInterval = 5.0f;

    float checkTime1;
    float checkTime2;

    public List<GameObject> renketuGears;

    // http://kan-kikuchi.hatenablog.com/entry/RayCast2
    // Gearレイヤーとだけ衝突する
    int layerMask = 1 << 9;

    Vector3[] vecs;

    public GameObject[] gears;// = GameObject.FindGameObjectsWithTag("gear");


    void Start()
    {
        //checkTime = checkTimeInterval;

        renketuGears = new List<GameObject>();

        Vector3[] vecsTmp = {
            new Vector3( 0.2f, 0, 0),
            new Vector3( 0.0f, 0, 0.2f),
            new Vector3(-0.2f, 0, 0),
            new Vector3( 0.0f, 0, -0.2f),
        };


        vecs = vecsTmp;
    }

    void FixedUpdate()
    {
        RaycastHit hitInfo = new RaycastHit();

        bool isHit = false;
        for (int i=0; i<vecs.Length; i++)
        {
            isHit = Physics.Raycast(transform.position + vecs[i], Vector3.up, out hitInfo, 10, layerMask);

            if (isHit)
                break;
        }

        //if (Physics.Raycast(transform.position, Vector3.up, out hitInfo, 10, layerMask))
        if (isHit)
        {
            if (!onFlag)
            {
                onFlag = true;
                checkTime2 = Time.time;

                float checkTimeDiff = checkTime2 - checkTime1;

                if (hitInfo.transform != null)
                {
                    Debug.Log("ClearFlagChecker RaycastHit!! : " + hitInfo.transform.name);

                    if (checkTimeDiff < checkTimeInterval)// && checkTimeDiff > 1.0f)
                    {
                        Gear8Checker g8c = null; // = hitInfo.transform.parent.GetComponent<Gear8Checker>();

                        foreach (Transform tmp in hitInfo.transform)
                        {
                            if (tmp.name == "Detector")
                                g8c = tmp.GetComponent<Gear8Checker>();
                        }

                        if (g8c.IsOK)
                        {
                            if (!SimpleSingleton.Instance.finished)
                            {
                                rend.material.color = Color.green;

                                // スコアの保存
                                int renketu_num = calcRenketuNum(hitInfo.transform.gameObject);
                                SimpleSingleton.Instance.clearScore = renketu_num;

                                // UIの表示
                                SimpleSingleton.Instance.ShowClearUI(renketu_num);
                                SimpleSingleton.Instance.finished = true;
                            }
                        }
                    }
                }

                checkTime1 = checkTime2;
            }
        }
        else // ヒットなし
        {
            onFlag = false;
        }
    }

    public int calcRenketuNum(GameObject root_child)
    {
        //GameObject gear_start = GameObject.Find("Gear8Start");//FindGameObjectWithTag("gearstart");

        GameObject root = root_child.transform.parent.gameObject;

        //GameObject[] gears = GameObject.FindGameObjectsWithTag("gear");
        gears = GameObject.FindGameObjectsWithTag("gear");

        renketuGears.Clear();
        renketuGears.Add(root); //gear_start

        GameObject currentNode = root; //gear_start

        bool continue_operate = true;

        while (continue_operate)
        {
            continue_operate = false;

            int i = 0;
            foreach (GameObject gear in gears)
            {
                Debug.Log(i);
                i++;

                bool alreadyInclude = false;

                foreach (GameObject obj in renketuGears)
                {
                    if (GameObject.ReferenceEquals(gear, obj)) //(gear == obj)
                        alreadyInclude = true;
                }

                if (!alreadyInclude)
                {
                    float dist = Vector3.Distance(gear.transform.position, currentNode.transform.position);

                    if (dist < 6.20f)
                    {
                        Debug.Log(gear.name);

                        //if (gear.name == "Gear8base")
                        if (gear.name.Contains("Gear8base"))
                        {
                            ChildComponetGetter cpg = gear.GetComponent<ChildComponetGetter>();

                            if (cpg.gear8checker.flagA)
                            {
                                renketuGears.Add(gear);
                                currentNode = gear;
                                continue_operate = true;
                            }
                        }
                        else // Gear8start
                        {
                            renketuGears.Add(gear);
                            currentNode = gear;
                        }
                    }
                }
            }

        }

        return renketuGears.Count;
    }
}
