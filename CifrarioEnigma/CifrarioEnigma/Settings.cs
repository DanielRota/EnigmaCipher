using System;
using System.Windows.Forms;

namespace CifrarioEnigma
{
    public partial class Settings : Form
    {
        public bool IsLoading = true;

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            IsLoading = false;
        }

        private void comboBoxRotor1Side_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRotor1Side.SelectedIndex == comboBoxRotor2Side.SelectedIndex || comboBoxRotor1Side.SelectedIndex == comboBoxRotor3Side.SelectedIndex)
            {
                comboBoxRotor1Side.SelectedIndex = -1;
            }

            if (comboBoxRotor2Side.SelectedIndex == comboBoxRotor1Side.SelectedIndex || comboBoxRotor2Side.SelectedIndex == comboBoxRotor3Side.SelectedIndex)
            {
                comboBoxRotor2Side.SelectedIndex = -1;
            }

            if (comboBoxRotor3Side.SelectedIndex == comboBoxRotor1Side.SelectedIndex || comboBoxRotor3Side.SelectedIndex == comboBoxRotor2Side.SelectedIndex)
            {
                comboBoxRotor3Side.SelectedIndex = -1;
            }
        }

        private void comboBoxRotor2Side_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRotor1Side.SelectedIndex == comboBoxRotor2Side.SelectedIndex || comboBoxRotor1Side.SelectedIndex == comboBoxRotor3Side.SelectedIndex)
            {
                comboBoxRotor1Side.SelectedIndex = -1;
            }

            if (comboBoxRotor2Side.SelectedIndex == comboBoxRotor1Side.SelectedIndex || comboBoxRotor2Side.SelectedIndex == comboBoxRotor3Side.SelectedIndex)
            {
                comboBoxRotor2Side.SelectedIndex = -1;
            }

            if (comboBoxRotor3Side.SelectedIndex == comboBoxRotor1Side.SelectedIndex || comboBoxRotor3Side.SelectedIndex == comboBoxRotor2Side.SelectedIndex)
            {
                comboBoxRotor3Side.SelectedIndex = -1;
            }
        }

        private void comboBoxRotor3Side_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRotor1Side.SelectedIndex == comboBoxRotor2Side.SelectedIndex || comboBoxRotor1Side.SelectedIndex == comboBoxRotor3Side.SelectedIndex)
            {
                comboBoxRotor1Side.SelectedIndex = -1;
            }

            if (comboBoxRotor2Side.SelectedIndex == comboBoxRotor1Side.SelectedIndex || comboBoxRotor2Side.SelectedIndex == comboBoxRotor3Side.SelectedIndex)
            {
                comboBoxRotor2Side.SelectedIndex = -1;
            }

            if (comboBoxRotor3Side.SelectedIndex == comboBoxRotor1Side.SelectedIndex || comboBoxRotor3Side.SelectedIndex == comboBoxRotor2Side.SelectedIndex)
            {
                comboBoxRotor3Side.SelectedIndex = -1;
            }
        }

        private void comboBoxReflectorConfiguration_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Root.Reflector.SetConfiguration(comboBoxReflectorConfiguration.SelectedIndex);
        }

        private void comboBoxRotorConfiguration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                Program.Root.Rotor1.SetConfiguration(comboBoxRotor1Configuration.SelectedIndex);
                Program.Root.Rotor2.SetConfiguration(comboBoxRotor2Configuration.SelectedIndex);
                Program.Root.Rotor3.SetConfiguration(comboBoxRotor3Configuration.SelectedIndex);
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (!(comboBoxRotor1Side.SelectedIndex == -1 || comboBoxRotor3Side.SelectedIndex == -1 || comboBoxRotor3Side.SelectedIndex == -1))
            {
                this.Hide();
            }
        }

        private void richTextBoxLogSettings_TextChanged(object sender, EventArgs e)
        {
            richTextBoxLogSettings.ScrollToCaret();
        }
    }
}
