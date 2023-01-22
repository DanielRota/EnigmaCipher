using System;
using System.Windows.Forms;

namespace CifrarioEnigma
{
    internal static class Program
    {
        public static CifrarioEnigma Root;

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Root = new CifrarioEnigma(); 
            Application.Run(Root);
        }
    }
}
