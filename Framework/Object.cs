using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
   public class Object
    {
        //Basic object.
        
        private List<Vector3> colors = new List<Vector3>();
        public List<Vector3> Colors
        {
            get
            {
                return this.colors;
            }
            set
            {
                this.colors = value;
            }
        }

        private List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();
        public List<Tuple<int,int,int>> Faces
        {
            get
            {
                return this.faces;
            }
            set
            {
                this.faces = value;
            }
        }
        private List<Vector3> verts = new List<Vector3>();
        public List<Vector3> Vertices
        {
            get
            {
                return this.verts;
            }
            set
            {
                this.verts = value;
            }
        }

        private Transform transform = new Transform();
        public Transform Transform
        {
            get
            {
                return this.transform;
            }
            set
            {
                this.transform = value;
            }
        }
        public Guid Guid
        {
            get; set;
        }
        private List<Vector2> textures = new List<Vector2>();
        public List<Vector2> Textures
        {
            get
            {
                return this.textures;
            }
            set
            {
                this.textures = value;
            }
        }
    }
}
