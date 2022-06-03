using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_CommerceBackEnd
{
    enum MessageErrorLevel
    {
        Warning,
        Error,
        Info,
        ValidationError
    }
    class Utils
    {
        public static string ValidateTextBoxs(params TextBox[] args)
        {

            foreach (TextBox arg in args)
            {
                if (arg.Text.Trim() == string.Empty)
                {
                    return string.Format("{0} est obligatoire", arg.Name.Replace("txt", ""));
                }
            }

            return "";
        }
        public static string ValidateComboBoxs(params ComboBox[] args)
        {

            foreach (ComboBox arg in args)
            {
                if (arg.SelectedIndex == -1)
                {
                    return string.Format("{0} est obligatoire", arg.Name.Replace("combo",""));
                }
            }

            return "";
        }
        public static void MessageBox(string text,  MessageErrorLevel level ,string caption )
        {
            switch (level)
            {
                case MessageErrorLevel.Error:
                    System.Windows.Forms.MessageBox.Show(text, caption ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case MessageErrorLevel.ValidationError:
                    System.Windows.Forms.MessageBox.Show(text, caption ?? "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                case MessageErrorLevel.Info:
                    System.Windows.Forms.MessageBox.Show(text, caption ?? "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case MessageErrorLevel.Warning:
                    System.Windows.Forms.MessageBox.Show(text, caption ?? "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
