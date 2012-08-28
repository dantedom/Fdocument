namespace FDocument
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public enum ValueType
    {
        Boolean,
        Byte,
        Char,
        Decimal,
        Double,
        Int16,
        Int32,
        Int64,
        SByte,
        Single,
        String,
        UInt16,
        UInt32,
        UInt64,
        Null,
    }
    public class Fdocument
    {

        public List<Fvalue> values;

        // the list of types our serializer supports

        public Fdocument()
        {
            values = new List<Fvalue>();
        }
        #region Methods
        public Boolean Check(string name, Boolean Defaultvalue, bool append = false,bool  Override = false)
        {
            object d = Defaultvalue;
            return (Boolean)Check(name, d, append, Override);
        }
        public Byte Check(string name, Byte Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (Byte)Check(name, d, append, Override);
        }
        public Char Check(string name, Char Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (Char)Check(name, d, append, Override);
        }
        public Decimal Check(string name, Decimal Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (Decimal)Check(name, d, append, Override);
        }
        public Double Check(string name, Double Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (Double)Check(name, d, append, Override);
        }
        public Int16 Check(string name, Int16 Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (Int16)Check(name, d, append, Override);
        }
        public Int32 Check(string name, Int32 Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (Int32)Check(name, d, append, Override);
        }
        public Int64 Check(string name, Int64 Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (Int64)Check(name, d, append, Override);
        }
        public SByte Check(string name, SByte Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (SByte)Check(name, d, append, Override);
        }
        public Single Check(string name, Single Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (Single)Check(name, d, append, Override);
        }
        public String Check(string name, String Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (String)Check(name, d, append, Override);
        }
        public UInt16 Check(string name, UInt16 Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (UInt16)Check(name, d, append, Override);
        }
        public UInt32 Check(string name, UInt32 Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (UInt32)Check(name, d, append, Override);
        }

        public UInt64 Check(string name, UInt64 Defaultvalue, bool append = false, bool Override = false)
        {
            object d = Defaultvalue;
            return (UInt64)Check(name, d, append, Override);
        }
        #endregion
        public object Check(string name, object Defaultvalue, bool append,bool  Override)
        {



            bool comment = IsComment(Defaultvalue);
            for (int i = 0; i < this.values.Count; i++)
            {
                if (!comment)
                {
                    if (this.values[i].Name == name)
                    {
                        var value = this.values[i].Value;
                        if (Override && value.GetType() == Defaultvalue.GetType()) { value = Defaultvalue; this.values[i].Value = value; }
                     
                        return value;
                    }
                }

            }
            if (append)
            {
                this.values.Add(new Fvalue(name, Defaultvalue));
                return Defaultvalue;
            }
            return null;
        }

        private static bool IsComment(object Defaultvalue)
        {
            return (Defaultvalue is string) && Defaultvalue.ToString().StartsWith(@"//");
        }

        public void ClearText(string path, int count)
        {
            if (File.Exists(path))
            {
                TextWriter writer = new StreamWriter(path);
                for (int k = 0; k < count; k++)
                {
                    writer.WriteLine("");
                }
                writer.Close();
            }
            else
            {
                File.Create(path);
            }
        }

        public void Comment(string comment)
        {
            Check("", @"//" + comment, true);
        }

        public static bool createFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                return true;
            }
            return false;
        }

        public static bool createfolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                if (Directory.Exists(path))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> GetLocalList(string path)
        {
            List<string> Content = new List<string>();
            if (!System.IO.File.Exists(path))
            {
                System.IO.FileStream f = System.IO.File.Create(path);
                f.Close();
            }


            using (StreamReader reader = new StreamReader(path))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        Content.Add(line);
                    }
                }
                if (reader.EndOfStream)
                {
                    reader.Close();
                }
            }
            return Content;
        }
        public Fdocument Load(string Settingsfile)
        {
            List<string> templist = GetLocalList(Settingsfile);

            for (int i = 0; i < templist.Count; i++)
            {
                string line = templist[i];
                if (!IsComment(line))
                {
                    string[] words = line.Split('=');
                    if (words.Length > 1)
                    {
                        string value = words[1];
                        int index = value.IndexOf(' ');
                        if (index > 0)
                        {
                            value = value.Remove(index);
                        }

                        string[] valuetype = value.Split('(');
                        if (valuetype.Length > 1)
                        {
                            value = valuetype[0];
                            string typevalue = valuetype[1];
                            int closedbracket = typevalue.IndexOf(')');
                            if (closedbracket > 0)
                            {
                                typevalue = typevalue.Remove(closedbracket);
                            }
                            var objectvalue = ParseString(value, typevalue);
                            if (objectvalue == null)
                            {
                                throw new Exception("could not parse object string");
                            }
                            this.values.Add(new Fvalue(words[0], objectvalue));
                        }
                        else
                        {
                            var objectvalue = ParseString(value, valuetype[0].GetType().Name);
                            if (objectvalue == null)
                            {
                                throw new Exception("could not parse object string");
                            }
                            this.values.Add(new Fvalue(words[0], objectvalue));


                        }

                    }
                }

                else
                {
                    bool Add = true;
                    for (int e = 0; e < this.values.Count; e++)
                    {
                        if (this.values[e].Value is string && this.values[e].Value == line)
                        {
                            Add = false;
                            break;
                        }
                    }
                    if (Add)
                    {

                        this.values.Add(new Fvalue("", line));
                    }

                }
            }
            return this;
        }
        public static object ParseString(string value, string typevalue)
        {
            object result = null;
            if (Enum.IsDefined(typeof(ValueType), typevalue))
            {
                ValueType catemp = (ValueType)Enum.Parse(typeof(ValueType), typevalue);
                switch (catemp)
                {
                    case ValueType.Boolean:
                        result = Convert.ToBoolean(value);
                        break;
                    case ValueType.Byte:
                        result = Convert.ToByte(value);

                        break;
                    case ValueType.Char:
                        result = Convert.ToChar(value);
                        break;
                    case ValueType.Decimal:
                        result = Convert.ToDecimal(value);
                        break;
                    case ValueType.Double:
                        result = Convert.ToDouble(value);
                        break;
                    case ValueType.Int16:
                        result = Convert.ToInt16(value);
                        break;
                    case ValueType.Int32:
                        result = Convert.ToInt32(value);
                        break;
                    case ValueType.Int64:
                        result = Convert.ToInt64(value);
                        break;
                    case ValueType.SByte:
                        result = Convert.ToSByte(value);
                        break;
                    case ValueType.Single:
                        result = Convert.ToSingle(value);
                        break;
                    case ValueType.String:
                        result = Convert.ToString(value);
                        break;
                    case ValueType.UInt16:
                        result = Convert.ToUInt16(value);
                        break;
                    case ValueType.UInt32:
                        result = Convert.ToUInt32(value);
                        break;
                    case ValueType.UInt64:
                        result = Convert.ToUInt64(value);
                        break;
                    case ValueType.Null:

                        break;
                    default:
                        break;
                }
            }
            return result;
        }
        public bool Save(string path)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < this.values.Count; i++)
            {
                object value = this.values[i].Value;
                if (value != null)
                {
                    if (!IsComment(value))
                    {
                        items.Add(this.values[i].Name + "=" + value.ToString() + @"(" + value.GetType().Name + @")");
                    }
                    else
                    {
                        string comment = value.ToString();

                        items.Add(comment);

                    }
                }
                else
                {
                    items.Add(this.values[i].Name + "=ERROR");
                }
            }
            return WriteDocument(path, items);
        }
        public static bool WriteDocument(string path, List<string> items)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                System.IO.FileStream f = System.IO.File.Create(path);
                f.Close();
            }
            else
            {
                System.IO.FileStream f = System.IO.File.Create(path);
                f.Close();
            }
            TextWriter writer = new StreamWriter(path);
            for (int k = 0; k < items.Count; k++)
            {
                writer.WriteLine(items[k].ToString());
            }
            writer.Close();
            return true;
        }
    }
    public class Fvalue
    {

        public string Name = "";
        public object Value;
        public Fvalue(string name, object value)
        {
            this.Name = name;
            this.Value = value;

        }
    }
}

