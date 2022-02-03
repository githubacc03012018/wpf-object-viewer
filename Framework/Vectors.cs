using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

        }
        public Vector3()
        {

        }
    }

    public class Vector2
    {
        public float u;
        public float v;


        public Vector2(float u, float v)
        {
            this.u = u;
            this.v = v;

        }
        public Vector2()
        {

        }

    }

}
