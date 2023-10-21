using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Proyecto1_01;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    public class Game : GameWindow
    {
        //static Punto p = new Punto(0, 0, 0);
        //Objeto pared = new Objeto(15 , 0, 0);
        //Objeto repisa = new Objeto(15 , 0, 0);
        //Objeto auto = new Objeto(15 , 0, 0 );
        Escenario escena1 = new Escenario();
        Escenario escena2 = new Escenario();
        int cnt = 0;
        float ace = 0;

        private Camera camera;
        //-----------------------------------------------------------------------------------------------------------------
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            camera.Update(e);
            base.OnUpdateFrame(e);
            // capture a keyboard
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            //---------------------------------------//
            //auto
            if (keyboardState.IsKeyDown(Key.Keypad4))//Left
            {
                escena2.objetos["auto"].Trasladar(-1, 0, 0);
            }
            if (keyboardState.IsKeyDown(Key.Keypad6))//Right
            {
                escena2.objetos["auto"].Trasladar(1, 0, 0);
            }
            if (keyboardState.IsKeyDown(Key.Keypad2))//Up
            {
                escena2.objetos["auto"].Trasladar(0, 1, 0);
            }
            if (keyboardState.IsKeyDown(Key.Keypad8))//Down
            {
                escena2.objetos["auto"].Trasladar(0, -1, 0);
            }
            if (keyboardState.IsKeyDown(Key.KeypadPlus))//Escalar a mas
            {
                escena2.objetos["auto"].Escalar(1.1f);
            }
            if (keyboardState.IsKeyDown(Key.KeypadMinus))//Escalar a menos
            {
                escena2.objetos["auto"].Escalar(0.9f);
            }
            //---------------------------------------------------
            //Escenario
            if (keyboardState.IsKeyDown(Key.A))//Left
            {
                escena2.Trasladar(-1, 0, 0);
            }
            if (keyboardState.IsKeyDown(Key.D))//Right
            {
                escena2.Trasladar(1, 0, 0);
            }
            if (keyboardState.IsKeyDown(Key.W))//Up
            {
                escena2.Trasladar(0, 1, 0);
            }
            if (keyboardState.IsKeyDown(Key.S))//Down
            {
                escena2.Trasladar(0, -1, 0);
            }
            if (keyboardState.IsKeyDown(Key.Q))//Z positivo
            {
                escena2.Trasladar(0, 0, 1);
            }
            if (keyboardState.IsKeyDown(Key.E))//Z negativo
            {
                escena2.Trasladar(0, 0, -1);
            }
            if (keyboardState.IsKeyDown(Key.M))//Escalar a mas
            {
                escena2.Escalar(1.1f);
            }
            if (keyboardState.IsKeyDown(Key.N))//Escalar a menos
            {
                escena2.Escalar(0.9f);
            }
            if (keyboardState.IsKeyDown(Key.X))//Rotar
            {
                escena2.Rotar(5, 'x');
            }
            if (keyboardState.IsKeyDown(Key.Y))//Z positivo
            {
                escena2.Rotar(5, 'y');
            }
            if (keyboardState.IsKeyDown(Key.Z))//Z positivo
            {
                escena2.Rotar(5, 'z');
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.SeaGreen);

            // Configura la cámara con la posición inicial y velocidad
            Vector3 initialPosition = new Vector3(0, 0, 3); // Posición inicial
            Vector3 initialFront = new Vector3(0, 0, -1);  // Dirección hacia donde mira
            Vector3 initialUp = Vector3.UnitY; // Vector "arriba"
            float cameraSpeed = 0.08f; // Velocidad de movimiento
            /// Serializar
            //escena2.AdicionarObjeto("pared", pared);
            //Utilidades.Guardar<Escenario>(escena2, "escena2.json");
            //moviendo
            //escena1 = new Escenario(Utilidades.Cargar<Escenario>("escena2.json"));
            
            escena2 = new Escenario(Utilidades.Cargar<Escenario>("escena1.json"));
            
            //Utilidades.Guardar<Escenario>(escena2, "escena1.txt");
            //escena2.mover(new Punto(15, 15, 15));
            //escena2.objetos["auto"].mover(new Punto(15, 0, 0) );

            //escena2.objetos["auto"].partes["llantas"].mover();
            //escena2.objetos["auto"].Dibujar(); 
           ///
            camera = new Camera(initialPosition, initialFront, initialUp, cameraSpeed);
            //////////////////////////////////////////////////////////////////////////
            base.OnLoad(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //GL.DeleteBuffer(VertexBufferObject);
            base.OnUnload(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //GL.DepthMask(true);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.LoadIdentity();
            //
            /////CAMARA//////////////////////////
            // Configura la matriz de vista (View Matrix) para cambiar la vista
            Matrix4 viewMatrix = camera.GetViewMatrix();
            // Configura la matriz de vista
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMatrix);
            /////CAMARA//////////////////////////
            //---------------------
            //escena.objetos["unaparte"].metodo();
            escena2.Dibujar();
            //
            // Dibuja los ejes de coordenadas
            GL.LineWidth(2.0f); // Cambia 2.0f al grosor deseado
            GL.Begin(PrimitiveType.Lines);

            // Eje X (rojo)
            GL.Color3(Color.Red); // Rojo
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(80f, 0.0f, 0.0f); // Punto en X positivo

            // Eje Y (verde)
            GL.Color3(Color.Green); // Verde
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(0.0f, 80f, 0.0f); // Punto en Y positivo

            // Eje Z (azul)
            GL.Color3(Color.Purple); // Azul
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(0.0f, 0.0f, 80f); // Punto en Z positivo

            GL.End();
            ///////////////////////////////////////////////
            //
           
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnResize(EventArgs e)
        {
            float d = 320;//80
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-d, d, -180, 180, -d, d);//16:9
            //GL.Frustum(-80, 80, -80, 80, 4, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            base.OnResize(e);
        }
    }
}
