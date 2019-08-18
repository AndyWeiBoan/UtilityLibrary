using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;


namespace UtilityLibaray.Test.Integration {

    [TestFixture]
    public class IniUtilTest {


        private string _file =Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Test\Integration\Setting.ini");

        [TestCase("Unicode", "Unicode", "")]
        [TestCase("System Access", "NewAdministratorName", "Administrator")]
        [TestCase("System Access", "NewGuestName", "Guest")]
        [TestCase("Event Audit", "AuditSystemEvents", "9")]
        public void 取得INI檔案設定值(string section, string key,string expected) {

            var actual = IniUtil.Read(this._file, section, key);

            Assert.AreEqual(expected, actual);

        }

        
        [TestCase("Event Audit", "AuditSystemEvents", "9")]
        public void INI檔案不存在(string section, string key, string expected) {

            var NotExistsFile = @"%Windir%/123.ini";

            Assert.Catch<FileNotFoundException>(()=> IniUtil.Read(NotExistsFile, section, key));

        }
    }
}
