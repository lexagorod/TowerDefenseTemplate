using UnityEngine;

//создается конфиг txt файл при первой загрузке игры, возможно поменяет количество доп противников в волне waveRandomFactor и кулдаун между волнами
[System.Serializable]
public class WaveData
{
      public int waveCD;
      public int waveRandomFactor;
      public WaveData(int i, int k)
       {
        //ограничивает кд до 5 и 40 секунд
           waveCD = Mathf.Clamp(i, 5, 40);
        //ограничивает количество до противников до 1 и 5
           waveRandomFactor = Mathf.Clamp(k, 1, 5);
       }       
}
    

