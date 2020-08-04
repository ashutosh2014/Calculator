using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.Win
{
    /*
    The partial HelpWin class
    Contains all methods which are used in Help WinForm. 
    */
    /// <summary>
    /// The partial <c>HelpWin</c> class.
    /// Contains all methods which are used in Help WinForm.
    /// <list type="bullet">
    /// <item>
    /// <term>HelpWinLoad</term>
    /// <description>Win Form Loaded</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>This class contain the help section.</para>
    /// </remarks>
    public partial class HelpWin : Form
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        private TextBox txtBoxHelp = new TextBox();
        
        // Initialize the Components
        ///<summary>
        /// Constructor of class which initialize the component.
        ///</summary>
        public HelpWin()
        {
            InitializeComponent();
        }
        
        // Code inside the method executed when form loaded
        ///<summary>
        ///Code inside the method executed when form loaded.
        ///</summary>
        ///<param name= "sender" > An object referrring the original sender.</param>
        ///<param name = "e" > An Event Args.</param>
        private void HelpWin_Load(object sender, EventArgs e)
        {
            //
            //txtBoxHelp
            //
            this.txtBoxHelp.Text = HelpWinData.HelpData;
            this.txtBoxHelp.Dock = DockStyle.Fill;
            this.txtBoxHelp.Multiline = true;
            this.txtBoxHelp.ScrollBars = ScrollBars.Vertical;
            this.txtBoxHelp.GotFocus += (s, e1) => { HideCaret(txtBoxHelp.Handle); };
            this.txtBoxHelp.SelectionStart = 0;
            this.txtBoxHelp.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.txtBoxHelp.ReadOnly = true;
            //
            //HelpWin
            //
            this.Controls.Add(txtBoxHelp);
        }
    }
}
