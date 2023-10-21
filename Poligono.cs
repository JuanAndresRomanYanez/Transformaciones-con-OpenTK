using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Proyecto1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Poligono
    {
        public List<Punto> puntos { get; set; }
        public Color4 color { get; set; }

        Punto p { get; set; }
        public Poligono()
        {
            puntos = new List<Punto>();
            this.color = color;
        }
        public Poligono(Color4 color)
        {
            puntos = new List<Punto>();
            this.color = color;
        }
        public Poligono(Color4 color, float x, float y, float z){
            puntos = new List<Punto>();
            this.color = color;
            this.p = new Punto(x, y, z);
        }
        public Poligono(Poligono p, Color4 color)
        {
            puntos = new List<Punto>();
            this.color = color;
            for (int i = 0; i < p.puntos.Count(); i++)
            {
                puntos.Add(new Punto(p.puntos[i]));
            }
        }

        public void Dibujar(){

            PrimitiveType primitiveType = PrimitiveType.Polygon;
            GL.Begin(primitiveType);
            GL.Color4(color);

            for (int i = 0; puntos.Count > i; i++)
            {
                GL.Vertex3(puntos.ElementAt(i).ToVector3());
            }

            GL.End();

        }

        public void Rotar(float angulo, Punto p)
        {
            GL.Rotate(20, p.ToVector3());
        }

        public void Adicionar(float x, float y, float z)
        {
            puntos.Add(new Punto(x + p.x, y + p.y, z + p.z));
        }

        public void Adicionar(Punto punto)
        {
            puntos.Add(punto);
        }

        public void Escalar(float scale)
        {
            Matrix4 m = Matrix4.CreateScale(scale, scale, scale);
            Vector4 v = new Vector4();
            for (int i = 0; i < puntos.Count; i++)
            {
                v = puntos.ElementAt(i).ToVector4() * m;
                puntos.ElementAt<Punto>(i).setPunto(v.X, v.Y, v.Z);
            }
        }

        public void Rotar(float angle, char c )
        {
            Matrix4 m = new Matrix4();
            switch (c)
            {
                case 'x': 
                    m = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle));
                    break;
                case 'y':
                    m = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle));
                    break;
                case 'z':
                    m = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle));
                    break;
                default: break;
            }
            Vector4 v = new Vector4();
            for (int i = 0; i < puntos.Count; i++)
            {
                v = puntos.ElementAt(i).ToVector4() * m;
                puntos.ElementAt<Punto>(i).setPunto(v.X, v.Y, v.Z);
            }
        }
        public void Trasladar(float x, float y, float z)
        {
            Matrix4 m = Matrix4.CreateTranslation(x, y, z);//la matriz 
            Vector4 v = new Vector4();
            for (int i = 0; i < puntos.Count; i++)
            {
                v = puntos.ElementAt(i).ToVector4() * m;
                puntos.ElementAt<Punto>(i).setPunto(v.X, v.Y, v.Z);
            }
        }

            public void Eliminar(int i)
        {
            puntos.RemoveAt(i);
        }
        public void mover(Punto p)
        {
            foreach (Punto pun in puntos)
            {
                pun.acumular(p);
            }
        }
    }
    // objeto auto= new objeto(new ); 
    // objeto cuarto = new objeto();
    // cuarto.add(new parte(new poligono()));
}
