using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PoolManager PoolManager_ = new PoolManager();
    public ResourceManager ResourceManager_ = new ResourceManager();
}