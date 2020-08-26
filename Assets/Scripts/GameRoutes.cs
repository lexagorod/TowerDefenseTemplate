using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//все воможные пути назначаются через Эдитор вручную
public class GameRoutes : MonoBehaviour
{

    public static GameRoutes instance;

    [SerializeField]
    public Route[] routes;

    public void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    public class Route
    {
        [SerializeField]
        public GameObject[] route;

        public GameObject[] populateRoutePoints()
        {
            return route;
        }
    }

}


