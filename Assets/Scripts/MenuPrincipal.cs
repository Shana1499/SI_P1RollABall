using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Iniciar()
    {
        SceneManager.LoadScene("MiniGame");
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
