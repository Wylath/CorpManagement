using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.ToolBox
{
    static class SpecialInformationMessage
    {
        // User
        public static string ErrorMessageLogin = "Pas d'utilisateur windows trouvé.";
        public static string ErrorMessageUserProfile = "Aucun niveau d'accès pour l'utilisateur n'a pu être trouvé.\nContacter l'administrateur pour vérifier le niveau d'accès du compte.";
        public static string ErrorMessageUserNotFound = "Aucun utilisateur correspondant à votre session n'a été trouvé.";
        public static string ErrorMessageUserNotActive = "Votre compte utilisateur n'est plus actif.\nVeuillez contacter l'administrateur.";
        public static string ErrorTitleLogin = "Erreur interface utilisateur";
        public static string ErrorTitleUserProfile = "Erreur profile utilisateur";
        public static string ErrorTitleUserNotFound = "Erreur utilisateur";
        public static string ErrorTitleUserNotActive = "Utilisateur inactif";
        // Result request DB
        public static string addstatusmessage = " Ajout réussi ! ";
        public static string updatestatusmessage = " Modification réussie ! ";
        public static string deletestatusmessage = " Suppression réussie ! ";
        // Error folder
        public static string ErrorMessageFilesFolder = "Aucun dossier n'a été choisis pour l'emplacement des fichiers.";
        public static string ErrorTitleFilesFolder = "Erreur dossier";
        public static string ErrorTitleOpenFiles = "Erreur ouverture du fichier";
        // Directory name
        public static string FileDirectoryArticle = "DirectoryFilesArticle";
        public static string FileDirectoryEquipment = "DirectoryFilesEquipment";
        public static string FileDirectoryInsurance = "DirectoryFilesInsurance";
        public static string FileDirectoryInvoice = "DirectoryFilesInvoice";
        public static string FileDirectoryServicing = "DirectoryFilesServicing";
        public static string FileDirectoryTires = "DirectoryFilesTires";
        public static string FileDirectoryVehicle = "DirectoryFilesVehicle";
    }
}
