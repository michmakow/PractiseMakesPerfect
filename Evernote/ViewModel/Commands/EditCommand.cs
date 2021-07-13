﻿using System;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class EditCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM ViewModel { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.StartEditing();
        }

        public EditCommand(NotesVM vm)
        {
            ViewModel = vm;
        }

    }
}
