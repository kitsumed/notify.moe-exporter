using System.Windows.Forms;

namespace NotifyDotMoeExport
{
    public partial class MessageBoxWithTextInput : Form
    {
        /// <summary>
        /// Customized MessageBox that allow for inputs
        /// </summary>
        /// <param name="content">Text displayed as message</param>
        /// <param name="title">Title of the message box</param>
        public MessageBoxWithTextInput(string content, string title)
        {
            InitializeComponent();
            labelMessage.Text = content;
            this.Text = title;
        }

        /// <summary>
        /// The input of the user inside the MessageBox
        /// </summary>
        public string GetUserInput 
        {
            get => textBoxInput.Text;
        }
    }
}
