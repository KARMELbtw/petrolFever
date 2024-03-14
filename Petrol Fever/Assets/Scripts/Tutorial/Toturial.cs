using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toturial : MonoBehaviour
{
    private TextMeshProUGUI dialogDisplay;
    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogDisplay = GameObject.Find("toturialText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
            // Chej nazywam się Ignacy Łukasiewicz, pozwól że pokażę ci co i jak, naciśnij spację aby kontynuować.
            // Jelenie potrafią wywęszyć ropę pod ziemią, kup i postaw swojego pierwszego jelenia i poczekaj aż znajdzie jakąś ropę.
            //
            // Zobacz! Twój jeleń znalazł ropę! postaw wiertło nad złożem i poprowadź do niego rurę aby rozpocząć wydobycie.
            //
            // Świetnie, twoja ropa właśnie się wydobywa.
            //
            // Teraz postaw silos, aby mieć gdzie ją przechowywać.
            //
            // Cena ropy rośnie kiedy jej nie sprzedajesz, więc opłaca się ją trzymać i sprzedać potem w dużych ilościach.
            // Cenę ropy możesz sprawdzić po kliknięciu w silos. Kiedy uznasz, że cena cię satysfakcjonuje sprzedaj swoją ropę.
            //
            // Świetnie, kiedy skończysz wydobywać ropę możesz sprzedać swoje wiertło lub silos klikając na nie i naciskając klawisz r gdy już go nie potrzebujesz.
            // Możesz również kliknąć klawisz c aby zmienić scenę ze swojej działki na miasto i z powrotem.
            // Teraz wiesz już wszystko co potrzeba aby zostać magnatem paliwowym.
            // Powodzenia!
                
        if (!start)
        {
            dialogDisplay.text = "Hej nazywam się Ignacy Łukasiewicz, pozwól że pokażę ci co i jak, naciśnij spację aby kontynuować.";
            start = true;
            if (Input.GetKeyDown(KeyCode.Space))//rozpoczynanie toturiala
            {
                if (!TutorialManager.deerFoundOil)//start pierwsza zmienna false
                {
                    dialogDisplay.text = "Jelenie potrafią wywęszyć ropę pod ziemią, kup i postaw swojego pierwszego jelenia i " +
                                         "poczekaj aż znajdzie jakąś ropę.";
                }
                else if (TutorialManager.deerFoundOil)//jelen znalazl rope
                {
                    dialogDisplay.text = "Zobacz! Twój jeleń znalazł ropę! postaw wiertło nad złożem i poprowadź do niego " +
                                         "rurę aby rozpocząć wydobycie.";
                }
            }
        }
                
        

    }
}