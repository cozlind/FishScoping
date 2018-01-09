using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoldfishScoping {
    public class KinGyoScenario : MonoBehaviour {
        [Header ("Process")]
        public float[] time;
        private int timeIndex = 0;
        private float currentTime = 0;
        private bool isSuccess = false;
        public int score = 0;
        [Header ("Audio")]
        public AudioClip[] audios;
        AudioSource audio;
        [Header ("GameObject")]
        public CatcherManager catcher;
        public GameObject redPfb;
        public GameObject yellowPfb;
        public GameObject blackPfb;
        public GameObject redWhitePfb;
        public GameObject brokenPoi;
        public GameObject poi;
        public GameObject[] pois;
        private List<GameObject> fishes;
        public int poiNum = 2;
        public int fishNum = 4;
        [Header("UI")]
        public Image energyUI;
        public Text fishNumUI;
        public Text fishNumUI2;
        public Image poiUI;
        public Text timeUI;
        public GameObject titleUI;
        public GameObject replayUI;
        private void Awake () {
            audio = GetComponent<AudioSource> ();
            fishes = new List<GameObject> ();
            currentTime = 0;
            replayUI.SetActive (false);
        }
        public void startPlay () {
            audio.clip = audios[0];
            audio.Play ();

            for (int i = fishes.Count - 1; i >= 0; i--) {
                Destroy (fishes[i]);
            }
            fishes.Clear ();
            for (int i = 0; i < fishNum; i++) {
                fishes.Add(Instantiate (redPfb, transform.position,Quaternion.identity));
                fishes.Add (Instantiate (yellowPfb, transform.position, Quaternion.identity));
                fishes.Add (Instantiate (blackPfb, transform.position, Quaternion.identity));
                fishes.Add (Instantiate (redWhitePfb, transform.position, Quaternion.identity));
            }
            currentTime = 0;
            timeIndex = 2;
            titleUI.SetActive (false);
            replayUI.SetActive (false);
        }
        public void endPlay () {
            audio.clip = audios[0];
            audio.Play ();
            Application.Quit ();
        }
        private void FixedUpdate () {

            currentTime += Time.fixedDeltaTime;
            switch (timeIndex) {
                case 0:
                    if (time[timeIndex]> currentTime) {
                        titleUI.GetComponent<Image> ().color = Color.Lerp (Color.black, Color.white, currentTime / time[timeIndex]);
                    } else {
                        timeIndex++;
                        currentTime = 0;
                    }
                    break;
                case 1: currentTime = 0;
                    break;
                case 2:
                    if (time[timeIndex] > currentTime) {
                        int t = Mathf.RoundToInt (time[timeIndex]-currentTime);
                        timeUI.text = t.ToString ();

                        if (catcher.isScoping) {
                            energyUI.fillAmount += Time.fixedDeltaTime;
                            if (energyUI.fillAmount >= 1) {
                                catcher.isScoping = false;
                                score += catcher.poi.currFish.score;
                                fishNumUI.text = score.ToString ();
                                
                                Destroy (catcher.poi.currFish.gameObject);
                            }
                        } else {
                            //energy out
                            energyUI.fillAmount = 0;
                        }

                        if (catcher.isUsePoi) {
                            poiUI.fillAmount += Time.fixedDeltaTime / 14;
                            if (poiUI.fillAmount >= 1) {
                                //poi out
                                audio.clip = audios[2];
                                audio.Play ();
                                pois[poiNum--].SetActive (false);
                                if (poiNum >= 0) {
                                    poiUI.fillAmount = 0;
                                    poi.SetActive (false);
                                    brokenPoi.SetActive (true);
                                    timeIndex--;
                                    currentTime = 0;
                                    replayUI.SetActive (true);
                                    audio.clip = audios[1];
                                    audio.Play ();
                                }
                            }
                        }

                    } else {
                        timeIndex--;
                        currentTime = 0;
                        replayUI.SetActive (true);
                        fishNumUI2.text = fishNumUI.text + "点";
                        audio.clip = audios[3];
                        audio.Play ();
                        //time out
                    }
                    break;
            }
        }
    }
}
