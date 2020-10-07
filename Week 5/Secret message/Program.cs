using System;

namespace Secret_message
{
    class Program
    {
        static void Main(string[] args)
        {
            var chars = new[]
             {
                (char)87, (char)97, (char)110, (char)116,  (char)32, (char)116, (char)111,  (char)32, (char)109, (char)101,
(char)101, (char)116,  (char)32, (char)102, (char)111, (char)114,  (char)32, (char)108, (char)117, (char)110,
 (char)99, (char)104, (char)63,  (char)32,  (char)73,  (char)39, (char)108, (char)108,  (char)32, (char)108,
(char)101,  (char)97, (char)118, (char)101,  (char)32, (char)116, (char)104, (char)101, (char)32, (char)114,
(char)101, (char)115, (char)116, (char)97, (char)117, (char)114, (char)97, (char)110, (char)116, (char)32,
 (char)97, (char)100, (char)100, (char)114, (char)101, (char)115, (char)115,  (char)32, (char)105, (char)110,
 (char)32, (char)116, (char)104, (char)101,  (char)32, (char)115, (char)111, (char)117, (char)116, (char)104,
 (char)32, (char)109,  (char)97, (char)105, (char)110, (char)116, (char)101, (char)110,  (char)97, (char)110,
 (char)99, (char)101,  (char)32,  (char)99, (char)108, (char)111, (char)115, (char)101, (char)116,  (char)46,
 (char)32,  (char)66, (char)114, (char)105, (char)110, (char)103,  (char)32,  (char)97, (char)110,  (char)32,
 (char)65,  (char)83,  (char)67,  (char)73,  (char)73,  (char)32,  (char)99, (char)104,  (char)97, (char)114,
(char)116,  (char)44,  (char)32, (char)116, (char)104, (char)101,  (char)32, (char)109, (char)101, (char)115,
(char)115,  (char)97, (char)103, (char)101,  (char)32, (char)119, (char)105, (char)108, (char)108,  (char)32,
 (char)98, (char)101,  (char)32,  (char)99, (char)111, (char)100, (char)101, (char)100,  (char)46
            };
            Console.WriteLine(string.Join(" ", chars));
        }
    }
}
