using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.Core.CommandLine
{
    class Program
    {
        public static void Main(params string[] args)
        {
            // Program.exe <-g|--greeting|-$ <greeting>> [name <fullname>]
            // [-?|-h|--help] [-u|--uppercase]
            CommandLineApplication cmd = new CommandLineApplication(throwOnUnexpectedArg: false);
            CommandArgument names = null;
            cmd.Command("name",
              (target) =>
                names = target.Argument(
                  "fullname",
                  "Enter the full name of the person to be greeted.",
                  multipleValues: true));
            CommandOption greeting = cmd.Option(
              "-$|-g |--greeting <greeting>",
              "The greeting to display. The greeting supports"
              + " a format string where {fullname} will be "
              + "substituted with the full name.",
              CommandOptionType.SingleValue);
            CommandOption uppercase = cmd.Option(
              "-u | --uppercase", "Display the greeting in uppercase.",
              CommandOptionType.NoValue);
            cmd.HelpOption("-? | -h | --help");
            cmd.OnExecute(() =>
            {
                if (greeting.HasValue())
                {
                    Greet(greeting.Value(), names.Values, uppercase.HasValue());
                }
                return 0;
            });
            cmd.Execute(args);
        }
        private static void Greet(
          string greeting, IEnumerable<string> values, bool useUppercase)
        {
            Console.WriteLine(greeting);
        }
    }
}
