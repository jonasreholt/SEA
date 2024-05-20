namespace Model.FrontEndAPI;
using Model.Survey;
    public static class FrontEndSuperUserMenu {
        // Flyt 3 nedenstående til separat interface?
        public static void DeleteSurvey(int surveyId) {

        }
        public static IModifySurvey? ModifySurvey(int surveyId) {
            return null;
        }
        public static IModifySurvey CreateSurvey() {
            return null;
        }

        public static string ExportSurvey(int surveyId) {
            return null;
        }

    }