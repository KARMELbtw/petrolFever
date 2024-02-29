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
            StartCoroutine(drilling());
        }
        
        IEnumerator drilling()
        {
            while (oilNow <= 0) {
                if (GameManager.checkOilMaxWithNow()) { 
                    GameManager.addOil(1);
                }
                oilNow--;
                int liczbaDzieci = transform.childCount;
                for (int i = 0; i < liczbaDzieci; i++) {
                    material = transform.GetChild(i).GetComponent<Renderer>().material;
                    material.color = new Color(material.color.r, material.color.g, material.color.b, (float)oilNow / capacity);
                }
                Debug.Log("Drilling");
                yield return new WaitForSeconds(1);
            }
            Debug.Log("Drilling stopped");
            Destroy(this.gameObject);
        }

        void Start() {
            Random rnd = new Random();
            capacity = numberOfOilBlocks * rnd.Next(5, 10);
            oilNow = capacity;
        }
    }
}