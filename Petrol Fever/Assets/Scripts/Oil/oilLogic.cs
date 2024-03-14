using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Oil
{
    public class oilLogic : MonoBehaviour
    {
        private Material material;
        [SerializeField]
        private int capacity;
        public int numberOfOilBlocks;
        [SerializeField]
        private int oilNow;
        [SerializeField]
        private bool isBeingDrilled = false;
        public void startDrilling()
        {
            if (isBeingDrilled) {
                return;
            }
            isBeingDrilled = true;
            Debug.Log("Drilling started");
            TutorialManager.firstOilDrilled = true;
            StartCoroutine(drilling());
        }
        
        IEnumerator drilling()
        {
            while (true) {
                yield return new WaitForSeconds(GameManager.drillingSpeed);
                if (GameManager.checkOilMaxWithNow()) { 
                    GameManager.addOil(1);
                }
                oilNow--;
                int liczbaDzieci = transform.childCount;
                for (int i = 0; i < liczbaDzieci; i++) {
                    material = transform.GetChild(i).GetComponent<Renderer>().material;
                    material.color = new Color(material.color.r, material.color.g, material.color.b, (float)oilNow / capacity);
                }
                TutorialManager.firstOilDrilled = true;
                Debug.Log("Drilling");
                if (oilNow <= 0) {
                    Debug.Log("Drilling stopped");
                    Destroy(this.gameObject);
                }
            }
        }

        void Start() {
            Random rnd = new Random();
            capacity = numberOfOilBlocks * rnd.Next(10, 15);
            oilNow = capacity;
        }
    }
}