using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatBot
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Nuevo = new RoutedUICommand
            (
                "Nuevo",
                "Nuevo",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.N, ModifierKeys.Control)
                }
            );

        

        public static readonly RoutedUICommand SendMessage = new RoutedUICommand
            (
                "SendMessage",
                "SendMessage",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.Enter)
                }
            );

        public static readonly RoutedUICommand CheckConnection = new RoutedUICommand
            (
                "CheckConnection",
                "CheckConnection",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.O, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Save = new RoutedUICommand
            (
                "Save",
                "Save",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.G, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand OpenSettings = new RoutedUICommand
            (
                "OpenSettings",
                "OpenSettings",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.C, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Close = new RoutedUICommand
            (
                "Close",
                "Close",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.S, ModifierKeys.Control)
                }
            );

    }
}
