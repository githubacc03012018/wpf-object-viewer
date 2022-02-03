using SharpGL;
using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class Renderer
    {
        static double inter = 0.0;
        private static ObjectManager objManager = new ObjectManager();
        public static ObjectManager ObjectManager
        {
            get
            {
                return Renderer.objManager;
            }
        }

        public static void Draw(OpenGL GL)
        {
            if (objManager.ObjectRepository.Objects.Count == 0)
            {
                return;
            }

            GL.LoadIdentity();

            GL.Translate(0.0f, 0.0f, -6.0f);
          
            foreach (Object obj in ObjectManager.ObjectRepository.Objects)
            {
                GL.PushMatrix();
                GL.Translate(obj.Transform.Position.x, obj.Transform.Position.y, obj.Transform.Position.z);
                GL.Rotate(obj.Transform.Rotation.x, obj.Transform.Rotation.y, 0);

                GL.Begin(SharpGL.Enumerations.BeginMode.Triangles);

                for (int i = 0; i < obj.Vertices.Count(); i++)
                {
                    GL.Normal(obj.Colors[i].x, obj.Colors[i].y, obj.Colors[i].z);
                    GL.Color(0.2+(obj.Colors[i].x * Math.Sin(inter)) /4, 0.2+(obj.Colors[i].y * Math.Sin(inter)) / 4, 0.2+(obj.Colors[i].z * Math.Sin(inter)) /4);
                   // GL.TexCoord(obj.Textures[i].u, obj.Textures[i].v);
                    GL.Vertex(obj.Vertices[i].x, obj.Vertices[i].y, obj.Vertices[i].z);
                }
            
                GL.End();
                GL.PopMatrix();
                
               
            }
            Renderer.inter += 0.03;
 
        }

        public static Object LoadFromString(string s)
        {
            bool success = true;
            List<string> lines = new List<string>(s.Split('\n'));
            List<Vector3> orderedVertices = new List<Vector3>();
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> textures = new List<Vector2>();
            List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();
        
            foreach (string line in lines)
            {
                if (line.StartsWith("v "))
                {
                    string temp = line.Substring(2);

                    Vector3 vec = new Vector3();

                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a vertex
                    {
                        string[] vertparts = temp.Split(' ');

                        // Attempt to parse each part of the vertice
                        success = float.TryParse(vertparts[0], out vec.x);
                        success |= float.TryParse(vertparts[1], out vec.y);
                        success |= float.TryParse(vertparts[2], out vec.z);
                     

                        // If any of the parses failed, report the error
                        if (!success)
                        {
                            Console.WriteLine("Error parsing vertex: {0}", line);
                        }
                    }

                    vertices.Add(vec);
                }

                else if (line.StartsWith("f "))
                {

                    // Cut off beginning of line
                    string temp = line.Substring(2);
                    List<string> f = new List<string>();
                    Tuple<int, int, int> face = new Tuple<int, int, int>(0, 0, 0);
                    string[] noBackLash;
                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a face
                    {
                        string[] faceparts = temp.Split(' ');

                        int i1, i2, i3;

                        // Attempt to parse each part of the face


                        foreach (string str in faceparts)
                        {
                            noBackLash = str.Split('/');
                            f.Add(noBackLash[0]);

                        }


                        // Some special case?
                        if (faceparts.Length > 3)
                        {
                            success = int.TryParse(f[0], out i1);
                            success |= int.TryParse(f[2], out i2);
                            success |= int.TryParse(f[3], out i3);
                        }

                        else
                        {
                            success = int.TryParse(f[0], out i1);
                            success |= int.TryParse(f[1], out i2);
                            success |= int.TryParse(f[2], out i3);

                        }
                        // If any of the parses failed, report the error
                        if (!success)
                        {
                            Console.WriteLine("Error parsing face: {0}", line);
                        }
                        else
                        {
                            // Decrement to get zero-based vertex numbers
                            face = new Tuple<int, int, int>(i1 - 1, i2 - 1, i3 - 1);
                            faces.Add(face);

                        }
                    }
                }
                else if (line.StartsWith("vt "))
                {
                    Vector2 tex = new Vector2();
                    string temp = line.Substring(2);
                    string[] textureParts = temp.Split(' ');
                    float ux;
                    float uy;
                    success = float.TryParse(textureParts[1], out tex.u);
                    success |= float.TryParse(textureParts[2], out tex.v); 

                    if (!success)
                    {
                        Console.WriteLine("Error parsing uv coordinate: {0}", line);
                    }

                    textures.Add(tex);
                }
            }
            foreach (Tuple<int, int, int> tuple in faces)
            {
                orderedVertices.Add(vertices[tuple.Item1]);
                orderedVertices.Add(vertices[tuple.Item2]);
                orderedVertices.Add(vertices[tuple.Item3]);
            }

            Object obj = new Object();
            obj.Transform = new Transform();
            obj.Vertices = orderedVertices; 
            obj.Faces = faces;
            obj.Colors = orderedVertices;
            obj.Textures = textures;
            obj.Guid = new Guid();
            return obj;
        } 

    }
}
