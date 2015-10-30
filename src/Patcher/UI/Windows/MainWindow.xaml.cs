﻿/// Copyright(C) 2015 Unforbidable Works
///
/// This program is free software; you can redistribute it and/or
/// modify it under the terms of the GNU General Public License
/// as published by the Free Software Foundation; either version 2
/// of the License, or(at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program; if not, write to the Free Software
/// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Patcher.Logging;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Threading;

namespace Patcher.UI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisplay, ILogger
    {
        ObservableCollection<LogItem> logItems = new ObservableCollection<LogItem>();
        ObservableCollection<ChoiceItem> choiceItems = new ObservableCollection<ChoiceItem>();

        public LogLevel MaxLogLevel { get; set; }

        bool autoScrollEnabled = true;

        bool terminating = false;

        Choice selectedChoice = null;
        Choice[] offeredChoices = null;

        AutoResetEvent waitFormChoseOption = new AutoResetEvent(false);

        public MainWindow()
        {
            InitializeComponent();

            LoggerItemsControl.DataContext = logItems;
            ChoiceItemsControl.DataContext = choiceItems;

            AppLabel.Content = Program.GetProgramVersionInfo();

            logItems.CollectionChanged += LogItems_CollectionChanged;
        }

        internal void Terminate()
        {
            terminating = true;
            WriteMessage(Brushes.Gold, "");
            WriteMessage(Brushes.Gold, "Press ESC to quit.");
            Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                StatusPanel.Visibility = Visibility.Collapsed;
            }));
        }            

        private void WriteMessage(Brush brush, string message)
        {
            Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => 
            {
                if (logItems.Count > 20000)
                    logItems.RemoveAt(0);

                logItems.Add(new LogItem()
                {
                    Brush = brush,
                    Text = message
                });
            }));
        }

        private void CreateChoiceButtons(Choice[] choices)
        {
            choiceItems.Clear();
            foreach (var choice in choices)
            {
                choiceItems.Add(new ChoiceItem()
                {
                    Brush = new SolidColorBrush(choice.Color),
                    Text = choice.Text,
                    Description = choice.Description
                });
            }
        }

        private void SelectChoice(Choice choice)
        {
            // Ignore if not one of the offered options
            if (offeredChoices.Contains(choice))
            {
                PromptControl.Visibility = Visibility.Collapsed;
                selectedChoice = choice;
                offeredChoices = null;

                waitFormChoseOption.Set();
            }
        }

        private void ChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string text = button.Content.ToString();

            var choice = offeredChoices.Where(o => o.Text.Equals(text, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (choice != null)
                SelectChoice(choice);
        }

        private void LogItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (LoggerItemsControl.IsLoaded)
            {
                var scrollViewer =
                    VisualTreeHelper.GetChild(LoggerItemsControl, 0) as ScrollViewer;

                if (autoScrollEnabled)
                    scrollViewer.ScrollToEnd();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    if (terminating)
                    {
                        Close();
                        e.Handled = true;
                    }
                    break;
            }

            var choices = offeredChoices;
            if (!e.Handled && choices != null)
            {
                var choice = choices.Where(c => c.Key == e.Key).FirstOrDefault();
                if (choice != null)
                {
                    SelectChoice(choice);
                    e.Handled = true;
                }
            }
        }

        private void LoggerItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer =
                 VisualTreeHelper.GetChild(LoggerItemsControl, 0) as ScrollViewer;

            scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
            scrollViewer.Focus();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange < 0 && e.ViewportHeightChange == 0)
            {
                // Stop auto scroll on manual offset change
                autoScrollEnabled = false;
            }
            else if (!autoScrollEnabled && e.VerticalOffset == e.ExtentHeight - e.ViewportHeight)
            {
                // Resume auto scroll on manual offset change at the end of the extent
                autoScrollEnabled = true;
            }
        }

        private void LoggerItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (LoggerItemsControl.IsLoaded)
            {
                var scrollViewer =
                    VisualTreeHelper.GetChild(LoggerItemsControl, 0) as ScrollViewer;

                if (autoScrollEnabled)
                    scrollViewer.ScrollToEnd();
            }
        }

        Progress currentProgress = null;
        private void Progess_Updated(object sender, EventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                if (currentProgress.IsCompleted)
                {
                    currentProgress.Updated -= Progess_Updated;
                    StatusPanel.Visibility = Visibility.Visible;
                    StatusProgressBar.Visibility = Visibility.Hidden;
                    StatusLabel.Content = currentProgress.Title + " finished";
                    StatusText.Text = string.Empty;
                }
                else
                {
                    StatusPanel.Visibility = Visibility.Visible;
                    StatusProgressBar.Visibility = Visibility.Visible;
                    StatusProgressBar.Value = currentProgress.Current;
                    StatusProgressBar.Maximum = currentProgress.Total;
                    StatusLabel.Content = currentProgress.Title;
                    StatusText.Text = currentProgress.Text;
                }
            }));
        }

        void IDisplay.StartProgress(Progress progess)
        {
            currentProgress = progess;
            progess.Updated += Progess_Updated;
        }

        Choice IDisplay.OfferChoice(string message, Choice[] choices)
        {
            Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                CreateChoiceButtons(choices);
                PromptLabel.Content = message;
                PromptControl.Visibility = Visibility.Visible;
            }));

            offeredChoices = choices;

            // Wait until an option is chosen
            waitFormChoseOption.Reset();
            waitFormChoseOption.WaitOne();

            return selectedChoice;
        }

        void IDisplay.WriteText(string text)
        {
            WriteMessage(Brushes.White, text);
        }

        LogLevel ILogger.MaxLogLevel
        {
            get
            {
                return MaxLogLevel;
            }
        }

        void ILogger.WriteLogEntry(LogEntry entry)
        {
            switch (entry.Level)
            {
                case LogLevel.Error:
                    WriteMessage(Brushes.OrangeRed, entry.Text);
                    break;

                case LogLevel.Warning:
                    WriteMessage(Brushes.Orange, entry.Text);
                    break;

                case LogLevel.Info:
                    WriteMessage(Brushes.White, entry.Text);
                    break;

                case LogLevel.Fine:
                    WriteMessage(Brushes.DarkGray, entry.Text);
                    break;
            }
        }
    }
}