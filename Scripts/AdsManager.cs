using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
 
    public void ShowRewardedAd()
    {
        if(Advertisement.IsReady("rewardedVideo"));
        {
            var options = new ShowOptions{resultCallback = HandleShowResult};
            Debug.Log("Advert Called");
            Advertisement.Show("rewardedVideo", options);
        }
    }

    void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                UIManager.Instance.gems += 100;
                UIManager.Instance.UpdateGemCount();
                Debug.Log("Advert Watched");
                break;
            case ShowResult.Skipped:
                Debug.Log("Advert Skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Advert Failed");
                break;
        }
    }
}
