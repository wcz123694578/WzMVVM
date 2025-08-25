using System;
using System.Collections.Generic;
using System.Text;

namespace WzMVVM.Service
{
    public interface IDialogAware
    {
        void OnDialogOpened(DialogParameters parameters);
        DialogResult OnDialogClosed();
    }
}
