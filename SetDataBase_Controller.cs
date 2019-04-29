/*
Titulo: "VittorioMusica"
Hecho en el año:2018 
-----
Title: "VittorioMusica"
Made in the year: 2018
*/
using UnityEngine;
using UnityEngine.UI;

public class SetDataBase_Controller : MonoBehaviour
{
    public GameObject[] sceneObjs;
    public InputField[] dataInputs;

    public MySQL_Script mysql;
    public Text consoleText;

    void Start()
    {
        if (PlayerPrefs.GetInt("Habilitado", 0) == 1)
        {
            mysql.LoadConnection();
            DoneValidation();
        }
    }

    public void DataValidate()
    {
        mysql.server= dataInputs[0].text;
        mysql.dataBase = dataInputs[1].text;
        mysql.user= dataInputs[2].text;
        mysql.password= dataInputs[3].text;
        mysql.SaveConnection();
        consoleText.text = "Conectando con el servidor...";
        Invoke("Validating", 1);
    }
    void Validating()
    {
        bool value = mysql.CheckConnection();
        if (value)
        {
            DoneValidation();
        }
        else
        {
            Reset();
            consoleText.text = "Fallo de conexion, revise los datos ingresados.";
            Debug.LogError("Fallo de conexion");
        }
    }

    void DoneValidation()
    {
        for (int i = 0; i < sceneObjs.Length; i++)
        {
            sceneObjs[i].SetActive(true);
        }
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("Habilitado", 1);

        mysql.DBConnect();
        mysql.CloseConection();
        mysql.DB_SelectStock(true, null);
    }

    //recibe los datos del select
    public void ReceiveResponse(string[] result)
    {
        mysql.StockSelectTable(result);
    }

    public void Reset()
    {
        for (int i = 0; i < sceneObjs.Length; i++)
        {
            sceneObjs[i].SetActive(false);
        }
        for (int i = 0; i < dataInputs.Length; i++)
        {
            dataInputs[i].text = string.Empty;
        }
    }
}
