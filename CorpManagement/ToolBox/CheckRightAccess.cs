using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.Model;
using CorpManagement.ViewModel;

namespace CorpManagement.ToolBox
{
    class CheckRightAccess
    {
        private static string access;
        private static string _VisuData = "V";
        private static string _AddData = "A";
        private static string _UpdateData = "M";
        private static string _DeleteData = "S";
        private const string adminaccess = "Admin";
        private const string allaccess = "All";
        private const string allaccessegal = "All=";
        private const string _accountAdmin = "ADM";
        private TimeSpan _TimerDisplayMessage;
        private TimeSpan _TimerDisplaySpecialMessage;
        protected static string method = "";
        protected static int line = 0;

        protected static void ShowDebugInfo()
        {
            StackFrame stackFrame = new StackFrame(1, true);

            method = stackFrame.GetMethod().ToString();
            line = stackFrame.GetFileLineNumber();
        }

        public string getLetterVis { get { return _VisuData; } }
        public string getLetterAdd { get { return _AddData; } }
        public string getLetterUpd { get { return _UpdateData; } }
        public string getLetterDel { get { return _DeleteData; } }
        public string getAccountAdminType { get { return _accountAdmin; } }
        public TimeSpan timerDisplayMessage { get { return _TimerDisplayMessage; } }
        public TimeSpan timerDisplaySpecialMessage { get { return _TimerDisplaySpecialMessage; } }

        public Dictionary<string, string> tabName = new Dictionary<string, string>();

        public CheckRightAccess()
        {
            if (CurrentUser.userId != null)
                access = CurrentUser.userId.idprofilelevel.name;

            _TimerDisplayMessage = new TimeSpan(0, 0, 10);
            _TimerDisplaySpecialMessage = new TimeSpan(0, 0, 30);

            try
            {
                tabName.Clear();
                tabName.Add("All", "All");
                tabName.Add("Users", "Utilisateurs");
                tabName.Add("Article", "Article");
                tabName.Add("Insurance", "Assurance");
                tabName.Add("Invoice", "Facture");
                tabName.Add("Provider", "Compagnie");
                tabName.Add("Servicing", "Service");
                tabName.Add("Tires", "Pneu");
                tabName.Add("Vehicle", "Vehicule");
                tabName.Add("PoliceLocality", "Localite");
                tabName.Add("AttributeArticle", "Attribution");
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

        }

        /// <summary>
        /// Check authorized access for view, exemple : Vehicule=V Article=M Pneu=S All=S (V=Visualization, A=Add, M=Modification, S=Suppression) => parameter exemple ("Article", "V")
        /// </summary>
        /// <param name="viewaccess"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public bool AuthorizedAccess(string viewaccess, string right)
        {
            if (string.IsNullOrEmpty(access))
                return false;

            if (access.ToLower().Contains(adminaccess.ToLower()))
                return true;

            int indexview;
            int lengthview;

            if (access.ToLower().Contains(allaccess.ToLower()))
            {
                indexview = access.ToLower().IndexOf(allaccessegal.ToLower());
                lengthview = allaccessegal.Length;
                if (checklevelaccess(access.Substring(indexview + lengthview, 1).ToLower(), right.ToLower()))
                    return true;
            }

            if (access.ToLower().Contains(viewaccess.ToLower()))
            {
                string viewegal = viewaccess + "=";
                indexview = access.ToLower().IndexOf(viewegal.ToLower());
                lengthview = viewegal.Length;

                if (checklevelaccess(access.Substring(indexview + lengthview, 1).ToLower(), right.ToLower()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check level access, S => Delete get all access, A => Add or M => Modification get access if right is V (Visualization), or get access if access is egal to right
        /// </summary>
        /// <param name="access"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private bool checklevelaccess(string access, string right)
        {
            if (access == right || access == getLetterDel.ToLower() || (right == getLetterVis.ToLower() && access == getLetterUpd.ToLower())
                || (right == getLetterVis.ToLower() && access == getLetterAdd.ToLower())
                || (right == getLetterAdd.ToLower() && access == getLetterUpd.ToLower()))
                return true;

            return false;
        }
    }
}
