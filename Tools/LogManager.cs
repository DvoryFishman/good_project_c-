using System;
using System.IO;
using System.Reflection;

namespace Tools
{
    public static class LogManager
    {
        // שימוש בניתוב התיקייה הנוכחית כבסיס
        private static string pathLog = Path.Combine(getPathCurrentFolder(), "Log");

        public static string getPathCurrentFolder()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string getPathCurrentFile()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        public static void writeToLog(string projectName, string funcName, string message)
        {
            // יצירת היררכיית תיקיות: שנה -> חודש
            string pathYear = Path.Combine(pathLog, DateTime.Now.Year.ToString());
            string pathMonth = Path.Combine(pathYear, DateTime.Now.Month.ToString());

            // יצירת התיקיות אם אינן קיימות
            if (!Directory.Exists(pathMonth))
            {
                Directory.CreateDirectory(pathMonth);
            }

            // נתיב הקובץ לפי היום
            string pathDayFile = Path.Combine(pathMonth, $"{DateTime.Now.Day}.txt");

            // כתיבה לקובץ (הפרמטר true אומר "הוסף לסוף הקובץ" במקום לדרוס)
            using (StreamWriter writer = new StreamWriter(pathDayFile, true))
            {
                writer.WriteLine($"{DateTime.Now}\t{projectName}.{funcName}:\t{message}");
            }
        }

        public static void CleanOldLogs()
        {
            // בדיקה אם תיקיית הלוגים הראשית בכלל קיימת
            if (!Directory.Exists(pathLog)) return;

            // חישוב תאריך הסף: חודשיים אחורה מהיום
            DateTime thresholdDate = DateTime.Now.AddMonths(-2);

            // קבלת כל התיקיות תחת "Log" (שנה/חודש)
            // השתמשתי ב-AllDirectories כדי להגיע לתיקיות החודשים הפנימיות
            string[] directories = Directory.GetDirectories(pathLog, "*", SearchOption.AllDirectories);

            foreach (string dir in directories)
            {
                // קבלת זמן היצירה של התיקייה
                DateTime creationTime = Directory.GetCreationTime(dir);

                // אם התיקייה נוצרה לפני תאריך הסף - מוחקים אותה
                if (creationTime < thresholdDate)
                {
                    try
                    {
                        // המחיקה מתבצעת באופן רקורסיבי (כולל קבצים ותתי-תיקיות)
                        Directory.Delete(dir, true);
                        Console.WriteLine($"Deleted old log directory: {dir}");
                    }
                    catch (Exception ex)
                    {
                        // טיפול במקרה שהקובץ בשימוש או שאין הרשאות
                        Console.WriteLine($"Error deleting {dir}: {ex.Message}");
                    }
                }
            }
        }

    }

}