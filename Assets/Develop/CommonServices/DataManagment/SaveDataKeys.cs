using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Develop.CommonServices.DataManagment
{
    public static class SaveDataKeys
    {
        private static Dictionary<Type, string> Keys = new Dictionary<Type, string>()
        {
        };

        public static string GetKeyFor<TData>() 
            => Keys[typeof(TData)];
    }
}