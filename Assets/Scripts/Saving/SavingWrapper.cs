using System;
using System.Collections;
using RPG.SceneManagement;
using UnityEngine;

namespace RPG.Saving{
    public class SavingWrapper : MonoBehaviour{
    const string defaultSaveFile = "save";
    //     [SerializeField] float fadeInTime = 0.2f;

    //     IEnumerator Start() {
    //         Fader fader = FindObjectOfType<Fader>();
    //         fader.FadeOutImmediate();
            
    //         yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
    //         yield return fader.FadeIn(fadeInTime);

    //     }

        void Update(){
            if(Input.GetKeyDown(KeyCode.L)){
                GetComponent<SavingSystem>().Load(defaultSaveFile);

            }
            if(Input.GetKeyDown(KeyCode.S)){
                GetComponent<SavingSystem>().Save(defaultSaveFile);

            }
        }

    //     public void Save()
    //     {
    //         GetComponent<SavingSystem>().Save(defaultSaveFile);
    //     }

    //     public void Load()
    //     {
    //         GetComponent<SavingSystem>().Load(defaultSaveFile);
    //     }
    }

}