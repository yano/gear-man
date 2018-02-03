using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverChecker : MonoBehaviour
{
    public RectTransform gameOverUI;
    public List<Transform> laycasters;

    bool onFlag = false;
    float checkTimeInterval = 5.0f;
    List<float> checkTimes;
    bool isGameOver = false;

    // http://kan-kikuchi.hatenablog.com/entry/RayCast2
    // Gearレイヤーとだけ衝突する
    int layerMask = 1 << 9;

    void Start()
    {
        checkTimes = new List<float>();

        Debug.Log(laycasters.Count.ToString());

        for(int i=0; i<laycasters.Count; i++)
        {
            checkTimes.Add(0.0f);
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            // calc checkTime
            for (int i = 0; i < laycasters.Count; i++)
            {
                float deltaTimeTmp = Time.deltaTime;

                if (Physics.Raycast(laycasters[i].position, Vector3.up, 10, layerMask))
                {
                    if (!onFlag)
                    {
                        checkTimes[i] = 0.0f;
                        onFlag = true;
                    }

                    checkTimes[i] += deltaTimeTmp;
                }
                else
                {
                    onFlag = false;
                }
            }

            // check checkTime
            for (int i = 0; i < laycasters.Count; i++)
            {
                if (checkTimes[i] > checkTimeInterval)
                    isGameOver = true;
            }

            if (isGameOver)
            {
                if (!SimpleSingleton.Instance.finished)
                {
                    gameOverUI.gameObject.SetActive(true);
                    SimpleSingleton.Instance.finished = true;
                }
            }
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
