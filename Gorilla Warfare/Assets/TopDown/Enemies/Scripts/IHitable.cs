using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public interface IHitable
    {
        void TakeDamage(int damage);
    }
}
