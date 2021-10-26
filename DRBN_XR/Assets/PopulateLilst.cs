using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PopulateLilst : MonoBehaviour
{
    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
    }

    void MoleculeCensus() { 
    }

    void PopulateList (){

        //test list
        //List<string> molecules = new List<string>() {"toto","caca","pipoudou", "poupoupidou" };

        List<string> molecules = new List<string>() { };

        string[] moleculesprefab_dir = System.IO.Directory.GetDirectories("Assets/DRBN_STEAMVR/Prefab/Simulation/PDBs_To_Scale/");
        //string[] moleculesprefab = System.IO.Directory.GetFiles("Assets/DRBN_STEAMVR/Prefab/Simulation/PDBs_To_Scale/");
        //string[] moleculesprefab = System.IO.Directory.GetFiles("Assets /");
        foreach (string dir in moleculesprefab_dir)
        {
            Debug.Log(dir + " directory");
            string[] moleculesprefab = System.IO.Directory.GetFiles(dir,"i*.prefab");
            foreach (string file in moleculesprefab)
            {
                {
                    Debug.Log(file + " filename");
                    molecules.Add(System.IO.Path.GetFileName(file));
                    //Debug.Log(molecules);
                }
            }
        }

        dropdown.AddOptions(molecules);
    }
}
