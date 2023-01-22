using System;

namespace CifrarioEnigma
{
    public sealed class Rotor : Reflector, ICloneable
    {
        public int Offset { get; set; }
        public RotorOrder Side { get; set; }

        public enum RotorOrder
        {
            Right,
            Middle,
            Left
        }

        public Rotor(int conf)
        {
            this.SetConfiguration(conf);
        }

        public sealed override void SetConfiguration(int conf)
        {
            switch (conf)
            {
                case 0:
                    this.Configuration = "JGDQOXUSCAMIFRVTPNEWKBLZYH".ToCharArray();
                    break;
                case 1:
                    this.Configuration = "NTZPSFBOKMWRCJDIVLAEYUXHGQ".ToCharArray();
                    break;
                case 2:
                    this.Configuration = "JVIUBHTCDYAKEQZPOSGXNRMWFL".ToCharArray();
                    break;
            }
        }

        public sealed override char Encrypt(char letter)
        {
            return this.Configuration[Array.IndexOf("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(), letter)];
        }

        public char ReverseEncrypt(char letter)
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Array.IndexOf(this.Configuration, letter)];
        }

        public void RotateArray(int pos)
        {
            char[] temp = new char[26];
            Array.Copy(this.Configuration, temp, 26);

            for (var i = 0; i < this.Configuration.Length; i++)
            {
                var newIndex = (i + pos) % 26;

                if (newIndex < 0)
                {
                    break;
                }

                this.Configuration[newIndex] = temp[i];
            }
        }

        public static void AdjustRotorOffset(Rotor rotor1, Rotor rotor2, Rotor rotor3, int pos)
        {
            if (rotor1.Offset == 25)
            {
                rotor1.Offset = -1;
                rotor2.Offset = rotor2.Offset + pos;
                rotor2.RotateArray(pos);
            }

            if (rotor2.Offset == 26)
            {
                rotor2.Offset = 0;
                rotor3.Offset = rotor3.Offset + pos;
                rotor3.RotateArray(pos);
            }

            if (rotor3.Offset == 26)
            {
                rotor3.Offset = 0;
            }

            rotor1.RotateArray(pos);
            rotor1.Offset = rotor1.Offset + pos;
        }

        public object Clone()
        {
            return this.Configuration.Clone();
        }
    }
}
