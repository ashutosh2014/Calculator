using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using Calculator.Lib;
using System.Text.RegularExpressions;
using System.Resources;
using System.Collections;

namespace Calculator.Win
{
    public partial class CalculatorWin : Form
    {

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        private MenuStrip mainMenu = new MenuStrip();
        private ToolStripMenuItem menuItemEdit = new ToolStripMenuItem();
        private ToolStripMenuItem menuItemCopy = new ToolStripMenuItem();
        private ToolStripMenuItem menuItemPaste = new ToolStripMenuItem();
        private ToolStripMenuItem menuItemExit = new ToolStripMenuItem();
        private ToolStripMenuItem menuItemHelp = new ToolStripMenuItem();
        private TextBox txtBoxValue = new TextBox();
        private Label lblDisplay = new Label();
        private ToolTip toolTip = new ToolTip();
        private Font fntConsolasBold = new Font(CalculatorWinResource.Consolas, 12F, FontStyle.Bold);
        private Font fntConsolas = new Font(CalculatorWinResource.Consolas, 9F, FontStyle.Regular);

        private double _storedValue = 0;
        private bool _buttonPressed = false;
        private bool _result = false;
        private bool _resultFlag = false;
        private int _openBracket = 0;
        private int _closeBracket = 0;
        
        CalcEngine calcEngine = new CalcEngine();
        ArithmeticOperations arithmeticOperations = new ArithmeticOperations();
        ScientificOperations scientificOperations = new ScientificOperations();
        Expression expression = new Expression();
        
        // Initialize the Components
        ///<summary>
        /// Constructor of class which initialize the component.
        ///</summary>
        public CalculatorWin()
        {
            InitializeComponent();
        }

        // Code inside the method executed when form loaded
        ///<summary>
        ///Code inside the method executed when form loaded.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void CalculatorWin_Load(object sender, EventArgs e)
        {
            Dictionary<string, ToolStripMenuItem> menuItems = new Dictionary<string, ToolStripMenuItem>()
            {
                { CalculatorWinMenu._Paste , this.menuItemPaste  },
                { CalculatorWinMenu._Copy , this.menuItemCopy  },
                { CalculatorWinMenu._Help , this.menuItemHelp  },
                { CalculatorWinMenu.E_xit , this.menuItemExit  }
            };
            Dictionary<string, EventHandler> menuEvents = new Dictionary<string, EventHandler>()
            {
                { CalculatorWinMenu._Paste , MenuItemPaste_Click  },
                { CalculatorWinMenu._Copy , MenuItemCopy_Click  },
                { CalculatorWinMenu._Help , MenuItemHelp_Click  },
                { CalculatorWinMenu.E_xit , MenuItemExit_Click  }
            };
            Dictionary<string, Font> actions = new Dictionary<string, Font>()
            {
                { CalculatorWinResource.fntConsolas, fntConsolas},
                { CalculatorWinResource.fntConsolasBold, fntConsolasBold},
            };
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemEdit, this.menuItemExit, this.menuItemHelp });
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mainMenu.Name = "maineMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 24);
            this.mainMenu.Text = "MainMenu";
            // 
            // ToolStripMenuItemEdit
            // 
            this.menuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemCopy, this.menuItemPaste });
            this.menuItemEdit.Name = "menuItemEdit";
            this.menuItemEdit.Size = new System.Drawing.Size(180, 20);
            this.menuItemEdit.Text = "&Edit";
            this.menuItemEdit.Font = fntConsolasBold;
            
            using (ResXResourceReader resx = new ResXResourceReader(@"../../CalculatorWinMenu.resx"))
            {
                foreach (DictionaryEntry entry in resx)
                {
                    menuItems[entry.Value.ToString()].Text = entry.Key.ToString();
                    menuItems[entry.Value.ToString()].Name = entry.Value.ToString();
                    menuItems[entry.Value.ToString()].Size = new System.Drawing.Size(180, 2);
                    menuItems[entry.Value.ToString()].Font = fntConsolasBold;
                    menuItems[entry.Value.ToString()].Click += menuEvents[entry.Value.ToString()];
                }
            }
            // 
            // txtBoxValue
            // 
            this.txtBoxValue.Font = new System.Drawing.Font(CalculatorWinResource.Consolas, 16F, System.Drawing.FontStyle.Bold);
            this.txtBoxValue.Location = new System.Drawing.Point(30, 74);
            this.txtBoxValue.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxValue.Name = "txtBoxValue";
            this.txtBoxValue.Size = new System.Drawing.Size(376, 32);
            this.txtBoxValue.TabIndex = 0;
            this.txtBoxValue.Text = "0";
            this.txtBoxValue.Cursor = Cursors.Arrow;
            this.txtBoxValue.SelectionStart = 0;
            this.txtBoxValue.GotFocus += (s, e1) => { HideCaret(txtBoxValue.Handle); };
            this.txtBoxValue.KeyPress += TxtBoxValue_KeyPress;
            this.txtBoxValue.LostFocus += TxtBoxValue_LostFocus;
            this.txtBoxValue.TextAlign = HorizontalAlignment.Right;
            // 
            // lblDisplay
            // 
            this.lblDisplay.Font = fntConsolasBold;
            this.lblDisplay.Location = new System.Drawing.Point(30, 39);
            this.lblDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(376, 26);
            this.lblDisplay.Text = String.Empty;
            this.lblDisplay.BackColor = Color.FromArgb(0x78D3D3D3);
            //
            //buttons are added referring from resource file "CalculatorWinControls.resx"
            //
            using (ResXResourceReader resx = new ResXResourceReader(@"../../CalculatorWinControls.resx"))
            {
                foreach (DictionaryEntry entry in resx)
                {
                    string[] valuesOfButton = entry.Value.ToString().Split(',');
                    Button button = new Button();
                    button.Name = entry.Key.ToString();
                    button.Text = valuesOfButton[0];
                    button.Size = new System.Drawing.Size(Convert.ToInt32(valuesOfButton[1]), Convert.ToInt32(valuesOfButton[2]));
                    button.Margin = new System.Windows.Forms.Padding(Convert.ToInt32(valuesOfButton[3]));
                    button.Location = new System.Drawing.Point(Convert.ToInt32(valuesOfButton[4]), Convert.ToInt32(valuesOfButton[5]));
                    button.Font = actions[valuesOfButton[6]];
                    button.Click += Btn_Click;
                    button.MouseDown += Btn_MouseDown;
                    this.Controls.Add(button);
                }
            }
            //
            //Adding controls to CalculatorWin Form
            //
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Controls.Add(this.txtBoxValue);
            this.Controls.Add(this.lblDisplay);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ActiveControl = this.txtBoxValue;
        }
        // Exit from the Application
        ///<summary>
        ///Exit from the Application.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // Copy Value from TextBox "txtBoxValue" to Clipboard
        ///<summary>
        ///Copy Value from TextBox "txtBoxValue" to Clipboard.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void MenuItemCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtBoxValue.Text);
        }
        // Paste Value to TextBox "txtBoxValue" from Clipboard
        ///<summary>
        ///Paste Value to TextBox "txtBoxValue" from Clipboard.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        /// <remarks>
        /// <para>This copies only numbers.</para>
        /// </remarks>
        private void MenuItemPaste_Click(object sender, EventArgs e)
        {
            string pasteValue = Clipboard.GetText();
            txtBoxValue.Text = String.Empty;
            foreach (char digit in pasteValue)
            {
                if (Char.IsDigit(digit))
                {
                    txtBoxValue.Text += digit;
                }
            }
            if (txtBoxValue.Text == String.Empty)
            {
                txtBoxValue.Text = CalculatorWinResource.zero;
            }
        }
        // Load the new Form "HelpWin"
        ///<summary>
        ///Load the new Form "HelpWin".
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void MenuItemHelp_Click(object sender, EventArgs e)
        {
            HelpWin helpWin = new HelpWin();
            helpWin.ShowDialog();
        }
        // Click event handler
        ///<summary>
        ///Click event handler.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void Btn_Click(object sender, EventArgs e)
        {
            string[] values = Regex.Split(sender.ToString(), @"[\s,]+");
            Evaluate(values[2]);
        }
        // MouseDown Event handler which right click to show tooltip.
        ///<summary>
        ///MouseDown Event handler which right click to show tooltip.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void Btn_MouseDown(object sender, MouseEventArgs e)
        {
            string[] values = Regex.Split(sender.ToString(), @"[\s,]+");
            switch (e.Button)
            {
                case MouseButtons.Right:
                    toolTip.SetToolTip((Button)sender, CalculatorWinResource.ResourceManager.GetString(CalculatorWinResource.value + values[2]));
                    break;
            }
        }
        // KeyPress Event handler on TextBox.
        ///<summary>
        ///KeyPress Event handler on TextBox.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void TxtBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            Evaluate(expression.ChangeKey(e.KeyChar.ToString()));
            e.Handled = true;

            switch (e.KeyChar)
            {
                case (char)13:
                    Result();
                    break;
                case (char)Keys.Back:
                    BackspaceEvent();
                    break;
            }
        }
        // To set focus on textbox when focus lost.
        ///<summary>
        /// To set focus on textbox when focus lost.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void TxtBoxValue_LostFocus(object sender, EventArgs e)
        {
            txtBoxValue.Focus();
        }
        // To append number to TextBox
        ///<summary>
        /// To append number <paramref name="number"/> to TextBox.
        ///</summary>
        ///<param name= "number" > A character.</param>
        private void AddNumber(char number)
        {
            if (_result)
            {
                txtBoxValue.Text = CalculatorWinResource.zero;
                _result = false;
            }
            if (txtBoxValue.Text == CalculatorWinResource.zero || _buttonPressed)
            {
                txtBoxValue.Text = String.Empty;
                _buttonPressed = false;
            }
            txtBoxValue.Text += number;
        }
        // To append dot to TextBox
        ///<summary>
        /// To append dot to TextBox.
        ///</summary>
        private void AddPoint()
        {
            if (!txtBoxValue.Text.Contains(CalculatorWinResource.Point))
            {
                txtBoxValue.Text += CalculatorWinResource.Point;
            }
        }
        // To clear whole text written on label as well as textbox
        ///<summary>
        ///To clear whole text written on label as well as textbox.
        ///</summary>
        private void ClearText()
        {
            txtBoxValue.Text = CalculatorWinResource.zero;
            lblDisplay.Text = String.Empty;
            _result = false;
            _resultFlag = false;
            _openBracket = 0;
            _closeBracket = 0;
        }
        
        // To clear whole text written on textbox
        ///<summary>
        ///To clear whole text written on textbox.
        ///</summary>
        private void ClearEntry()
        {
            txtBoxValue.Text = CalculatorWinResource.zero;
        }

        // To clear last character written  on textbox
        ///<summary>
        ///To clear last character written on textbox.
        ///</summary>
        private void BackspaceEvent()
        {
            if ((txtBoxValue.Text.Length == 2 && txtBoxValue.Text.Contains(CalculatorWinResource.minusSign)) || txtBoxValue.Text.Length == 1)
            {
                txtBoxValue.Text = CalculatorWinResource.zero;
            }
            else if(!_result)
            {
                txtBoxValue.Text = txtBoxValue.Text.Substring(0, txtBoxValue.Text.Length - 1);
            }
        }
        // To change the sign of number written over textbox
        ///<summary>
        ///To change the sign of number written over textbox.
        ///</summary>
        private void SignChange()
        {

            if (txtBoxValue.Text == CalculatorWinResource.zero)
            {
                return;
            }
            if (txtBoxValue.Text.Contains(CalculatorWinResource.minusSign))
            {
                txtBoxValue.Text = txtBoxValue.Text.Replace(CalculatorWinResource.minusSign, String.Empty);
            }
            else
            {
                txtBoxValue.Text = CalculatorWinResource.minusSign + txtBoxValue.Text;
            }
        }

        // To perform basic arithmetic operations ( +, -, * ,/ )
        ///<summary>
        ///To perform basic arithmetic operations ( +, -, * ,/ ) based on character <paramref name="operation"/> 
        ///</summary>
        ///<param name= "operation" > A character.</param>
        private void BasicOperations(char operation)
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                if (_resultFlag)
                {
                    lblDisplay.Text = String.Empty;
                    _resultFlag = false;
                }
                if (_buttonPressed && !(lblDisplay.Text == String.Empty) && expression.IsOperator(lblDisplay.Text[lblDisplay.Text.Length - 1]))
                {
                    lblDisplay.Text = lblDisplay.Text.Substring(0, lblDisplay.Text.Length - 1);
                }
                else
                {
                    lblDisplay.Text += txtBoxValue.Text;
                }
                lblDisplay.Text += operation;
                _buttonPressed = true;
            }
        }
        // Evalutes the whole expression and copies the result back to TextBox .
        ///<summary>
        ///Evalutes the whole expression and copies the result back to TextBox. 
        ///</summary>
        private void Result()
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                if (_resultFlag)
                {
                    lblDisplay.Text = String.Empty;
                }
                if (!lblDisplay.Text.EndsWith(")"))
                {
                    lblDisplay.Text += txtBoxValue.Text;
                }
                txtBoxValue.Text = expression.CheckResult(calcEngine.Calculate(expression.ChangeExpression(lblDisplay.Text, _closeBracket - _openBracket)));
                _result = true;
                _resultFlag = true;
                _openBracket = 0;
                _closeBracket = 0;
            }
        }
        // Find out the percentage and copies the result back to TextBox.
        ///<summary>
        ///Find out the percentage and copies the result back to TextBox. 
        ///</summary>
        private void PerCentEvaulation()
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                double res = 0;
                try
                {
                    if (lblDisplay.Text.Substring(lblDisplay.Text.Length - 1, 1) == "*" || lblDisplay.Text.Substring(lblDisplay.Text.Length - 1, 1) == "/")
                    {
                        txtBoxValue.Text = arithmeticOperations.Divide(Convert.ToDouble(txtBoxValue.Text), 100).ToString();
                    }
                    else if (lblDisplay.Text != String.Empty)
                    {
                        res = calcEngine.Calculate(expression.ChangeExpression(lblDisplay.Text.Substring(0, lblDisplay.Text.Length - 1), _closeBracket - _openBracket));
                        txtBoxValue.Text = arithmeticOperations.Divide(arithmeticOperations.Multiply(res, Convert.ToDouble(txtBoxValue.Text)), 100).ToString();
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(CalculatorWinResource.Exception + ex.Message);
                    txtBoxValue.Text = arithmeticOperations.Divide(Convert.ToDouble(txtBoxValue.Text), 100).ToString();
                }
            }
        }
        
        // To perform scientific operations ( sin, cos, tan, log, ln, 1/x )
        ///<summary>
        ///To perform scientific operations ( sin, cos, tan, log, ln, 1/x )  based on string <paramref name="sender"/>. 
        ///</summary>
        ///<param name= "sender" > A string.</param>
        private void ScientificOperations(string sender)
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                double result = 0;
                switch (sender)
                {
                    case "sin":
                        result = scientificOperations.Sine(Convert.ToDouble(txtBoxValue.Text));
                        break;
                    case "cos":
                        result = scientificOperations.Cosine(Convert.ToDouble(txtBoxValue.Text));
                        break;
                    case "tan":
                        result = scientificOperations.Tangent(Convert.ToDouble(txtBoxValue.Text));
                        break;
                    case "log":
                        result = scientificOperations.LogBase10(Convert.ToDouble(txtBoxValue.Text));
                        break;
                    case "ln":
                        result = scientificOperations.LogBaseE(Convert.ToDouble(txtBoxValue.Text));
                        break;
                    case "1/x":
                        result = arithmeticOperations.Divide(1, Convert.ToDouble(txtBoxValue.Text));
                        break;
                }
                txtBoxValue.Text = expression.CheckResult(result);
                _result = true;
            }
        }

        // To add open bracket to textbox
        ///<summary>
        ///To add open bracket to textbox. 
        ///</summary>
        private void OpenBracket()
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                if (_resultFlag)
                {
                    lblDisplay.Text = String.Empty;
                    _resultFlag = false;
                    _result = false;
                }
                if (!(txtBoxValue.Text == CalculatorWinResource.zero))
                {
                    lblDisplay.Text += txtBoxValue.Text;
                }
                lblDisplay.Text += CalculatorWinResource.OpenBrace;
                _buttonPressed = true;
                _openBracket += 1;
            }
        }
        // To add close bracket to textbox
        ///<summary>
        ///To add close bracket to textbox. 
        ///</summary>
        private void CloseBracket()
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                if (_resultFlag)
                {
                    lblDisplay.Text = String.Empty;
                    _resultFlag = false;
                    _result = false;
                }
                if (_openBracket - _closeBracket > 0)
                {
                    if (!_buttonPressed && !(txtBoxValue.Text == CalculatorWinResource.zero))
                    {
                        lblDisplay.Text += txtBoxValue.Text;
                    }
                    lblDisplay.Text += CalculatorWinResource.CloseBrace;
                    _buttonPressed = true;

                    _closeBracket += 1;
                }
            }
        }
        // To add a value of TextBox to memory and copies the result back to TextBox.
        ///<summary>
        ///To add a value of TextBox to memory and copies the result back to TextBox. 
        ///</summary>
        private void MplusEvaluation()
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                Result();
                _storedValue = arithmeticOperations.Add(_storedValue, Convert.ToDouble(txtBoxValue.Text));
                txtBoxValue.Text = _storedValue.ToString();
                _buttonPressed = true;
            }
        }
        // To subtract a value of TextBox from memory and copies the result back to TextBox.
        ///<summary>
        ///To subtract a vasclue of TextBox from memory and copies the result back to TextBox. 
        ///</summary>
        private void MminusEvaluation()
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                Result();
                _storedValue = arithmeticOperations.Subtract(_storedValue, Convert.ToDouble(txtBoxValue.Text));
                txtBoxValue.Text = _storedValue.ToString();
                _buttonPressed = true;
            }
        }
        // To Read a value from memory and copy to TextBox.
        ///<summary>
        ///To Read a value from memory and copy to TextBox. 
        ///</summary>
        private void MREvaluation()
        {
            txtBoxValue.Text = _storedValue.ToString();
            _buttonPressed = true;
        }
        // To Store a value from TextBox to memory.
        ///<summary>
        ///To Store a value from TextBox to memory. 
        ///</summary>
        private void MSEvaluation()
        {
            if (expression.CheckInput(txtBoxValue.Text))
            {
                Result();
                _storedValue = Convert.ToDouble(txtBoxValue.Text);
                _buttonPressed = true;
            }
        }
        // To Clear a value from memory.
        ///<summary>
        ///To Clear a value from memory. 
        ///</summary>
        private void MCEvaluation()
        {
            _storedValue = 0;
        }
        /// <summary>
        /// This find which functin to be performed.
        /// </summary>
        /// <param name="value"></param>
        private void SomeLeftOperations(string value)
        {
            switch (value)
            {
                case "M+":
                    MplusEvaluation();
                    break;
                case "M-":
                    MminusEvaluation();
                    break;
                case "MR":
                    MREvaluation();
                    break;
                case "MS":
                    MSEvaluation();
                    break;
                case "MC":
                    MCEvaluation();
                    break;
                case ".":
                    AddPoint();
                    break;
                case "=":
                    Result();
                    break;
                case "Result":
                    Result();
                    break;
                case "%":
                    PerCentEvaulation();
                    break;
                case "+/-":
                    SignChange();
                    break;
                case "(":
                    OpenBracket();
                    break;
                case ")":
                    CloseBracket();
                    break;
                case "AC":
                    ClearText();
                    break;
                case "CE":
                    ClearEntry();
                    break;
                case "<-":
                    BackspaceEvent();
                    break;
            }
        }
        //This find out which function to evaluated.
        /// <summary>
        /// This find out which function to evaluated based on <paramref name="value"/>
        /// </summary>
        /// <param name="value">A string</param>
        private void Evaluate(string value)
        {
            if (Char.IsDigit(value[0]))
            {
                AddNumber(value[0]);
                return;
            }
            else if (expression.IsOperator(value[0]) && value.Length == 1)
            {
                BasicOperations(value[0]);
                return;
            }
            else if (expression.IsScientific(value))
            {
                ScientificOperations(value);
            }
            else
            {
                SomeLeftOperations(value);
            }
        }
    }
}