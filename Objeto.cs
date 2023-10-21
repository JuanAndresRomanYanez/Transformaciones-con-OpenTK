using OpenTK.Input;
using Proyecto1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Objeto
    {
        public Dictionary<string, Parte> partes { get; set; }
        public Punto p { get; set; }
        public Objeto()
        {
            partes = new Dictionary<string, Parte>();
        }
        public Objeto(float x,float y, float z)
        {
            this.p = new Punto(x, y, z);
            partes = new Dictionary<string, Parte>();
        }
        public Objeto(Objeto obj)
        {
            partes = new Dictionary<string, Parte>();
            foreach (KeyValuePair<string, Parte> k in obj.partes)
            {
                partes.Add((k.Key), new Parte(k.Value));
            }
        }
        public void AdicionarParte(String s, Parte p)
        {
            ActualizarParte(p);
            partes.Add(s, p);
        }
        public void ActualizarParte(Parte p)
        {
            foreach(Poligono pol in p.Lista){
                foreach(Punto pun in pol.puntos){
                    pun.acumular(this.p.x, this.p.y, this.p.z);
                    //pun.acumular(this.p);
                }
            }
        }
        public void EliminarParte(String s)
        {
            partes.Remove(s);
        }

        public void Dibujar()
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Dibujar();
            }
        }
        
        public void mover(Punto p)
        {
            foreach (Parte par in partes.Values)
            {
                par.mover(p);
            }
        }
        public void Escalar(float scale)
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Escalar(scale);
            }
        }
        public void Rotar(float angle, char c)
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Rotar(angle, c);
            }
        }
        public void Trasladar(float x, float y, float z)
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Trasladar(x, y, z);
            }
        }
    }
}
