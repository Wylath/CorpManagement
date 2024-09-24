using System;
using System.Windows;
using CorpManagement;
using CorpManagement.Model;
using CorpManagement.View;
using CorpManagement.ViewModel;

namespace CorpManagement.Toolbox
{
    class SwitchWindows
    {
        public void ChangeViewWindows(string nameWindow, User parameterId)
        {
            Window wd = Application.Current.MainWindow;

            switch(nameWindow)
            {
                case "AllView":
                    AllView allview = new AllView
                    {
                        DataContext = new AllUserViewModel()
                    };
                    wd.Title = "Corp Management : " + parameterId.lastname + " " + parameterId.firstname + ", Accès : " + parameterId.idprofilelevel.name;
                    wd.Content = allview.Content;
                    break;
                case "MainWindow":
                    MainWindow main = new MainWindow
                    {
                        DataContext = new AllUserViewModel()
                    };
                    wd.Title = "Corp Management";
                    wd.Content = main.Content;
                    break;
                default:
                    break;
            }
        }
    }
}
