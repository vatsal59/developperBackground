using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject panel;
    public Text text;
    private string initialText = "Hmm… cette île… rien ne me dit où je suis. Pas de signal, pas de traces de vie.Quelque chose cloche ici.J'vais devoir rester sur mes gardes… Ça sent pas bon. 'A l'AIDEEEEE !!'. Ce cris provient de cette cabane. Je vais aller voir";

    void Start()
    {
        text.text = "";
        ShowPanel(initialText);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WriteSentences(string textToWrite)
    {
        foreach(char letter in textToWrite)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1.5f); 
        panel.SetActive(false);
    }

    void ShowPanel(string text){
        panel.SetActive(true);
        StartCoroutine(WriteSentences(text));
    }
}
