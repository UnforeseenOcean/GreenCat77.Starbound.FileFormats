using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using DialogResult = System.Windows.Forms.DialogResult;
using Path = System.IO.Path;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.ComponentModel;

namespace GreenCat77.Starbound.FileFormats.TestApp
{
    public enum FileOpenMode
    {
        None,
        Opened,
        New
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer mainTimer = new DispatcherTimer();
        public static FileOpenMode FileOpen = FileOpenMode.None;
        public static FileOpenMode Prev_FileOpen = FileOpenMode.None;
        public static FileInfo CurrentFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Starbound Player.player"));
        public static bool UnsavedChanges = false;
        /// <summary>
        /// This is only a temporary variable since we only have one kind of save file in the lib.
        /// </summary>        
        [ExpandableObject]
        public static PlayerFile CurrentPlayerFile = new PlayerFile();

        public MainWindow()
        {
            InitializeComponent();
            mainTimer.Interval = new TimeSpan(166667L);
            mainTimer.Tick += mainTimer_Tick;
            mainTimer.Start();
        }

        void mainTimer_Tick(object sender, EventArgs e)
        {
            if (FileOpen == FileOpenMode.None)
            {
                textBoxCurFile.Text = "N/A";
            }
            else if (FileOpen == FileOpenMode.Opened)
            {
                textBoxCurFile.Text = CurrentFile.FullName;
            }
            else if (FileOpen == FileOpenMode.New)
            {
                textBoxCurFile.Text = "New Untitled File";
            }
            menuItemClose.IsEnabled = (FileOpen != FileOpenMode.None);
            menuItemSave.IsEnabled = ((FileOpen == FileOpenMode.Opened) && UnsavedChanges) || (FileOpen == FileOpenMode.New);
            menuItemSaveAs.IsEnabled = (FileOpen != FileOpenMode.None);
            buttonBreak.IsEnabled = (FileOpen != FileOpenMode.None);
            propertyGrid.IsEnabled = (FileOpen != FileOpenMode.None);
            RefreshPropertyGrid();
        }

        private void menuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            if (PromptToSaveUnsavedChanges())
            {
                OpenFileDialog ofd = GetOpenFileDialog();
                ofd.ShowDialog();
                if (!string.IsNullOrEmpty(ofd.FileName))
                {
                    CurrentFile = new FileInfo(ofd.FileName);
                    LoadCurrentFile();
                    FileOpen = FileOpenMode.Opened;
                    UnsavedChanges = false;
                }
            }
        }

        private void RefreshPropertyGrid()
        {
            if (FileOpen != Prev_FileOpen)
            {
                propertyGrid.SelectedObject = (FileOpen != FileOpenMode.None) ? CurrentPlayerFile : null;
            }

            Prev_FileOpen = FileOpen;
        }

        /// <summary>
        /// The contents of this method are temporary.
        /// </summary>
        private void SaveCurrentFile()
        {
            MainIO.SavePlayerFile(CurrentPlayerFile, CurrentFile.FullName);

            UnsavedChanges = false;
        }


        /// <summary>
        /// The contents of this method are temporary.
        /// </summary>
        private void LoadCurrentFile()
        {
            CurrentPlayerFile = MainIO.LoadPlayerFile(CurrentFile.FullName);
        }

        private bool SaveAs()
        {
            SaveFileDialog sfd = GetSaveFileDialog();

            DialogResult dr = sfd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
            {
                CurrentFile = new FileInfo(sfd.FileName);
                FileOpen = FileOpenMode.Opened;
                SaveCurrentFile();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PromptToSaveUnsavedChanges()
        {
            if ((FileOpen == FileOpenMode.Opened && UnsavedChanges) || FileOpen == FileOpenMode.New)
            {
                MessageBoxResult mbr = MessageBox.Show(string.Format("Save file '{0}'?", (FileOpen == FileOpenMode.Opened) ? CurrentFile.Name.Replace(CurrentFile.Extension, "") : "New Untitled File"), "Save", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (mbr == MessageBoxResult.Yes)
                {
                    if (FileOpen == FileOpenMode.Opened)
                    {
                        SaveCurrentFile();
                    }
                    else
                    {
                        if (SaveAs())
                        {
                            return true;
                        }
                        else
                        {
                            return PromptToSaveUnsavedChanges();
                        }
                    }

                    return true;
                }
                else
                {
                    return (mbr == MessageBoxResult.No);
                }
            }
            else
            {
                return true;
            }
        }

        private OpenFileDialog GetOpenFileDialog()
        {
            OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.AddExtension = false;
            ofd.AutoUpgradeEnabled = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "Starbound Assets|*.player|All files (*.*)|*.*";
            ofd.InitialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam\\steamapps\\common\\Starbound\\assets");
            ofd.RestoreDirectory = true;
            ofd.SupportMultiDottedExtensions = true;
            ofd.DereferenceLinks = true;
            ofd.ValidateNames = true;

            return ofd;
        }

        private SaveFileDialog GetSaveFileDialog()
        {
            SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.AddExtension = false;
            sfd.AutoUpgradeEnabled = true;
            sfd.CheckFileExists = true;
            sfd.CheckPathExists = true;
            sfd.Filter = "Starbound Assets|*.player|All files (*.*)|*.*";            
            sfd.RestoreDirectory = true;
            sfd.SupportMultiDottedExtensions = true;
            sfd.DereferenceLinks = true;
            sfd.Title = "Save As";
            sfd.ValidateNames = true;
            if (FileOpen == FileOpenMode.Opened)
            {
                sfd.InitialDirectory = CurrentFile.Directory.FullName;
                sfd.FileName = CurrentFile.Name;
            }
            else if (FileOpen == FileOpenMode.New)
            {
                sfd.InitialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam\\steamapps\\common\\Starbound\\assets");
                sfd.FileName = "New Untitled File";
            }
            

            return sfd;
        }

        /// <summary>
        /// Completely empty method. Just there for future.
        /// </summary>
        private void CreateNew()
        {

        }

        private void menuItemNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The ability to create new files is not implemented yet.", "Not Yet D:", MessageBoxButton.OK, MessageBoxImage.Stop);

            /*
            if (PromptToSaveUnsavedChanges())
            {
                CreateNew();
            }
            */
        }

        private void menuItemClose_Click(object sender, RoutedEventArgs e)
        {
            if (PromptToSaveUnsavedChanges())
            {
                FileOpen = FileOpenMode.None;
                UnsavedChanges = false;
                CurrentFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Starbound Player.player"));           
            }
        }

        private void menuItemSave_Click(object sender, RoutedEventArgs e)
        {
            if (FileOpen == FileOpenMode.Opened && UnsavedChanges)
            {
                SaveCurrentFile();
            }
        }

        private void menuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (FileOpen != FileOpenMode.None)
            {
                SaveAs();   
            }
        }

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            if (PromptToSaveUnsavedChanges())
            {
                Close();
            }
        }

        private void buttonBreak_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
