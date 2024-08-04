using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Interface
{
    public interface IPoolableObject
    {
        int ObjectID { get; }
        Transform Transform { get; }
    }
}