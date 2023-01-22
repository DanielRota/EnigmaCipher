using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CifrarioEnigma
{
    public partial class CifrarioEnigma : Form
    {
        public Rotor Rotor1, Rotor2, Rotor3;
        public Reflector Reflector;
        public Plugboard Plugboard;
        private List<Rotor> _rotors;
        private Settings settingsForm;
        private PlugboardColor randomColor;
        private bool IsImport, IsFirstRotation, Interrupt = false;
        private int CryptCount = 1;

        public CifrarioEnigma()
        {
            InitializeComponent();

            Rotor1 = new Rotor(0);
            Rotor2 = new Rotor(1);
            Rotor3 = new Rotor(2);
            Reflector = new Reflector();
            Plugboard = new Plugboard();
            _rotors = new List<Rotor> { Rotor1, Rotor2, Rotor3 };
            settingsForm = new Settings();
            randomColor = new PlugboardColor();
            Plugboard._checked = new SortedDictionary<string, string>();
        }

        #region FormPrincipale
        private void MainForm_Load(object sender, EventArgs e)
        {
            richTextBoxMessage.Select();

            settingsForm.comboBoxRotor1Configuration.SelectedIndex = 0;
            settingsForm.comboBoxRotor2Configuration.SelectedIndex = 1;
            settingsForm.comboBoxRotor3Configuration.SelectedIndex = 2;
            settingsForm.comboBoxReflectorConfiguration.SelectedIndex = 0;

            settingsForm.comboBoxRotor1Side.SelectedIndex = 0;
            settingsForm.comboBoxRotor2Side.SelectedIndex = 1;
            settingsForm.comboBoxRotor3Side.SelectedIndex = 2;

            Rotor1.Side = (Rotor.RotorOrder)settingsForm.comboBoxRotor1Side.SelectedIndex;
            Rotor2.Side = (Rotor.RotorOrder)settingsForm.comboBoxRotor2Side.SelectedIndex;
            Rotor3.Side = (Rotor.RotorOrder)settingsForm.comboBoxRotor3Side.SelectedIndex;
        }
        #endregion

        #region ControlliInserimento
        private void richTextBoxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }

            if (char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar).ToString()[0];
            }
        }

        private void richTextBoxMessage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 8 || e.KeyValue == 13)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void richTextBoxMessage_TextChanged(object sender, EventArgs e)
        {
            if (richTextBoxMessage.Text != String.Empty)
            {
                if (!IsImport)
                {
                    richTextBoxLog.Clear();
                    ConvertChar(richTextBoxMessage.Text[richTextBoxMessage.Text.Length - 1]);
                }
            }

            richTextBoxMessage.SelectionStart = richTextBoxMessage.Text.Length;
            richTextBoxMessage.ScrollToCaret();
        }

        private void richTextBoxEncrypted_TextChanged(object sender, EventArgs e)
        {
            richTextBoxEncrypted.SelectionStart = richTextBoxEncrypted.Text.Length;
            richTextBoxEncrypted.ScrollToCaret();
        }
        #endregion

        #region GestionePlugboard
        private void Plugboard_Click(object sender, EventArgs e)
        {
            var label = sender as Label;
            var labelText = label.Text.ToString();
            var IsSelecting = true;

            if (Plugboard.OnSelection == 0)
            {
                randomColor = Plugboard._colors.Where(x => x.Used == false).FirstOrDefault();
            }

            if (Plugboard._checked.ContainsKey(labelText))
            {
                IsSelecting = false;

                if (Plugboard._checked.TryGetValue(labelText, out var value))
                {
                    var thisColor = Plugboard._colors.Where(x => x.CurrentColor == label.BackColor && x.Used == true).First();
                    thisColor.Used = false;
                    var item = Plugboard._checked.Where(x => x.Key == labelText).FirstOrDefault();
                    var keyLabel = this.Controls.Find($"label{item.Value}", true).FirstOrDefault() as Label;
                    keyLabel.BackColor = SystemColors.ButtonFace;
                    label.BackColor = SystemColors.ButtonFace;
                    Plugboard._checked.Remove(labelText);
                    Plugboard.SwapChars(item.Value, labelText);
                }
            }
            else if (Plugboard._checked.ContainsValue(labelText))
            {
                IsSelecting = false;

                foreach (var pair in Plugboard._checked)
                {
                    if (pair.Value == labelText)
                    {
                        var thisColor = Plugboard._colors.Where(x => x.CurrentColor == label.BackColor && x.Used == true).First();
                        thisColor.Used = false;
                        var valueLabel = this.Controls.Find($"label{pair.Key}", true).FirstOrDefault() as Label;
                        valueLabel.BackColor = SystemColors.ButtonFace;
                        label.BackColor = SystemColors.ButtonFace;
                        Plugboard._checked.Remove(pair.Key);
                        Plugboard.SwapChars(valueLabel.Text, label.Text);
                        break;
                    }
                }
            }

            if (IsSelecting)
            {
                if (Plugboard._colors.TrueForAll(x => x.Used == true))
                {
                    return;
                }

                Plugboard.OnSelection++;

                if (Plugboard.OnSelection == 1 || Plugboard.OnSelection == 2)
                {
                    label.BackColor = randomColor.CurrentColor;

                    if (Plugboard.OnSelection == 1)
                    {
                        Plugboard.FirstChar = labelText;
                    }
                    else
                    {
                        Plugboard.SecondChar = labelText;

                        if (!Plugboard._checked.ContainsKey(Plugboard.FirstChar) && !Plugboard._checked.ContainsValue(Plugboard.SecondChar))
                        {
                            Plugboard.SwapChars(Plugboard.FirstChar, Plugboard.SecondChar);
                            Plugboard._checked.Add(Plugboard.FirstChar, Plugboard.SecondChar);
                        }

                        Plugboard.OnSelection = 0;
                        randomColor.Used = true;
                    }
                }
            }
        }

        private void buttonLabelOffset_Click(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button.Name == "buttonLabel1OffsetPlus")
            {
                if (Rotor1.Offset == 25)
                    return;

                Rotor1.Offset++;
                Rotor1.RotateArray(1);
                label1Offset.Text = "I » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Rotor1.Offset].ToString();
            }

            if (button.Name == "buttonLabel2OffsetPlus")
            {
                if (Rotor2.Offset == 25)
                    return;

                Rotor2.Offset++;
                Rotor2.RotateArray(1);
                label2Offset.Text = "II » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Rotor2.Offset].ToString();
            }

            if (button.Name == "buttonLabel3OffsetPlus")
            {
                if (Rotor3.Offset == 25)
                    return;

                Rotor3.Offset++;
                Rotor3.RotateArray(1);
                label3Offset.Text = "III » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Rotor3.Offset].ToString();
            }

            if (button.Name == "buttonLabel1OffsetMinus")
            {
                if (Rotor1.Offset == 0)
                    return;

                Rotor1.Offset--;
                Rotor1.RotateArray(25);
                label1Offset.Text = "I » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Rotor1.Offset].ToString();
            }

            if (button.Name == "buttonLabel2OffsetMinus")
            {
                if (Rotor2.Offset == 0)
                    return;

                Rotor2.Offset--;
                Rotor2.RotateArray(25);
                label2Offset.Text = "II » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Rotor2.Offset].ToString();
            }

            if (button.Name == "buttonLabel3OffsetMinus")
            {
                if (Rotor3.Offset == 0)
                    return;

                Rotor3.Offset--;
                Rotor3.RotateArray(25);
                label3Offset.Text = "III » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Rotor3.Offset].ToString();
            }
        }
        #endregion

        #region ClickPulsanti
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (settingsForm.IsDisposed)
            {
                settingsForm = new Settings();
            }

            settingsForm.Show();
            settingsForm.Activate();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            var file = Utilities.ReadTextFile().ToUpper();
            panelPlugboard.Enabled = false;
            IsImport = true;
            Interrupt = false;
            IsFirstRotation = false;

            if (file != String.Empty)
            {
                richTextBoxMessage.Clear();
                richTextBoxEncrypted.Clear();
                Regex.Replace(file, "[^a-zA-Z ]", "");

                foreach (var letter in file)
                {
                    if (Interrupt)
                    {
                        break;
                    }

                    if (65 <= letter && letter <= 90)
                    {
                        richTextBoxMessage.Text += char.ToUpper(letter);
                        ConvertChar(char.ToUpper(letter));
                        richTextBoxLog.ClearUndo();
                        Application.DoEvents();
                        Refresh();
                    }
                    else
                    {
                        richTextBoxEncrypted.Text += " ";
                        richTextBoxMessage.Text += " ";
                    }
                }
            }

            panelPlugboard.Enabled = true;
            IsImport = false;
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            Interrupt = true;

            foreach (Control control in panelPlugboard.Controls)
            {
                if (control is Label)
                {
                    (control as Label).BackColor = SystemColors.ButtonFace;
                }
            }

            Rotor1 = new Rotor(0);
            Rotor2 = new Rotor(1);
            Rotor3 = new Rotor(2);
            Reflector = new Reflector();
            Plugboard = new Plugboard();
            Plugboard._checked = new SortedDictionary<string, string>();
            _rotors = new List<Rotor> { Rotor1, Rotor2, Rotor3 };

            richTextBoxMessage.Clear();
            richTextBoxEncrypted.Clear();
            richTextBoxLog.Clear();
            
            label1Offset.Text = "";
            label2Offset.Text = "";
            label3Offset.Text = "";

            settingsForm.comboBoxReflectorConfiguration.SelectedIndex = 0;
            settingsForm.comboBoxRotor1Side.SelectedIndex = 0;
            settingsForm.comboBoxRotor2Side.SelectedIndex = 1;
            settingsForm.comboBoxRotor3Side.SelectedIndex = 2;
            settingsForm.comboBoxRotor1Configuration.SelectedIndex = 0;
            settingsForm.comboBoxRotor2Configuration.SelectedIndex = 1;
            settingsForm.comboBoxRotor3Configuration.SelectedIndex = 2;
            settingsForm.richTextBoxFullLog.Clear();

            panelPlugboard.Enabled = true;
        }
        #endregion

        #region Funzioni
        public void ConvertChar(char letter)
        {
            if (IsFirstRotation)
            {
                Rotor1.RotateArray(Array.IndexOf("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(), label1Offset.Text));
                Rotor2.RotateArray(Array.IndexOf("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(), label2Offset.Text));
                Rotor3.RotateArray(Array.IndexOf("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(), label3Offset.Text));
                IsFirstRotation = false;
            }

            Rotor1.Side = (Rotor.RotorOrder)settingsForm.comboBoxRotor1Side.SelectedIndex;
            Rotor2.Side = (Rotor.RotorOrder)settingsForm.comboBoxRotor2Side.SelectedIndex;
            Rotor3.Side = (Rotor.RotorOrder)settingsForm.comboBoxRotor3Side.SelectedIndex;

            var firstRotor = _rotors.Where(x => x.Side.Equals(Rotor.RotorOrder.Right)).First();
            var secondRotor = _rotors.Where(x => x.Side.Equals(Rotor.RotorOrder.Middle)).First();
            var thirdRotor = _rotors.Where(x => x.Side.Equals(Rotor.RotorOrder.Left)).First();

            richTextBoxLog.Text = default;
            var log = (string)default;
            richTextBoxLog.Text = "Character Path:\n";
            letter = Plugboard.SwappedConfiguration[Array.IndexOf("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(), letter)];
            log += $"{letter} ➜ ";
            letter = firstRotor.Encrypt(letter);
            log += $"{letter} ➜ ";
            letter = secondRotor.Encrypt(letter);
            log += $"{letter} ➜ ";
            letter = thirdRotor.Encrypt(letter);
            log += $"{letter} ➜ ";
            letter = Reflector.Encrypt(letter);
            log += $"{letter} ➜ ";
            letter = thirdRotor.ReverseEncrypt(letter);
            log += $"{letter} ➜ ";
            letter = secondRotor.ReverseEncrypt(letter);
            log += $"{letter} ➜ ";
            letter = firstRotor.ReverseEncrypt(letter);
            log += $"{letter} ➜ ";
            letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[Array.IndexOf(Plugboard.SwappedConfiguration, letter)];
            log += $"{letter}";

            Rotor.AdjustRotorOffset(firstRotor, secondRotor, thirdRotor, 1);

            label1Offset.Text = "I » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[firstRotor.Offset].ToString();
            label2Offset.Text = "II » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[secondRotor.Offset].ToString();
            label3Offset.Text = "III » " + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()[thirdRotor.Offset].ToString();

            var lightingLabel = this.FindLabelByName($"labelLight{letter}");
            lightingLabel.BackColor = Color.LightSeaGreen;
            settingsForm.richTextBoxLogSettings.Text += $"{CryptCount}) " + log + "\n";
            richTextBoxLog.Text += log;
            richTextBoxEncrypted.Text += letter;
            CryptCount++;

            Task.Run(() =>
            {
                Thread.Sleep(500);
                lightingLabel.BackColor = SystemColors.ButtonFace;
            });
        }
        #endregion
    }
}
