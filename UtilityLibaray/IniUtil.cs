using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace UtilityLibaray {

    public static class IniUtil {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
            string section,
            string key,
            string val,
            string file);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string file);

        private static bool EnsureFileExists(string file) {
            if (!File.Exists(file))
                throw new FileNotFoundException(file);

            return true;
        }

        /// <summary>
        /// Write a value to a specific section with a particular key in ini file,
        /// and it will returns a integer value. (success: great than 0; failed: Equals 0)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static int Write(string file,string section,string key,string value) {

            EnsureFileExists(file);

            return (int)WritePrivateProfileString(section, key, value, file);
        }

        /// <summary>
        /// Read a specific section with a particular key from a ini file and returns a string value.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Read(string file, string section, string key) {

            EnsureFileExists(file);

            var retVal = new StringBuilder(256);
            GetPrivateProfileString(section, key, "", retVal, 256, file);
            return retVal.ToString();
        }

    }
}
