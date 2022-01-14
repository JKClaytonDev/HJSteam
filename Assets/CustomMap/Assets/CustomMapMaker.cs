using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using System.IO;

public class CustomMapMaker : MonoBehaviour
{


    public GameObject[] prefabs;
    public char[] charPrefabs;
    public bool useData;

    

    public Vector3[] PositionInput;
    public Vector3[] RotationInput;
    public Vector3[] ScaleInput;
    public char[] PrefabInput;

    public String pos;
    public String rot;
   public String scl;
    public String pre;

    public String fullString;// = "-1.374414X-0.8481967Y2.455494Z-0.3645067X-0.7204889Y2.433939Z0.6519375X-0.8481967Y2.557558Z-0.4820854X-0.8481967Y-1.413494Z-1.317394X-0.714544Y1.426412Z-0.3544734X-0.4260125Y1.366474Z0.6471774X-0.6812739Y1.477234Z0.6500386X-0.8481967Y0.4478747Z-0.3684473X-0.7548525Y0.250568Z-1.341787X-0.8481967Y0.329258Z/12.20415X8.538605E-05Y31.8426Z29.74345X2.747152E-05Y2.899184Z19.85701X-4.084853E-06Y334.3722Z0X0Y0Z356.1915X-9.626212E-07Y39.04594Z358.1652X266.4541Y0.3456617Z2.93565X-1.709791E-06Y329.8893Z344.2798X-6.43038E-06Y337.9081Z332.459X1.384151E-06Y357.3143Z338.106X-6.624994E-05Y34.05312Z/1X-0.0504327Y1Z1X-0.0504327Y1Z1X-0.0504327Y1Z-3.367577X-0.0504327Y0.9465993Z1X-0.0504327Y1Z1X-0.0504327Y1Z1X-0.0504327Y1Z1X-0.0504327Y1Z1X-0.0504327Y1Z1X-0.0504327Y1Z/";
    public Vector3[] PositionOutput;
    public Vector3[] RotationOutput;
    public Vector3[] ScaleOutput;

    public GameObject cube;
    public string directory;

    int ind;
    Vector3 storedValues;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<MapData>())
        {
            if (FindObjectOfType<MapData>().mapData != null)
            {
                Debug.Log("PLAYERPREFS " + FindObjectOfType<MapData>().mapData);
                useData = true;
                String mapData = FindObjectOfType<MapData>().mapData;
                Debug.Log("MAPDATA IS " + mapData);
                Destroy(FindObjectOfType<MapData>().gameObject);

                directory = mapData;
                StreamReader reader = new StreamReader(mapData);
                fullString = reader.ReadToEnd();
                reader.Close();
            }

        }
        Debug.Log("FULL STRING IS " + fullString);
        //fullString = "-30.27101X14.5217Y-10.81186Z-14.50608X14.5217Y-4.564064Z-10.31901X14.5217Y-34.71118Z-37.15583X14.5217Y-29.82719Z-28.77061X14.5217Y-4.676342Z-24.8073X14.5217Y-13.02653Z-37.88554X16.0089Y-39.40115Z-38.64703X16.0089Y-24.26074Z-38.92862X16.0089Y-16.7376Z-26.69512X16.0089Y-25.2665Z-18.81292X16.0089Y0.990004Z-12.41178X16.0089Y-7.487002Z-11.13545X16.0089Y1.074224Z-2.950316X16.0089Y0.9938182Z5.223811X16.0089Y2.035483Z5.140402X16.0089Y-6.161688Z5.84111X16.0089Y-30.9028Z5.826344X16.0089Y-14.45961Z-4.724412X16.0089Y-14.50351Z-4.698494X16.0089Y-30.72484Z-6.146395X16.0089Y-41.09391Z-12.74958X16.0089Y-40.74033Z-12.47205X16.0089Y-26.59749Z-31.389X16.0089Y-41.71947Z-31.38224X16.0089Y-33.77682Z-20.3907X16.0089Y-33.98012Z-20.49502X16.0089Y-41.60721Z-2.863107X14.5217Y-42.55065Z0.6242334X14.5217Y-22.53393Z3.742692X14.5217Y-34.9351Z-8.569794X14.5217Y-20.7221Z-2.22038X14.5217Y-10.78755Z-8.715494X14.5217Y-14.17643Z-15.24412X14.5217Y-22.4678Z-16.64097X14.5217Y-37.49938Z-35.65406X14.5217Y-29.267Z-15.95224X14.5217Y2.922847Z-22.56469X14.5217Y-15.43708Z-29.07545X14.5217Y-29.81738Z-30.22649X14.5217Y2.736294Z-37.543X14.5217Y-13.07489Z-16.00686X16.0089Y-14.68404Z-15.3405X14.5217Y4.775585Z-15.3405X14.5217Y-19.56768Z-41.73492X14.5217Y-20.18753Z-15.3405X14.5217Y-45.37606Z8.495617X14.5217Y-20.18753Z/0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X90Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X0Y0Z0X90Y0Z0X0Y0Z0X0Y0Z0X90Y0Z0X0Y0Z0X90Y0Z/-1.267717X6.183738Y-5.712648Z3.908869X6.183738Y-1.448348Z3.908869X6.183738Y-1.448348Z3.908869X6.183738Y-1.448348Z3.908869X6.183738Y-1.448348Z3.908869X6.183738Y-1.448348Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z5.55652X5.556516Y5.55652Z1.164815X6.183738Y16.37085Z6.593985X6.183738Y-10.58787Z14.16692X6.183738Y-1.126995Z1.722847X6.183738Y29.65098Z14.16692X6.183738Y-1.126995Z1.164815X6.183738Y16.37085Z14.16692X6.183738Y-1.126995Z1.164815X6.183738Y16.37085Z1.164815X6.183738Y16.37085Z1.164815X6.183738Y16.37085Z1.722847X6.183738Y29.65098Z14.16692X6.183738Y-1.126995Z1.164815X6.183738Y16.37085Z14.16692X6.183738Y-1.126995Z5.55652X5.556516Y5.55652Z56.35015X6.183738Y0.5715032Z56.35015X0.220981Y56.35015Z56.35017X6.183738Y0.5715039Z56.35015X6.183738Y0.5715032Z56.35017X6.183738Y0.5715039Z/AAAAAABBBBBBBBBBBBBBBBBBBBBAAAAAAAAAAAAAABAAAAA";
        pos = "A";
        rot = "B";
        scl = "K";
        Debug.Log(pos + rot + scl + "String " + fullString);
        int val = 0;
        String full = "";
        foreach (char c in fullString)
        {
            if (c == '/')
            {
                if (val == 0)
                    pos = full;
                if (val == 1)
                    rot = full;
                if (val == 2)
                    scl = full;
                full = "";
                val++;
            }
            else
                full += c;
        }
        Debug.Log(pos + rot + scl);
        if (!useData)
        compile();
        decompile();
        if (useData)
            makeMap();
    }

    public void compile()
    {
        pos = "";
        foreach (Vector3 v in PositionInput)
        {
            pos += v.x;
            pos += "X";
            pos += v.y;
            pos += "Y";
            pos += v.z;
            pos += "Z";
        }

        rot = "";
        foreach (Vector3 v in RotationInput)
        {
            rot += v.x;
            rot += "X";
            rot += v.y;
            rot += "Y";
            rot += v.z;
            rot += "Z";
        }

        scl = "";
        foreach (Vector3 v in ScaleInput)
        {
            scl += v.x;
            scl += "X";
            scl += v.y;
            scl += "Y";
            scl += v.z;
            scl += "Z";
        }
    }
    public void decompile()
    {
        PositionOutput = new Vector3[255];
        RotationOutput = new Vector3[255];
        ScaleOutput = new Vector3[255];
        //Position
        ind = 0;
        Vector3 value = new Vector3();
        char[] s = pos.ToCharArray();
        for (int i = 0; i < s.Length; i++)
        {
            String ks = "";
            for (int k = i; !char.IsLetter(s[i]) && i < s.Length; i++)
                ks += (s[i]);
            if (s[i] == 'X')
                value.x = (float)Convert.ToDouble(ks);
            if (s[i] == 'Y')
                value.y = (float)Convert.ToDouble(ks);
            if (s[i] == 'Z')
            {
                value.z = (float)Convert.ToDouble(ks);
                ks = "";
                if (ind < PositionOutput.Length)
                PositionOutput[ind] = value;
                ind++;
            }
        }

        //Rotation
        ind = 0;
        value = new Vector3();
        s = rot.ToCharArray();
        for (int i = 0; i < s.Length; i++)
        {
            String ks = "";
            for (int k = i; !char.IsLetter(s[i]) && i < s.Length; i++)
                ks += (s[i]);
            if (s[i] == 'X')
                value.x = (float)Convert.ToDouble(ks);
            if (s[i] == 'Y')
                value.y = (float)Convert.ToDouble(ks);
            if (s[i] == 'Z')
            {
                value.z = (float)Convert.ToDouble(ks);
                ks = "";
                if (ind < RotationOutput.Length)
                    RotationOutput[ind] = value;
                ind++;
            }
        }

        //Scale
        ind = 0;
        value = new Vector3();
        s = scl.ToCharArray();
        for (int i = 0; i < s.Length; i++)
        {
            String ks = "";
            for (int k = i; !char.IsLetter(s[i]) && i < s.Length; i++)
                ks += (s[i]);
            if (s[i] == 'X')
                value.x = (float)Convert.ToDouble(ks);
            if (s[i] == 'Y')
                value.y = (float)Convert.ToDouble(ks);
            if (s[i] == 'Z')
            {
                value.z = (float)Convert.ToDouble(ks);
                ks = "";
                if (ind < ScaleOutput.Length)
                    ScaleOutput[ind] = value;
                ind++;
            }
        }
    }

    public void makeMap()
    {
        String found = "";
        int spot = fullString.Length - 1;
        while (Array.IndexOf(charPrefabs,fullString.ToCharArray()[spot]) > -1)
        {
            found = fullString.ToCharArray()[spot] + found;
            spot--;
        }
        Debug.Log("LETTERS ARE " + found);
        for (int i = 0; i < ScaleOutput.Length; i++) {
            if (ScaleOutput[i] != new Vector3())
            {
                GameObject ci = Instantiate(prefabs[Array.IndexOf(charPrefabs, found[i])]);
                Debug.Log("CREATED PREFAB " + prefabs[Array.IndexOf(charPrefabs, found[i])].name);
                ci.transform.parent = null;
                ci.transform.position = PositionOutput[i]+new Vector3(0.1f, 0.1f, 0.1f);
                ci.transform.localEulerAngles = RotationOutput[i];
                ci.transform.localScale = ScaleOutput[i];
            }
        }
    }
    public void saveMap()
    {

        CubeMover[] c = FindObjectsOfType<CubeMover>();
        PositionInput = new Vector3[c.Length];
        RotationInput = new Vector3[c.Length];
        ScaleInput = new Vector3[c.Length];
        PrefabInput = new char[c.Length];
        int posi = 0;
        foreach (CubeMover k in c)
        {
            PositionInput[posi] = k.gameObject.transform.position;
            RotationInput[posi] = k.gameObject.transform.localEulerAngles;
            ScaleInput[posi] = k.gameObject.transform.localScale;
            
            PrefabInput[posi] = k.gameObject.GetComponent<CubeMover>().letter;
            posi++;
        }
        
        compile();
        String charString = "";
        foreach (char ch in PrefabInput)
            charString += ch;
        fullString = (pos + "/" + rot + "/" + scl + "/" + charString);
        pos = "";
        rot = "";
        scl = "";

        GUIUtility.systemCopyBuffer = (fullString);
        System.IO.File.WriteAllText(directory, fullString);

    }
}
