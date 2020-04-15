using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   internal class PeriodicTable {
      private static string[] elements = {"H", "D", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar", "K", "Ca",
                                   "Sc", "Ti", "V", "Cr", "Mn", "Fe", "Co", "Ni", "Cu", "Zn", "Ga", "Ge", "As", "Se", "Br", "Kr", "Rb", "Sr", "Y", "Zr",
                                   "Nb", "Mo", "Tc", "Ru", "Rh", "Pd", "Ag", "Cd", "In", "Sn", "Sb", "Te", "I", "Xe", "Cs", "Ba", "La", "Ce", "Pr", "Nd",
                                   "Pm", "Sm", "Eu", "Gd", "Tb", "Dy", "Ho", "Er", "Tm", "Yb", "Lu", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg",
                                   "Tl", "Pb", "Bi", "Po", "At", "Rn", "Fr", "Ra", "Ac", "Th", "Pa", "U", "Np", "Pu", "Am", "Cm", "Bk", "Cf", "Es", "Fm", 
                                   "Md", "No", "Lr", "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds", "Rg", "Uub", "Uut", "Uuq", "Uup", "Uuh"};  

      private static IDictionary<string, string> elementNameTable = new Dictionary<string, string>(); 
      private static IDictionary<string, double> elementWeightTable = new Dictionary<string, double>();

      static PeriodicTable() {
         elementNameTable.Add("H", "Hydrogen"); elementWeightTable.Add("H", 1.00794);
         elementNameTable.Add("D", "Deuterium"); elementWeightTable.Add("D", 2.0140);//A second isotope of hydrogen 
         elementNameTable.Add("He", "Helium"); elementWeightTable.Add("He", 4.0026);
         elementNameTable.Add("Li", "Lithium"); elementWeightTable.Add("Li", 6.941);
         elementNameTable.Add("Be", "Beryllium"); elementWeightTable.Add("Be", 9.01218);
         elementNameTable.Add("B", "Boron"); elementWeightTable.Add("B", 10.81);
         elementNameTable.Add("C", "Carbon"); elementWeightTable.Add("C", 12.011);
         elementNameTable.Add("N", "Nitrogen"); elementWeightTable.Add("N", 14.0067);
         elementNameTable.Add("O", "Oxygen"); elementWeightTable.Add("O", 15.9994);
         elementNameTable.Add("F", "Fluorine"); elementWeightTable.Add("F", 18.998403);
         elementNameTable.Add("Ne", "Neon"); elementWeightTable.Add("Ne", 20.179);
         elementNameTable.Add("Na", "Sodium"); elementWeightTable.Add("Na", 22.98977);
         elementNameTable.Add("Mg", "Magnesium"); elementWeightTable.Add("Mg", 24.305);
         elementNameTable.Add("Al", "Aluminum"); elementWeightTable.Add("Al", 26.98154);
         elementNameTable.Add("Si", "Silicon"); elementWeightTable.Add("Si", 28.0855);
         elementNameTable.Add("P", "Phosphorus"); elementWeightTable.Add("P", 30.97376);
         elementNameTable.Add("S", "Sulfur"); elementWeightTable.Add("S", 32.06);
         elementNameTable.Add("Cl", "Chlorine"); elementWeightTable.Add("Cl", 35.453);
         elementNameTable.Add("Ar", "Argon"); elementWeightTable.Add("Ar", 39.948);
         elementNameTable.Add("K", "Potassium"); elementWeightTable.Add("K", 39.0983);
         elementNameTable.Add("Ca", "Calcium"); elementWeightTable.Add("Ca", 40.08);
         elementNameTable.Add("Sc", "Scandium"); elementWeightTable.Add("Sc", 44.9559);
         elementNameTable.Add("Ti", "Titanium"); elementWeightTable.Add("Ti", 47.88);
         elementNameTable.Add("V", "Vanadium"); elementWeightTable.Add("V", 50.9415);
         elementNameTable.Add("Cr", "Chromium"); elementWeightTable.Add("Cr", 51.996);
         elementNameTable.Add("Mn", "Manganese"); elementWeightTable.Add("Mn", 54.9380);
         elementNameTable.Add("Fe", "Iron"); elementWeightTable.Add("Fe", 55.847);
         elementNameTable.Add("Co", "Cobalt"); elementWeightTable.Add("Co", 58.9332);
         elementNameTable.Add("Ni", "Nickel"); elementWeightTable.Add("Ni", 58.69);
         elementNameTable.Add("Cu", "Copper"); elementWeightTable.Add("Cu", 63.546);
         elementNameTable.Add("Zn", "Zinc"); elementWeightTable.Add("Zn", 65.38);
         elementNameTable.Add("Ga", "Gallium"); elementWeightTable.Add("Ga", 69.72);
         elementNameTable.Add("Ge", "Germanium"); elementWeightTable.Add("Ge", 72.59);
         elementNameTable.Add("As", "Arsenic"); elementWeightTable.Add("As", 74.9216);
         elementNameTable.Add("Se", "Selenium"); elementWeightTable.Add("Se", 78.96);
         elementNameTable.Add("Br", "Bromine"); elementWeightTable.Add("Br", 79.904);
         elementNameTable.Add("Kr", "Krypton"); elementWeightTable.Add("Kr", 83.80);
         elementNameTable.Add("Rb", "Rubidium"); elementWeightTable.Add("Rb", 85.4678);
         elementNameTable.Add("Sr", "Strontium"); elementWeightTable.Add("Sr", 87.62);
         elementNameTable.Add("Y", "Yttrium"); elementWeightTable.Add("Y", 88.9059);
         elementNameTable.Add("Zr", "Zirconium"); elementWeightTable.Add("Zr", 91.22);
         elementNameTable.Add("Nb", "Niobium"); elementWeightTable.Add("Nb", 92.9064);
         elementNameTable.Add("Mo", "Molybdenum"); elementWeightTable.Add("Mo", 95.94);
         elementNameTable.Add("Tc", "Technetium"); elementWeightTable.Add("Tc", 98);
         elementNameTable.Add("Ru", "Ruthenium"); elementWeightTable.Add("Ru", 101.07);
         elementNameTable.Add("Rh", "Rhodium"); elementWeightTable.Add("Rh", 102.9055);
         elementNameTable.Add("Pd", "Palladium"); elementWeightTable.Add("Pd", 106.42);
         elementNameTable.Add("Ag", "Silver"); elementWeightTable.Add("Ag", 107.8682);
         elementNameTable.Add("Cd", "Cadmium"); elementWeightTable.Add("Cd", 112.41);
         elementNameTable.Add("In", "Indium"); elementWeightTable.Add("In", 114.82);
         elementNameTable.Add("Sn", "Tin"); elementWeightTable.Add("Sn", 118.69);
         elementNameTable.Add("Sb", "Antimony"); elementWeightTable.Add("Sb", 121.75);
         elementNameTable.Add("Te", "Tellurium"); elementWeightTable.Add("Te", 127.60);
         elementNameTable.Add("I", "Iodine"); elementWeightTable.Add("I", 126.9045);
         elementNameTable.Add("Xe", "Xenon"); elementWeightTable.Add("Xe", 131.29);
         elementNameTable.Add("Cs", "Cesium"); elementWeightTable.Add("Cs", 132.9054);
         elementNameTable.Add("Ba", "Barium"); elementWeightTable.Add("Ba", 137.33);
         elementNameTable.Add("La", "Lanthanum"); elementWeightTable.Add("La", 138.9055);
         elementNameTable.Add("Ce", "Cerium"); elementWeightTable.Add("Ce", 140.12);
         elementNameTable.Add("Pr", "Praseodymium"); elementWeightTable.Add("Pr", 140.9077);
         elementNameTable.Add("Nd", "Neodymium"); elementWeightTable.Add("Nd", 144.24);
         elementNameTable.Add("Pm", "Promethium"); elementWeightTable.Add("Pm", 145);
         elementNameTable.Add("Sm", "Samarium"); elementWeightTable.Add("Sm", 150.36);
         elementNameTable.Add("Eu", "Europium"); elementWeightTable.Add("Eu", 151.96);
         elementNameTable.Add("Gd", "Gadolinium"); elementWeightTable.Add("Gd", 157.25);
         elementNameTable.Add("Tb", "Terbium"); elementWeightTable.Add("Tb", 158.9254);
         elementNameTable.Add("Dy", "Dysprosium"); elementWeightTable.Add("Dy", 162.50);
         elementNameTable.Add("Ho", "Holmium"); elementWeightTable.Add("Ho", 164.9304);
         elementNameTable.Add("Er", "Erbium"); elementWeightTable.Add("Er", 167.26);
         elementNameTable.Add("Tm", "Thulium"); elementWeightTable.Add("Tm", 168.9342);
         elementNameTable.Add("Yb", "Ytterbium"); elementWeightTable.Add("Yb", 173.04);
         elementNameTable.Add("Lu", "Lutetium"); elementWeightTable.Add("Lu", 174.967);
         elementNameTable.Add("Hf", "Hafnium"); elementWeightTable.Add("Hf", 178.49);
         elementNameTable.Add("Ta", "Tantalum"); elementWeightTable.Add("Ta", 180.9479);
         elementNameTable.Add("W", "Tungsten"); elementWeightTable.Add("W", 183.85);
         elementNameTable.Add("Re", "Rhenium"); elementWeightTable.Add("Re", 186.207);
         elementNameTable.Add("Os", "Osmium"); elementWeightTable.Add("Os", 190.2);
         elementNameTable.Add("Ir", "Iridium"); elementWeightTable.Add("Ir", 192.22);
         elementNameTable.Add("Pt", "Platinum"); elementWeightTable.Add("Pt", 195.08);
         elementNameTable.Add("Au", "Gold"); elementWeightTable.Add("Au", 196.9665);
         elementNameTable.Add("Hg", "Mercury"); elementWeightTable.Add("Hg", 200.59);
         elementNameTable.Add("Tl", "Thallium"); elementWeightTable.Add("Tl", 204.383);
         elementNameTable.Add("Pb", "Lead"); elementWeightTable.Add("Pb", 207.2);
         elementNameTable.Add("Bi", "Bismuth"); elementWeightTable.Add("Bi", 208.9804);
         elementNameTable.Add("Po", "Polonium"); elementWeightTable.Add("Po", 209);
         elementNameTable.Add("At", "Astatine"); elementWeightTable.Add("At", 210);
         elementNameTable.Add("Rn", "Radon"); elementWeightTable.Add("Rn", 222);
         elementNameTable.Add("Fr", "Francium"); elementWeightTable.Add("Fr", 223);
         elementNameTable.Add("Ra", "Radium"); elementWeightTable.Add("Ra", 226.0254);
         elementNameTable.Add("Ac", "Actinium"); elementWeightTable.Add("Ac", 227.0278);
         elementNameTable.Add("Th", "Thorium"); elementWeightTable.Add("Th", 232.0381);
         elementNameTable.Add("Pa", "Protactinium"); elementWeightTable.Add("Pa", 231.0359);
         elementNameTable.Add("U", "Uranium"); elementWeightTable.Add("U", 238.0289);
         elementNameTable.Add("Np", "Neptunium"); elementWeightTable.Add("Np", 237.0482);
         elementNameTable.Add("Pu", "Plutonium"); elementWeightTable.Add("Pu", 244);
         elementNameTable.Add("Am", "Americium"); elementWeightTable.Add("Am", 243);
         elementNameTable.Add("Cm", "Curium"); elementWeightTable.Add("Cm", 247);
         elementNameTable.Add("Bk", "Berkelium"); elementWeightTable.Add("Bk", 247);
         elementNameTable.Add("Cf", "Californium"); elementWeightTable.Add("Cf", 251);
         elementNameTable.Add("Es", "Einsteinium"); elementWeightTable.Add("Es", 252);
         elementNameTable.Add("Fm", "Fermium"); elementWeightTable.Add("Fm", 257);
         elementNameTable.Add("Md", "Mendelevium"); elementWeightTable.Add("Md", 258);
         elementNameTable.Add("No", "Nobelium"); elementWeightTable.Add("No", 259);
         elementNameTable.Add("Lr", "Lawrencium"); elementWeightTable.Add("Lr", 262);
         elementNameTable.Add("Rf", "Rutherfordium"); elementWeightTable.Add("Rf", 261);
         elementNameTable.Add("Db", "Dubnium"); elementWeightTable.Add("Db", 262);
         elementNameTable.Add("Sg", "Seaborgium"); elementWeightTable.Add("Sg", 266);
         elementNameTable.Add("Bh", "Bohrium"); elementWeightTable.Add("Bh", 262);
         elementNameTable.Add("Hs", "Hassium"); elementWeightTable.Add("Hs", 265);
         elementNameTable.Add("Mt", "Meitnerium"); elementWeightTable.Add("Mt", 266);
         elementNameTable.Add("Ds", "Darmstadtium"); elementWeightTable.Add("Ds", 271);
         elementNameTable.Add("Rg", "Roentgenium"); elementWeightTable.Add("Rg", 272);
         elementNameTable.Add("Uub", "Ununbium"); elementWeightTable.Add("Uub", 285);
         elementNameTable.Add("Uut", "Ununtrium"); elementWeightTable.Add("Uut", 284);
         elementNameTable.Add("Uuq", "Ununquadium"); elementWeightTable.Add("Uuq", 289);
         elementNameTable.Add("Uup", "Ununpentium"); elementWeightTable.Add("Uup", 288);
         elementNameTable.Add("Uuh", "Ununhexium"); elementWeightTable.Add("Uuh", 292);
      }

      internal static string[] GetElements() {
         return elements;
      }

      internal static double GetElementWeight(string elementName) {
         return elementWeightTable[elementName];
      }

      internal static string GetElementFullName(string elementName) {
         return elementNameTable[elementName];
      }
   }
}
