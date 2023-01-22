using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CifrarioEnigma
{
    public static class Utilities
    {
        public static Label FindLabelByName(this CifrarioEnigma program, string name)
        {
            foreach (Control control in program.Controls)
            {
                var a = control.Name;

                if (control is Label)
                {
                    if (control.Name == name)
                    {
                        return (Label)control;
                    }
                }
            }

            return null;
        }

        public static string ReadTextFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = File.ReadAllText(openFileDialog.FileName);
                return file;
            }

            return "";
        }

        public static IEnumerable<Control> AllControls(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrls => AllControls(ctrls, type)).Concat(controls).Where(c => c.GetType() == type);
        }
    }
}
