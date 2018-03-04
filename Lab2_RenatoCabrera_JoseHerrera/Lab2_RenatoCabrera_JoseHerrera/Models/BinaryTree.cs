using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace ArbolBinario
{
    class BinaryTree
    {
        class NodoT <T>
        {
            public NodoT<T> NodoIzquierdo;
            public T Informacion;
            public NodoT<T> NodoDerecho;
            //Constructor
            public NodoT()
            {
                this.NodoIzquierdo = null;
                this.Informacion = default(T);
                this.NodoDerecho = null;
            }
        }
        public void Insertar(NodoT Raiz, NodoT<T> Dato)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (String.Compare(Dato.ToString(), Raiz.Informacion.Key.ToString()) < 0)
            {
                if (Raiz.NodoIzquierdo == null)
                {
                    NodoT NuevoNodo = new NodoT();
                    NuevoNodo.Informacion.Key = Dato;
                    Raiz.NodoIzquierdo = NuevoNodo;
                }
                else
                {
                    //Llamada recursiva
                    Insertar(Raiz.NodoIzquierdo, Dato);
                }
            }
            else//Buscar por el lado derecho
            {
                if (String.Compare(Dato.ToString(), Raiz.Informacion.Key.ToString()) > 0)
                {
                    if (Raiz.NodoDerecho == null)
                    {
                        NodoT NuevoNodo = new NodoT();
                        NuevoNodo.Informacion.Key = Dato;
                        Raiz.NodoDerecho = NuevoNodo;
                    }
                    else
                    {
                        //Llamada recursiva por el lado derecho
                        Insertar(Raiz.NodoDerecho, Dato);
                    }
                }
                else
                {
                    //El Nodo existe en el Arbol
                    Console.WriteLine("Nodo Existente, Imposible Insertar...");
                    Console.ReadLine();
                }
            }
        }
        //Metodo de Buscar un nodo
        public void BuscarNodo(NodoT Raiz, int Dato)
        {
            if (Dato < Raiz.Informacion.Key)
            {
                //Buscar por el Sub-Arbol izquierdo
                if (Raiz.NodoIzquierdo == null)
                {
                    Console.WriteLine("ERROR, No se encuentra el Nodo...");
                    Console.ReadLine();
                }
                else
                {
                    BuscarNodo(Raiz.NodoIzquierdo, Dato);
                }
            }
            else
            {
                if (Dato > Raiz.Informacion.Key)
                {
                    //Buscar por el Sub-Arbol derecho
                    if (Raiz.NodoDerecho == null)
                    {
                        Console.WriteLine("ERROR, No se encuentra el Nodo...");
                        Console.ReadLine();
                    }
                    else
                    {
                        BuscarNodo(Raiz.NodoDerecho, Dato);
                    }
                }
                else
                {
                    //El nodo se encontro
                    Console.WriteLine("Nodo Localizado en el Arbol...");
                    Console.ReadLine();
                }
            }
        }
        public void EliminarNodo(ref NodoT Raiz, int Dato)
        {
            if (Raiz != null)
            {
                if (Dato < Raiz.Informacion.Key)
                {
                    EliminarNodo(ref Raiz.NodoIzquierdo, Dato);
                }
                else
                {
                    if (Dato > Raiz.Informacion.Key)
                    {
                        EliminarNodo(ref Raiz.NodoDerecho, Dato);
                    }
                    else
                    {
                        //Si lo Encontro
                        NodoT NodoEliminar = Raiz;
                        if (NodoEliminar.NodoDerecho == null)
                        {
                            Raiz = NodoEliminar.NodoIzquierdo;
                        }
                        else
                        {
                            if (NodoEliminar.NodoIzquierdo == null)
                            {
                                Raiz = NodoEliminar.NodoDerecho;
                            }
                            else
                            {
                                NodoT AuxiliarNodo = null;
                                NodoT Auxiliar = Raiz.NodoIzquierdo;
                                bool Bandera = false;
                                while (Auxiliar.NodoDerecho != null)
                                {
                                    AuxiliarNodo = Auxiliar;
                                    Auxiliar = Auxiliar.NodoDerecho;
                                    Bandera = true;
                                }
                                Raiz.Informacion = Auxiliar.Informacion;
                                NodoEliminar = Auxiliar;
                                if (Bandera == true)
                                {
                                    AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                }
                                else
                                {
                                    Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                }
                            }
                        }
                    }
                }
            }   
            else
            {
                Console.WriteLine("ERROR, EL Nodo no se Encuentra en el Arbol...");
                Console.ReadLine();
            }
        }
        
    }*/
}
