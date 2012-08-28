using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FDocument;
namespace ConsoleApplication1
{
    class Program
    {
        public static Fdocument document;
        public static string SavePath = string.Concat(new object[] { Environment.GetFolderPath(Environment.SpecialFolder.Personal), System.IO.Path.DirectorySeparatorChar, "My Games", System.IO.Path.DirectorySeparatorChar, "FredsGame" });
        public static string SettingsfilePath = (SavePath + System.IO.Path.DirectorySeparatorChar + "test.ini");
        private static string Filename = "";
        private static bool testbool = false;
        private static String testString = "meererfds";
        private static Byte testbyte = 1;
        private static Char testChar = '&';
        private static Decimal testDecimal = 1;
        private static Double testDouble = 1.0;
        private static Int16 testInt16 = 1;
        private static Int32 testInt32 = 2;
        private static Int64 testInt64 = 3;
        private static SByte testSByte = -123;
        private static Single testSingle = -1.2f;
        private static UInt16 testUInt16 = 1;
        private static UInt32 testUInt32 = 2;
        private static UInt64 testUInt64 = 3;


        static void Main(string[] args)
        {


            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
            document = new Fdocument();
            document.Load(SettingsfilePath);
            CheckAllSettings();

            document.Save(SettingsfilePath);


        }
        public static void CheckAllSettings()
        {
            document.Comment("testing comments rew");
            testbool = document.Check("testingBool", testbool, true);
            document.Comment("testing comments");
            testString = document.Check(testString.GetType().Name, testString, true);
            document.Comment("testing comments rew");
            testbyte = document.Check(testbyte.GetType().Name, testbyte, true);
            testChar = document.Check(testChar.GetType().Name, testChar, true);
            testDecimal = document.Check(testDecimal.GetType().Name, testDecimal, true);
            testDouble = document.Check(testDouble.GetType().Name, testDouble, true);

            testInt16 = document.Check(testInt16.GetType().Name, testInt16, true);

            testInt32 = document.Check(testInt32.GetType().Name, testInt32, true);

            testInt64 = document.Check(testInt64.GetType().Name, testInt64, true);
            testSByte = document.Check(testSByte.GetType().Name, testSByte, true);
            document.Comment("76767 er");
            testSingle = document.Check(testSingle.GetType().Name, testSingle, true);
            testUInt16 = document.Check(testUInt16.GetType().Name, testUInt16, true);
            testUInt32 = document.Check(testUInt32.GetType().Name, testUInt32, true);
            testUInt64 = document.Check(testUInt64.GetType().Name, testUInt64, true);



        }
    }
}

