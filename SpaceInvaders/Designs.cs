using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Designs
    {
        static Dictionary<string, Design> designs;  //słownik przechowujący nasze Tekstury i ich nazwy.

        public Designs()    //Konstruktor tworzący nowy słownik
        {
            designs = new Dictionary<string, Design>();
        }
        /// <summary>
        /// Dodanie nowej tekstury do słownika
        /// </summary>
        /// <param name="name"> Nazwa tekstury</param>
        /// <param name="path">Obiekt Uri posiadający scieżkę do obrazka</param>
        public void addDesign(string name,Uri path)
        {
            Design design = new Design(path,name);
            designs.Add(name, design);
        }

        /// <summary>
        /// Zwraca teksturę o podanej nazwie
        /// </summary>
        /// <param name="name">Nazwa tekstury</param>
        /// <returns>Obiekt klasy Design</returns>
        public static Design getDesign(string name)
        {
            return designs[name];
        }

    }
}
