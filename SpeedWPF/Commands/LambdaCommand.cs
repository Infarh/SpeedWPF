using System;
using System.Collections.Generic;
using System.Text;
using SpeedWPF.Commands.Base;

namespace SpeedWPF.Commands
{
    public class LambdaCommand : Command
    {
        private readonly Action _CommandAction;
        private readonly Func<bool> _CanExecute;

        public LambdaCommand(Action CommandAction, Func<bool> CanExecute)
        {
            _CommandAction = CommandAction;
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke() ?? true;

        public override void Execute(object parameter) => _CanExecute();
    }

    public class LambdaCommand<T> : Command
    {
        private readonly Action<T> _CommandAction;
        private readonly Func<T, bool> _CanExecute;

        public LambdaCommand(Action<T> CommandAction, Func<T, bool> CanExecute)
        {
            _CommandAction = CommandAction;
            _CanExecute = CanExecute;
        }

        private static T GetParameter(object parameter) => (T)Convert.ChangeType(parameter, typeof(T));

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(GetParameter(parameter)) ?? true;

        public override void Execute(object parameter) => _CanExecute(GetParameter(parameter));
    }
}
