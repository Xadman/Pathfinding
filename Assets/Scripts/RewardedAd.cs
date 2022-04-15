using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

namespace Unity.Example
{
    public class RewardedAd : MonoBehaviour
    {
        IRewardedAd ad; 
        string androidAdUnitId = "Rewarded_Android";
        string androidGameId = "4699254";
        string iosAdUnitId = "Rewarded_iOS";
        string iosGameId = "4699255";
        string gameId, adUnitId;

        private bool adSetup;

        public void playAd()
        {
            if(UnityServices.State == ServicesInitializationState.Initialized)
            {
                if (adSetup)
                {
                    ad.Load();
                }
                else
                {
                    InitializationComplete();
                }
           
            }else if(UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                InitServices();
            }
        }

        public async void InitServices()
        {
            try
            { 
                // Instantiate an interstitial ad object with platform-specific Ad Unit ID
                if (Application.platform == RuntimePlatform.Android)
                {
                    InitializationOptions initializationOptions = new InitializationOptions();
                    gameId = androidGameId;
                    adUnitId = androidAdUnitId;
                    initializationOptions.SetGameId(gameId);
                    await UnityServices.InitializeAsync(initializationOptions);


                }

                else if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    InitializationOptions initializationOptions = new InitializationOptions();
                    gameId = iosGameId;
                    adUnitId = iosAdUnitId;
                    initializationOptions.SetGameId(gameId);
                    await UnityServices.InitializeAsync(initializationOptions);


                }
#if UNITY_EDITOR
                else
                {
                    await UnityServices.InitializeAsync();

                }
#endif
                InitializationComplete();
            }
            catch (Exception e)
            {
                InitializationFailed(e);
            }
        }

        public void SetupAd()
        {

            adSetup = true;

            //create
#if UNITY_EDITOR
            ad = MediationService.Instance.CreateRewardedAd("myExampleAdUnitId");

#else
            ad = MediationService.Instance.CreateRewardedAd(adUnitId);

#endif
            //Subscribe to events
            ad.OnLoaded += AdLoaded;
            ad.OnFailedLoad += AdFailedLoad;

            ad.OnShowed += AdShown;
            ad.OnFailedShow += AdFailedShow;
            ad.OnClosed += AdClosed;
            ad.OnClicked += AdClicked;
            ad.OnUserRewarded += UserRewarded;

            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
        }

        public void ShowAd()
        {
            if (ad.AdState == AdState.Loaded)
            {
                ad.Show();
            }
        }

        void InitializationComplete()
        {
            SetupAd();
            ad.Load();
        }

        void InitializationFailed(Exception e)
        {
            Debug.Log("Initialization Failed: " + e.Message);
        }

        void AdLoaded(object sender, EventArgs args)
        {
            Debug.Log("Ad loaded");
            ShowAd();
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs args)
        {
            Debug.Log("Failed to load ad");
            Debug.Log(args.Message);
        }

        void AdShown(object sender, EventArgs args)
        {
            Debug.Log("Ad shown!");
        }

        void AdClosed(object sender, EventArgs e)
        {
            // Pre-load the next ad
            
            Debug.Log("Ad has closed");
            // Execute logic after an ad has been closed.
        }

        void AdClicked(object sender, EventArgs e)
        {
            Debug.Log("Ad has been clicked");
            // Execute logic after an ad has been clicked.
        }

        void AdFailedShow(object sender, ShowErrorEventArgs args)
        {
            Debug.Log(args.Message);
        }

        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
        }

        void UserRewarded(object sender, RewardEventArgs e)
        {
            Debug.Log($"Received reward: type:{e.Type}; amount:{e.Amount}");
            Player.Instance.GrantCoins(10);
        }

    }
}
    
