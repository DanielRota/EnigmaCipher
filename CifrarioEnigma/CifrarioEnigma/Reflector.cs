using System;

namespace CifrarioEnigma
{
    public class Reflector
    {
        public char[] Configuration { get; set; }

        public Reflector()
        {
            this.SetConfiguration(0);
        }

        public virtual void SetConfiguration(int conf)
        {
            switch (conf)
            {
                case 0:
                    this.Configuration = "QYHOGNECVPUZTFDJAXWMKISRBL".ToCharArray();
                    break;
                case 1:
                    this.Configuration = "QWERTZUIOASDFGHJKPYXCVBNML".ToCharArray();
                    break;
            }
        }

        public virtual char Encrypt(char letter)
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Array.IndexOf(this.Configuration, letter)];
        }
    }
}
