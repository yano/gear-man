using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildComponetGetter : MonoBehaviour {

    public Gear8Checker gear8checker;

	void Start () {
        gear8checker = GetComponentInChildren<Gear8Checker>();
    }
	
}
