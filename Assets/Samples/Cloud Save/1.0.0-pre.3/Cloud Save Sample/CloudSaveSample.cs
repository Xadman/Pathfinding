﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using Com.KevinNipper.Pathfinding;
using UnityEngine.SceneManagement;

namespace CloudSaveSample 
{

    public class CloudSaveSample : MonoBehaviour
    {
        private static CloudSaveSample instance;

        public static CloudSaveSample Instance {  get { return instance; } }


        public string playerId;
        private Player player;
        private async void Awake()
        {

            if(instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);  

            player = Player.Instance;
            // Cloud Save needs to be initialized along with the other Unity Services that
            // it depends on (namely, Authentication), and then the user must sign in.
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync();

            }
            if (AuthenticationService.Instance.IsSignedIn)
            {
                //Continue to Login/Load Player Data
            }
            else
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Debug.Log("Signed in?");
            playerId = AuthenticationService.Instance.PlayerId;


            // Retrieve saved player data
            SavePlayerData incomingSample = await RetrieveSpecificData<SavePlayerData>("object_key");
            Debug.Log($"Loaded sample object: {incomingSample.level}, {incomingSample.coins}, {incomingSample.xpPoints}");


            if (incomingSample != null)
                player.LoadPlayerData(incomingSample); // loads cloud player data.
            {
                try
                {       // loads local player data.
                    player.LoadPlayerData(SaveSystem.LoadPlayer());
                }
                catch
                { 
                    //creates new player data.
                    SavePlayerData data = new SavePlayerData(playerId);
                    player.LoadPlayerData(data);
                }
                }
            Login();
        }
        private void Login()
        {
            SceneManager.LoadScene(0);
        }
        private void LoadSignInLevel()
        {
            SceneManager.LoadScene(2);
        }

        private void logout()
        {
            SaveLogOut();

            LoadSignInLevel();
        }

        public async void SaveCloudData()
        {
            if (AuthenticationService.Instance.IsSignedIn)
            {
                SavePlayerData data = new SavePlayerData(player);
                await ForceSaveObjectData(playerId, data);
            }
        }
            public void SaveLogOut()
        {
            SaveCloudData();

            if (AuthenticationService.Instance.IsSignedIn)
            {
                AuthenticationService.Instance.SignOut();
            }

            player.ResetPlayerData();
        }
          
        private async Task ListAllKeys()
        {
            try
            {
                var keys = await SaveData.RetrieveAllKeysAsync();

                Debug.Log($"Keys count: {keys.Count}\n" + 
                          $"Keys: {String.Join(", ", keys)}");
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }
        }

        private async Task ForceSaveSingleData(string key, string value)
        {
            try
            {
                Dictionary<string, object> oneElement = new Dictionary<string, object>();

                // It's a text input field, but let's see if you actually entered a number.
                if (Int32.TryParse(value, out int wholeNumber))
                {
                    oneElement.Add(key, wholeNumber);
                }
                else if (Single.TryParse(value, out float fractionalNumber))
                {
                    oneElement.Add(key, fractionalNumber);
                }
                else
                {
                    oneElement.Add(key, value);
                }

                await SaveData.ForceSaveAsync(oneElement);

                Debug.Log($"Successfully saved {key}:{value}");
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }
        }

                private async Task ForceSaveObjectData(string key,SavePlayerData value)
              {
            try
            {
                // Although we are only saving a single value here, you can save multiple keys
                // and values in a single batch.
                Dictionary<string, object> oneElement = new Dictionary<string, object>
                {
                    { key, value }
                };

                await SaveData.ForceSaveAsync(oneElement);

                Debug.Log($"Successfully saved {key}:{value}");
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }
        }

        private async Task<T> RetrieveSpecificData<T>(string key)
        {
            try
            {
                var results = await SaveData.LoadAsync(new HashSet<string>{key});
                
                if (results.TryGetValue(key, out string value))
                {
                    return JsonUtility.FromJson<T>(value);
                }
                else
                {
                    Debug.Log($"There is no such key as {key}!");
                }
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }

            return default;
        }

        private async Task RetrieveEverything()
        {
            try
            {
                // If you wish to load only a subset of keys rather than everything, you
                // can call a method LoadAsync and pass a HashSet of keys into it.
                var results = await SaveData.LoadAllAsync();

                Debug.Log($"Elements loaded!");
                
                foreach (var element in results)
                {
                    Debug.Log($"Key: {element.Key}, Value: {element.Value}");
                }
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }
        }

        private async Task ForceDeleteSpecificData(string key)
        {
            try
            {
                await SaveData.ForceDeleteAsync(key);

                Debug.Log($"Successfully deleted {key}");
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }
        }
    }
}