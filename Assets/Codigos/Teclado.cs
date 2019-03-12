using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Teclado : MonoBehaviour {

    public InputField txtTexto;
    public GameObject Inicial;
    bool mayusculas = false;
    bool Paso = true;

    public void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Inicial);

        Paso = true;
        mayusculas = true;
        Mayusculas();
    }

    public void Update()
    {
        if (Input.GetAxis("joystick1_left_trigger") != 0 && Paso)
        {
            Paso = false;

            Mayusculas();

            StartCoroutine(SeguirPasando());
        }

        if (Input.GetAxis("joystick1_right_trigger") != 0 && Paso)
        {
            Paso = false;

            Listo();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Borrar();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && txtTexto.text.Length <= 250)
        {
            txtTexto.text += " ";
        }
    }

    private IEnumerator SeguirPasando()
    {
        yield return new WaitForSeconds(0.5f);
        Paso = true;
    }

    public void BotonClick(string Letra)
    {
        if (txtTexto.text.Length <= 250)
        {
            if (mayusculas)
            {
                txtTexto.text += Letra.ToUpper();
            }
            else
            {
                txtTexto.text += Letra;
            }
        }
    }

    public void Mayusculas()
    {
        mayusculas = !mayusculas;

        if (mayusculas)
        {
            for (int i = 11; i < transform.childCount - 18; i++)
            {

                transform.GetChild(i).GetComponentInChildren<Text>().text = transform.GetChild(i).GetComponentInChildren<Text>().text.ToUpper();
            }
        }
        else
        {
            for (int i = 11; i < transform.childCount - 18; i++)
            {
                transform.GetChild(i).GetComponentInChildren<Text>().text = transform.GetChild(i).GetComponentInChildren<Text>().text.ToLower();
            }
        }
    }

    public void Borrar()
    {
        if (txtTexto.text.Length > 0)
        {
            txtTexto.text = txtTexto.text.Substring(0, txtTexto.text.Length - 1);
        }
    }

    public void Listo()
    {
        if (txtTexto.text.Trim() != "")
        {
            if (Controlador.instancia.EsNuevaCarpeta)
            {
                Controlador.instancia.NuevaCarpetaNombre = txtTexto.text;
                Controlador.instancia.NuevaCarpetaAccion();
            }
            else
            {
                Controlador.instancia.TextoCambiado = txtTexto.text;
                Controlador.instancia.RenombrarAccion();
            }
            
            this.gameObject.SetActive(false);
        }
    }
}
