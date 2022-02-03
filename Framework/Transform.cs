using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class Transform
    {
        private Vector3 rotation;
        private Vector3 position;

        public Vector3 Position
        {
            get
            {
                if(this.position == null)
                {
                   return this.position = new Vector3();
                }
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                if(this.rotation == null)
                {
                    return this.rotation = new Vector3();
                }
                return this.rotation;
            }
            set
            {
                this.rotation = value;
            }
        }
    }
}
