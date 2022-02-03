using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using Framework;
    using Microsoft.Win32;
    using System.IO;
    using SharpGL;
    using SharpGL.Shaders;
    using SharpGL.VertexBuffers;
    using System.Drawing.Imaging;
    using System.Reflection;

    public partial class MainWindow : Window
    {
        System.Windows.Point prevMousePosition = new System.Windows.Point();
        float moveSmoothing = 0.05f;
        float scroolSmoothing = 0.05f;
        SharpGL.OpenGL GL;
        bool isLightEnabled = true;
        public MainWindow()
        {
            InitializeComponent();
            MouseMove += MainWindow_MouseMove;
            MouseWheel += MainWindow_MouseWheel;
            KeyDown += button_pressed;
            this.SceneTree.SelectedItemChanged += SceneTree_Selected;
        }

        private void button_pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                Renderer.ObjectManager.ObjectRepository.Objects.Remove(ObjectManager.currentSelectedObject);

                this.SceneTree.Items.Remove(this.SceneTree.SelectedItem);
            }
            if (e.Key == Key.A)
            {
                GL.Enable(OpenGL.GL_LIGHTING);
                if (isLightEnabled)
                {
                    GL.Disable(OpenGL.GL_LIGHTING);
                }
                isLightEnabled = !isLightEnabled;

            }
        }

        private void SceneTree_Selected(object sender, RoutedEventArgs e)
        {
            if (this.SceneTree.Items.Count != 0)
            {
                ObjectManager.currentSelectedObject = ((RendererTreeViewItem)((TreeView)sender).SelectedItem).Object;
            }

        }

        private void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        { 
            if (ObjectManager.currentSelectedObject == null)
            {
                return;
            }

            if (e.Delta > 0)
            {
                ObjectManager.currentSelectedObject.Transform.Position.z += e.Delta * scroolSmoothing;

            }
            else
            {
                ObjectManager.currentSelectedObject.Transform.Position.z += e.Delta * scroolSmoothing;

            }

        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Renderer.ObjectManager.ObjectRepository.Objects.FirstOrDefault() == null)
                {
                    return;
                }

                ObjectManager.currentSelectedObject.Transform.Rotation.x += (float)(Mouse.GetPosition(Application.Current.MainWindow) - prevMousePosition).Y;
                ObjectManager.currentSelectedObject.Transform.Rotation.y += (float)(Mouse.GetPosition(Application.Current.MainWindow) - prevMousePosition).X;
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                ObjectManager.currentSelectedObject.Transform.Position.x += (float)(Mouse.GetPosition(Application.Current.MainWindow) - prevMousePosition).X * moveSmoothing;
                ObjectManager.currentSelectedObject.Transform.Position.y -= (float)(Mouse.GetPosition(Application.Current.MainWindow) - prevMousePosition).Y * moveSmoothing;
            }

            prevMousePosition = Mouse.GetPosition(Application.Current.MainWindow);
        }

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

            GL = args.OpenGL;

            GL.ClearColor(0.69f, 0.69f, 0.69f, 0.5f);
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            Renderer.Draw(args.OpenGL);

            GL.Flush();

        }


        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            GL = args.OpenGL;

            //TODO: Implement init thru the framework


            GL.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] { 1.2f, 1.2f, 1.2f, 1.0f };
            float[] light0pos = new float[] { 0.0f, 5.0f, 5.0f, 1.0f };
            float[] light0ambient = new float[] { 0.4f, 0.2f, 0.2f, 1.0f };
            float[] light0diffuse = new float[] { 0.1f, 0.3f, 0.2f, 1.0f };
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            GL.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            GL.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            GL.Enable(OpenGL.GL_LIGHTING);
            GL.Enable(OpenGL.GL_LIGHT0);

            GL.ShadeModel(OpenGL.GL_SMOOTH);



            GL.Enable(OpenGL.GL_TEXTURE_2D);

             
        }
      
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Object files (*.obj)|*.obj";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {

                    RendererTreeViewItem newItem = new RendererTreeViewItem();
                    newItem.Header = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    newItem.IsSelected = true;

                    newItem.Object = Renderer.ObjectManager.ObjectRepository.LoadObject(Renderer.LoadFromString(File.ReadAllText(openFileDialog.FileName)));
                    ObjectManager.currentSelectedObject = newItem.Object;

                    this.SceneTree.Items.Add(newItem);
                }
                catch (NotSupportedException notSupportedEx)
                {
                    MessageBox.Show(notSupportedEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
