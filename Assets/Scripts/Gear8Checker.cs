using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gear8Checker : MonoBehaviour
{
    //public Transform collider1;
    //public Transform collider2;

    // 【条件A】レイキャストで時間内に①→②となっている（①→②の間のインターバルが一定時間以内の場合にOKフラグがたつ）
    // 【条件B】レイキャストで、時間内に①→②→①と必ず①または②が２回通過するようにする。
    public bool flagA = false;
    public bool flagB = false;

    // 条件A用
    float timeForA;

    // 条件B用
    public int count1 = 0;
    public int count2 = 0;
    //bool countable = false;

    //// http://kan-kikuchi.hatenablog.com/entry/RayCast2
    //// Checkerレイヤーとだけ衝突する
    //int layerMask = 1 << 8;

    void Start() { }

    //void Update()
    //{
    //    bool hit2 = Physics.Raycast(laycaster2.position, Vector3.up, 10, layerMask);
    //    bool hit1 = Physics.Raycast(laycaster1.position, Vector3.up, 10, layerMask);

    //    Debug.DrawRay(laycaster1.position, Vector3.up * 10, Color.red);
    //    Debug.DrawRay(laycaster2.position, Vector3.up * 10, Color.green);

    //    if (hit1)
    //    {
    //        Debug.Log("Hit1");

    //        // 条件A
    //        if (!isState1)
    //        {
    //            isState1 = true;
    //            if (Time.time - timeForA < 5.0f)
    //            {
    //                flagA = true;
    //            }
    //            timeForA = Time.time;
    //        }

    //        // 条件B
    //        if (countable)
    //        {
    //            count1++;
    //            countable = false;
    //        }
    //    }

    //    if (hit2)
    //    {
    //        Debug.Log("Hit2");

    //        // 条件A
    //        if (isState1)
    //        {
    //            isState1 = false;
    //            if (Time.time - timeForA < 5.0f)
    //            {
    //                flagA = true;
    //            }
    //            timeForA = Time.time;
    //        }

    //        // 条件B
    //        if (countable)
    //        {
    //            count2++;
    //            countable = false;
    //        }
    //    }

    //    if (!hit1 && !hit2)
    //    {
    //        // 条件B
    //        countable = true;
    //    }

    //    // 条件A
    //    if (Time.time - timeForA > 5.0f)
    //        flagA = false;

    //    if (count1 >= 2 || count2 >= 2)
    //        flagB = true;
    //}

    public bool IsOK
    {
        get
        {
            return flagA && flagB;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter : " + other.gameObject.name);
        //Destroy(other.gameObject);

        bool hit1 = false;
        bool hit2 = false;

        if (other.gameObject.name == "collider1")
            hit1 = true;
        if (other.gameObject.name == "collider2")
            hit2 = true;

        if (hit1)
        {
            //Debug.Log("Hit1");

            // 条件A
            if (Time.time - timeForA < 15.0f)
            {
                flagA = true;
            }
            timeForA = Time.time;

            // 条件B
            //if (countable)
            //{
                count1++;
            //countable = false;
            //}
        }

        if (hit2)
        {
            //Debug.Log("Hit2");

            // 条件A
            if (Time.time - timeForA < 15.0f)
            {
                flagA = true;
            }
            timeForA = Time.time;

            // 条件B
            //if (countable)
            //{
                count2++;
            //    countable = false;
            //}
        }

        //if (!hit1 && !hit2)
        //{
        //    // 条件B
        //    countable = true;
        //}

        // 条件A
        if (Time.time - timeForA > 15.0f)
            flagA = false;

        if (count1 >= 2 || count2 >= 2)
            flagB = true;
    }
}
