using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MyZIPControl : MonoBehaviour
{
    IFileFormatter fileformatter;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    static void ReadZip(string path)
    {
        
        using (ZipArchive archive = ZipFile.OpenRead(path))
        {
            foreach (var item in archive.Entries)
            {
                //load in directory
                Debug.Log(item.FullName);
            }
        }
    }

    UnityEngine.Object Deserialize(string path)
    {
        List<string> data = new List<string>();
        string extension = new FileInfo(path).Extension.ToString().Trim();

        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default))
            {
                while (sr.Peek() > -1)
                {
                    data.Add(sr.ReadLine());
                }
            }
        }
        fileformatter.Parsing(data);
        UnityEngine.Object a = Activator.CreateInstance(Type.GetType(fileformatter.TypeStr(extension))) as UnityEngine.Object;
        return a;
    }
}

public interface IFileFormatter
{
    void Parsing(List<string> data);
    string TypeStr(string extension);
}


public class AssetsLoader<T>
{
    public T Load(string path)
    {
        using (FileStream fs = File.OpenRead(path))
        {
            var x = new BinaryFormatter().Deserialize(fs);
            return (T)x;
        }
    }
}