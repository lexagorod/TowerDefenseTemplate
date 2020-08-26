using UnityEngine;
using System.IO;
using BayatGames.Serialization.Formatters.Json;

//создается конфиг txt файл при первой загрузке игры,  дает возможность поменять количество доп противников в волне waveBias и кулдаун между волнами
public static class ConfigLoader
{
    [RuntimeInitializeOnLoadMethod]
    public static WaveData LoadData()
    {
        string path = Application.dataPath + "/WaveData.txt";
        if (File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                WaveData wd = ( WaveData )JsonFormatter.DeserializeObject ( json, typeof ( WaveData ) );
                 return wd;
            }
           
        }
        else
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            
            using(StreamWriter writer = new StreamWriter(fileStream))
            {
                WaveData wd = new WaveData(20, 4);
                writer.Write(JsonFormatter.SerializeObject (wd));
                return wd;
            }
        }
    }
}
