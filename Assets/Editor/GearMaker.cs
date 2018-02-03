using UnityEngine;
using UnityEditor;
using System.Collections;

public class GearMaker : EditorWindow
{
    int toothNum = 0;

    [MenuItem("GameObject/Gear")]
    public static void makeGear()
    {
        GearMaker.GetWindow(typeof(GearMaker), false, "Gear");

        return;
    }

    void OnGUI()
    {
        Event e = Event.current;

        bool keyPress = false;
        switch (e.type)
        {
            case EventType.KeyDown:
                switch (e.keyCode)
                {
                    case KeyCode.Return:
                        keyPress = true;
                        break;
                }
                break;
        }

        int num = this.toothNum;
        EditorGUILayout.BeginHorizontal();
        {
            num = EditorGUILayout.IntField("歯数", num);

            if (num < 0)
                num = this.toothNum = 0;
            else
                this.toothNum = num;

        }
        EditorGUILayout.EndHorizontal();

        if (num > 4)
        {
            if (GUILayout.Button("作成") | keyPress)
            {
                this.makeGear(num);

                this.Close();
            }
        }

        return;
    }

    void makeGear(int num)
    {
        float angle = 360.0f / (float)num;

        float rad = (360.0f / (float)(num * 2)) * Mathf.Deg2Rad;
        float leng = 0.5f / Mathf.Tan(rad / 2.0f);
        float pos = leng / 2.0f;

        GameObject parent = new GameObject("Gear" + num);
        Transform pTrans = parent.transform;

        HingeJoint hj = parent.AddComponent<HingeJoint>();
        hj.axis = Vector3.up;

        JointMotor jm = hj.motor;
        jm.targetVelocity = 100.0f;
        jm.force = 100.0f;
        hj.motor = jm;

        Rigidbody rBody = parent.GetComponent<Rigidbody>(); //parent.rigidbody;
        rBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        GameObject obj, edge;
        Transform trans;
        Vector3 scale = new Vector3(leng, 1.0f, 1.0f);
        Vector3 eScale = new Vector3(1.0f, 0.5f, 1.0f);
        Vector3 ePos = new Vector3(leng / 2.0f, 0.0f, 0.0f);
        for (int i = 0; i < num; ++i)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            trans = obj.transform;
            trans.localScale = scale;

            edge = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            edge.transform.localScale = eScale;
            edge.transform.position = ePos;

            edge.transform.parent = trans;

            if (i != 0)
                trans.Rotate(0, angle * (float)i, 0);
            trans.Translate(pos, 0.0f, 0.0f);

            trans.parent = pTrans;
        }

        return;
    }
}
