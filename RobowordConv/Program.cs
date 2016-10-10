using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace RobowordConv
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() >= 2)
            {
                File.WriteAllText(args[1], Convert(new FileStream(args[0], FileMode.Open)),Encoding.UTF8);
            }else if (args.Count() == 1)
            {
                Console.WriteLine(Convert(new FileStream(args[0], FileMode.Open)));
            }else
            {
                Console.WriteLine(Convert(Console.OpenStandardInput()));
            }
        }

        static string Convert(Stream sr)
        {
            var result = new TextConverter();
            if (sr.CanSeek)
            {
                sr.Seek(0, SeekOrigin.Begin);
            }

            Encoding defaultLatinEncoding = Encoding.GetEncoding(28591);
            result.CurrentEncoding = defaultLatinEncoding;

            while (sr.Position < sr.Length)
            {
                var f = (byte)sr.ReadByte();
                if (f == 0xE1)
                {
                    f = (byte)sr.ReadByte();
                    if (f == 0x46)
                    {
                        var temp = new byte[2];
                        sr.Read(temp, 0, 2);
                        if (temp[0] == 0x30 && temp[1] == 0x30)
                        {
                            result.CurrentEncoding = defaultLatinEncoding;
                        }
                        else if (temp[0] == 0x38 && temp[1] == 0x30)
                        {
                            result.CurrentEncoding = Encoding.GetEncoding(932);
                        }
                        else if (temp[0] == 0x38 && temp[1] == 0x36)
                        {
                            result.CurrentEncoding = Encoding.GetEncoding(20936);
                        }
                        else if (temp[0] == 0x38 && temp[1] == 0x38)
                        {
                            result.CurrentEncoding = Encoding.GetEncoding(950);
                        }else
                        {
                            result.WriteByte(0xE1);
                            if (sr.Length > sr.Position) sr.Seek(-3, SeekOrigin.Current);
                        }
                    }
                    else
                    {
                        var g = (byte)sr.ReadByte();
                        if (f == 0x41 && g == 0x6c)
                        {
                            result.Append("</a>");
                        }
                        else if (f == 0x41 && g == 0x4c)
                        {
                            result.Append("<a href=\"\">");
                        }
                        else if (f == 0x42 && g == 0x30)
                        {
                            result.Append("</bold>");
                        }
                        else if (f == 0x42 && g == 0x31)
                        {
                            result.Append("<bold>");
                        }
                        else if (f == 0x49 && g == 0x30)
                        {
                            result.Append("</i>");
                        }
                        else if (f == 0x49 && g == 0x31)
                        {
                            result.Append("<i>");
                        }
                        else if (f == 0x4C && g == 0x31)
                        {
                            //Rank1
                        }
                        else if (f == 0x4C && g == 0x32)
                        {
                            //Rank2
                        }
                        else if (f == 0x4C && g == 0x33)
                        {
                            //Rank3
                        }
                        else if (f == 0x4C && g == 0x34)
                        {
                            //Rank4
                        }
                        else if (f == 0x4C && g == 0x35)
                        {
                            //Rank5
                        }
                        else if (f == 0x53 && g == 0x30)
                        {
                            result.Append("</sup>");
                        }
                        else if (f == 0x53 && g == 0x31)
                        {
                            result.Append("<sup>");
                        }else
                        {
                            result.WriteByte(0xE1);
                            if (sr.Length > sr.Position) sr.Seek(-2, SeekOrigin.Current);
                        }

                    }
                }else if (f == 0xE0)
                {
                    var temp = new byte[2];
                    sr.Read(temp, 0, 2);
                    if (temp[0] == 0x50 && temp[1] == 0x36)
                    {
                        result.Append("\t");
                    }
                    else if (temp[0] == 0x50 && temp[1] == 0x45)
                    {
                    }
                    else if (temp[0] == 0x50 && temp[1] == 0x47)
                    {
                    }
                    else if (temp[0] == 0x54 && temp[1] == 0x31)
                    {
                        result.Append(" ");
                    }
                    else if (temp[0] == 0x70 && temp[1] == 0x36)
                    {
                    }
                    else if (temp[0] == 0x70 && temp[1] == 0x45)
                    {
                    }
                    else if (temp[0] == 0x70 && temp[1] == 0x47)
                    {
                    }
                    else if (temp[0] == 0x74 && temp[1] == 0x31)
                    {
                    }
                    else
                    {
                        result.WriteByte(0xE0);
                        if (sr.Length > sr.Position) sr.Seek(-2, SeekOrigin.Current);
                    }
                }
                else if (f == 0x00)
                {

                }
                else if (f == 0x0A)
                {
                    result.WriteByte(0x0A);
                    result.CurrentEncoding = defaultLatinEncoding;
                }
                else
                {
                    result.WriteByte(f);
                }
            }
            return result.ToString();
        }

        public class TextConverter
        {
            public Encoding CurrentEncoding { get { return _CurrentEncoding; } set { LoadBuffer(); _CurrentEncoding = value; } }
            private Encoding _CurrentEncoding;

            private MemoryStream _Stream;

            private StringBuilder sb = new StringBuilder();

            public void LoadBuffer()
            {
                sb.Append(CurrentEncoding.GetString(_Stream.ToArray()));
                _Stream = new MemoryStream();
            }

            public TextConverter()
            {
                _CurrentEncoding = Encoding.ASCII;
                _Stream = new MemoryStream();
            }

            public void WriteByte(byte b)
            {
                _Stream.WriteByte(b);
            }

            public void Append(string str)
            {
                LoadBuffer();
                sb.Append(str);
            }

            public override string ToString()
            {
                LoadBuffer();
                return sb.ToString();
            }

        }

    }
}
