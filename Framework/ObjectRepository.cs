using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class ObjectRepository
    {
        private List<Object> objects = new List<Object>();

        public List<Object> Objects
        {
            get
            {
                return this.objects;
            }
        }
        public Object LoadObject(Object obj)
        { 
            Objects.Add(obj);
            return obj;
        }
    }
}
