using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Define
    {
        private static Camera _mainCamera;
        public static Camera MainCamera
        {
            get=> _mainCamera;
            set=> _mainCamera = value;
        }
    }
}

