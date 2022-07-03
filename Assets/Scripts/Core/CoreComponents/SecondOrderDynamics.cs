using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Core.CoreComponents
{ 
    //Доделать 
    public class SecondOrderDynamics:MonoBehaviour
    {
        private Vector<float> _xp;
        private Vector<float> _y, _yd;
        private float _k1, _k2, _k3;

        public SecondOrderDynamics(float f,float z,float r, Vector<float> x0)
        {
            _k1 = z / (Mathf.PI * f);
            _k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
            _k3 = r * z / (2 * Mathf.PI * f);

            _xp = x0 ;
            _y = x0;
            _yd= Vector<float>.Zero;

        }
    
        // public Vector<float> Update(float T, Vector<float> x, Vector<float> xd=default)
        // {
        //     xd = (x - _xp) / T;
        //     _xp = x;
        //
        //     var k2Stable = Mathf.Max(_k2, T * T / 2 + T * _k1 / 2, T * _k1);
        //     _y += T * _yd;
        //     _yd += T * (x + _k3 * xd - _y - _k1 * _yd) / k2Stable;
        //     return _y;
        // }
    }
}
