using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isPlayed = false;
        private void OnTriggerEnter(Collider other){
            if(isPlayed) return;
            if(other.tag == "Player"){
                GetComponent<PlayableDirector>().Play();
                isPlayed = true;
            }
        }
    }

}

