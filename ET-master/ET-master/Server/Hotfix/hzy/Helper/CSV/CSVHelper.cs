using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ETHotfix
{
        partial class GameTables
        {
            /// <summary>
            /// ';',','
            /// </summary>
            public static char[] itemSeparator1 = new char[] { ';', ',' };
            /// <summary>
            /// |
            /// </summary>
            public static char[] itemSeparator2 = new char[] { '|' };

        }


        public class CSVReader
        {
            public static int head = 2;
            public static int start = 3;
            public static List<string[]> Load(string fn, string encoding = "GBK")
            {
                var content = File.ReadAllText(fn, Encoding.GetEncoding(encoding));
                var lines = new List<string[]>();
                ParseLines(lines, content);
                return lines;
            }

            public static List<T> LoadAsObjects<T>(string fn, string encoding = "GBK") where T : new()
            {
                var lines = Load(fn);
                var header = lines[head];
                var objects = new List<T>();
                for (int i = start; i < lines.Count; i++)
                    objects.Add(new T());
                for (int i = 0; i < header.Length; i++)
                {
                    if (string.IsNullOrEmpty(header[i]))
                    {
                        continue;
                    }
                    var field = typeof(T).GetField(header[i]);
                    if (field == null)
                    {
                        Console.WriteLine("unknown column: '{0}'.", header[i]);
                        continue;
                    }
                    for (int j = start; j < lines.Count; j++)
                    {
                        var str = lines[j][i];
                        if (field.FieldType == typeof(string))
                            field.SetValue(objects[j - start], str);
                        else
                        {
                            if (str == "")
                                continue;
                            if (field.FieldType.IsEnum)
                            {

                                field.SetValue(objects[j - start], Enum.Parse(field.FieldType, str));
                            }
                            else
                            {
                                try
                                {
                                    var val = Convert.ChangeType(str, field.FieldType);
                                    field.SetValue(objects[j - start], val);
                                }
                                catch (Exception e)
                                {
                                    //Log.Error("{0}\n{1}", e.Message, e.StackTrace);
                                    throw e;
                                }
                            }
                        }
                    }
                }
                return objects;
            }

            public static List<T> LoadAsObjects<T>(List<string[]> lines) where T : new()
            {
                var count = lines.Count;
                var header = lines[head];
                var objects = new List<T>(count - start);
                for (int i = start; i < count; i++)
                    objects.Add(new T());
                for (int i = 0; i < header.Length; i++)
                {
                    if (string.IsNullOrEmpty(header[i]))
                    {
                        continue;
                    }
                    var field = typeof(T).GetField(header[i]);
                    if (field == null)
                    {
                        Console.WriteLine("unknown column: '{0}'.", header[i]);
                        continue;
                    }
                    for (int j = start; j < lines.Count; j++)
                    {
                        var str = lines[j][i];
                        if (field.FieldType == typeof(string))
                            field.SetValue(objects[j - start], str);
                        else
                        {
                            if (str == "")
                                continue;
                            if (field.FieldType.IsEnum)
                            {

                                field.SetValue(objects[j - start], Enum.Parse(field.FieldType, str));
                            }
                            else
                            {
                                try
                                {
                                    var val = Convert.ChangeType(str, field.FieldType);
                                    field.SetValue(objects[j - start], val);
                                }
                                catch (Exception e)
                                {
                                    //Log.Error("{0}\n{1}", e.Message, e.StackTrace);
                                    throw e;
                                }
                            }
                        }
                    }
                }
                return objects;
            }

            private static void ParseLines(List<string[]> lines, string content)
            {
                content = content.Replace("\r\n", "\n");
                content = content.Replace("\r", "\n");
                List<string> ret = new List<string>();
                bool inQuote = false;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < content.Length; ++i)
                {
                    var c = content[i];
                    if (!inQuote)
                    {
                        if (c == ',')
                        {
                            ret.Add(sb.ToString());
                            sb.Remove(0, sb.Length);
                        }
                        else if (c == '\n')
                        {
                            ret.Add(sb.ToString());
                            sb.Remove(0, sb.Length);
                            lines.Add(ret.ToArray());
                            ret.Clear();
                        }
                        else if (c == '"')
                            inQuote = true;
                        else
                            sb.Append(c);
                    }
                    else
                    {
                        if (c == '"')
                        {
                            if (i < content.Length - 1 && content[i + 1] == '"')
                                sb.Append(c);
                            inQuote = false;
                        }
                        else
                            sb.Append(c);
                    }
                }
            }


        }
}
