using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opciones : MonoBehaviour {

    private int Opcion = 0;
    private bool Paso = true;
        
    public GameObject[] MisOpciones = new GameObject[6];
    public Font arialJapon;
    public Font arialChina;

    string CadenaEliminar = "Delete";
    string CadenaSeguro = "Sure ?";
    
    public void Awake()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Portuguese:
                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "Nova Pasta";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "Recortar";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "Copiar";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "Colar";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "Renomear";

                CadenaEliminar = "Deletar";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "Certeza ?";
                break;
            case SystemLanguage.Spanish:
                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "Nueva carpeta";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "Cortar";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "Copiar";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "Pegar";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "Renombrar";

                CadenaEliminar = "Eliminar";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "Seguro ?";
                break;
            case SystemLanguage.Japanese:
                MisOpciones[0].transform.GetComponentInChildren<Text>().font = arialJapon;
                MisOpciones[1].transform.GetComponentInChildren<Text>().font = arialJapon;
                MisOpciones[2].transform.GetComponentInChildren<Text>().font = arialJapon;
                MisOpciones[3].transform.GetComponentInChildren<Text>().font = arialJapon;
                MisOpciones[4].transform.GetComponentInChildren<Text>().font = arialJapon;
                MisOpciones[5].transform.GetComponentInChildren<Text>().font = arialJapon;

                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "新しいフォルダ";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "切り取り";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "コピー";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "貼り付け";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "名前を変える";

                CadenaEliminar = "削除";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "本当に削除してもよろしいですか？";
                break;
            case SystemLanguage.French:
                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "Nouveau dossier";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "Couper";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "Copier";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "Coller";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "Renommer";

                CadenaEliminar = "Supprimer";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "Sur ?";
                break;
            case SystemLanguage.German:
                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "Neuer Ordner";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "Ausschneiden";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "Kopieren";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "Einfügen";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "Umbenennen";

                CadenaEliminar = "Löschen";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "Sicher ?";
                break;
            case SystemLanguage.Ukrainian:
                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "Нова папка";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "Вирізати";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "Копіювати";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "Вставити";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "Переіменувати";

                CadenaEliminar = "Видалити";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "Певен ?";
                break;
            case SystemLanguage.Italian:
                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "Nuova Cartella";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "Taglia";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "Copia";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "Incolla";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "Rinomina";

                CadenaEliminar = "Cancella";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "Sicuro ?";
                break;
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
            case SystemLanguage.ChineseTraditional:
                MisOpciones[0].transform.GetComponentInChildren<Text>().font = arialChina;
                MisOpciones[1].transform.GetComponentInChildren<Text>().font = arialChina;
                MisOpciones[2].transform.GetComponentInChildren<Text>().font = arialChina;
                MisOpciones[3].transform.GetComponentInChildren<Text>().font = arialChina;
                MisOpciones[4].transform.GetComponentInChildren<Text>().font = arialChina;
                MisOpciones[5].transform.GetComponentInChildren<Text>().font = arialChina;

                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "新建文件夹";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "剪切";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "复制";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "粘贴";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "重命名";

                CadenaEliminar = "删除";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "确定？";
                break;
            case SystemLanguage.Arabic:
                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "ﺪﻳﺪﺟ ﺪﻠﺠﻣ";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "ﺺﻗ";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "ﺦﺴﻧ";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "ﻖﺼﻟ";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "ﺔﻴﻤﺴﺗ ةدﺎﻋإ";

                CadenaEliminar = "فﺬﺣ";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "؟ﺪﻛﺎﺘﻣ ﺖﻧأ ﻞﻫ";
                break;
            case SystemLanguage.Vietnamese:
                MisOpciones[0].transform.GetComponentInChildren<Text>().text = "Tạo thư mục mới";
                MisOpciones[1].transform.GetComponentInChildren<Text>().text = "Cắt";
                MisOpciones[2].transform.GetComponentInChildren<Text>().text = "Chép";
                MisOpciones[3].transform.GetComponentInChildren<Text>().text = "Dán";
                MisOpciones[4].transform.GetComponentInChildren<Text>().text = "Đổi tên";

                CadenaEliminar = "Xóa";
                MisOpciones[5].transform.GetComponentInChildren<Text>().text = CadenaEliminar;
                CadenaSeguro = "Bạn có chắc chắn ?";
                break;
        }
    }

    public void OnDisable()
    {
        Opcion = 0;
        MisOpciones[0].transform.GetChild(0).gameObject.SetActive(true);
        MisOpciones[1].transform.GetChild(0).gameObject.SetActive(false);
        MisOpciones[2].transform.GetChild(0).gameObject.SetActive(false);
        MisOpciones[3].transform.GetChild(0).gameObject.SetActive(false);
        MisOpciones[4].transform.GetChild(0).gameObject.SetActive(false);
        MisOpciones[4].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        MisOpciones[4].transform.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1f);
        MisOpciones[5].transform.GetChild(0).gameObject.SetActive(false);
        MisOpciones[5].transform.GetChild(1).GetComponent<Text>().text = CadenaEliminar;
        Controlador.instancia.Seguro = false;
    }

    public void OnEnable()
    {
        if (Controlador.instancia.Multiseleccion)
        {
            MisOpciones[4].GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
            MisOpciones[4].transform.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0.3f);
        }
    }
    
	void Update ()
    {
        if ((Input.GetAxis("dpad1_vertical") > 0 || Input.GetKey(KeyCode.UpArrow)) && Opcion > 0 && Paso)
        {
            Paso = false;

            MisOpciones[Opcion].transform.GetChild(0).gameObject.SetActive(false);
            Opcion--;
            MisOpciones[Opcion].transform.GetChild(0).gameObject.SetActive(true);

            if (Controlador.instancia.Seguro)
            {
                MisOpciones[5].transform.GetChild(1).GetComponent<Text>().text = CadenaEliminar;
                Controlador.instancia.Seguro = false;
            }

            StartCoroutine(SeguirPasando());
        }

        if ((Input.GetAxis("dpad1_vertical") < 0 || Input.GetKey(KeyCode.DownArrow)) && Opcion < 5 && Paso)
        {
            Paso = false;

            MisOpciones[Opcion].transform.GetChild(0).gameObject.SetActive(false);
            Opcion++;
            MisOpciones[Opcion].transform.GetChild(0).gameObject.SetActive(true);

            if (Controlador.instancia.Seguro)
            {
                MisOpciones[5].transform.GetChild(1).GetComponent<Text>().text = CadenaEliminar;
                Controlador.instancia.Seguro = false;
            }

            StartCoroutine(SeguirPasando());
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            switch (Opcion)
            {
                case 0:
                    Controlador.instancia.NuevaCarpeta();
                    break;
                case 1:
                    Controlador.instancia.Cortar();
                    break;
                case 2:
                    Controlador.instancia.Copiar();
                    break;
                case 3:
                    Controlador.instancia.Pegar();
                    break;
                case 4:
                    Controlador.instancia.Renombra();
                    break;
                case 5:
                    if (Controlador.instancia.Seguro)
                    {
                        Controlador.instancia.Eliminar();
                    }
                    else
                    {
                        MisOpciones[Opcion].transform.GetChild(1).GetComponent<Text>().text = CadenaSeguro;
                        Controlador.instancia.Seguro = true;
                    }
                    break;
            }
        }
	}

    private IEnumerator SeguirPasando()
    {
        yield return new WaitForSeconds(0.2f);
        Paso = true;
    }
}
