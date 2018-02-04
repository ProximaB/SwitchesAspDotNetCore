using System;
using System.Linq;
using System.Text;

namespace SwitchesAPI.Extensions
{
    static class StringExtensions
    {
        public static string GetSalt(this string value, int length) // bardziej pasowałby helper, chyba, że bedzie dodawal
        {                                                               // do isteijace stringa dodatkową wartośc bedąca identyfikatorem
            StringBuilder GetSalt = new StringBuilder();               // np "id_".IdBuilder(6) => "id_E4Sfs3"
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(length)
                .ToList().ForEach(e => GetSalt.Append(e));

            return value + GetSalt;
        }   
    }
}
