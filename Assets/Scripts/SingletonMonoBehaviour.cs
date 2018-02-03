using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogWarning(typeof(T) + "is nothing");
                }
            }

            return instance;
        }
    }

    protected void Awake()
    {
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (T)this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }

        Destroy(this);
        return false;
    }
}



//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class SimpleSingleton : MonoBehaviour
//{
//    private static SimpleSingleton mInstance;

//    public Text clearUIText;
//    public Button clearUIButton;
//    public Image clearUIButtonImage;
//    public Text clearUIButtonText;

//    public void ShowClearUI()
//    {
//        clearUIText.enabled = true;
//        clearUIButton.enabled = true;
//        clearUIButtonImage.enabled = true;
//        clearUIButtonText.enabled = true;
//    }

//    private SimpleSingleton()
//    {
//        Debug.Log("Create SampleSingleton GameObject instance.");
//    }

//    public static SimpleSingleton Instance
//    {
//        get
//        {
//            if (mInstance == null)
//            {
//                //GameObject go = new GameObject("SampleSingleton");
//                mInstance = //go.AddComponent<SimpleSingleton>();
//            }

//            return mInstance;
//        }
//    }

//    public bool finished = false;

//    void Start() { }

//    //void Update()
//    //{
//    //}
//}
