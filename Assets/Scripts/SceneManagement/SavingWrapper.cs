using System;
using System.Collections;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 0.2f;

        private void Start() {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene() {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);

            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            
            yield return fader.FadeIn(fadeInTime);
         }

        void Update(){
            if(Input.GetKeyDown(KeyCode.L)){
                Load();
            }
            if(Input.GetKeyDown(KeyCode.S)){
                Save();
            }
            if(Input.GetKeyDown(KeyCode.D)){
                Delete();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            print("Loaded");
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete(){
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
            
        }
    }

}