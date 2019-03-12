using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour {

    //[DllImport("plugins")]
    //private static extern int Unjail505();

    //[DllImport("plugins")]
    //private static extern int FTP();
    
    //[DllImport("plugins")]
    //private static extern int Mount_RW();
    
    

    public static Controlador instancia;
    
    public Text txtCamino;
    public Text txtLog;
    string LOG = "";

    public GameObject PanelOpciones;
    public GameObject PanelOpcionesAvanzadas;
    public GameObject PanelImagen;
    public Image PanelImagenImagen;
    public GameObject PanelEditorTexto;
    public AudioSource miAudioSource;
    public GameObject PanelCopiando;
    public GameObject PanelTeclado;
    public Text textoOptions;
    public Sprite instrucciones;
    public Font arialJapon;
    public Font arialChina;
        
    public GameObject carpetasPrefab;
    public GameObject carpetasPrefabBlackList;
    public GameObject carpetasPrefabEspecial;
    public GameObject ficherosPrefab;
    public GameObject ficherosPrefabMP3;
    public GameObject ficherosPrefabMP4;
    public GameObject ficherosPrefabFOT;
    public GameObject ficherosPrefabTXT;
    public ScrollRect scrollRect;
    public RectTransform contentPanel;

    private List<GameObject> ObjetosCreados = new List<GameObject>();
    private List<string> TODOS = new List<string>();

    private int Posicion = 0;
    private string[] scaneo;

    public string camino = "/"; // para PS4 cambiar por /
    private bool Paso = true;
    private bool EnOpciones = false;
    private bool EnImagen = false;
    private bool EnEditorTexto = false;
    private bool Pegando = false;
    private bool EnTeclado = false;

    public bool Multiseleccion = false;
    private bool MultiseleccionCopiada = false;
    private bool MultiseleccionCortada = false;
    private List<int> ObjetosSelecionados = new List<int>();
    private List<string> ObjetosCaminos = new List<string>();

    public GameObject PanelVideo;
    public Image VideoImagen;
    public bool EnVideo = false;

    // cadenas para la traduccion...
    string CadenaMoviendo = "Moving...";
    string CadenaCopiando = "Copying...";
    string CadenaNoHasCopiado = "You have not cut or copied anything to paste";
    string CadenaErrorCrearCarpeta = "The folder that you try to create already exists";
        
    public void Awake()
    {
        instancia = this;

        switch (Application.systemLanguage)
        {
            case SystemLanguage.Portuguese:
                CadenaMoviendo = "Movendo...";
                CadenaCopiando = "Copiando...";
                CadenaNoHasCopiado = "Você não cortou nem copiou nada para colar";
                textoOptions.text = "OPTIONS\npara instruções";
                break;
            case SystemLanguage.Spanish:
                CadenaMoviendo = "Moviendo...";
                CadenaCopiando = "Copiando...";
                CadenaNoHasCopiado = "No has cortado o copiado nada que pegar";
                CadenaErrorCrearCarpeta = "La carpeta que intentas crear ya existe";
                textoOptions.text = "OPTIONS\npara instrucciones";
                break;
            case SystemLanguage.Japanese:
                txtCamino.font = arialJapon;
                txtLog.font = arialJapon;
                textoOptions.font = arialJapon;
                PanelCopiando.GetComponentInChildren<Text>().font = arialJapon;

                CadenaMoviendo = "移動しています...";
                CadenaCopiando = "コピーしています...";
                CadenaNoHasCopiado = "貼り付けるものを、切り取り又はコピーしていない";
                textoOptions.text = "OPTIONS\n操作方法";
                break;
            case SystemLanguage.French:
                CadenaMoviendo = "Bouger...";
                CadenaCopiando = "Copie en cours...";
                CadenaNoHasCopiado = "Vous n'avez rien àcouper ou coller";
                textoOptions.text = "OPTIONS\npour instruction";
                break;
            case SystemLanguage.German:
                CadenaMoviendo = "Verschiebe...";
                CadenaCopiando = "Kopiere...";
                CadenaNoHasCopiado = "Du hast nichts zum einfügen ausgeschnitten oder kopiert";
                textoOptions.text = "OPTIONS\nfür Anleitung";
                break;
            case SystemLanguage.Ukrainian:
                CadenaMoviendo = "Переміщаю...";
                CadenaCopiando = "Копіюю...";
                CadenaNoHasCopiado = "Ви не вирізали та не скопіювали нічого щоб вставити";
                textoOptions.text = "OPTIONS\nдля інструкцій";
                break;
            case SystemLanguage.Italian:
                CadenaMoviendo = "Spostamento in corso...";
                CadenaCopiando = "Copia in corso...";
                CadenaNoHasCopiado = "Non hai tagliato o copiato nulla da incollare";
                textoOptions.text = "OPTIONS\nper istruzioni";
                break;
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
            case SystemLanguage.ChineseTraditional:
                txtCamino.font = arialChina;
                txtLog.font = arialChina;
                textoOptions.font = arialChina;
                PanelCopiando.GetComponentInChildren<Text>().font = arialChina;

                CadenaMoviendo = "移动...";
                CadenaCopiando = "复制...";
                CadenaNoHasCopiado = "您没有剪切或复制任何要粘贴的内容";
                textoOptions.text = "OPTIONS\n说明";
                break;
            case SystemLanguage.Arabic:
                CadenaMoviendo = "ﻞﻘﻨﻟا ِرﺎﺟ";
                CadenaCopiando = "ﺦﺴﻨﻟا ِرﺎﺟ";
                CadenaNoHasCopiado = "ﻪﻘﺼﻟ ﻢﺘﻴﻟ ءﻲﺷ يأ ﺺﻗ وأ ﺦﺴﻨﺑ ﻢﻘﺗ ﻢﻟ";
                textoOptions.text = "OPTIONS\nتادﺎﺷرﻹا ﺢﺘﻔﻟ";
                break;
            case SystemLanguage.Vietnamese:
                CadenaMoviendo = "Đang di chuyển";
                CadenaCopiando = "Đang sao chép";
                CadenaNoHasCopiado = "Bạn chưa cắt hoặc sao chép thứ gì";
                textoOptions.text = "OPTIONS\nChỉ dẫn";
                break;
        }
    }

    void Start()
    {
        try
        {
            //Unjail505();
        }
        catch { ;}

        CrearDirectorio();
    }

    void CrearDirectorio()
    {
        txtCamino.text = camino;

        List<string> CarpetasCreadas = new List<string>();
        List<string> FicherosCreados = new List<string>();
        GameObject objeto = null;

        scaneo = Directory.GetFileSystemEntries(txtCamino.text);
        foreach (string registro in scaneo)
        {
            if (Directory.Exists(registro))
            {
                CarpetasCreadas.Add(registro);
            }
            else
            {
                FicherosCreados.Add(registro);
            }
        }

        for (int i = 0; i < CarpetasCreadas.Count; i++)
        {
            switch (CarpetasCreadas[i])
            {
                case "/dev":
                    objeto = Instantiate(carpetasPrefabBlackList, transform);
                    break;
                case "/mnt":
                case "/mnt/usb0":
                case "/mnt/usb1":
                    objeto = Instantiate(carpetasPrefabEspecial, transform);
                    break;
                default:
                    objeto = Instantiate(carpetasPrefab, transform);
                    break;
            }

            objeto.GetComponentInChildren<Text>().text = Path.GetFileName(CarpetasCreadas[i]);

            ObjetosCreados.Add(objeto);
            TODOS.Add(CarpetasCreadas[i]);
        }
        for (int i = 0; i < FicherosCreados.Count; i++)
        {
            switch (Path.GetExtension(FicherosCreados[i]).ToLower())
            {
                case ".ogg":
                case ".wav":
                    objeto = Instantiate(ficherosPrefabMP3, transform);
                    break;
                case ".mp4":
                    objeto = Instantiate(ficherosPrefabMP4, transform);
                    break;
                case ".jpg":
                case ".png":
                    objeto = Instantiate(ficherosPrefabFOT, transform);
                    break;
                case ".txt":
                case ".ini":
                case ".bat":
                case ".xml":
                    objeto = Instantiate(ficherosPrefabTXT, transform);
                    break;
                default:
                    objeto = Instantiate(ficherosPrefab, transform);
                    break;
            }
            
            objeto.GetComponentInChildren<Text>().text = Path.GetFileName(FicherosCreados[i]);
            objeto.transform.GetChild(3).GetComponent<Text>().text = Tamaño(FicherosCreados[i]);

            ObjetosCreados.Add(objeto);
            TODOS.Add(FicherosCreados[i]);
        }

        scrollRect.verticalNormalizedPosition = 1;
        
        // si hay algo selecionar el 1ro de la lista
        if (ObjetosCreados.Count > 0)
        {
            ObjetosCreados[0].transform.GetChild(0).gameObject.SetActive(true);
            camino = TODOS[0];
        }
        else
        {
            camino = "";
        }

        Paso = true;
        ObjetosSelecionados.Clear();
        Multiseleccion = false;
    }

    private string Tamaño(string fichero)
    {
        FileInfo fileData = new FileInfo(fichero);
        float KB = fileData.Length / 1024f;
        string sufijo = " Kb";

        if (KB >= 1024)
        {
            KB = KB / 1024f;
            sufijo = " Mb";
        }

        if (KB >= 1024)
        {
            KB = KB / 1024f;
            sufijo = " Gb";
        }

        KB = Mathf.Round(KB);

        if (KB == 0)
            KB = 1;

        return KB.ToString() + sufijo;
    }

    void Update()
    {
        if (EnEditorTexto)
        {
            if ((Input.GetAxis("dpad1_vertical") > 0 || Input.GetAxis("leftstick1vertical") < 0 || Input.GetAxis("rightstick1vertical") < 0 || Input.GetKey(KeyCode.UpArrow)) && PanelEditorTexto.GetComponentInChildren<Scrollbar>().value < 1 && Paso)
            {
                Paso = false;
                PanelEditorTexto.GetComponentInChildren<Scrollbar>().value += 0.05f;
                StartCoroutine(SeguirPasando());
            }

            if ((Input.GetAxis("dpad1_vertical") < 0 || Input.GetAxis("leftstick1vertical") > 0 || Input.GetAxis("rightstick1vertical") > 0 || Input.GetKey(KeyCode.DownArrow)) && PanelEditorTexto.GetComponentInChildren<Scrollbar>().value > 0 && Paso)
            {
                Paso = false;
                PanelEditorTexto.GetComponentInChildren<Scrollbar>().value -= 0.05f;
                StartCoroutine(SeguirPasando());
            }
        }

        if (!EnOpciones && !EnImagen && !Pegando && !EnEditorTexto && !EnTeclado)
        {
            // multiselecion
            if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.M))
            {
                // selecionar todos si RT esta presionado...
                if (!Directory.Exists(camino) && Input.GetAxis("joystick1_right_trigger") != 0) // Input.GetKey(KeyCode.LeftShift)
                {
                    Multiseleccion = true;
                    MultiseleccionCopiada = false;
                    MultiseleccionCortada = false;
                    ObjetosSelecionados.Clear();
                    ObjetosCaminos.Clear();

                    for (int i = 0; i < TODOS.Count; i++)
                    {
                        if (!Directory.Exists(TODOS[i]))
                        {
                            ObjetosSelecionados.Add(i);
                            ObjetosCaminos.Add(TODOS[i]);
                            ObjetosCreados[i].transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }

                    return;
                }
                
                if (!Multiseleccion)
                    ObjetosCaminos.Clear();
                
                Multiseleccion = true;
                bool ok = true;
                for (int i = 0; i < ObjetosSelecionados.Count; i++)
                {
                    if (ObjetosSelecionados[i] == Posicion)
                    {
                        if (MultiseleccionCopiada || MultiseleccionCortada)
                        {
                            MultiseleccionCopiada = false;
                            MultiseleccionCortada = false;
                            ObjetosSelecionados.Clear();
                        }

                        ObjetosSelecionados.RemoveAt(i);
                        ObjetosCaminos.RemoveAt(i);
                        ObjetosCreados[Posicion].transform.GetChild(1).gameObject.SetActive(false);
                        ok = false;
                    }
                }

                if (ok == true)
                {
                    if (!Directory.Exists(camino))
                    {
                        if (MultiseleccionCopiada || MultiseleccionCortada)
                        {
                            MultiseleccionCopiada = false;
                            MultiseleccionCortada = false;
                            ObjetosSelecionados.Clear();
                        }

                        ObjetosSelecionados.Add(Posicion);
                        ObjetosCaminos.Add(camino);
                        ObjetosCreados[Posicion].transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
                if (ObjetosSelecionados.Count == 0)
                {
                    Multiseleccion = false;
                }
            }

            // cancelar multiselecion
            if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.C))
            {
                ObjetosSelecionados.Clear();
                ObjetosCaminos.Clear();
                Multiseleccion = false;
                MultiseleccionCopiada = false;
                MultiseleccionCortada = false;

                for (int i = 0; i < scaneo.Length; i++)
                {
                    ObjetosCreados[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }

            // movimientos arriba y abajo
            if ((Input.GetAxis("dpad1_vertical") > 0 || Input.GetAxis("leftstick1vertical") < 0 || Input.GetAxis("rightstick1vertical") < 0 || Input.GetKey(KeyCode.UpArrow)) && Posicion > 0 && Paso)
            {
                Paso = false;

                ObjetosCreados[Posicion].transform.GetChild(0).gameObject.SetActive(false);
                Posicion--;
                ObjetosCreados[Posicion].transform.GetChild(0).gameObject.SetActive(true);
                camino = TODOS[Posicion];

                if (contentPanel.anchoredPosition.y > 0)
                {
                    contentPanel.anchoredPosition -= new Vector2(0, 69);
                }
                       
                StartCoroutine(SeguirPasando());
            }

            if ((Input.GetAxis("dpad1_vertical") < 0 || Input.GetAxis("leftstick1vertical") > 0 || Input.GetAxis("rightstick1vertical") > 0 || Input.GetKey(KeyCode.DownArrow)) && Posicion < scaneo.Length - 1 && Paso)
            {
                Paso = false;

                ObjetosCreados[Posicion].transform.GetChild(0).gameObject.SetActive(false);
                Posicion++;
                ObjetosCreados[Posicion].transform.GetChild(0).gameObject.SetActive(true);
                camino = TODOS[Posicion];

                if (scrollRect.verticalNormalizedPosition >= 0 && Posicion > 9)
                {
                    contentPanel.anchoredPosition += new Vector2(0, 69);
                }

                StartCoroutine(SeguirPasando());
            }

            // accesos directos a los USB con el DPad <- , ->
            if ((Input.GetAxis("dpad1_horizontal") < 0 || Input.GetKeyDown(KeyCode.LeftArrow)) && Paso)
            {
                Paso = false;

                LOG = "";
                LimpiarTodo();
                camino = "/mnt/usb0";
                CrearDirectorio();

                StartCoroutine(SeguirPasando());
            }

            if ((Input.GetAxis("dpad1_horizontal") > 0 || Input.GetKeyDown(KeyCode.RightArrow)) && Paso)
            {
                Paso = false;

                LOG = "";
                LimpiarTodo();
                camino = "/mnt/usb1";
                CrearDirectorio();

                StartCoroutine(SeguirPasando());
            }
        }
        
        // abrir o ejecutar
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            try
            {
                LOG = "";

                if (!EnOpciones && !Pegando && !EnEditorTexto && !EnTeclado && !EnImagen)
                {
                    if (Directory.Exists(camino)) // si es una carpeta abrirla
                    {
                        LimpiarTodo();
                        CrearDirectorio();
                    }
                    else // si no ejecutar el fichero si esta soportado
                    {
                        switch (Path.GetExtension(camino).ToLower())
                        {
                            case ".ogg":
                            case ".wav":
                                PlayAudio();
                                break;
                            case ".mp4":
                                PlayVideo();
                                break;
                            case ".jpg":
                            case ".png":
                                MostrarImagen();
                                break;
                            case ".txt":
                            case ".ini":
                            case ".bat":
                            case ".xml":
                                MostrarTexto();
                                break;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LOG = "Error " + ex.Message;
            }
        }

        // mostrar opciones
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (Input.GetAxis("joystick1_left_trigger") == 0) // normales
            {
                if (!EnImagen && !Pegando && !EnEditorTexto && !EnTeclado && !PanelOpcionesAvanzadas.gameObject.activeInHierarchy)
                {
                    if (FullRwActivado)
                    {
                        LOG = "";
                        EnOpciones = !EnOpciones;
                        PanelOpciones.SetActive(EnOpciones);
                    }
                    else if (camino.IndexOf("/dev") != 0) // "/dev"
                    {
                        LOG = "";
                        EnOpciones = !EnOpciones;
                        PanelOpciones.SetActive(EnOpciones);
                    }
                }
            }
            else // avanzadas
            {
                if (!EnImagen && !Pegando && !EnEditorTexto && !EnTeclado && !PanelOpciones.gameObject.activeInHierarchy)
                {
                    LOG = "";
                    EnOpciones = !EnOpciones;
                    PanelOpcionesAvanzadas.SetActive(EnOpciones);
                }
            }
        }

        // stop musica
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (!EnTeclado)
            {
                LOG = "";
                miAudioSource.Stop();
            }
        }

        // atras o cerrar opciones
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            if (EnOpciones)
            {
                LOG = "";
                EnOpciones = false;
                PanelOpciones.SetActive(false);
                PanelOpcionesAvanzadas.SetActive(false);
                return;
            }
            
            if (EnImagen)
            {
                LOG = "";
                EnImagen = false;
                PanelImagen.gameObject.SetActive(false);
                PanelVideo.gameObject.SetActive(false);
                return;
            }                

            if (EnEditorTexto)
            {
                LOG = "";
                EnEditorTexto = false;
                PanelEditorTexto.gameObject.SetActive(false);
                return;
            }

            if (EnTeclado)
            {
                LOG = "";
                EnTeclado = false;
                PanelTeclado.gameObject.SetActive(false);
                return;
            }
            
            if (!Pegando)
            {
                LOG = "";
                LimpiarTodo();

                camino = txtCamino.text.Substring(0, txtCamino.text.LastIndexOf("/")); // para PS4 cambiar por /
                if (camino.Length <= 1) // para PS4 cambiar por 1
                {
                    camino += "/"; // para PS4 cambiar por /
                }

                CrearDirectorio();
            }
        }

        // Instrucciones
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.O))
        {
            EnImagen = true;

            PanelImagenImagen.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1300, 900);
            PanelImagen.gameObject.SetActive(true);
            PanelImagenImagen.sprite = instrucciones;
        }

        // progressbar para cuando este pegando
        if (PanelCopiando.gameObject.activeInHierarchy)
        {
            if (CantArchivos <= 0)
            {
                PanelCopiando.gameObject.SetActive(false);
                Pegando = false;
                SistemaSonidos.instancia.PlayFinalizoCopia();
            }
            else
            {
                PanelCopiando.GetComponentInChildren<Slider>().value = PanelCopiando.GetComponentInChildren<Slider>().maxValue - CantArchivos;
            }
        }

        txtLog.text = LOG;
    }

    private void LimpiarTodo()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        ObjetosCreados.Clear();
        TODOS.Clear();
        Posicion = 0;
    }

    private IEnumerator SeguirPasando()
    {
        if (Input.GetAxis("joystick1_left_trigger") != 0)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(0.15f);
        }
        
        Paso = true;
    }

    private void MostrarImagen()
    {
        EnImagen = true;

        byte[] bytes = File.ReadAllBytes(camino);
        Texture2D texture = new Texture2D(0, 0, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.0f, 0.0f), 1.0f);

        float AR = 0;
        float XX = 0;
        float YY = 0;
        if (texture.height >= 1080)
        {
            AR = (float)texture.width / (float)texture.height;
            YY = Mathf.Min(texture.width, 1080);
            XX = YY * AR;

            if (XX > 1920)
            {
                XX = 1920;
                YY = 1920 / AR;
            }
        }
        else
        {
            AR = (float)texture.width / (float)texture.height;
            XX = Mathf.Min(texture.width, 1920);
            YY = XX / AR;
        }

        PanelImagenImagen.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(XX, Mathf.RoundToInt(YY));
        PanelImagen.gameObject.SetActive(true);
        PanelImagenImagen.sprite = sprite;
    }

    private void PlayAudio()
    {
        StartCoroutine(loadFile(camino));
    }

    private void PlayVideo()
    {
        EnImagen = true;
        EnVideo = true;
        PanelVideo.gameObject.SetActive(true);
    }

    private void MostrarTexto()
    {
        EnEditorTexto = true;

        string elTexto = "";
        PanelEditorTexto.gameObject.SetActive(true);
        StreamReader sr = new StreamReader(camino, System.Text.Encoding.Default);
        elTexto += sr.ReadToEnd();
        sr.Close();

        PanelEditorTexto.GetComponentInChildren<Text>().text = elTexto;

        Canvas.ForceUpdateCanvases();

        if (PanelEditorTexto.GetComponentInChildren<Scrollbar>().size == 1)
        {
            PanelEditorTexto.GetComponentInChildren<Scrollbar>().value = 0;
        }
        else
        {
            PanelEditorTexto.GetComponentInChildren<Scrollbar>().value = 1;
        }
    }

    IEnumerator loadFile(string path)
    {
        WWW www = new WWW("file://" + path);

        AudioClip myAudioClip = www.GetAudioClip();
        while (myAudioClip.loadState != AudioDataLoadState.Loaded)
            yield return www;

        miAudioSource.clip = myAudioClip;
        miAudioSource.Play();
    }

    // Opciones /////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool EsNuevaCarpeta = false;
    public string NuevaCarpetaNombre = "";

    private IEnumerator TecladoOff()
    {
        yield return new WaitForSeconds(0.5f);
        EnTeclado = false;
    }

    public void NuevaCarpeta()
    {
        EnOpciones = false;
        PanelOpciones.SetActive(false);

        EsNuevaCarpeta = true;
        EnTeclado = true;
        PanelTeclado.GetComponentInChildren<InputField>().text = "New folder";
        PanelTeclado.gameObject.SetActive(true);
    }

    public void NuevaCarpetaAccion()
    {
        EsNuevaCarpeta = false;
        
        try
        {
            if (!Directory.Exists(txtCamino.text + "/" + NuevaCarpetaNombre)) // ps4 cambiar por /
            {
                Directory.CreateDirectory(txtCamino.text + "/" + NuevaCarpetaNombre); // ps4 cambiar por /
            }
            else
            {
                LOG = CadenaErrorCrearCarpeta;
                EnTeclado = false;
                NuevaCarpetaNombre = "";
                return;
            }

            camino = txtCamino.text;
            LimpiarTodo();
            CrearDirectorio();
        }
        catch (System.Exception ex)
        {
            LOG = "Error " + ex.Message;
            Pegando = false;
            SistemaSonidos.instancia.PlayError();
        }

        NuevaCarpetaNombre = "";
        StartCoroutine(TecladoOff());
    }

    string FicheroACortar = "";
    string FicheroACopiar = "";
    
    public void Cortar()
    {
        EnOpciones = false;
        PanelOpciones.SetActive(false);

        if (!Multiseleccion)
        {
            FicheroACopiar = "";
            FicheroACortar = camino;

            OscurecerCortado();
            ObjetosCreados[Posicion].GetComponent<Image>().color = new Color(255, 255, 255, 0.4f);
            ObjetosCreados[Posicion].GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0.4f);
        }
        else
        {
            FicheroACortar = "";

            for (int i = 0; i < ObjetosSelecionados.Count; i++)
            {
                ObjetosCreados[ObjetosSelecionados[i]].GetComponent<Image>().color = new Color(255, 255, 255, 0.4f);
                ObjetosCreados[ObjetosSelecionados[i]].GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0.4f);
            }
        }

        MultiseleccionCortada = Multiseleccion;
        MultiseleccionCopiada = false;

    }

    private void OscurecerCortado()
    {
        for (int i = 0; i < scaneo.Length; i++)
        {
            ObjetosCreados[i].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            ObjetosCreados[i].GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1f);
        }
    }

    public void Copiar()
    {
        EnOpciones = false;
        PanelOpciones.SetActive(false);

        if (!Multiseleccion)
        {
            FicheroACortar = "";
            FicheroACopiar = camino;
        }
        else
        {
            FicheroACopiar = "";
        }
        
        MultiseleccionCopiada = Multiseleccion;
        MultiseleccionCortada = false;

        OscurecerCortado();
    }

    public void Pegar()
    {
        EnOpciones = false;
        PanelOpciones.SetActive(false);

        if (MultiseleccionCopiada)
        {
            Pegando = true;
            CantArchivos = ObjetosCaminos.Count;
            PanelCopiando.GetComponentInChildren<Slider>().maxValue = CantArchivos;
            PanelCopiando.gameObject.SetActive(true);
            PanelCopiando.GetComponentInChildren<Text>().text = CadenaCopiando;

            try
            {
                StartCoroutine(CopiarMultiplesFichero());
            }
            catch (System.Exception ex)
            {
                LOG = "Error " + ex.Message;
                CantArchivos = 0;
                Pegando = false;
                SistemaSonidos.instancia.PlayError();
            }
        }
        else if (MultiseleccionCortada)
        {
            Pegando = true;
            CantArchivos = ObjetosCaminos.Count;
            PanelCopiando.GetComponentInChildren<Slider>().maxValue = CantArchivos;
            PanelCopiando.gameObject.SetActive(true);
            PanelCopiando.GetComponentInChildren<Text>().text = CadenaCopiando;

            try
            {
                StartCoroutine(CortarMultiplesFichero());
            }
            catch (System.Exception ex)
            {
                LOG = "Error " + ex.Message;
                CantArchivos = 0;
                Pegando = false;
                SistemaSonidos.instancia.PlayError();
            }
        }
        else
        {
            if (FicheroACortar != "")
            {
                try
                {
                    if (Directory.Exists(FicheroACortar))
                    {
                        Pegando = true;
                        ContarFicheros(FicheroACortar);
                        PanelCopiando.GetComponentInChildren<Slider>().maxValue = CantArchivos;
                        PanelCopiando.gameObject.SetActive(true);
                        PanelCopiando.GetComponentInChildren<Text>().text = CadenaMoviendo;

                        StartCoroutine(MoverDirectorio(FicheroACortar, txtCamino.text + "/" + Path.GetFileName(FicheroACortar))); // para PS4 cambiar por /
                        StartCoroutine(EliminarDirectorioMovidos(FicheroACortar));

                        FicheroACortar = "";

                        camino = txtCamino.text;
                        LimpiarTodo();
                        CrearDirectorio();
                    }
                    else
                    {
                        Pegando = true;
                        CantArchivos = 1;
                        PanelCopiando.GetComponentInChildren<Slider>().maxValue = 1;
                        PanelCopiando.gameObject.SetActive(true);
                        PanelCopiando.GetComponentInChildren<Text>().text = CadenaMoviendo;

                        StartCoroutine(MoverFichero(FicheroACortar, txtCamino.text + "/" + Path.GetFileName(FicheroACortar))); // para PS4 cambiar por /
                    }
                }
                catch (System.Exception ex)
                {
                    LOG = "Error " + ex.Message;
                    CantArchivos = 0;
                    Pegando = false;
                    SistemaSonidos.instancia.PlayError();
                }
            }
            else if (FicheroACopiar != "")
            {
                try
                {
                    if (Directory.Exists(FicheroACopiar))
                    {
                        Pegando = true;
                        ContarFicheros(FicheroACopiar);
                        PanelCopiando.GetComponentInChildren<Slider>().maxValue = CantArchivos;
                        PanelCopiando.gameObject.SetActive(true);
                        PanelCopiando.GetComponentInChildren<Text>().text = CadenaCopiando;

                        StartCoroutine(CopiarDirectorio(FicheroACopiar, txtCamino.text + "/" + Path.GetFileName(FicheroACopiar))); // para PS4 cambiar por /

                        camino = txtCamino.text;
                        LimpiarTodo();
                        CrearDirectorio();
                    }
                    else
                    {
                        Pegando = true;
                        CantArchivos = 1;
                        PanelCopiando.GetComponentInChildren<Slider>().maxValue = 1;
                        PanelCopiando.gameObject.SetActive(true);
                        PanelCopiando.GetComponentInChildren<Text>().text = CadenaCopiando;

                        StartCoroutine(CopiarFichero(FicheroACopiar, txtCamino.text + "/" + Path.GetFileName(FicheroACopiar))); // para PS4 cambiar por /
                    }
                }
                catch (System.Exception ex)
                {
                    LOG = "Error " + ex.Message;
                    CantArchivos = 0;
                    Pegando = false;
                    SistemaSonidos.instancia.PlayError();
                }
            }
            else
            {
                LOG = CadenaNoHasCopiado;
            }
        }
    }

    string TextoOriginal = "";
    public void Renombra()
    {
        if (!Multiseleccion && camino != "")
        {
            if (FicheroACopiar == camino)
                FicheroACopiar = "";

            if (FicheroACortar == camino)
                FicheroACortar = "";

            EnOpciones = false;
            PanelOpciones.SetActive(false);

            EnTeclado = true;
            PanelTeclado.gameObject.SetActive(true);

            TextoOriginal = Path.GetFileName(camino);
            PanelTeclado.GetComponentInChildren<InputField>().text = TextoOriginal;
        }
    }

    public string TextoCambiado = "";
    public void RenombrarAccion()
    {
        try
        {
            if (TextoOriginal != TextoCambiado)
            {
                if (Directory.Exists(camino))
                {
                    Directory.Move(camino, txtCamino.text + "/" + TextoCambiado); // para PS4 cambiar por /
                }
                else
                {
                    File.Move(camino, txtCamino.text + "/" + TextoCambiado); // para PS4 cambiar por /
                }

                camino = txtCamino.text;
                LimpiarTodo();
                CrearDirectorio();
            }
        }
        catch (System.Exception ex)
        {
            LOG = "Error " + ex.Message;
            SistemaSonidos.instancia.PlayError();
        }

        StartCoroutine(TecladoOff());
    }

    public bool Seguro = false;
    public void Eliminar()
    {
        if (FicheroACopiar == camino)
            FicheroACopiar = "";

        if (FicheroACortar == camino)
            FicheroACortar = "";

        EnOpciones = false;
        PanelOpciones.SetActive(false);

        if (Multiseleccion)
        {
            for (int i = 0; i < ObjetosCaminos.Count; i++)
            {
                try
                {
                    File.Delete(ObjetosCaminos[i]);
                }
                catch (System.Exception ex)
                {
                    LOG = "Error " + ex.Message;
                    SistemaSonidos.instancia.PlayError();
                }

                camino = txtCamino.text;
                LimpiarTodo();
                CrearDirectorio();
            }
        }
        else
        {
            try
            {
                if (Directory.Exists(camino))
                {
                    Directory.Delete(camino, true);
                }
                else
                {
                    File.Delete(camino);
                }

                camino = txtCamino.text;
                LimpiarTodo();
                CrearDirectorio();
            }
            catch (System.Exception ex)
            {
                LOG = "Error " + ex.Message;
                SistemaSonidos.instancia.PlayError();
            }
        }
    }

    // Auxiliares ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    int CantArchivos = 0;
    void ContarFicheros(string origen)
    {
        string[] files;
        files = Directory.GetFileSystemEntries(origen);
        foreach (string element in files)
        {
            if (Directory.Exists(element))
                ContarFicheros(element);
            else
                CantArchivos++;
        }
    }

    IEnumerator MoverDirectorio(string origen, string destino)
    {
        string[] files;
        if (destino[destino.Length - 1] != Path.DirectorySeparatorChar)
            destino += Path.DirectorySeparatorChar;
        if (!Directory.Exists(destino))
            Directory.CreateDirectory(destino);
        files = Directory.GetFileSystemEntries(origen);
        foreach (string element in files)
        {
            if (Directory.Exists(element))
                StartCoroutine(MoverDirectorio(element, destino + Path.GetFileName(element)));
            else
            {
                if (File.Exists(destino + Path.GetFileName(element)))
                    File.Delete(destino + Path.GetFileName(element));
                
                File.Move(element, destino + Path.GetFileName(element));
                CantArchivos--;

                if (CantArchivos == 0)
                    StopAllCoroutines();
            }

            yield return null;
        }
    }

    IEnumerator EliminarDirectorioMovidos(string origen)
    {
        while (CantArchivos > 0)
            yield return null;
        
        Directory.Delete(origen, true);
    }
    
    IEnumerator CopiarDirectorio(string origen, string destino)
    {
        string[] files;
        if (destino[destino.Length - 1] != Path.DirectorySeparatorChar)
            destino += Path.DirectorySeparatorChar;
        if (!Directory.Exists(destino))
            Directory.CreateDirectory(destino);
        files = Directory.GetFileSystemEntries(origen);
        foreach (string element in files)
        {
            if (Directory.Exists(element))
                StartCoroutine(CopiarDirectorio(element, destino + Path.GetFileName(element)));
            else
            {
                File.Copy(element, destino + Path.GetFileName(element), true);
                CantArchivos--;

                if (CantArchivos == 0)
                    StopAllCoroutines();
            }

            yield return null;
        }
    }

    IEnumerator MoverFichero(string origen, string destino)
    {
        yield return null;

        if (File.Exists(destino))
            File.Delete(destino);

        File.Move(origen, destino);
        CantArchivos = 0;

        FicheroACortar = "";
        camino = txtCamino.text;
        LimpiarTodo();
        CrearDirectorio();

        yield return null;
    }

    IEnumerator CopiarFichero(string origen, string destino)
    {
        yield return null;

        File.Copy(origen, destino, true);
        CantArchivos = 0;

        camino = txtCamino.text;
        LimpiarTodo();
        CrearDirectorio();

        yield return null;
    }

    IEnumerator CopiarMultiplesFichero()
    {
        yield return null;

        for (int i = 0; i < ObjetosCaminos.Count; i++)
        {
            File.Copy(ObjetosCaminos[i], txtCamino.text + "/" + Path.GetFileName(ObjetosCaminos[i]), true);
            CantArchivos--;

            yield return null;
        }

        if (CantArchivos == 0)
        {
            camino = txtCamino.text;
            LimpiarTodo();
            CrearDirectorio();
        }
    }

    IEnumerator CortarMultiplesFichero()
    {
        yield return null;

        for (int i = 0; i < ObjetosCaminos.Count; i++)
        {
            File.Move(ObjetosCaminos[i], txtCamino.text + "/" + Path.GetFileName(ObjetosCaminos[i]));
            CantArchivos--;

            yield return null;
        }

        if (CantArchivos == 0)
        {
            MultiseleccionCortada = false;
            camino = txtCamino.text;
            LimpiarTodo();
            CrearDirectorio();
        }
    }

    // Opciones avanzadas /////////////////////////////////////////////////////////////////////////////////////////////////////////
    bool FtpActivado = false;
    bool FullRwActivado = false;

    public void ActivarFTP()
    {
        try
        {
            if (!FtpActivado)
            {
                //FTP();
                FtpActivado = true;
            }

            LOG = "FTP is Active - I.P: " + Network.player.ipAddress + ", Port: 21";
        }
        catch (System.Exception ex)
        {
            LOG = "Error " + ex.Message;
            SistemaSonidos.instancia.PlayError();
        }
    }

    public void ActivarFullRW()
    {
        try
        {
            if (!FullRwActivado)
            {
                //Mount_RW();
                FullRwActivado = true;
            }

            LOG = "Full R/W Permisions in systems folders (dangerous, be careful !)";
        }
        catch (System.Exception ex)
        {
            LOG = "Error " + ex.Message;
            SistemaSonidos.instancia.PlayError();
        }
    }

    public void ExportarPFS_Map()
    {
        
    }
}