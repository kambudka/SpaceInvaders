using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Designs
    {
        static Dictionary<string, Design> designs;

        public Designs()
        {
            designs = new Dictionary<string, Design>();
        }



        public void addDesign(string name,Uri path)
        {

            Design design = new Design(path,name);
            designs.Add(name, design);

        }

        public static Design getDesign(string name)
        {
            return designs[name];
        }

    }
}
