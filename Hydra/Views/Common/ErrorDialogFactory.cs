using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Hydra.Views.Common {
    /// <summary>
    /// I just wanted a common, reusable NetworkErrorDialog, but that would mean extending
    /// MessageDialog, and *no one* at Microsoft thought it could come in handy to be able
    /// to extend things.
    /// </summary>
    public class ErrorDialogFactory {
        public static MessageDialog NetworkErrorDialog() {
            MessageDialog dialog = new MessageDialog("Fout: er kon geen verbinding gemaakt worden met het netwerk.");
            dialog.Commands.Add(new UICommand("OK") { Id = 0 });
            return dialog;
        }
    }
}
