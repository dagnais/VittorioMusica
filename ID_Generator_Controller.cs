/*
Titulo: "VittorioMusica"
Hecho en el año:2018 
-----
Title: "VittorioMusica"
Made in the year: 2018
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID_Generator_Controller : MonoBehaviour
{
    public bool isSaveIndexs;
    public Dictionary<string, int> myIDs= new Dictionary<string, int>();
    public MySQL_Script mySqlDB;

    void Start()
    {
        IndexsInicialization();
    }

    void IndexsInicialization()
    {
        if (!mySqlDB.isConnInternet)
        {
            myIDs.Add(DataBaseEstructure.tableStock, PlayerPrefs.GetInt(DataBaseEstructure.tableStock, mySqlDB.GetIndex(DataBaseEstructure.tableStock,
                DataBaseEstructure.idStock)));
            myIDs.Add(DataBaseEstructure.tableCodigo, PlayerPrefs.GetInt(DataBaseEstructure.tableCodigo, mySqlDB.GetIndex(DataBaseEstructure.tableCodigo,
                DataBaseEstructure.idCodigo)));
        }
        else
        {
            myIDs.Add(DataBaseEstructure.tableStock, mySqlDB.GetIndex(DataBaseEstructure.tableStock,
                DataBaseEstructure.idStock));
            myIDs.Add(DataBaseEstructure.tableCodigo, mySqlDB.GetIndex(DataBaseEstructure.tableCodigo,
                DataBaseEstructure.idCodigo));
        }
        SaveIndexs(DataBaseEstructure.tableStock, myIDs[DataBaseEstructure.tableStock]);
        SaveIndexs(DataBaseEstructure.tableCodigo, myIDs[DataBaseEstructure.tableCodigo]);
    }

    public int GenerateID(string table)
    {
        int value= PlayerPrefs.GetInt(table,0);
        Debug.Log("value: " + value);

        if (myIDs.TryGetValue(table, out value))
        {
            value++;
            myIDs[table]= value;
            Debug.Log("Index añadido con éxito: "+ value);
            return value;
        }
        else
        {
            Debug.LogError("tabla no encontrada <diccionario>");
            return 0;
        }
    }

    public void SaveIndexs(string table,int lastIndex)
    {

        PlayerPrefs.SetInt(table, lastIndex);
    }
}
