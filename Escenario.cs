using Proyecto1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Escenario
    {
        public Dictionary<string, Objeto> objetos { get; set; }
        
        public Escenario()
        {
            objetos = new Dictionary<string, Objeto>();
        }
        public Escenario(Escenario esc){
            objetos = new Dictionary<string, Objeto>();
            foreach (KeyValuePair<string, Objeto> k in esc.objetos){
                objetos.Add((k.Key), new Objeto(k.Value));
            }
        }
        public void AdicionarObjeto(String s, Objeto p)
        {

            objetos.Add(s, p);
        }
        public void Dibujar()
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Dibujar();
            }
        }
        // Firstly, Calcular el centro de masa
        
        public Punto getCentro()
        {
            float xMin, xMax, yMin, yMax, zMin, zMax;
            xMin = yMin = zMin = float.MaxValue;
            xMax = yMax = zMax = float.MinValue;
            foreach (Objeto valor in objetos.Values)
            {
                foreach (Parte par in valor.partes.Values)
                {
                    foreach (Poligono pol in par.Lista)
                    {
                        foreach (Punto pun in pol.puntos)
                        {
                            xMin = Math.Min(xMin, pun.x);
                            xMax = Math.Max(xMax, pun.x);
                            yMin = Math.Min(yMin, pun.y);
                            yMax = Math.Max(yMax, pun.y);
                            zMin = Math.Min(zMin, pun.z);
                            zMax = Math.Max(zMax, pun.z);
                        }
                    }
                }
            }
            float x, y, z;
            x = (xMin + xMax) / 2.0f;
            y = (yMin + yMax) / 2.0f;
            z = (zMin + zMax) / 2.0f;
            Punto origen = new Punto(x,y,z);
            return origen;
        }
        public void Rotar(float angle, char dir)
        {
            Punto origen = new Punto(getCentro());
            Trasladar(-origen.x, -origen.y, -origen.z);
            foreach (Objeto valor in objetos.Values)
            {
                valor.Rotar(angle, dir);
            }
            Trasladar(origen.x, origen.y, origen.z);
        }
        public void Escalar(float scale)
        {
            Punto origen = new Punto(getCentro());
            Trasladar(-origen.x, -origen.y, -origen.z);
            foreach (Objeto valor in objetos.Values)
            {
                valor.Escalar(scale);
            }
            Trasladar(origen.x, origen.y, origen.z);
        }
        public void Trasladar(float x, float y, float z)
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Trasladar(x, y, z);
            }
        }


        public void mover(Punto p)
        {
            foreach(Objeto valor in objetos.Values)
            {
                valor.mover(p);
            }
        }
        /*
        public void mover(Punto p)
        {
            foreach (Objeto valor in objetos.Values)
            {
                foreach (Parte par in valor.partes.Values) 
                { 
                    foreach(Poligono pol in par.Lista)
                    {
                        foreach(Punto pun in pol.puntos)
                        {
                            pun.acumular(p);
                        }
                    }
                }
            }
        }
        */
    }
}
