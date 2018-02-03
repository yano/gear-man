using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleSingleton : SingletonMonoBehaviour<SimpleSingleton>
{
    protected SimpleSingleton() { }

    private static SimpleSingleton mInstance;

    public RectTransform clearUI;
    public Text clearUIText;

    public int clearScore = -1;

    public ClickPositionCreatePrefabScript cpcps;

    //public Button clearUIButton;
    //public Image clearUIButtonImage;
    //public Text clearUIButtonText;

    public void ShowClearUI(int renketu_num)
    {
        clearUI.gameObject.SetActive(true);
        clearUIText.text += renketu_num.ToString();

        //clearUIText.text += renketu_num.ToString();
        //clearUIText.enabled = true;
        //clearUIButton.enabled = true;
        //clearUIButtonImage.enabled = true;
        //clearUIButtonText.enabled = true;
    } 

    public void ShowRanking()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(clearScore);
    }

    public void Tweet()
    {
        naichilab.UnityRoomTweet.Tweet("gearman", "連結数 " + clearScore.ToString() + " でギアマンの者をClear。" , "gearman", "unityroom");
    }

    public bool finished
    {
        get { return _finished; }
        set
        {
            _finished = value;

            if (_finished)
               cpcps.enabled = false;
        }
    }
      
    bool _finished = false;

    void Start() { }
}
