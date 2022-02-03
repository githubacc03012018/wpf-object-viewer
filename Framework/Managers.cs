using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{ 
    public class ObjectManager  
    {
        public static Object currentSelectedObject;

        private ObjectRepository repo = new ObjectRepository();
        public ObjectRepository ObjectRepository
        {
            get
            {
                return this.repo;
            }
        }
       
    } 

}
