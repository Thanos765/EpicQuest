using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System;
using Unity.VisualScripting;

namespace Inventory.Model{
public  class SaveController : MonoBehaviour
{
[Header("Debbuging")]
[SerializeField] private bool initializedataifnull = false;
[Header("File Storage Config ")]
[SerializeField] private string fileName;

[SerializeField] private bool useEncryption;
private FileDataHandler dataHandler;
        private string savedScene;

        private SaveData saveData;
   private List<ISaveController> ISaveControllerObjects;
   public static SaveController instance {get; private set;}

   
private void Awake(){

   if(instance !=null){
      Debug.Log("more than one SaveController in the scene.The newest one is destroyed");
      Destroy(this.gameObject);
      return;
   }
   
   instance=this;
   DontDestroyOnLoad(this.gameObject);
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }
   private void OnEnable()
   {
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadGame();

        }
    private void OnDisable()
    {

            SceneManager.sceneLoaded -= OnSceneLoaded;
          
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
            this.ISaveControllerObjects = FindAllISaveControllerObjects();
            if (scene.name == PlayerPrefs.GetString("SavedScene"))
            {
                if (this.saveData != null)
                {
                    LoadGame();
                }
                else
                {
                    NewGame();
                }
            }

    }



 public void NewGame(){
            Debug.Log("NewGame() called");
   this.saveData=new SaveData();
        }
    
public void LoadGame(){

            //load any saved data from a file
            try
            {
                this.saveData = dataHandler.Load();
            }
            catch (Exception e)
            {
                Debug.Log("An error occurred while loading data: " + e.Message);
            }
            if (this.saveData == null && initializedataifnull) 
   { 
        NewGame();      
   }

  // if no data can be loaded dont continue start a new game
            if(this.saveData==null)
            {
                 Debug.Log("No data found.A new game needs to be started before data can be loaded");  
                 return;
                 }
            if (ISaveControllerObjects != null)
            {
                foreach (ISaveController saveControllerObj in ISaveControllerObjects)
                {

                    saveControllerObj.LoadGameData(saveData);
                }
                
            }
        }


        public void SaveGame()
        {
            SaveData saveData = new SaveData();

            
            
            if (ISaveControllerObjects != null)
            {
                ISaveControllerObjects = ISaveControllerObjects.Where(obj => obj != null).ToList();


                foreach (ISaveController saveControllerObj in ISaveControllerObjects)
                {

                    if (saveControllerObj != null)
                    {
                        // Save the scene name
                    saveData.savedSceneName = SceneManager.GetActiveScene().name;

                        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
                        Debug.Log("Saving data for: " + saveControllerObj.GetType().FullName);
                        saveControllerObj.SaveGameData(ref saveData);
                    }
                    else
                    {
                        Debug.Log("ISaveController object is null.");
                    }

                }
                dataHandler.Save(saveData);
            }
 }
       


public void OnApplicationQuit(){
   SaveGame();
}

 private List<ISaveController> FindAllISaveControllerObjects()
 {
           IEnumerable<ISaveController> saveController = FindObjectsOfType<MonoBehaviour>().OfType<ISaveController>();
   return new List<ISaveController>(saveController);
 }
    }

}

