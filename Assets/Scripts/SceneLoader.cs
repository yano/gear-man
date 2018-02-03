using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	void Start () {
		
	}
	
    public void LoadScene01()
    {
        SceneManager.LoadScene("scene01");
    }

}
